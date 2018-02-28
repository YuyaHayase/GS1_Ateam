using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oBase : MonoBehaviour
{
    GameObject player;
    public int i;
    float angle;
    int reflection;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int Playerposition(Vector3 enemypos)//移動方向を取得
    {
        player = GameObject.Find("Player");
        if (player.transform.position.x > enemypos.x)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
    public float Playerangle(Vector3 bullerPos)//角度を返す
    {
        float x = bullerPos.x - player.transform.position.x;
        float y = bullerPos.y - player.transform.position.y;
        angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;//角度を求める
        return angle;//値(角度)を返す 
    }
    public float Reflection(int angleFlg, int reflect, float angle)//壁反射
    {//                          壁　　　　　　入射角　　　　当たったときの角度
        switch (angleFlg)
        {
            case 1://天井
                angle *= -1;//角度に-を掛ける
                break;
            case 2://床
                angle *= -1;//角度に-を掛ける
                break;
            case 3://左の壁
                switch (reflect)//入射角
                {
                    case 1://180度以下
                        angle = Mathf.Abs(angle + 180);
                        angle *= -1;//角度に-を掛ける
                        break;
                    case 2://180度より大きい
                        angle = Mathf.Abs(angle - 180);
                        angle *= -1;//角度に-を掛ける
                        break;
                    case 3:
                        angle = 0;
                        break;
                }
                break;
            case 4://右の壁
                switch (reflect)//入射角
                {
                    case 1://180度以下
                        angle = Mathf.Abs(angle + 180);
                        angle *= -1;//角度に-を掛ける
                        break;
                    case 2://180度より大きい
                        angle = Mathf.Abs(angle - 180);
                        angle *= -1;//角度に-を掛ける
                        break;
                    case 3:
                        angle = 180;
                        break;
                }
                break;
        }
        return angle;
    }
    public int firstangle(float z)
    {
        if (z == 0 || z == 90 || z == 180 || z == 270)
        {
            reflection = 3;
        }
        else if (z < 180)
        {
            reflection = 1;
        }
        else
        {
            reflection = 2;
        }
        return reflection;
    }
    void save()
    {
        //oBase mother;//※必須
        //obj = GameObject.Find("GameObject");
        //bool posflg = false;//ポジションをとるためのフラグ
        //GameObject obj;//※必須
        //if (posflg == false)
        //{
        //    //※必須
        //    oBase tester = obj.GetComponent<oBase>();
        //    tester.Playerposition(transform.position);
        //    //※必須
        //    posflg = true;
        //}
        //posflg = false;//※必須
    }
}