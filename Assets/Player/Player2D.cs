using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    private float _moveSpeed = 280.0f;
    private Rigidbody _rb;
    private float _jumpPower = 400.0f;
    private float _inputHorizontal;
    private bool _doJump = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //次元判定に名前を使うため設定
        this.name = "Player2D";
    }

    // Update is called once per frame
    void Update()
    {
        _inputHorizontal = Input.GetAxis("Horizontal");

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (isJumping)return;
            //isJumping = true;
            _doJump = true;
        }
    }

    void FixedUpdate()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
 
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward + Camera.main.transform.right * _inputHorizontal;
        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す
        _rb.velocity = moveForward * _moveSpeed * Time.deltaTime + new Vector3(0, _rb.velocity.y, 0);

        if(_doJump)
        {
            _doJump = false;
            _rb.AddForce(transform.up * _jumpPower * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
