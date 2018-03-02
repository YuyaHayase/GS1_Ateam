using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hLines : MonoBehaviour {

    // 線の屈折点
    [SerializeField, Header("線の主要点")]
    Vector2[] PointPosition;

    [SerializeField, Header("線の幅")]
    float LineWidth = 0.5f;

    [SerializeField, Header("LineRendererコンポーネント")]
    LineRenderer rend;

    // 何個目のポイントか
    int cnt = 0;

    // ラインの先端の位置
    Vector2 pos;
    float dx = 0, dy = 0;

    [SerializeField, Header("線の遅延")]
    float deray = 20;

    [SerializeField, Header("表示したいテキスト")]
    string txt;
    bool end = false;

    [SerializeField]
    GameObject disp;

    // Use this for initialization
    void Start () {
        if(rend == null) rend = gameObject.GetComponent<LineRenderer>();
        
        rend.SetWidth(LineWidth, LineWidth);

        rend.SetVertexCount(PointPosition.Length);

        for(int i = 0; i < PointPosition.Length; i++)
        {
            rend.SetPosition(i, PointPosition[0]);
        }

        pos = PointPosition[0];
    }

    // Update is called once per frame
    void Update () {
        LineDraw();
    }

    void LineDraw()
    {
        try
        {
            dy += (PointPosition[cnt + 1].y - PointPosition[cnt].y) * Time.deltaTime;
            dx += (PointPosition[cnt + 1].x - PointPosition[cnt].x) * Time.deltaTime;

            pos += new Vector2(dx, dy) / deray;
            if ((int)pos.x == (int)PointPosition[cnt + 1].x)
            {
                pos = PointPosition[cnt + 1];
                if (cnt < PointPosition.Length-1) cnt++;
                dx = dy = 0;
            }

            for (int i = cnt + 1; i < PointPosition.Length; i++)
            {
                rend.SetPosition(i, pos);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
            Box();
        }
    }

    void Box()
    {
        if (!end)
        {
            try
            {
            GameObject g = Instantiate(disp) as GameObject;
            g.transform.position = PointPosition[cnt] + new Vector2(0, -1);
            g.transform.parent = gameObject.transform;
            g.GetComponent<TextMesh>().text = txt;
            end = true;
            }catch(Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }
}
