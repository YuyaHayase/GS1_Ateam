using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTutorialUSA : MonoBehaviour {

    bool ro = false;
    float VectorX;
    float delta = 2;
    float d;

    AudioSource aus;

    void Start()
    {
        if (aus == null) aus = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
        if (ro)
        {
            d += Time.deltaTime;
            if (transform.rotation.z > 20 || transform.rotation.z < -20) delta = -delta;

            if (d > 3.2f)
            {
                transform.rotation = Quaternion.identity;
                delta = 2;
                d = 0;
                ro = false;
            }

            VectorX += Time.deltaTime * delta;
            transform.Rotate(new Vector3(0, 0, 1), Mathf.Cos(VectorX));
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "weapon")
        {
            ro = true;
            VectorX = -(transform.position.x - col.transform.position.x) / 4f;
            aus.Play();
        }
    }
}
