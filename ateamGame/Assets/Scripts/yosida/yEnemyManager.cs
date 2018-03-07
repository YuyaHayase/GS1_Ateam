using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yEnemyManager : MonoBehaviour {

    GameObject ground;
    [SerializeField]
    int enemyHp = 100;
    float ySpeed = 0.2f;

    bool flg = false;

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
        if (transform.position.y < 30.0f && transform.position.y > ground.transform.position.y)
            flg = true;
    }
	
	// Update is called once per frame
	void Update () {

        //Destroy処理
        if (transform.position.y < ground.transform.position.y)
        {
            print("下に行った");
            _waveManagement.enemyNumber[_waveManagement.WaveNumber - 1]--;
            Destroy(gameObject);
        }

        if (transform.position.y > 30.0f)
        {
            print("上に行った");
            _waveManagement.enemyNumber[_waveManagement.WaveNumber - 1]--;
            Destroy(gameObject);
        }

        if (transform.position.x >= 50.0f || transform.position.x <= -50.0f)
        {
            print("左右画面外");
            _waveManagement.enemyNumber[_waveManagement.WaveNumber - 1]--;
            Destroy(gameObject);
        }


        if (flg)
        {
            transform.Translate(0, -ySpeed, 0);
            if (hKeyConfig.GetKey("Zone"))
                transform.Translate(0, -ySpeed / 15.0f, 0);
            else
                transform.Translate(0, -ySpeed, 0);

            if (transform.position.y <= 0)
                flg = false;
        }
	}
}
