using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyBullet14 : MonoBehaviour {
    Vector3 pos;
    GameObject obj;//※必須
    oBase mother;//※必須
    float angle;
    bool flg = false;
    // Use this for initialization
    void Start () {
        pos = transform.position;
        obj = GameObject.Find("Reference");
        mother = obj.GetComponent<oBase>();
    }
	
	// Update is called once per frame
	void Update () {
        if (flg == false)//角度取得用のフラグがfalseなら
        {
            angle = mother.Playerangle(pos);//プレイヤーとの角度を取得
            transform.rotation = Quaternion.Euler(0, 0, angle);//角度を変える
            flg = true;//角度取得用のフラグをtrueにする
        }
        transform.Translate(-0.1f * mother.enemySpeed, 0, 0);
    }
}
