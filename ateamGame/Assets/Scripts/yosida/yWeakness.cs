using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yWeakness : MonoBehaviour {
    GameObject parent;

    [SerializeField, Header("弱点の回転スピード")]
    float rotateSpeed = 1.0f;

    [SerializeField,Header("弱点のMaxサイズ")]
    float scaleMax = 0.8f;

    [SerializeField, Header("弱点のMinサイズ")]
    float scaleMin = 0.5f;

    [SerializeField,Header("弱点サイズの変化値")]
    float sAccel = 2.0f;

    float scaleSize;
    bool flgScaleMax = true;
    bool flgScaleMin = false;

    tWeakPointParent _tWeakPointParent;
    // Use this for initialization
    void Start () {
        parent = transform.root.gameObject;
        _tWeakPointParent = parent.transform.Find("HitParent").GetComponent<tWeakPointParent>();
        Weakness(_tWeakPointParent.WeakpointDir);

        scaleSize = scaleMin;

    }
	
	// Update is called once per frame
	void Update () {

        if (flgScaleMax)
        {
            scaleSize = Mathf.Min(scaleMax, scaleSize += Time.deltaTime / sAccel);
            if(scaleSize == scaleMax)
            {
                flgScaleMax = false;
                flgScaleMin = true;
            }
        }
        else if (flgScaleMin)
        {
            scaleSize = Mathf.Max(scaleMin, scaleSize -= Time.deltaTime / sAccel);
            if(scaleSize == scaleMin)
            {
                flgScaleMax = true;
                flgScaleMin = false;
            }
        }

        transform.Rotate(0, 0, rotateSpeed,Space.World);
        transform.localScale = new Vector3(scaleSize, scaleSize, 0);
	}

    private void Weakness(int weakness)
    {
        switch (weakness)
        {
            case 0://左
                transform.position = parent.transform.position + new Vector3(-1.0f, 0, 0);
                break;
            case 1://右
                transform.position = parent.transform.position + new Vector3(1.0f, 0, 0);
                break;
            case 2://下
                transform.position = parent.transform.position + new Vector3(0, -1.0f, 0);
                break;
            case 3://上
                transform.position = parent.transform.position + new Vector3(0, 1.0f, 0);
                break;

        }
    }
}
