using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyMove11 : MonoBehaviour {
    int random;
    float playerPos;
    float randomTime = 0.0f;//攻撃間隔
    float randomPos = 0.0f;
    int direction;
    public bool randomFlg = false;
    bool posFlg = false;
    GameObject parent;
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject bullet3;
    int count = 0;
    float time;
    bool att3Flg = false;
    GameObject obj;//※必須
    oBase mother;//※必須
    float rotateNumber;
    float[] pos = new float[5];
    // Use this for initialization
    void Start () {
        parent = GameObject.Find("Boss3child");
        obj = GameObject.Find("Reference");
        mother = obj.GetComponent<oBase>();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.tag == "enemy")
        {
            randomTime += Time.deltaTime * mother.enemySpeed;
            if (randomTime >= 2)
            {
                if (randomFlg == false)
                {

                    random = Random.Range(1, 4);
                    randomFlg = true;
                }

                switch (random)
                {
                    case 1:
                        att1();
                        break;
                    case 2:
                        att2();
                        break;
                    case 3:
                        att3();
                        break;
                }
            }
        }
    }
    void att1()
    {
        playerPos =  mother.playerposition();
        for(int i = 0; i <= 1; i++)
        {
            GameObject bulletInstance = Instantiate(bullet) as GameObject;
            if (i == 0)
            {
                bulletInstance.transform.position = new Vector3(playerPos + 3, 8, 0);
            }
            else
            {
                bulletInstance.transform.position = new Vector3(playerPos - 3, 8, 0);
            }
        }
        attStop();
    }
    void att2()
    {
        transform.Rotate(0, 0, rotateNumber);
        rotateNumber += 0.1f;
        if (rotateNumber >= 20)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GameObject bulletInstance = Instantiate(bullet2) as GameObject;
            bulletInstance.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            rotateNumber = 0;
            attStop();
        }

    }
    void att3()
    {
        if(att3Flg == false)
        {
            for (int i = 0; i <= 4; i++)
            {
                randomPos = Random.Range(-9, 7);
                pos[i] = randomPos;
                GameObject bulletInstance = Instantiate(bullet3) as GameObject;
                bulletInstance.transform.position = new Vector3(pos[i], transform.position.y, 0);
            }
            att3Flg = true;
        }
        time += Time.deltaTime * mother.enemySpeed;
        if(time >= 0.5f)
        {
            parent.transform.position = new Vector3(pos[count], transform.position.y, 0);
            count++;
            time = 0;
        }
        if(count == 5)
        {
            att3Flg = false;
            count = 0;
            attStop();
        }
    }
    public void attStop()
    {
        posFlg = false;
        randomTime = 0;
        randomFlg = false;
    }
}
