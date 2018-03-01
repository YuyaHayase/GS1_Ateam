using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hBackGroundHuman : MonoBehaviour {

    [SerializeField, Header("カメラオブジェクトを入れてください")]
    GameObject Camera;

    public enum Direction
    {
        Left = -1,
        Right = 1
    }

    // 向き
    public Direction dir = Direction.Right;

    // アニメーション
    Animator anim;

    void Start()
    {
        // カメラが空なら
        if (Camera == null) Camera = GameObject.Find("Main Camera");

        //アニメーション, キャラの向きの設定
        anim = GetComponent<Animator>();
        int r = Random.Range(0, 11) % 2;
        if (0 == r) dir = Direction.Left;
        else dir = Direction.Right;

        // ほぼ画面外から出現
        transform.position = new Vector2(-Random.Range(0, 45) * (int)dir, 4);
        transform.localScale = new Vector2((int)dir * 0.6f, 0.6f);
    }

	// Update is called once per frame
	void Update () {
        // 集中時の減速
        if (hKeyConfig.GetKeyDown("Zone")) anim.speed = 0.5f;
        if (hKeyConfig.GetKeyUp("Zone")) anim.speed = 1.0f;

        // 移動
        transform.Translate(new Vector3(Time.deltaTime * (int)dir,0,0));

        if ((Camera.transform.position - transform.position).magnitude > 50)
        {
            GameObject g = Instantiate(gameObject) as GameObject;
            g.transform.parent = transform.parent;
            g.transform.position = new Vector2(-Random.Range(44, 50) * (int)dir, 4);
            Destroy(gameObject);
        }
	}
}
