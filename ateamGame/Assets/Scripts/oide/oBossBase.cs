using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oBossBase : MonoBehaviour {
    GameObject player;
    float[] ii = new float[50];//何度間隔で弾を配置するかの記憶場所
    bool flg = false;
    GameObject bulletInstance;

    float rnd;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Att1(int way,int min,int max,int enemyMode, float angle, GameObject bullet,float posx,float posy)//拡散弾
    {
        //間隔
        if(flg == false)
        {
            player = GameObject.Find("Player");//使うときはPlayerに変える
            if (max == Mathf.Abs(min))//maxとminの絶対値の値が同じなら
            {
                ii[0] = min;//0番に最小値を記憶
            }
            else if (enemyMode == 1)//maxとminの絶対値の値が違い、Playerをロックオンする場合
            {
                //maxとminの絶対値を揃える作業
                max = max + Mathf.Abs(min) / 2;
                min = max * -1;
                ii[0] = min;//0番に最小値を記憶
            }
            if (max + Mathf.Abs(min) > 180)//角度が180度で収まらない場合
            {
                for (int i = 1; i < way; i++)
                {
                    ii[i] = ii[i - 1] + (max + Mathf.Abs(min)) / way;//間隔を求めている
                }
            }
            else
            {
                for (int i = 1; i < way; i++)
                {
                    ii[i] = ii[i - 1] + (max + Mathf.Abs(min)) / (way - 1);
                }
            }
            flg = true;
        }
        //弾発射
        if (way == 1)//1wayの時
        {
            bulletInstance = Instantiate(bullet) as GameObject;
            bulletInstance.transform.rotation = Quaternion.Euler(0, 0, 0 + angle - 180);
            bulletInstance.transform.position = new Vector3(posx, posy, 0);//弾を配置
        }
        else//way数が1より大きい時
        {
            for (int i = 0; i < way; i++)
            {
                bulletInstance = Instantiate(bullet) as GameObject;
                if (enemyMode == 1)
                {
                    bulletInstance.transform.rotation = Quaternion.Euler(0, 0, ii[i] + angle - 180);
                }
                else
                {
                    bulletInstance.transform.rotation = Quaternion.Euler(0, 0, ii[i] + angle);
                }
                bulletInstance.transform.position = new Vector3(posx, posy, 0);//弾を配置
            }
        }
    }
    public void Att2(GameObject bullet2,Vector3 enemyPos)
    {
        do
        {
            rnd = Random.Range(enemyPos.x -5, enemyPos.x +  5);
        } while (Mathf.Abs(rnd - enemyPos.x) <= 1);
        bulletInstance = Instantiate(bullet2) as GameObject;
        bulletInstance.transform.position = new Vector3(rnd, 0, 0);//弾を配置
        rnd = 0;
    }
}
