using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyMove12parts : MonoBehaviour {
    oBase mother;//※必須
    GameObject obj;
    GameObject boss;
    oEnemyMove12 boss4;
    Vector3 pos;
    GameObject player;
    Vector3 playerPos;
    public int hund;
    bool modeChangeFlg = true;
    bool att1Flg = false;
    public bool _att1Flg
    {
        get
        {
            return att1Flg;
        }
    }
    int i = 0;
    // Use this for initialization
    void Start () {
        if(hund == 0)
        {
            i = 1;
        }
        else
        {
            i = -1;
        }
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        boss = GameObject.Find("boss4body");
        boss4 = boss.GetComponent<oEnemyMove12>();
        obj = GameObject.Find("Reference");//oBaseの入っているオブジェクトを探す
        mother = obj.GetComponent<oBase>();
        pos = transform.position;

    }

    // Update is called once per frame
    void Update () {
        if(boss4.att1Flg == true)//攻撃1
        {
            if(boss4.hundMoveFlg[hund] == false)
            {
                boss4.hund[hund] = false;
                pos = transform.position;
                boss4.hundMoveFlg[hund] = true;
            }
            hundatt1();
        }
    }
    void hundatt1()
    {
        switch (hund)
        {
            case 0:
                if (boss4.hundatt1Flg[0] == 4 && transform.position.x - pos.x > 0.001f)
                {
                    transform.position = new Vector3(pos.x, pos.y, pos.z);
                    boss4.hund[0] = true; 
                }
                if (boss4.hundatt1Flg[0] < 4 && boss4.hund[0] == false || transform.position.x - pos.x <= 0.01f && boss4.hund[0] == false)
                {
                    transform.Translate(0, 0.25f, 0);
                }

                break;
            case 1:
                if (boss4.hundatt1Flg[1] == 4 && transform.position.x - pos.x < -0.001f)
                {
                    transform.position = new Vector3(pos.x, pos.y, pos.z);
                    boss4.hund[1] = true;
                }
                if (transform.position.x - pos.x >= -0.01f && boss4.hund[1] == false|| boss4.hundatt1Flg[1] < 4 && boss4.hund[1] == false)
                {
                    transform.Translate(0, 0.25f, 0);
                }
                break;
        }
    }
    void OnBecameInvisible()
    {
        if(boss4.hundatt1Flg[hund] == 1)//一回目
        {
            transform.position = new Vector3(-30 * i, playerPos.y, 0);
            boss4.hundatt1Flg[hund] = 2;
        }
        else if(boss4.hundatt1Flg[hund] == 2)//二回目
        {
            transform.position = new Vector3(-30 * i, pos.y, 0);
            boss4.hundatt1Flg[hund] = 3;
        }
    }
    void OnBecameVisible()
    {
        if (boss4.hundatt1Flg[hund] == 3)
        {

            boss4.hundatt1Flg[hund] = 4;
        }
    }
}
