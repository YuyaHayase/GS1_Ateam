using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyBullet3 : MonoBehaviour {
    public float bulletSpeed;
    GameObject obj;//※必須
    oBase mother;//※必須
                 // Use this for initialization
    void Start () {
        obj = GameObject.Find("Reference");
        mother = obj.GetComponent<oBase>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(bulletSpeed * mother.enemySpeed, 0, 0);
	}
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
