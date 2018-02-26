using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyMove9Parts : MonoBehaviour {
    GameObject enemy9;
    oEnemyMove92 enemy9Move;
    // Use this for initialization
    void Start () {
        enemy9 = GameObject.Find("Enemy9");
        enemy9Move = enemy9.GetComponent<oEnemyMove92>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        enemy9Move.oEnemymove9parts(1);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        enemy9Move.oEnemymove9parts(2);
    }
}
