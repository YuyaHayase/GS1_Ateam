using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class hTutorialCtrl : MonoBehaviour {

    float delta = 0.1f;
    bool sleep = false;

    [SerializeField, Header("MainCamera")]
    GameObject Camera;

	// Use this for initialization
	void Start () {
        if (Camera == null) Camera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
        if (hKeyConfig.GetKey("Submit") || Input.GetKey(KeyCode.Space))
        {
            delta += Time.deltaTime / 4f;
            sleep = true;
        }
        if (hKeyConfig.GetKeyUp("Submit") || Input.GetKeyUp(KeyCode.Space))
        {
            delta = 0.1f;
            sleep = false;
        }

        if (delta >= 0.999f)
        {
            delta = 0.999f;
            SceneManager.LoadScene("Main");
        }
        if (sleep) Camera.GetComponent<VignetteAndChromaticAberration>().intensity = delta;
    }
}
