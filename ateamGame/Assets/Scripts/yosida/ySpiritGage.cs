using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ySpiritGage : MonoBehaviour
{

    Image spiritGage;
    [SerializeField,Header("精神ゲージが増えいていくフレームカウント")]
    int frameCountAdd = 5;
    [SerializeField,Header("")]
    int frameCountMinus = 5;

    [SerializeField,Header("精神ゲージのfillAmountが減っていく値")]
    float minus = 0.01f;

    bool flgAdd = true;//精神ゲージが少しずつ増えていく
    bool flgMinus = false;//減っていく

    // Use this for initialization
    void Start()
    {
        spiritGage = transform.FindChild("SpiritGage").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
            flgMinus = true;
        else
            flgAdd = true;


        if (flgAdd)
            SpiritGageAdd();


        if (flgMinus)
            SpiritGageMinus();

    }

    public void SpiritGageAdd()
    {
        flgAdd = false;

        if (Time.frameCount % frameCountAdd == 0)
            spiritGage.fillAmount += minus;
    }

    private void SpiritGageMinus()
    {
        flgMinus = false;

        if (Time.frameCount % frameCountMinus == 0)
            spiritGage.fillAmount -= minus;
    }
}
