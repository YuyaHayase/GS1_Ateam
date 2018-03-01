using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hBackGround : MonoBehaviour {
    enum Direction
    {
        Right = -1,
        Left = 1
    }

    [SerializeField, Header("回転の向き")]
    Direction dir = Direction.Right;

    [SerializeField, Header("回転速度")]
    float RotateSpeed = 2.0f;

	// Update is called once per frame
	void Update () {
        if (hKeyConfig.GetKey("Zone")) transform.Rotate(new Vector3(0, 0, 1), (int)dir * RotateSpeed * Time.deltaTime / 8f);
        else transform.Rotate(new Vector3(0, 0, 1), (int)dir * RotateSpeed * Time.deltaTime);
    }
}
