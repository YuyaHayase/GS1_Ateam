using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;



public class yVignetteFade : MonoBehaviour {
    [SerializeField, Header("ビネット")]
    VignetteAndChromaticAberration vignette;

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


    // Use this for initialization
    void Start () {
        if (vignette == null)
            vignette = GetComponent<VignetteAndChromaticAberration>();
        vignette.intensity = 1;
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
        vignette.intensity = Mathf.Min(1.0f, vignette.intensity -= Time.deltaTime / 2.0f);

        if (vignette.intensity <= min)
            flgFadeIn = false;
    }
    private void FadeOut()
    {
        print("FadeOut");
        vignette.intensity = Mathf.Min(1.0f, vignette.intensity += Time.deltaTime / 2.0f);

        if (vignette.intensity >= 1.0f)
            Invoke("Next", 1.0f);
    }

    private void Next()
    {
        SceneManager.LoadScene(nextScene);
    }
}
