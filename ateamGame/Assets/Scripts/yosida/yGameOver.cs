using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yGameOver : MonoBehaviour {

    Image[] gameOverImage;
    Vector3[] imagePos;
    float speed = 1.0f;

	// Use this for initialization
	void Start () {
        //子オブジェクトにあるものを全て取得
        gameOverImage = this.GetComponentsInChildren<Image>();

        imagePos = new Vector3[gameOverImage.Length];

        for(int i = 0;i < imagePos.Length; i++)
        {
            imagePos[i] = gameOverImage[i].transform.localPosition;
            gameOverImage[i].transform.localPosition += new Vector3(0, 200.0f, 0);
            StartCoroutine("ImageMove");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ImageMove()
    {
        for(int i = 0;i < gameOverImage.Length; i++)
        {
            StartCoroutine(GameOver(gameOverImage[i], imagePos[i]));
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    IEnumerator GameOver(Image charcter,Vector3 pos)
    {
        while (true)
        {
            charcter.transform.localPosition = Vector3.MoveTowards(charcter.transform.localPosition, pos, speed);
            if (charcter.transform.localPosition == pos)
                break;
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
}
