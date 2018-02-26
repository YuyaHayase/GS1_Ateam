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

	// Use this for initialization
	void Start () {
        // アタッチされてないなら代入する
        if (Vignette == null) Vignette = GetComponent<VignetteAndChromaticAberration>();
        if(MotionsBlur == null) MotionsBlur = GetComponent<MotionBlur>();
	}
	
	// Update is called once per frame
	void Update () {
        try
        {
            // 集中時
            if (hKeyConfig.GetKey("Zone")) {
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
        }
        catch (Exception e)
        {
            print(e.Message);
        }
	}
}
