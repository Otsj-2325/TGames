using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SCR_ChangeUIScript : MonoBehaviour
{
    //����UI
    [SerializeField] GameObject UI;
    //���\������UI
    [SerializeField] GameObject nextUI;
    //���ŏ��ɑI������{�^��
    [SerializeField] GameObject nextButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //�{�^����������UI�؂�ւ�
    public void OnStart()
    {
       
        //��\��
        UI.SetActive(false);

        //����UI��\��
        nextUI.SetActive(true);

        //�C�x���g�V�X�e���ɍŏ��ɑI�����Ă���{�^����ݒ�
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(nextButton);
    }
}
