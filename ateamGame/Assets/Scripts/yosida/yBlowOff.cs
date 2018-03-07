﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yBlowOff : MonoBehaviour {

    GameObject parent;
    GameObject particle;
    GameObject hitParticle;
    GameObject blowParticle2;
    BoxCollider2D box2D;
    float x, y, angle;
    float magnification;
    int reflect;

    string enemyName;

    bool flg = false;
    bool flgAngle = false;
    bool flgBlow = false;
    bool flgFall = false;

    yHpgage hpGage;
    yCombo combo;
    oBase _oBase;
    tWeakPointParent _tWeakPointParent;
    // Use this for initialization
    void Start() {
        hitParticle = Resources.Load("yResources/HitParticle") as GameObject;
        blowParticle2 = Resources.Load("yResources/BlowParticle") as GameObject;
        
        parent = transform.root.gameObject;
        hpGage = parent.transform.Find("EnemyHPgage/HPbar").GetComponent<yHpgage>();
        combo = GameObject.Find("Combo").GetComponent<yCombo>();
        _oBase = GameObject.Find("Reference").GetComponent<oBase>();
        _tWeakPointParent = GetComponent<tWeakPointParent>();

        box2D = parent.GetComponent<BoxCollider2D>();

        enemyName = parent.name.Substring(0, 4);
        print(enemyName);
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
                {
                    flgAngle = true;
                    Destroy(particle);
                }
            }
            else
            {
                //parent.transform.position += new Vector3(x /*+= Time.deltaTime*/, 0, 0);
                parent.transform.Translate(x += Time.deltaTime, 0, 0);
                if (x > 0)
                {
                    flgAngle = true;
                    Destroy(particle);
                }
            }
            parent.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (flgAngle)
        {
            flg = false;
            flgBlow = false;
            box2D.enabled = true;
            Angle();
        }

        if (flgFall && enemyName != "boss")
        {
            //落下速度
            if (hKeyConfig.GetKey("Zone"))
                parent.transform.Translate(0, -0.2f / 15.0f, 0);
            else
                parent.transform.Translate(0, -0.2f, 0);

            if (parent.transform.position.y < 0)
                flgFall = false;
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
            if (parent.transform.position.y > 0)
                flgFall = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "weapon")
        {
            combo.Combo();
            //x = collision.transform.position.x - transform.position.x;
            //y = collision.transform.position.y - transform.position.y;
            x = transform.position.x - collision.transform.position.x;
            y = transform.position.y - collision.transform.position.y;

            angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            reflect = _oBase.firstangle(angle);

            try
            {
                Destroy(particle);
            }
            catch (System.Exception e)
            {

            }

            if (collision.transform.position.x > transform.position.x)
                angle -= 180;

            if (_tWeakPointParent.FlgWeakness || hpGage.Remaining <= 0.0f || (enemyName != "boss" || parent.gameObject.name != "Enemy9"))
            {
                magnification = 2.0f;
                particle = Instantiate(blowParticle2, parent.transform.position, Quaternion.identity);
                particle.name = "BlowParticle";
                particle.transform.parent = this.parent.transform;
                particle.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                magnification = 0.2f;
                particle = Instantiate(hitParticle, parent.transform.position, Quaternion.identity);
                particle.name = "HitParticle";
                Destroy(particle, 0.2f);
            }

            x *= magnification;

            flg = true;
            flgBlow = true;
            box2D.enabled = false;
        }
        if(flgBlow)
            Reflect(collision.gameObject.tag);
    }

    private void Reflect(string tag)
    {
        reflect = _oBase.firstangle(angle);
        switch (tag)
        {
            case "topwall"://上
                angle = _oBase.Reflection(1, reflect, angle);
                break;
            case "ground"://下
                angle = _oBase.Reflection(2, reflect, angle);
                break;
            case "leftwall"://左
                angle = _oBase.Reflection(3, reflect, angle);
                break;
            case "rightwall"://右
                angle = _oBase.Reflection(4, reflect, angle);
                break;
            case "enemy":
                break;
            default:
                break;
        }

        try
        {
            particle.transform.rotation = Quaternion.Euler(0, 0, angle + 360);
        }
        catch (System.Exception e)
        {

        }
    }
}
