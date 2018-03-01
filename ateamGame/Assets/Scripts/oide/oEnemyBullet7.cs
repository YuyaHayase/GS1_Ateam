using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyBullet7 : MonoBehaviour {//エ〇サー
    GameObject obj;//※必須
    oBase mother;//※必須
    float time = 0.0f; 
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Reference");
        mother = obj.GetComponent<oBase>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, 100 * mother.enemySpeed);
        time += Time.deltaTime * mother.enemySpeed;
        if(time >= 2)
        {
            Destroy(gameObject);
        }

    }

}
