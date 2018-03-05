using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hTitleCtrl : MonoBehaviour {

    [SerializeField, Header("羊のオブジェクト")]
    GameObject sheep;

    [SerializeField, Header("歯車のオブジェクト")]
    GameObject setting;

    [SerializeField, Header("タイトルロゴオブジェクト")]
    GameObject title_logo;

    GameObject Select;
    float sel = 0;

    // Use this for initialization
    void Start () {
        if (sheep == null) sheep = GameObject.Find("sheep");
        if (setting == null) setting = GameObject.Find("background");
        if (title_logo == null) setting = GameObject.Find("Title_Logo");
        if (Select == null) Select = title_logo;


        hJoyStickReceiver jsr = new hJoyStickReceiver();
        hKeyConfig.Config["Jump"] = jsr.GetPlayBtn(hJoyStickReceiver.PlayStationContoller.Cross);
        hKeyConfig.Config["Zone"] = jsr.GetPlayBtn(hJoyStickReceiver.PlayStationContoller.L1);
        hKeyConfig.Config["Submit"] = jsr.GetPlayBtn(hJoyStickReceiver.PlayStationContoller.Square);
    }

    // Update is called once per frame
    void Update () {

        // コントローラのアクシス取得
        float X = Input.GetAxis("Horizontal");

        // アクシスによって選択をする
        switch (Select.name)
        {
            case "Title":
                if (X > 0.5f) Select = setting;
                if (X < -0.5f) Select = sheep;
                break;
            case "Tutorial":
                if (X > 0.5f) Select = title_logo;
                if (X < -0.5f) Select = sheep;
                break;
            case "KeyConfig":
                if (X > 0.5f) Select = setting;
                if (X < -0.5f) Select = title_logo;
                break;
        }

        // カメラ移動
        if (Select != null)transform.position = Select.transform.position - new Vector3(0,0,10);

        if ((hKeyConfig.GetKeyDown("Submit") || Input.GetKeyDown(KeyCode.Space)) && Select != null) SceneManager.LoadScene(Select.name);
    }
}
