﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyMove2 : MonoBehaviour {
    float time = 0.0f;//回転を始める間隔
    bool flg = false;//falseならf(回転の数値)を増やす、trueならfを減らす
    float f;//回転の数値、少しずつ大きくしていく
    float enemyMoveDistance　= 0.0f;//移動距離、値が増えれば移動距離が多くなる
    public float x;//この値だけ移動距離を足していく
    bool posflg = false;//ポジションをとるためのフラグ
    GameObject obj;//※必須
    oBase mother;//※必須
    int direction;//向き※必須
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Reference");//ベースの入っているオブジェクトを取得、名前を変えて
        mother = obj.GetComponent<oBase>();
    }
	// Update is called once per frame
	void Update () {
        if (transform.tag == "enemy")
        {
            time += Time.deltaTime * mother.enemySpeed;
            if (time > 2)//2秒後
            {
                if (posflg == false)
                {
                    //※必須
                    mother = obj.GetComponent<oBase>();
                    direction = mother.Playerposition(transform.position);
                    posflg = true;
                    //※必須
                }

                if (f <= 40 && flg == false)//回転の大きさが40以下かつ、フラグがfalseなら
                {
                    f += 0.1f * mother.enemySpeed;//回転の大きさを増やす
                }
                else//回転の大きさが40を超えたら
                {
                    flg = true;//減らすためにフラグをtrueにする
                }
                if (flg == true)//フラグがtrueなら
                {
                    if (f >= 0)//回転の大きさが0以上なら
                    {
                        f -= 0.1f * mother.enemySpeed;//減らしていく
                        enemyMoveDistance += x * mother.enemySpeed;//移動する値を増やしていく
                        transform.position = new Vector3(transform.position.x + enemyMoveDistance * direction, transform.position.y, transform.position.z);//移動
                    }
                    else
                    {
                        enemyMoveDistance -= x * mother.enemySpeed;//移動する値を減らしていく
                        if (enemyMoveDistance <= 1)//移動距離が0以下になったら
                        {
                            time = 0;//時間のカウントを0にする
                            flg = false;//フラグをfalseにする
                            posflg = false;//※必須
                        }
                    }
                }
            }
        }
	}
}
