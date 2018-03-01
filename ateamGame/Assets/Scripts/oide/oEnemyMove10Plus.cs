using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyMove10Plus : MonoBehaviour {
    public GameObject bullet;
    int count;
    float time;
    public int i;
    public int bulletCount
    {
        get
        {
            return count;
        }
    }
    public int maxCount;
    GameObject obj;//※必須
    oBase mother;//※必須
    GameObject boss2;//※必須
    oEnemyMove10 parent;//※必須
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Reference");
        mother = obj.GetComponent<oBase>();
        boss2 = GameObject.Find("boss2");
        parent = boss2.GetComponent<oEnemyMove10>();
    }
	
	// Update is called once per frame
	void Update () {
        if (parent.flg3 == true)
        {
            bulletshot();
        }
	}
    void bulletshot()
    {
        time += Time.deltaTime * mother.enemySpeed;
        if(time >= 1)
        {
            GameObject bulletInstance = Instantiate(bullet) as GameObject;
            bulletInstance.transform.rotation = Quaternion.Euler(0, 0, Mathf.Abs(30 - i));
            bulletInstance.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            parent.count++;
            time = 0;
        }
    }
}
