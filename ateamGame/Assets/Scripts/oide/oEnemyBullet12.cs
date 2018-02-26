using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oEnemyBullet12 : MonoBehaviour {
    float alpha = 0.93f;
    float time = 0.0f;
    Renderer rend;
    Color color;
    // Use this for initialization
    void Start () {
        color.r = 1.0f;
        color.g = 1.0f;
        color.b = 1.0f;
        //Destroy(gameObject,1);
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time <= 1)
        {
            transform.Translate(0, 0.104f, 0);
            transform.localScale = new Vector3(transform.localScale.x + 0.08f, transform.localScale.y + 0.08f, transform.localScale.z);
        }
        else
        {
            Renderer rend = GetComponent<SpriteRenderer>();
            rend.material.color = color;
            if (alpha >= 0.05f)
            {
                alpha -= 0.01f;
                color.a = alpha;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }
}
