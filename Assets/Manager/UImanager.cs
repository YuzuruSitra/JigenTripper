using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    //Fキーパネル
    [SerializeField]
    private GameObject _pushF;
    public bool DisplayPushF = false;

    void Start()
    {
        _pushF.SetActive(false);    
    }

    void Update()
    {
        //_pushF表示切り替え
        if(DisplayPushF)
        {
            _pushF.SetActive(true); 
        }
        else
        {
            _pushF.SetActive(false); 
        }
    }
}
