using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class yCsvWriter : MonoBehaviour {

    FileStream fs;
    StreamWriter sw;

    Text No;
    Image content;
    Image scrollPanel;
    Image lineColor;
    InputField[] input;
    InputField[,] inputInstans;
    InputField[] instans;
    Color color;

    int number = 0;
    float inputPosy = -50.0f;

    List<string[]> wave = new List<string[]>();

    Vector2 contentSizeDelta = new Vector2(0,-50.0f);
    [SerializeField,Header("カラーの有無")]
    bool flgColor = true;

    // Use this for initialization
    void Start () {
        TextAsset csv = Resources.Load("yCSV/Wave") as TextAsset;
        StringReader sr = new StringReader(csv.text);
        CsvReader(wave, sr);

        string pass = Application.dataPath + "/Resources/yCSV/";
        try
        {
            fs = new FileStream(pass + "Wave.csv", FileMode.OpenOrCreate);
        }
        catch(IOException)
        {
            print("見つからない");
        }

        scrollPanel = transform.Find("ScrollPanel").GetComponent<Image>();
        content = scrollPanel.transform.Find("Content").GetComponent<Image>();
        input = content.transform.GetComponentsInChildren<InputField>();
        No = content.transform.Find("No").GetComponent<Text>();
        lineColor = content.transform.Find("LineColor").GetComponent<Image>();

        wave.RemoveAt(0);

        //CanvasのHeightを設定
        for (int y = 0; y < wave.Count; y++)
            contentSizeDelta.y -= 50.0f;
        content.GetComponent<RectTransform>().sizeDelta -= contentSizeDelta;

        //カラーの設定
        color = lineColor.color;

        //InputFiledとNoをインスタンスで生成
        for (int y = 0; y < wave.Count; y++)
        {
            inputPosy -= -50.0f;
            TextNo();
            number++;
            LineColor(y);

            for (int x = 0; x < input.Length; x++)
            {
                //インスタンスされる位置を設定
                Vector3 InputPos = input[x].transform.localPosition;
                InputPos.y -= inputPosy;

                InputField Input = Instantiate(input[x]) as InputField;
                Input.transform.parent = content.transform;
                Input.transform.localPosition = InputPos;

                //インスタンスされたtextをcsvから読み込んだ文字を入れる
                Input.text = wave[y][x];
            }
        }


        //なんかこれ残しとくとおかしかったから強引やけど消しとく
        for (int i = 0; i < input.Length; i++)
            input[i].gameObject.SetActive(false);
        No.gameObject.SetActive(false);
        lineColor.gameObject.SetActive(false);
        //Destroy(input[i].gameObject);

        //今あるcontentの子オブジェクトを全て取得
        instans = content.GetComponentsInChildren<InputField>();

        //2次元配列で保存する
        inputInstans = new InputField[instans.Length / input.Length, input.Length];
        for (int y = 0, _y = 0; _y < instans.Length; y++)
        {
            for (int x = 0; x < inputInstans.GetLength(1); x++)
            {
                inputInstans[y, x] = instans[_y];
                _y++;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		for(int y = 0;y < inputInstans.GetLength(0); y++)
        {
            switch (inputInstans[y, 0].text)
            {
                case "Enemy1":
                    inputInstans[y, 6].text = "50";
                    break;
                case "Enemy2plus":
                    inputInstans[y, 6].text = "10";
                    break;
                case "Enemy3":
                    inputInstans[y, 6].text = "80";
                    break;
                case "Enemy4":
                    inputInstans[y, 6].text = "40";
                    break;
                case "Enemy5":
                    inputInstans[y, 6].text = "150";
                    break;
                case "Enemy6":
                    inputInstans[y, 6].text = "40";
                    break;
                case "Enemy8":
                    inputInstans[y, 6].text = "30";
                    break;
                default:
                    break;
            }
        }
	}
    private void TextNo()
    {
        //テキストがインスタンスされる位置を設定
        Vector3 TextPos = No.transform.localPosition;
        TextPos.y -= inputPosy;

        //インスタンス
        Text _No = Instantiate(No) as Text;
        _No.transform.parent = content.transform;
        _No.transform.localPosition = TextPos;
        _No.text = number.ToString();
        _No.name = "No";

    }

    private void LineColor(int y)
    {
        if (flgColor)
        {
            Vector3 LinePos = lineColor.transform.localPosition;
            LinePos.y -= inputPosy;

            Image _lineColor = Instantiate(lineColor) as Image;
            _lineColor.transform.parent = content.transform;
            _lineColor.transform.localPosition = LinePos;
            _lineColor.transform.localScale = new Vector3(1, 1, 1);
            _lineColor.color = color;
            if (y % 5 == 0)
            {
                if (color == lineColor.color)
                    color = GetColor(255, 255, 93, 33);
                else
                    color = lineColor.color;
            }
        }
    }

    private void CsvReader(List<string[]> list, StringReader sr)
    {
        while (sr.Peek() != -1)
        {
            string line = sr.ReadLine();
            list.Add(line.Split(',','/'));
        }
    }

    private Color GetColor(float r,float g,float b,float a)
    {
        Color color = new Color(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);

        return color;
    }

    public void LineAdd()
    {
        inputPosy -= -50.0f;

        Vector3 TextPos = No.transform.localPosition;
        TextPos.y -= inputPosy;

        Text _No = Instantiate(No) as Text;
        _No.transform.parent = content.transform;
        _No.transform.localPosition = TextPos;
        _No.text = number.ToString();
        _No.gameObject.SetActive(true);
        _No.name = "No";
        number++;

        for (int i = 0;i < input.Length;i++)
        {
            Vector3 InputPos = input[i].transform.localPosition;
            InputPos.y -= inputPosy;

            InputField Input = Instantiate(input[i]) as InputField;
            Input.transform.parent = content.transform;
            Input.transform.localPosition = InputPos;
            Input.gameObject.SetActive(true);
        }
    }

    public void ListRemove()
    {
        inputPosy -= +50.0f;
        number--;


        for (int i = 1; i <= input.Length; i++)
        {
            //今あるcontentの子オブジェクトを全て取得
            instans = content.GetComponentsInChildren<InputField>();
            Destroy(instans[instans.Length - i].gameObject);
        }
        //Text[] _No = content.GetComponentsInChildren<Text>();
        //Destroy(_No[_No.Length - 1].gameObject);
        //print(_No[_No.Length - 1].transform.parent.name);
        //print(number);

    }

    public void Save()
    {
        sw = new StreamWriter(fs);

        sw.Write("ID,Stage,Wave,Pos,Time,HP");

        //今あるcontentの子オブジェクトを全て取得
        instans = content.GetComponentsInChildren<InputField>();

        //2次元配列で保存する
        inputInstans = new InputField[instans.Length / input.Length, input.Length];
        for (int y = 0,_y = 0; _y < instans.Length;y++)
        {
            for (int x = 0; x < inputInstans.GetLength(1); x++)
            {
                inputInstans[y, x] = instans[_y];
                _y++;
            }
        }


        //csvに書き込み
        for (int y = 0; y < inputInstans.GetLength(0); y++)
        {
            string line = "";
            sw.WriteLine();
            for (int x = 0; x < inputInstans.GetLength(1); x++)
            {
                line += inputInstans[y, x].text;
                if (x == 3)
                    line += "/";
                else if (x != 6)
                    line += ",";

            }
            sw.Write(line);
        }
        sw.Close();

        print("Save完了");
    }
}
