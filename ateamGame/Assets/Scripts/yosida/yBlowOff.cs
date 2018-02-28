using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yBlowOff : MonoBehaviour {

    GameObject parent;
    float x, y, angle;
    float magnification;
    int reflect;
    bool flg = false;
    bool flgAngle = false;

    yHpgage hpGage;
    oBase _oBase;
    tWeakPointParent _tWeakPointParent;
    // Use this for initialization
    void Start() {
        parent = transform.root.gameObject;
        hpGage = parent.transform.Find("EnemyHPgage/HPbar").GetComponent<yHpgage>();
        _oBase = GameObject.Find("Reference").GetComponent<oBase>();
        _tWeakPointParent = GetComponent<tWeakPointParent>();
    }

    // Update is called once per frame
    void Update() {
        if (flg)
        {

            if (x > 0)
            {
                //parent.transform.position += new Vector3(x /*-= Time.deltaTime*/, 0, 0);
                parent.transform.Translate(x -= Time.deltaTime, 0, 0);
                if (x < 0)
                    flgAngle = true;
            }
            else
            {
                //parent.transform.position += new Vector3(x /*+= Time.deltaTime*/, 0, 0);
                parent.transform.Translate(x += Time.deltaTime, 0, 0);
                if (x > 0)
                    flgAngle = true;
            }
            parent.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (flgAngle)
        {
            flg = false;
            Angle();
        }


    }

    private void Angle()
    {
        angle = Mathf.MoveTowardsAngle(parent.transform.eulerAngles.z, 0.0f, 2.0f);
        parent.transform.rotation = Quaternion.Euler(0, 0, angle);
        if (angle == 0)
        {
            flg = false;
            flgAngle = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "weapon")
        {
            //x = collision.transform.position.x - transform.position.x;
            //y = collision.transform.position.y - transform.position.y;
            x = transform.position.x - collision.transform.position.x;
            y = transform.position.y - collision.transform.position.y;

            angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            reflect = _oBase.firstangle(angle);
            //if (angle <= 180)
            //    reflect = 1;
            //else
            //    reflect = 2;


            if (collision.transform.position.x > transform.position.x)
                angle -= 180;

            if (_tWeakPointParent.FlgWeakness)
                magnification = 2.0f;
            else
                magnification = 0.2f;

            if (hpGage.Remaining <= 0.0f)
                magnification = 2.0f;

            x *= magnification;

            flg = true;
            //print("入射角" + angle);
        }
        else
            Reflect(collision.gameObject.tag);
    }

    private void Reflect(string tag)
    {
        reflect = _oBase.firstangle(angle);
        switch (tag)
        {
            case "topwall"://上
                angle = _oBase.Reflection(1, reflect, angle);
                print("上");
                break;
            case "ground"://下
                angle = _oBase.Reflection(2, reflect, angle);
                print("下");
                break;
            case "leftwall"://左
                angle = _oBase.Reflection(3, reflect, angle);
                print("左");
                break;
            case "rightwall"://右
                angle = _oBase.Reflection(4, reflect, angle);
                print("右");
                break;
            case "enemy":
                break;
            default:
                //print("入ってる");
                break;
        }
        //Debug.Log("反射角" + angle);

    }
}
