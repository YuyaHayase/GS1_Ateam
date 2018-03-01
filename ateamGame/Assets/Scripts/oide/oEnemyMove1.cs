using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyMove1 : MonoBehaviour{
    float cos;//コサインの値
    public float movement;//移動距離　0.1ぐらいからちょうどいい
    float time = 2;
    bool posflg = false;
    Vector2 enemyPotision;//敵のポジションを取得
    GameObject obj;//※必須
    oBase mother;//※必須
    int direction;//向き※必須
    Vector3 pos;
    bool cosFlg = false;
    // Use this for initialization
    void Start () {
        enemyPotision = transform.position;//enemyの座標を取得(必要かどうかは知らないです)
        obj = GameObject.Find("Reference");//名前を変えて
        mother = obj.GetComponent<oBase>();
    }

    // Update is called once per frame
    void Update()//コサインの値を変更させて移動
    {
        pos = transform.position;
        if (transform.tag == "enemy")
        {
            time += Time.deltaTime * mother.enemySpeed;
            if (time >= 2.0f)//2秒間隔
            {
                if (posflg == false)
                {
                    //※必須
                    mother = obj.GetComponent<oBase>();
                    direction = mother.Playerposition(transform.position);
                    //※必須
                    posflg = true;
                }
                if(cosFlg == true)
                {
                    transform.Translate(0.13f * direction * mother.enemySpeed, -0.33f * mother.enemySpeed, 0);
                }
                else
                {
                    cos += 0.1f * mother.enemySpeed;//コサインの値を増やす
                    transform.Translate(movement * direction * mother.enemySpeed, Mathf.Cos(cos) * 0.5f * mother.enemySpeed, 0);//山なりに移動
                }
                if (pos.y > transform.position.y)
                {
                    cosFlg = true;
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)//床などに当たった時に移動を停止させる(Playerに当たったときはそのまま移動を続けるようにお願いします)
    {
        //tagか何かで判定できるといいかもしれない
        cos = 0;//コサインの値を0にする
        time = 0;
        posflg = false;
        cosFlg = false;
    }
}
