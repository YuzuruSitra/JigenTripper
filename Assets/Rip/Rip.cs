using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rip : MonoBehaviour
{
    //3D...0 2D...1
    public static int RipState;
    [SerializeField]
    private GameObject[] _ripPlayer = new GameObject[2];

    private Quaternion _insRot;
    // Start is called before the first frame update
    void Start()
    {
        RipState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //プレイヤースクリプト取得
            Destroy(collision.gameObject);
            float ripRocate;
            if(RipState == 0)
            {
                RipState = 1;
                ripRocate = 0.0f;
                _insRot = Quaternion.Euler(0f,0f,0f);
            }
            else
            {
                RipState = 0;
                ripRocate = -2.0f;
                _insRot = Quaternion.Euler(0f,0f,0f); //プレイヤー側から角度変更あり
            }

            Instantiate(_ripPlayer[RipState], gameObject.transform.position + new Vector3(2.0f, 0f, ripRocate),_insRot);
        }

    }
}
