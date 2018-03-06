using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyBullet12 : MonoBehaviour {//爆発
    float alpha = 0.93f;
    float time = 0.0f;
    Renderer rend;
    Color color;
    GameObject obj;//※必須
    oBase mother;//※必須
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Reference");
        mother = obj.GetComponent<oBase>();
        color.r = 1.0f;
        color.g = 1.0f;
        color.b = 1.0f;
        //Destroy(gameObject,1);
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime　* mother.enemySpeed;
        if(time <= 0.7f)
        {
            transform.Translate(0, 0.104f * mother.enemySpeed, 0);
            transform.localScale = new Vector3(transform.localScale.x + 0.08f * mother.enemySpeed, transform.localScale.y + 0.08f * mother.enemySpeed, transform.localScale.z);
        }
        else
        {
            Renderer rend = GetComponent<SpriteRenderer>();
            rend.material.color = color;
            if (alpha >= 0.05f)
            {
                alpha -= 0.01f * mother.enemySpeed;
                color.a = alpha;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }
}
