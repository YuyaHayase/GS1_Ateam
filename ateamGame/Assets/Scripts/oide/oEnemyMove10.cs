using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyMove10 : MonoBehaviour {
    Vector3 pos;
    int random;
    float sin = 0;
    float randomDistance;
    public bool randomFlg = false;
    public bool att1Flg = true;
    bool jumpFlg = false;
    bool fallflg = false;
    public int maxCount;
    public int count;
    bool att3Flg = false;
    public bool flg3
    {
        get
        {
            return att3Flg;
        }
    }
    //プレイヤーの方向を向いたかどうかのフラグ
    bool posFlg = false;
    public bool directionFlg
    {
        get
        {
            return posFlg;
        }
    }
    public GameObject bullet;
    GameObject obj;//※必須
    oBase mother;//※必須
    float randomTime = 0.0f;//攻撃間隔
    int direction;
    public int boss2Direction
    {
        get
        {
            return direction;
        }
    }
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Reference");
        mother = obj.GetComponent<oBase>();
    }
	
	// Update is called once per frame
	void Update () { 
        if (transform.tag == "enemy")
        {
            randomTime += Time.deltaTime * mother.enemySpeed;
            if(randomTime >= 2)
            {
                if(randomFlg == false)
                {

                    random = Random.Range(1, 4);
                    randomFlg = true;
                }

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
                }
            }
        }
    }
    void att1()//攻撃１
    {
        if (posFlg == false)
        {
            pos = transform.position;
            direction = mother.Playerposition(pos);
            posFlg = true;
        }
        if(jumpFlg == false)
        {
            sin += 0.3f;
            transform.Translate(0, Mathf.Sin(sin ), 0);
            fallflg = true;
        }
        if (att1Flg == false)
        {
            GameObject bulletInstance = Instantiate(bullet) as GameObject;
            bulletInstance.transform.position = new Vector3(pos.x + 3 * direction, pos.y - 2, pos.z);
            att1Flg = true;

        }
    }
    void att2()//体当たり
    {
        if(posFlg == false)
        {
            randomDistance = Random.Range(5.0f, 12.0f);
            pos = transform.position;
            direction = mother.Playerposition(pos);
            posFlg = true;
        }
        if(Mathf.Abs(pos.x - transform.position.x) < randomDistance)
        {
            transform.Translate(0.1f * direction * mother.enemySpeed, 0, 0);
        }
        else
        {
            attStop();
        }

    }
    void att3()//反射弾
    {
        if(count <= maxCount)
        {
            att3Flg = true;
        }
        else
        {
            count = 0;
            att3Flg = false;
            attStop();
        }
    }
    public void attStop()
    {
        posFlg = false;
        randomTime = 0;
        randomFlg = false;
        jumpFlg = false;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "leftwall" || other.gameObject.tag == "rightwall")
        {
            attStop();
        }
        if(fallflg == true && other.gameObject.tag == "ground")
        {
            sin = 0;
            att1Flg = false;
            jumpFlg = true;
            fallflg = false;
        }
    }
}
