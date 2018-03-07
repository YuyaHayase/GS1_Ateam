using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yGameClear : MonoBehaviour {

    SpriteRenderer[] gameClear;
    Vector3[] endPos;
    bool flgGameClear = false;

    public bool FlgGameClear
    {
        set { flgGameClear = value; }
    }

    // Use this for initialization
    void Start () {
        //子オブジェクトを全て取得
        gameClear = this.GetComponentsInChildren<SpriteRenderer>();

        endPos = new Vector3[gameClear.Length];

        for (int i = 0; i < endPos.Length; i++)
        {
            //初期値をendPosに入れる
            endPos[i] = gameClear[i].transform.localPosition;

            gameClear[i].enabled = false;
        }


    }

    // Update is called once per frame
    void Update () {
        if (flgGameClear)
        {
            for (int i = 0; i < gameClear.Length; i++)
                gameClear[i].enabled = true;

            flgGameClear = false;

            StartCoroutine("ImageMove");
        }

        if(Input.GetKeyDown(KeyCode.Return))
            flgGameClear = true;
    }

    IEnumerator ImageMove()
    {


        for (int i = 0;i < gameClear.Length; i++)
        {
            StartCoroutine(GameClear(gameClear[i], endPos[i],i));
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    IEnumerator GameClear(SpriteRenderer character,Vector3 pos,int len)
    {
        float f = 0.1f;
        bool[] flgWhole = new bool[gameClear.Length];

        for (int i = 0; i < flgWhole.Length; i++)
            flgWhole[i] = true;

        while (true)
        {
            float sin = Mathf.Abs(Mathf.Sin(f) / 2.0f);
            //float sin = Mathf.Abs(Mathf.PingPong(f, 1.0f));
            character.transform.localPosition = new Vector3(character.transform.localPosition.x, sin, 0);

            if (character.transform.localPosition.y <= pos.y + 0.2)
            {
                flgWhole[len] = false;
            }


            if(flgWhole[len])
            {
                f += 0.2f;
            }
            else if (flgCount(flgWhole))
            {
                yield return new WaitForSeconds(1.0f);
                for (int i = 0; i < flgWhole.Length; i++)
                    flgWhole[i] = true;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private bool flgCount(bool[] flgWhole)
    {
        int number = 0;
        for (int i = 0; i < flgWhole.Length; i++)
        {
            if (flgWhole[i])
                number++;
        }

        if (number >= flgWhole.Length)
            return true;
        return false;
    }
}
