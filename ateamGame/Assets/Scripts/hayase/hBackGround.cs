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

    float Rotation = 0;

    void Start()
    {
        Rotation = (int)dir * RotateSpeed * Time.deltaTime;
    }

	// Update is called once per frame
	void Update () {
        if (hKeyConfig.GetKeyDown("Zone")) Rotation= (int)dir * RotateSpeed * Time.deltaTime / 8f;
        if(hKeyConfig.GetKeyUp("Zone") || !hPlayerMove.ZoneForce) Rotation = (int)dir * RotateSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, 1), Rotation);
    }
}
