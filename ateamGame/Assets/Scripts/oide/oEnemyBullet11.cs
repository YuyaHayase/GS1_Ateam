using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyBullet11 : MonoBehaviour//分裂弾
{
    public GameObject bullet;//弾
    GameObject bulletInstance;
    GameObject player;
    float time = 0.0f;
    int min = 0;//角度の最小値
    int max = 360;//角度の最大値
    public float f;
    public float way;//拡散弾をいくつに分けるか
    float[] ii = new float[50];//何度間隔で弾を配置するかの記憶場所
    float x, y;//ベクトルx.y
    GameObject obj;//※必須
    oBase mother;//※必須
                 // Use this for initialization
    void Start()
    {
        obj = GameObject.Find("Reference");
        mother = obj.GetComponent<oBase>();
        ii[0] = min;//0番に最小値を記憶
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
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.08f * mother.enemySpeed, 0, 0);
        oEnemymove5_Att1();
    }
    public void oEnemymove5_Att1()
    {
        time += Time.deltaTime * mother.enemySpeed;
        if (time >= 2)
        {
            for (int i = 0; i < way; i++)
            {
                bulletInstance = Instantiate(bullet) as GameObject;
                bulletInstance.transform.rotation = Quaternion.Euler(0, 0, ii[i]);
                bulletInstance.transform.position = new Vector3(transform.position.x, transform.position.y, 0);//弾を配置
            }
            Destroy(gameObject);
        }
    }
}
