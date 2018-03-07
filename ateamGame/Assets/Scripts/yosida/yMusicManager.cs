using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

[Serializable]
public class yAudioSet
{
    public string Name;
    public AudioClip seClip;
    public float volume;
}

public class yMusicManager : MonoBehaviour {

    #region//シングルトン
    public static yMusicManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion

    AudioSource musicSound, soundEffect;

    [SerializeField,Header("BGM専用")]
    List<yAudioSet> bgmList = new List<yAudioSet>();
    [SerializeField,Header("効果音専用")]
    List<yAudioSet> seList = new List<yAudioSet>();
    //public enum clip

	// Use this for initialization
	void Start () {
        AudioSource[] audio = GetComponents<AudioSource>();
        musicSound = audio[0];
        soundEffect = audio[1];

    }
	
	// Update is called once per frame
	void Update () {
        #region//デバッグ用
        if (Input.GetKeyDown(KeyCode.A))
            MusicSound(0);
        if (Input.GetKeyDown(KeyCode.B))
            Sound(1);
        if (Input.GetKeyDown(KeyCode.C))
            Sound(2);
        if (Input.GetKeyDown(KeyCode.D))
            Sound(3);
        if (Input.GetKeyDown(KeyCode.Space))
            MusicEffectStop();
        #endregion
    }

    public void MusicSound(int listNum)
    {
        if (!musicSound.isPlaying)
        {
            musicSound.volume = bgmList[listNum].volume;
            musicSound.clip = bgmList[listNum].seClip;
            musicSound.Play();
        }
        else
        {
            musicSound.volume = bgmList[listNum].volume;
            musicSound.clip = bgmList[listNum].seClip;
            musicSound.Play();
        }
    }

    public void Sound(int listNum)
    {
        //BGM流す
        soundEffect.volume = seList[listNum].volume;
        soundEffect.clip = seList[listNum].seClip;
        soundEffect.PlayOneShot(soundEffect.clip);
    }

    public void MusicEffectStop()
    {
        musicSound.Stop();//音楽止める
    }

}
