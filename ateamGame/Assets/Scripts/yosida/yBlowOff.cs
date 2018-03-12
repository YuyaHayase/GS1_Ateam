using System.Collections;
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
    [SerializeField, Header("弱点吹っ飛び倍率")]
    float hitAcseel = 2.0f;

    [SerializeField, Header("通常吹っ飛び倍率")]
    float Weakness = 0.2f;
    int reflect;

    string enemyName;

    bool flg = false;
    bool flgAngle = false;
    bool flgBlow = false;
    bool flgFall = false;
    [SerializeField]
    bool flgBoss = false;

    yHpgage hpGage;
    yCombo combo;
    oBase _oBase;
    tWeakPointParent _tWeakPointParent;
    // Use this for initialization
    void Start() {
        //パーティクル
        hitParticle = Resources.Load("yResources/HitParticle") as GameObject;
        blowParticle2 = Resources.Load("yResources/BlowParticle") as GameObject;
        
        //親オブジェクト
        parent = transform.root.gameObject;

        //子オブジェクト
        hpGage = parent.transform.Find("EnemyHPgage/HPbar").GetComponent<yHpgage>();

        //オブジェクトの参照
        combo = GameObject.Find("Combo").GetComponent<yCombo>();
        _oBase = GameObject.Find("Reference").GetComponent<oBase>();

        _tWeakPointParent = GetComponent<tWeakPointParent>();

        //親オブジェクトのあたり判定
        box2D = parent.GetComponent<BoxCollider2D>();

        //親オブジェクトの頭文字４文字(Enemyかbossか)
        enemyName = parent.name.Substring(0, 4);
        print(enemyName);
    }

    // Update is called once per frame
    void Update() {
        if (flg)//Playerの攻撃が当たったとき
        {

            if (x > 0)//右に飛ぶ
            {
                parent.transform.Translate(x -= Time.deltaTime, 0, 0);
                if (x < 0)
                {
                    flgAngle = true;
                    Destroy(particle);
                }
            }
            else//左に飛ぶ
            {
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

        //ボス以外の敵が上に吹っ飛び終わったときの落下
        if (flgFall && !flgBoss)
        {
            if (parent.transform.position.y > 0)
            {
                //落下速度
                if (hKeyConfig.GetKey("Zone"))
                    parent.transform.Translate(0, -0.2f / 15.0f, 0);
                else
                    parent.transform.Translate(0, -0.2f, 0);

                if (parent.transform.position.y < 0)
                    flgFall = false;
            }
            else
            {
                print("上がる");
                //上昇速度
                if (hKeyConfig.GetKey("Zone"))
                    parent.transform.Translate(0, 0.2f / 15.0f, 0);
                else
                    parent.transform.Translate(0, 0.2f, 0);

                if (parent.transform.position.y > 0)
                    flgFall = false;
            }
        }

    }

    private void Angle()
    {
        //回転角を戻す
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

            if (_tWeakPointParent.FlgWeakness || hpGage.Remaining <= 0.0f || (enemyName != "boss" && parent.gameObject.name != "Enemy9"))
            {
                //吹っ飛び倍率
                magnification = hitAcseel;

                //パーティクル生成
                particle = Instantiate(blowParticle2, parent.transform.position, Quaternion.identity);
                particle.name = "BlowParticle";
                particle.transform.parent = this.parent.transform;
                particle.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                //吹っ飛び倍率
                magnification = Weakness;

                //パーティクル生成
                particle = Instantiate(hitParticle, parent.transform.position, Quaternion.identity);
                particle.name = "HitParticle";
                Destroy(particle, 0.2f);
            }

            x *= magnification;

            flg = true;
            flgBlow = true;
            box2D.enabled = false;//跳ね返ってPlayerに当たらないように
        }

        //壁や床に当たったとき反射する
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
