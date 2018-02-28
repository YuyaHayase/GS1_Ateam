using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyBullet5Plus : MonoBehaviour {
    public GameObject bullet;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1.5f);
        GameObject go = Instantiate(bullet) as GameObject;
        go.transform.position = new Vector3(transform.position.x, transform.position.y + 13, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
