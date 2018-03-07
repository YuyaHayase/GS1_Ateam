using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;



public class yVignetteFade : MonoBehaviour {
    [SerializeField, Header("ビネット")]
    GameObject vignette;

    [SerializeField, Header("次のシーンへ")]
    string nextScene;

    [SerializeField,Header("ビネットの最小値")]
    float min = 0.324f;
    bool flgFadeIn = true;
    bool flgFadeOut = false;

    public string NextScene
    {
        set { nextScene = value; }
    }
    public bool FlgFadeIn
    {
        get { return flgFadeIn; }
    }
    public bool FlgFadeOut
    {
        set { flgFadeOut = value; }
    }

    float delta;
    // Use this for initialization
    void Start () {
        if (vignette == null)
            vignette = GameObject.Find("Main Camera");
        delta = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (flgFadeIn)
            FadeIn();
        if (flgFadeOut)
            FadeOut();

    }

    private void FadeIn()
    {
        print("FadeIn");
        vignette.GetComponent<VignetteAndChromaticAberration>().intensity -= Time.deltaTime / 2.0f;

        if (vignette.GetComponent<VignetteAndChromaticAberration>().intensity <= min)
            flgFadeIn = false;
    }
    private void FadeOut()
    {
        delta += Time.deltaTime / 4.0f;
        print(delta);
        vignette.GetComponent<VignetteAndChromaticAberration>().intensity = delta;

        if (delta >= 0.99999f) Next();
            //Invoke("Next", 1.0f);
    }

    private void Next()
    {
        SceneManager.LoadScene(nextScene);
    }
}
