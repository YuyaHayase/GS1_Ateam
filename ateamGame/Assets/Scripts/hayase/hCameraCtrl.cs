using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.ImageEffects;

public class hCameraCtrl : MonoBehaviour {

    // ビネット
    [SerializeField, Header("ビネット")]
    VignetteAndChromaticAberration Vignette;

    // モーションブラー
    [SerializeField, Header("モーションブラー")]
    MotionBlur MotionsBlur;

    // ビネットの最大値
    [SerializeField, Header("ビネットの最大値")]
    float vMax = 0.23f;

    // ビネットの変化値
    [SerializeField, Header("ビネットの変化値(値が大きいほどゆっくり)")]
    float vAccel = 2.0f;

    bool zone = false;

    // プレイヤー
    [SerializeField, Header("プレイヤー")]
    GameObject Player;
    bool CameraMove = true;
    GameObject wall;

	// Use this for initialization
	void Start () {
        // アタッチされてないなら代入する
        if (Vignette == null) Vignette = GetComponent<VignetteAndChromaticAberration>();
        if(MotionsBlur == null) MotionsBlur = GetComponent<MotionBlur>();

        hKeyConfigSettings hk = new hKeyConfigSettings();
        hk.Init();
        print(hKeyConfigSettings.ParallaxEffect);
        if (false == hKeyConfigSettings.ParallaxEffect)
        {
            GetComponent<VignetteAndChromaticAberration>().enabled = false;
            GetComponent<MotionBlur>().enabled = false;
        }

        if (Player == null) Player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (hKeyConfigSettings.ParallaxEffect)
        {
            try
            {
                // 集中時
                if (hKeyConfig.GetKey("Zone"))
                {
                    zone = true;
                    if (Vignette.intensity < vMax) Vignette.intensity += Time.deltaTime / vAccel;
                    MotionsBlur.blurAmount = 0.8f;
                }

                // 集中が終わったとき
                if (hKeyConfig.GetKeyUp("Zone"))
                {
                    zone = false;
                    MotionsBlur.blurAmount = 0.6f;
                }

                // ビネットの自動解除
                if (Vignette.intensity > 0.1f && !zone)
                {
                    Vignette.intensity -= Time.deltaTime / vAccel;

                }

                if (CameraMove)
                {
                    // カメラ移動
                    Vector2 pPos = Player.transform.position;
                    transform.position = new Vector3(pPos.x / 3.5f, pPos.y / 10.0f + 11.82f, -10);
                }
                else
                {
                    if ((wall.transform.position - Player.transform.position).magnitude > 15) CameraMove = true;
                    print((wall.transform.position - Player.transform.position).magnitude);
                }
            }
            catch (Exception e)
            {
                print(e.Message);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        try
        {
            if (col.tag == "wall")
            {
                CameraMove = false;
                wall = col.gameObject;
            }
        }catch(Exception e)
        {
            print(e.Message + " : 'wall' タグを追加してみたら多分治るかもしれません");
        }
    }
}
