using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    //UIマネージャー
    [SerializeField]
    private UImanager _cntUI; 

    [SerializeField]
    private GameObject _moveObj; //動かすゲームオブジェクト
    private Vector3 _moveObjPos; //動かすゲームオブジェクトの座標
    private float _moveObjposZ;
    private bool _stayCol; //コリジョンに居座っているか否か
    void Start()
    {
        /*---UI制御のコンポーネント取得---*/
        _cntUI = GameObject.Find("UIManager").GetComponent<UImanager> ();
        //オブジェクト座標の取得
        _moveObjPos = _moveObj.transform.position;
        //オブジェクトZ座標を格納
        _moveObjposZ = _moveObjPos.z;
    }

    void Update()
    {
        //Exitが呼ばれずに次元遷移した時の修正処理
        GameObject PlayerObj = GameObject.FindWithTag("Player");
        if(PlayerObj.name == "Player3D")_stayCol = false;


        if(_stayCol)
        {
            _cntUI.DisplayPushF = true;
            if(Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine("doMove");
            }
        }
        else
        {
            _cntUI.DisplayPushF = false;
        }
    }

    private IEnumerator doMove()
    {
        float setPos = this.transform.position.z - 1.8f;
        float moveSpeed = 0.002f;

        //加速的に移動する処理
        while(_moveObjposZ >= setPos)
        {
            _moveObjposZ -= moveSpeed;
            moveSpeed += moveSpeed * 0.02f;
            _moveObj.transform.position = new Vector3(_moveObjPos.x, _moveObjPos.y, _moveObjposZ);
            yield return new WaitForSeconds (0.02f);
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //ボタン制御
            _stayCol = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        _stayCol = false;
    }
}
