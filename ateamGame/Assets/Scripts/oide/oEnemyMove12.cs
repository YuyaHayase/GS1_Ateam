using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyMove12 : MonoBehaviour {
    oBase mother;//※必須
    Vector3 size;//自身の大きさ
    GameObject obj;
    GameObject boss4;
    GameObject player;
    public GameObject bullet;//弾１
    public GameObject bullet2;//弾２
    int random = 1;//攻撃パターン
    int i2 = 1;
    int reflectAngle;
    //public bool[] hund = {false,false};
    public bool[] hund = new bool [2];
    public bool[] hundMoveFlg = new bool[2];
    public int refrect
    {
        get
        {
            return reflectAngle;
        }
    }
    float angle;//プレイヤーとの角度
    float rotation = 0.0f;
    bool posFlg = false;
    bool flg = true;
    float time = 0.0f;
    public bool att1Flg = false;
    bool att3Flg = false;
    public bool att4Flg = false;
    public int [] hundatt1Flg = {1,1};
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        size = transform.localScale;
        boss4 = GameObject.Find("boss4");
        obj = GameObject.Find("Reference");//oBaseの入っているオブジェクトを探す
        mother = obj.GetComponent<oBase>();
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime * mother.enemySpeed;
        if (flg == false && time >= 3.0f)
        {
            switch (random)
            {
                 case 1:
                    att1();
                    break;
                 case 2:
                    att2();
                    break;
                 case 3:
                    att3();
                    break;
                 case 4:
                    att4();
                    break;
             }
          }
          else if (flg == true)
          { 
               time = 0.0f;
               random = Random.Range(1, 5);
               flg = false;
               posFlg = false;//randomFlgをfalseにする
               hund[0] = false; hund[1] = false;
               hundatt1Flg[0] = 1; hundatt1Flg[1] = 1;
           }
	    }
    void att1()//左右から腕が攻めてくる
    {
        if (hund[0] == true && hund[1] == true)
        {
            hundMoveFlg[0] = false;
            hundMoveFlg[1] = false;
            flg = true;
            att1Flg = false;
        }
        if(hund[0] == false || hund[1] == false)
        {
            att1Flg = true;
        }
    }
    void att2()
    {
        for (int i = 1; i <= 5; i++)
        {
            GameObject bulletInstance = Instantiate(bullet2) as GameObject;
            bulletInstance.transform.rotation = Quaternion.Euler(0, 0, 230 + i * 15);
            bulletInstance.transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
        }
        flg = true;
    }
    void att3()
    {
        transform.Translate(0, 0.3f, 0);
    }
    void att4()
    {
        if (transform.localScale.x <= 1.5f && att4Flg == false)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.02f * mother.enemySpeed, transform.localScale.y + 0.02f * mother.enemySpeed, transform.localScale.z);
        }
        else
        {
            att4Flg = true;
            if (transform.localScale.x >= size.x)
            {
                transform.localScale = new Vector3(transform.localScale.x - 0.02f * mother.enemySpeed, transform.localScale.y - 0.02f * mother.enemySpeed, transform.localScale.z);
            }
            else
            {
                angle = mother.Playerangle(transform.position);
                GameObject bulletInstance = Instantiate(bullet) as GameObject;
                bulletInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
                bulletInstance.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                att4Flg = false;
                flg = true;
            }
        }
    }
    void OnBecameInvisible()
    {
        if(att3Flg == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            transform.position = new Vector3(10, player.transform.position.y, 0);
            att3Flg = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = new Vector3(player.transform.position.x, 5, 0);
            att3Flg = false;
            flg = true;
        }
    }

}
