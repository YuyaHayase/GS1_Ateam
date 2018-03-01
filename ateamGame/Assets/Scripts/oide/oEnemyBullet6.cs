using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyBullet6 : MonoBehaviour {//反射弾
    Quaternion f;
    float bulletAngre;//現在の角度を取得
    int i;//Zが180度より大きいと2,違うなら1
    public int maxCount;//反射の最大数
    int count = 0;//反射した回数
    oBase mother;//※必須
    GameObject obj;
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Reference");//oBaseの入っているオブジェクトを探す
        mother = obj.GetComponent<oBase>();
        bulletAngre = transform.localEulerAngles.z;//今の角度を取得
        i =  mother.firstangle(bulletAngre);
        //transform.rotation = Quaternion.Euler(0, 0, bulletAngre);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0.2f　* mother.enemySpeed, 0, 0);
        //transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        bulletAngre = transform.localEulerAngles.z;//今の角度を取得
        i =  mother.firstangle(bulletAngre);

        //firstangle(bulletAngre);//判定
        if(other.gameObject.tag != "damage")//弾以外に当たった時
        {
            if (count >= maxCount)//既定の回数だけ反射していたら
            {
                Destroy(gameObject);//オブジェクトを消す
            }

        }
        if (other.gameObject.tag == "topwall")//上の壁
        {
            count += 1;
            bulletAngre =  mother.Reflection(1, i, bulletAngre);
        }
        else if (other.gameObject.tag == "ground")//床
        {
            count += 1;
            bulletAngre = mother.Reflection(2, i, bulletAngre);
        }
        else if (other.gameObject.tag == "leftwall")//左の壁
        {
            count += 1;
            bulletAngre = mother.Reflection(3, i, bulletAngre);
        }
        else if (other.gameObject.tag == "rightwall")
        {
            count += 1;
            bulletAngre = mother.Reflection(4, i, bulletAngre);
        }
        else if(other.gameObject.tag == "damage2")
        {
            Destroy(gameObject);
        }
        transform.rotation = Quaternion.Euler(0, 0, bulletAngre);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
