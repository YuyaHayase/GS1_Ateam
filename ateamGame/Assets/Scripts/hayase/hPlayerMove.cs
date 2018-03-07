using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class hPlayerMove : MonoBehaviour {

    // アナログスティック
    Vector2 Axis;

    // 武器
    GameObject _child;

    // ジャンプしているか
    bool jumping = false;
    float delta = 0;
    [SerializeField,Tooltip("最低ライン"), Header("最低ライン")]
    float underLine = -4;

    // ジャンプ力
    float jumpPower = 0;

    [SerializeField, Tooltip("ジャンプ力ぅ......ですかね"), Header("ジャンプ力ぅ......ですかね")]
    float HighjumpPower = 0.5f;

    [SerializeField, Tooltip("集中時のジャンプ力"), Header("集中時のジャンプ力")]
    float ZoneInjumpPower = 0.05f;

    [SerializeField, Tooltip("重力"), Header("重力")]
    float Gravity = 9.8f;

    //ジョイスティック
    hJoyStickReceiver jsr;
    hKeyConfigSettings kcs;

    //ジョイスティックの制限
    [SerializeField, Tooltip("左スティック(ボタン)の制限係数"), Header("左スティック(ボタン)の制限係数")]
    float joyLeftAxisComp = 5.0f;
    [SerializeField, Tooltip("右スティックの加速係数"), Header("右スティックの加速係数")]
    float joyRightAxisAccel = 1.5f;


    // 吉田スクリプト
    yHpgage yhp;
    [SerializeField, Header("プレイヤーが敵に当たったときの受けるダメージ")]
    int PlayerReceiveDamage = 5;

    [SerializeField, Header("ノックバック量")]
    float KnockBack = 5f;

    bool damaged = false;
    float damage_delta = 0;

    [SerializeField, Header("Player子")]
    GameObject pChildren;
    Color pChildColor;

    [SerializeField, Header("無敵時間")]
    float NotReceiveDamageTime = 3f;
    void Awake()
    {
        try
        {
        yhp = GameObject.Find("Canvas/HPvar").GetComponent<yHpgage>();
        yhp.PlayerHps = 150;

        yhp.Acquisition();
        }catch(Exception e)
        {
            print(e.Message + ": HPゲージ");
        }
    }

    // Use this for initialization
    void Start () {
        // 子オブジェクトの取得
        _child = transform.FindChild("humer").gameObject;
        if (pChildren == null) pChildren = GameObject.Find("player");
        pChildColor = pChildren.GetComponent<SpriteRenderer>().color;

        jsr = new hJoyStickReceiver();
        kcs = new hKeyConfigSettings();
        kcs.Init();

        if(yhp == null) yhp = new yHpgage();
        //yhp.Acquisition();
    }

    // Update is called once per frame
    void Update () {
        if(damaged)
        {
            damage_delta += Time.deltaTime;
            if (damage_delta > NotReceiveDamageTime)
            {
                damaged = false;
                damage_delta = 0;
                pChildren.GetComponent<SpriteRenderer>().color = new Color(pChildColor.r, pChildColor.g, pChildColor.b, 1f);
            }
        }

        float py = 0;
        /*
        y = Vo*t - (g*t^2)/2
            Vo:初速(jumpPowerに分類されるところ)
			t:時間(ジャンプしてからのフレーム数。)
			g:重力加速度(9.8が一般的ですが、1ピクセル当たりの換算距離によります)
        */
        
        // ×ボタンが押されたら
        if (hKeyConfig.GetKey("Jump")) jumping=true;
        if (jumping)
        {
            // 集中時ゆっくりになる？やつ
            if (hKeyConfig.GetKey("Zone") || Input.GetKey(KeyCode.LeftShift))
            {
                jumpPower = ZoneInjumpPower;
                delta += Time.deltaTime / 15.0f;
            }
            else
            {
                jumpPower = HighjumpPower;
                delta += Time.deltaTime;
            }

            py = jumpPower - (Gravity * Mathf.Pow(delta, 2) / 2);
            //Debug.Log(py);
        }

        // 最低ラインにキたら
        if (underLine > transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, underLine);
            py = 0;
            delta = 0;
            jumping = false;
        }

        // アクシスの調整 左ステック
        if (Input.GetAxis("Vertical") == 0)
        {
            if (Input.GetAxis("The Cross Key UpDown") > 0.8f) jumping = true;
        }
        else if(Input.GetAxis("Vertical") > 0.8f)
        {
            jumping = true;
        }
        if (Input.GetAxis("Horizontal") == 0) Axis.x = Input.GetAxis("The Cross Key LeftRight") / joyLeftAxisComp;
        else Axis.x = Input.GetAxis("Horizontal") / joyLeftAxisComp;
        if (hKeyConfig.GetKey("Zone")) Axis.x = Axis.x / 3.5f;
        transform.position += new Vector3(Axis.x, py, 0);

        float dirx = transform.localScale.x;
        if (Axis.x < 0) dirx = 1.5f;
        else if(Axis.x > 0) dirx = -1.5f;
        transform.localScale = new Vector3(dirx, transform.localScale.y);

        // アクシスの調整 右ステック
        float RightX = Input.GetAxis("Horizontal R") * joyRightAxisAccel;
        float RightY = -Input.GetAxis("Vertical R") * joyRightAxisAccel; ;
        switch (hKeyConfigSettings.mo)
        {
            case 0:
                RightY = -Input.GetAxis("Vertical R") * joyRightAxisAccel;
                break;
            case 1:
                RightY = -Input.GetAxis("Vertical Ru") * joyRightAxisAccel;
//                RightY = -Input.GetAxis("Vertical Ru") * joyRightAxisAccel;
                break;
        }

        // 武器の座標
        _child.transform.position = new Vector3(transform.position.x + RightX,
                                                transform.position.y + RightY,
                                                0);

        // 角度調節
        float rot = Mathf.Atan2(_child.transform.position.y - transform.position.y,
                                _child.transform.position.x - transform.position.x);

        // 右スティックが入力されてないなら
        if (RightX == 0 && RightY==0) _child.transform.rotation = Quaternion.AngleAxis(45,Vector3.forward);
        else _child.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rot * Mathf.Rad2Deg-90);

        // 集中時以外武器の判定を消す
        if ((hKeyConfig.GetKey("Zone") || Input.GetKey(KeyCode.LeftShift)) && (RightX != 0 || RightY != 0 )) _child.SetActive(true);
        else _child.SetActive(false);

        if (0 >= yhp.PlayerHps) SceneManager.LoadScene("GameOver");
    }

    void OnTriggerStay2D(Collider2D col)
    {
        try
        {
            // 壁に当たったら跳ね返る
            if (col.tag == "wall")
            {
                if (col.transform.position.x < transform.position.x) transform.position = new Vector3(30,0,0);
                if (col.transform.position.x > transform.position.x) transform.position = new Vector3(-30, 0, 0);
            }
        }
        catch (Exception e)
        {
            print(e.Message + " : 'wall' タグを追加してみたら多分治るかもしれません");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    { 
        // 
        if(!damaged && col.tag == "enemy")
        {
            // ノックバック
            if (transform.position.x < col.transform.position.x) transform.Translate(new Vector3(-KnockBack, 0, 0));
            else transform.Translate(new Vector3(KnockBack, 0, 0));

            damaged = true;
            pChildren.GetComponent<SpriteRenderer>().color = new Color(pChildColor.r, pChildColor.g, pChildColor.b, 0.5f);

            // ダメージ
            yhp.PlayerDamage(PlayerReceiveDamage, col);
        }
            
    }
}
