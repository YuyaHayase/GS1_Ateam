using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ySpiritGage : MonoBehaviour
{

    Image spiritGage;

    [SerializeField]
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
        {
            SpiritGageAdd();
        }
        else
        {
            flgAdd = true;
        }

        if (flgAdd)
        {
            if (Time.frameCount % 20 == 0)
                spiritGage.fillAmount += minus;
        }
    }

    public void SpiritGageAdd()
    {
        flgAdd = false;

        if (Time.frameCount % 20 == 0)
            spiritGage.fillAmount -= minus;
    }
}
