using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyMove4 : MonoBehaviour {//ランダム移動
    bool flg = false;
    float time;
    int enemyAttack = 1;//1なら移動、2なら単発、3なら連射
    int count = 0;//連射のカウント
    int direction;
    public int lifeCount = 0;//自爆までのカウント、積み防止
    Vector3 enemyPosition;//処置開始時のポジション
    public GameObject bullet;//弾
    GameObject bulletInstance;
    GameObject obj;//※必須
    oBase mother;//※必須
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Reference");//ベースの入っているオブジェクトを取得、名前を変えて
        mother = obj.GetComponent<oBase>();
    }
	// Update is called once per frame
	void Update () {
        if (transform.tag == "enemy")
        {
            if (enemyAttack == 1)
            {
                oEnemymove4_pattern1();
            }
            else
            {
                oEnemymove4_pattern3(enemyAttack);
            }
        }
        if(lifeCount >= 4)
        {
            //Destroy(gameObject);
        }
    }
    public void oEnemymove4_pattern1()//移動
    {
        if(flg == false)
        {
            enemyPosition = transform.position;
            int random = Random.Range(1, 9);//移動する角度を決める
            transform.rotation = Quaternion.Euler(0, 0, 45 * random);//角度を変える
            flg = true;
        }
        if(Mathf.Abs(transform.position.x - enemyPosition.x) <= 2 && Mathf.Abs(transform.position.y - enemyPosition.y) <= 2)//一定距離移動するまで
        {
            transform.Translate(0.02f * mother.enemySpeed, 0, 0);//移動
        }
        else
        {
            oEnemymove4_pattern2(ref enemyAttack);//待機するメソッド
        }
        //StartCoroutine("oEnemymove4_pattern2");
    }
    public void oEnemymove4_pattern2(ref int i)//待機
    {
        time += Time.deltaTime * mother.enemySpeed;
        if (time >= 2)
        {
            flg = false;
            time = 0;
            i = Random.Range(2, 4);
        }
    }
    public void oEnemymove4_pattern3(int enemy)//弾発射
    {
        if(enemy == 2)//単発
        {
            direction =  mother.Playerposition(transform.position);
            bulletInstance = Instantiate(bullet) as GameObject;
            if(direction == -1)
            {
                bulletInstance.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            bulletInstance.transform.position = new Vector3(transform.position.x, transform.position.y, 0);//弾を配置
            enemyAttack = 1;
            lifeCount++;
        }
        else if(enemy == 3)//3連射
        {
            time += Time.deltaTime;
            if(time >= 0.5f)
            {
                time = 0;
                bulletInstance = Instantiate(bullet) as GameObject;
                if (direction == -1)
                {
                    bulletInstance.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
                bulletInstance.transform.position = new Vector3(transform.position.x, transform.position.y, 0);//弾を配置
                count++;
            }
            if(count == 3)//3発撃ったら
            {
                count = 0;
                enemyAttack = 1;
                lifeCount++;
            }
            
        }
    }
}
