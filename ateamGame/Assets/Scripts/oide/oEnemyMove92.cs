using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyMove92 : MonoBehaviour {
    //プレイヤーの方向を向いたかどうかのフラグ
    bool flg = false;
    public bool directionFlg
    {
        get
        {
            return flg;
        }
    }

    //Enemy移動のフラグがfalseなら移動trueなら停止
    bool moveFlg = false;
    public bool enemy9MoveFlg
    {
        get
        {
            return moveFlg;
        }
    }

    GameObject enemy9;
    oEnemyMove9 enemy9Move;
    GameObject obj;//※必須
    oBase mother;//※必須

    //自身のポジション
    Vector3 pos;
    public Vector3 enemyPosition
    {
        get
        {
            return pos;
        }
    }

    float time = 0.0f;
    float angle;//角度

    // Use this for initialization
    void Start () {
        enemy9 = GameObject.Find("Enemy9");
        enemy9Move = enemy9.GetComponent<oEnemyMove9>();
        obj = GameObject.Find("Reference");
        mother = obj.GetComponent<oBase>();
    }
	
	// Update is called once per frame
	void Update () {
        pos = transform.position;
    }
    public void oEnemymove92(int direction)//回転しながら移動(回転は子オブジェクトが担当)
    {
        if (moveFlg == false)//Enemy移動のフラグがfalseなら
        {
            if (flg == false)//角度取得用のフラグがfalseなら
            {
                angle =  mother.Playerangle(pos);//プレイヤーとの角度を取得
                transform.rotation = Quaternion.Euler(0, 0, angle);//角度を変える
                flg = true;//角度取得用のフラグをtrueにする
            }
            transform.Translate(-0.1f, 0, 0);//移動
        }
    }
    public void oEnemymove9parts(int i)
    {
        switch (i)
        {
            case 1:
                moveFlg = true;
                flg = false;//角度取得用のフラグがfalseに戻す
                enemy9Move.oEnemymove9_time();
                enemy9Move.oEnemymove9_childposflg();
                break;
            case 2:
                moveFlg = false;
                break;
        }
    }
}
