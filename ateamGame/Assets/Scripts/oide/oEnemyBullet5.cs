using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyBullet5 : MonoBehaviour {
    public float speed;
    GameObject obj;//※必須
    oBase mother;//※必須
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Reference");
        mother = obj.GetComponent<oBase>();
        Destroy(gameObject, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, 20);
        transform.position = new Vector3(transform.position.x,transform.position.y - speed * mother.enemySpeed, transform.position.z);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
