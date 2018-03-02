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
        float Y = Input.GetAxis("Vertical");
        switch (hKeyConfigSettings.mo)
        {
            case 0:
                Y = Input.GetAxis("Vertical");
                break;
            case 1:
                Y = Input.GetAxis("Vertical");
                break;
        }

        // アクシスによって選択をする
        if (X < -0.5f) Select = sheep;
        if (X > 0.5f) Select = setting;
        
        // カメラ移動
        transform.position = Select.transform.position - new Vector3(0,0,10);

        if (hKeyConfig.GetKeyDown("Submit")) SceneManager.LoadScene(Select.name);
        if (hKeyConfig.GetKeyDown("Jump")) Select = title_logo;
    }
}
