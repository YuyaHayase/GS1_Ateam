using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yEnemyManager : MonoBehaviour {

    GameObject ground;
    [SerializeField]
    int enemyHp = 100;
    float ySpeed = 0.2f;

    bool flg = false;
    bool flgGrow = false;

    yWaveManagement _waveManagement;

    public int EnemyHP
    {
        set { enemyHp = value; }
        get { return enemyHp; }
    }

	// Use this for initialization
	void Start () {
        ground = GameObject.Find("ground");
        _waveManagement = GameObject.Find("Wave").GetComponent<yWaveManagement>();
        string enemyName = this.name;
        if (transform.position.y < 30.0f && transform.position.y > ground.transform.position.y
            && (enemyName != "Enemy9" && enemyName.Substring(0, 4) != "boss"))
            flg = true;

        if (transform.position.y < 0)
            flgGrow = true;
    }
	
	// Update is called once per frame
	void Update () {

        //Destroy処理
        if (transform.position.y < ground.transform.position.y)
        {
            //print("下に行った");
            print(this.name);
            _waveManagement.enemyNumber[_waveManagement.WaveNumber - 1]--;
            Destroy(gameObject);
        }
        else if (transform.position.y > 30.0f)
        {
            //print("上に行った");
            print(this.name);
            _waveManagement.enemyNumber[_waveManagement.WaveNumber - 1]--;
            Destroy(gameObject);
        }
         else if (transform.position.x >= 50.0f || transform.position.x <= -50.0f)
        {
            //print("左右画面外");
            print(this.name);
            _waveManagement.enemyNumber[_waveManagement.WaveNumber - 1]--;
            Destroy(gameObject);
        }


        if (flg)
        {
            if (hKeyConfig.GetKey("Zone"))
                transform.Translate(0, -ySpeed / 15.0f, 0);
            else
                transform.Translate(0, -ySpeed, 0);

            if (transform.position.y <= 0)
                flg = false;
        }

        if (flgGrow)
        {
            if (hKeyConfig.GetKey("Zone"))
                transform.Translate(0, ySpeed / 15.0f, 0);
            else
                transform.Translate(0, ySpeed, 0);

            if (transform.position.y > 0)
                flgGrow = false;
        }
    }
}
