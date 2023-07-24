using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ChangeUIScript : MonoBehaviour
{
    //次表示するUI
    public GameObject nextUI;
    //次最初に選択するボタン
    public GameObject nextButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //スタート押したら
    public void OnStart(InputAction.CallbackContext context)
    {
        

        if (context.performed && gameObject.active)
        {
            //非表示
            gameObject.SetActive(false);

            //次のUIを表示
            nextUI.SetActive(true);

            //イベントシステムに最初に選択しているボタンを設定
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(nextButton);
        }
    }

    //ボタン押したらUI切り替え
    public void OnStart()
    {
       

        //非表示
        gameObject.transform.parent.gameObject.SetActive(false);

        //次のUIを表示
        nextUI.SetActive(true);

        //イベントシステムに最初に選択しているボタンを設定
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(nextButton);
    }
}
