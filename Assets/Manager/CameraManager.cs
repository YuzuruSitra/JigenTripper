using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("追跡するゲームオブジェクト")]
    public GameObject TargetObj;

    [SerializeField]
    private float _cameraRotateSpeed = 120.0f;     // カメラの回転速度

    [SerializeField]
    private float _maxLimit = 45.0f;              // X 軸方向の最大可動範囲

    [SerializeField]
    private float _minLimit = 25.0f;              // X 軸方向の最小可動範囲

    //private Vector3 offset;
    Vector3 _targetPos;
    Vector3 _localAngle;

    
    //シネマシーン
    [SerializeField]
    private GameObject _vCam1;
    private CinemachineVirtualCamera virtualCamera1;

    [SerializeField]
    private GameObject _vCam2;
    private CinemachineVirtualCamera _virtualCamera2;

    void Start()
    {
        //シネマシーン取得
        _vCam1 = GameObject.Find("CMvcam1");
        virtualCamera1 = _vCam1.GetComponent<CinemachineVirtualCamera>();

        _vCam2 = GameObject.Find("CMvcam2");
        _virtualCamera2 = _vCam2.GetComponent<CinemachineVirtualCamera>();

        //マウスカーソル
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {

        if(TargetObj == null)
        {
            TargetObj= GameObject.FindGameObjectWithTag("Player");
            virtualCamera1.Follow = TargetObj.transform;
            _virtualCamera2.Follow = TargetObj.transform;
        }

        
        if(Rip.RipState == 0)   //3Dカメラ
        {
            virtualCamera1.Priority = 100;
            _virtualCamera2.Priority = 10;
            // カメラの回転
            RotateCamera();
        }
        else    //2D カメラ
        {
            virtualCamera1.Priority = 10;
            _virtualCamera2.Priority = 100;
        }
    }

    //カメラの公転回転
    private void RotateCamera() {
        // マウスの入力値を取得
        float x = Input.GetAxis("Mouse X");
        float z = Input.GetAxis("Mouse Y");

        // カメラを追従対象の周囲を公転回転させる
        _vCam1.transform.RotateAround(TargetObj.transform.position, Vector3.up, x * Time.deltaTime * _cameraRotateSpeed);

        //カメラの回転情報の初期値をセット
        var localAngle = _vCam1.transform.localEulerAngles;

        // X 軸の回転情報をセット
        localAngle.x -= z;

        // X 軸を稼働範囲内に収まるように制御
        if (localAngle.x > _maxLimit) {
            localAngle.x = _maxLimit;
        }

        if (localAngle.x < _minLimit) {
            localAngle.x = _minLimit;
        }

        // カメラの回転
        _vCam1.transform.localEulerAngles = localAngle;
        
    }
}
