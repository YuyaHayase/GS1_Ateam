using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class yFade : MonoBehaviour {

    Image fadeImage;

    float alpha = 1;

    [SerializeField, Header("In,Outするときのフレームカウント")]
    float frameCount = 5;

    [SerializeField, Header("Alpha値が減っていくスピード")]
    float alphaSpeed = 0.1f;
    [SerializeField,Header("次のシーンへ")]
    string nextScene;
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
        fadeImage = this.GetComponent<Image>();
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
    }

    // Update is called once per frame
    void Update () {
        if (flgFadeIn && Time.frameCount % frameCount == 0)
            FadeIn();
        if (flgFadeOut && Time.frameCount % frameCount == 0)
            FadeOut();
	}
    private void FadeIn()
    {
        print("FadeIn");
        alpha = Mathf.Min(1.0f, alpha -= alphaSpeed);
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);

        if (alpha <= 0.0f)
            flgFadeIn = false;
    }
    private void FadeOut()
    {
        print("FadeOut");
        alpha = Mathf.Max(0.0f, alpha += alphaSpeed);
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);

        if (alpha >= 1.0f)
            SceneManager.LoadScene(nextScene);
    }
}
