using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyBullet13 : MonoBehaviour {
    public GameObject bullet;//弾
    public float f;
    Vector3 pos;//自身のポジション
    bool flg = false;
    bool flg2 = false;
    int direction;//向き※必須
    GameObject reference;
    oBase mother;
    // Use this for initialization
    void Start () {
        pos = transform.position;
        Destroy(gameObject,f);//f秒後に消す
        reference = GameObject.Find("Reference");
        mother = reference.GetComponent<oBase>();
        direction = mother.Playerposition(pos);
    }
	
	// Update is called once per frame
	void Update () {
        if (flg2 == false)
        {
            transform.Translate(0, 0.05f, 0);
        }
        if (Mathf.Abs(pos.y - transform.position.y) >= 1)
        {
            flg2 = true;
        }
		if(flg == false && flg2 == true)
        {
            GameObject bulletInstance = Instantiate(bullet) as GameObject;
            bulletInstance.transform.position = new Vector3(transform.position.x + 1 * direction, 0, 0);
            flg = true;
        }
	}
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
