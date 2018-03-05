using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTitleArrow : MonoBehaviour {

    float colors;

	// Update is called once per frame
	void Update () {
        colors = 1.0f - Mathf.Cos(Time.time) / 2f;
        GetComponent<SpriteRenderer>().color = new Color(colors, colors, colors);
	}
}
