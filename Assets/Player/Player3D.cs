using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player3D : MonoBehaviour
{
    private float _inputHorizontal;
    private float _inputVertical;
    private Rigidbody _rb;

    //アニメーター取得
    //Animator animator;
    //UI制御用スクリプト取得

    private float _moveSpeed = 280.0f;
 

    void Start() {
        //animator = GetComponent<Animator>();
        _rb = this.gameObject.GetComponent<Rigidbody>();
        //次元判定に名前を使うため設定
        this.name = "Player3D";
    }
 
    void Update() 
    {
        _inputHorizontal = Input.GetAxis("Horizontal");
        _inputVertical = Input.GetAxis("Vertical");
    }
 
    
    void FixedUpdate() 
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
 
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * _inputVertical + Camera.main.transform.right * _inputHorizontal;
 
        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す
        _rb.velocity = moveForward * _moveSpeed * Time.deltaTime + new Vector3(0, _rb.velocity.y, 0);
 
        // キー入力により移動方向が決まっている場合には、キャラクターの向きを進行方向に合わせる
        if (moveForward != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

    /*
        //アニメーター
        if(_inputHorizontal == 0 && _inputVertical == 0)
        {
            animator.SetBool("isWalk",false);
        }
        else
        {
            animator.SetBool("isWalk",true);
        }
    */
    }

    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerEnter(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {

    }
}
