using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyBullet13 : MonoBehaviour {
    public GameObject bullet;//弾
    public float f;
    Vector3 pos;//自身のポジション
    bool flg = false;//オブジェクトを出す
    bool flg2 = false;//上に上がる
    int direction;//向き※必須
    GameObject reference;
    oBase mother;
    GameObject boss;
    oEnemyMove10 boss2;
    // Use this for initialization
    void Start () {
        pos = transform.position;
        Destroy(gameObject,f);//f秒後に消す
        reference = GameObject.Find("Reference");
        mother = reference.GetComponent<oBase>();
        boss = GameObject.Find("boss2");
        boss2 = boss.GetComponent<oEnemyMove10>();
    }
	
	// Update is called once per frame
	void Update () {
        if (flg2 == false)
        {
            transform.Translate(0, 0.1f * mother.enemySpeed, 0);
        }
        if (Mathf.Abs(pos.y - transform.position.y) >= 1)
        {
            flg2 = true;
        }
		if(flg == false && flg2 == true)
        {
            GameObject bulletInstance = Instantiate(bullet) as GameObject;
            bulletInstance.transform.position = new Vector3(transform.position.x + 1 * boss2.boss2Direction, 0, 0);
            flg = true;
        }
        if(transform.position.x >= 10||transform.position.x <= -10)
        {
            boss2.attStop();
            Destroy(gameObject);
        }
	}
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
