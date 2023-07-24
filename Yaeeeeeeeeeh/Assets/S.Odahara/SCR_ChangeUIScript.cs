using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SCR_ChangeUIScript : MonoBehaviour
{
    //消すUI
    [SerializeField] GameObject UI;
    //次表示するUI
    [SerializeField] GameObject nextUI;
    //次最初に選択するボタン
    [SerializeField] GameObject nextButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //ボタン押したらUI切り替え
    public void OnStart()
    {
       
        //非表示
        UI.SetActive(false);

        //次のUIを表示
        nextUI.SetActive(true);

        //イベントシステムに最初に選択しているボタンを設定
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(nextButton);
    }
}
