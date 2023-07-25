using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class SCR_ChangeScene : MonoBehaviour
{

    //�J�ڐ�̃V�[��
    public string nextScene;

    //���Ԍo�߂ŃV�[���J�ڂ���ꍇ
    [SerializeField] bool DelayFlag;
    [SerializeField] float DelayTime = 2.0f;
    [SerializeField] bool m_IsAbottonflg = false;

    public static string loadAfterScene;

 
    // Start is called before the first frame update
    void Start()
    {
        if(DelayFlag)
        {
            Invoke("Change", DelayTime);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsAbottonflg)
        {
            if(Gamepad.current.buttonSouth.isPressed)
            {
                Invoke("Change", 0.0f);
            }
        }


    }

    //�V�[���؂�ւ�
    public void Change()
    {
        Time.timeScale = 1f;
        //���[�h��̃V�[��
        loadAfterScene = nextScene;
        SceneManager.LoadScene("LoadScene");
    }
}
