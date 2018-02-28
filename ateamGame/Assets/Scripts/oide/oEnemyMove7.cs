using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyMove7 : MonoBehaviour {
    float time = 0;
    GameObject obj;
    public GameObject bomb;
    float sin;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("x");
        time += Time.deltaTime;

        if(time >= 2)
        {
            GameObject go = Instantiate(bomb) as GameObject;
            go.transform.position = new Vector3(transform.position.x, transform.position.y + 2, 0);
            Destroy(gameObject);
        }
	}
    IEnumerator x()
    {
        sin += 1.0f;
        transform.Translate(Mathf.Sin(sin), 0, 0);
        yield return new WaitForSeconds(2);
    }
}
