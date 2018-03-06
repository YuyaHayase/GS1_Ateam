using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hBGM : MonoBehaviour {

    AudioSource audiosrc;

    [SerializeField, Header("BGMにしたい曲")]
    AudioClip bgm;

    [SerializeField, Header("操作音")]
    AudioClip ctrl_SE;

	// Use this for initialization
	void Start () {
        audiosrc = GetComponent<AudioSource>();

        if (bgm == null || ctrl_SE == null) print("スクリプトに曲をいれてください");

        audiosrc.clip = bgm;
        audiosrc.Play();
	}

    public void ctrl_SE_OneShot()
    {
        //audiosrc.clip = ctrl_SE;
        audiosrc.PlayOneShot(ctrl_SE);
    }
}
