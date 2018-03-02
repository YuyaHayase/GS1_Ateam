using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hTutorialCtrl : MonoBehaviour {

    float delta = 0;

    [SerializeField, Header("BAr")]
    GameObject Bar;

	// Use this for initialization
	void Start () {
        if (Bar == null) Bar = GameObject.Find("Bar");
	}
	
	// Update is called once per frame
	void Update () {
        if (hKeyConfig.GetKey("Submit")) delta += Time.deltaTime;
        if (hKeyConfig.GetKeyUp("Submit")) delta = 0;

        Bar.transform.localScale = new Vector3(delta, 1, 1);
        if (delta > 5) SceneManager.LoadScene("Main");
	}
}
