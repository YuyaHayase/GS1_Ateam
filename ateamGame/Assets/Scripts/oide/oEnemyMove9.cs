using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyMove9 : MonoBehaviour {
    float angle;
    public float enemyAngle
    {
        get
        {
            return angle;
        }
    }
    bool posFlg = false;//ポジションをとるためのフラグ
    public bool childPosFlg
    {
        get
        {
            return posFlg;
        }
    }
    int direction;//向き※必須
    GameObject obj;oBase mother; oBossBase boss;//1つは共通、もう一つはボス専用
    GameObject enemy9; oEnemyMove92 enemy9Move;
    GameObject Boss;
    Vector3 pos;//自身のポジション取得
    public GameObject att1;//パターン1の弾
    public GameObject att2;//パターン3の弾
    GameObject bulletInstance;
    int count = 0;//出した弾の数のカウント
    float time = 0.0f;
    int rotationSpeed = 1;//回転速度
    public int min; public int max;public int way;public int enemyMode;//拡散弾を出すために必要
    public float att1time, att2time, atttime3;
    
    float randomTime = 0.0f;bool randomFlg = false;int rnd;
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Reference");//oBaseの入っているオブジェクトを探す
        mother = obj.GetComponent<oBase>();
        enemy9 = GameObject.Find("Enemy9");//oBossBaseの入っているオブジェクトを探す
        enemy9Move = enemy9.GetComponent<oEnemyMove92>();
        boss = obj.GetComponent<oBossBase>();

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.tag == "enemy")
        {
            StartCoroutine("rotate");
            randomTime += Time.deltaTime * mother.enemySpeed;
            if (randomTime >= 1)//2秒たったら
            {
                if (randomFlg == false)
                {
                    rnd = Random.Range(1, 4);//乱数発生
                    randomFlg = true;
                }
                switch (rnd)
                {
                    case 1:
                        oEnemymove9_att1();
                        break;
                    case 2:
                        oEnemymove9_att2();
                        break;
                    case 3:
                        oEnemymove9_att3();
                        break;
                }
            }
            if (enemy9Move.enemy9MoveFlg == true)//2終了時
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    enemy9Move.transform.rotation = Quaternion.Euler(0, 0, 0);
                    if (enemy9Move.transform.position.y <= 15)
                    {
                        enemy9Move.transform.Translate(0.01f * direction * -1 * mother.enemySpeed, 0.12f, 0);
                    }
                    else
                    {
                        rotationSpeed = 1;
                        oEnemymove9_attstop();//攻撃終了の処理をする
                        enemy9Move.oEnemymove9parts(2);
                    }
                }

            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "weapon"&& other.gameObject.tag != "Player")
        {
            enemy9Move.oEnemymove9parts(1);
        }
    }
    void oEnemymove9_rotation()
    {
        transform.Rotate(0, 0, 1 * rotationSpeed * mother.enemySpeed);
    }
    void oEnemymove9_att1()//一定間隔で弾を出す
    {
        time += Time.deltaTime * mother.enemySpeed;
        if(time >= att1time && count<= 3 )
        {
            pos = transform.position;
            angle = mother.Playerangle(enemy9Move.enemyPosition);
            boss.Att1(way,min,max,enemyMode,angle,att1,pos.x,pos.y);
            count++;
            time = 0;
        }
        else if(count == 4)
        {
            count = 0;
            oEnemymove9_attstop();
        }
    }
    void oEnemymove9_att2()//早く回転してから移動
    {
        rotationSpeed = 5;//回転スピードを変更させる
        time += Time.deltaTime * mother.enemySpeed;
        if(time >= att2time)//5秒たったら
        {
            if (posFlg == false)//Playerのポジションをとっていないなら
            {
                //※必須
                direction = mother.Playerposition(transform.position);//Playerの位置を判断
                posFlg = true;
            }
            enemy9Move.oEnemymove92(direction);//移動
        }
    }
    void oEnemymove9_att3()//落雷
    {
        time += Time.deltaTime * mother.enemySpeed;
        if(time >= atttime3)
        {
            boss.Att2(att2,transform.position);
            time = 0;
            count++;
        }
        if(count >= 10)
        {
            oEnemymove9_attstop();
            count = 0;
        }
    }
    void oEnemymove9_attstop()//攻撃終了時
    {
        posFlg = false;//※必須
        time = 0;
        randomFlg = false;
        randomTime = 0.0f;
    }
    public void oEnemymove9_childposflg()
    {
        posFlg = false;
    }
    public void oEnemymove9_time()
    {
        time = 0;
    }
    IEnumerator rotate()
    {
        if (enemy9Move.enemy9MoveFlg == false)
        {
            oEnemymove9_rotation();
        }
        yield return null;
    }
}
