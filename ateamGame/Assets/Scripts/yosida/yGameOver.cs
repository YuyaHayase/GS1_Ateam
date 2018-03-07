using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yGameOver : MonoBehaviour {

    SpriteRenderer[] gameOverImage;
    Vector3[] endPos;

    int number = 0;

    [SerializeField,Header("文字が落ちてくるスピード")]
    float speed = 1.0f;

    yVignetteFade vFade;

	// Use this for initialization
	void Start () {
        vFade = Camera.main.GetComponent<yVignetteFade>();

        //子オブジェクトにあるものを全て取得
        gameOverImage = this.GetComponentsInChildren<SpriteRenderer>();

        endPos = new Vector3[gameOverImage.Length];

        for(int i = 0;i < endPos.Length; i++)
        {
            //初期値をendPosに入れる
            endPos[i] = gameOverImage[i].transform.position;
            //ImageのPositionを変える
            gameOverImage[i].transform.position += new Vector3(0, 10.0f, 0);
        }

        StartCoroutine("ImageMove");
    }

    // Update is called once per frame
    void Update () {

    }

    IEnumerator ImageMove()
    {
        yield return new WaitWhile(() => vFade.FlgFadeIn);//FadeInがtrueになったとき
        yMusicManager.instance.MusicSound(0);//ゲームオーバーのBGMを流す

        for (int i = 0;i < gameOverImage.Length; i++)
        {
            StartCoroutine(GameOver(gameOverImage[i], endPos[i]));
            yield return new WaitForSeconds(0.1f);//0.1秒松待つ
        }
        yield break;
    }

    IEnumerator GameOver(SpriteRenderer charcter,Vector3 pos)
    {
        while (true)
        {
            charcter.transform.position = Vector3.MoveTowards(charcter.transform.position, pos, speed);
            if (charcter.transform.position == pos)
            {
                number++;
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }

        //最後の文字がendPosについたらFadeOut
        if (number == gameOverImage.Length)
        {
            yield return new WaitForSeconds(5.0f);
            vFade.FlgFadeOut = true;
            yield return new WaitForSeconds(1.0f);
            yMusicManager.instance.MusicEffectStop();

        }


        yield break;
    }
}
