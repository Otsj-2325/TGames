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
    [Header("��ʑJ�ڂ̑O�Ɏ��Ԃ���邩")]
    [SerializeField] bool m_IsDelayFlag;
    [SerializeField] float m_DelayTime = 1.0f;
    [Header("A�{�^�����g�p���邩")]
    [SerializeField] bool m_IsAbottonflg = false;
    [Header("B�{�^�����g�p���邩")]
    [SerializeField] bool m_IsBbottonflg = false;

    [Header("���[�h�V�[����ʂ���")]
    [SerializeField] bool m_IsLoadflg = false;


    [SerializeField] Color m_FadeOutColor = Color.black;
    [SerializeField] float m_FadeOutTime = 0.4f;

    public static string loadAfterScene;


    private float m_time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //�L�[�{�[�h����
        if(m_IsAbottonflg)
        {
            if(Input.GetKey(KeyCode.Return))
            {
                if (m_IsDelayFlag) Invoke("Change", m_DelayTime);
                else Change();
            }
        }
        if (m_IsBbottonflg)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                {
                if (m_IsDelayFlag) Invoke("Change", m_DelayTime);
                else Change();
            }
        }

        //�Q�[���p�b�h����
        if (Gamepad.current == null) { return; }
        else
        {
            if (m_IsAbottonflg)
            {
                if (Gamepad.current.buttonSouth.isPressed)
                {
                    if (m_IsDelayFlag) Invoke("Change", m_DelayTime);
                    else Change();
                }
            }
            if (m_IsBbottonflg)
            {
                if (Gamepad.current.bButton.isPressed)
                {
                    if (m_IsDelayFlag) Invoke("Change", m_DelayTime);
                    else Change();
                }
            }
        }
    }

    //�V�[���؂�ւ�
    public void NextScene()
    {        
        //���[�h��̃V�[��
        loadAfterScene = nextScene;
        if (m_IsLoadflg) SCR_FadeManager.FadeOut("LoadScene", m_FadeOutColor, m_FadeOutTime);
        else SCR_FadeManager.FadeOut(loadAfterScene, m_FadeOutColor, m_FadeOutTime);

    }

    public void Change()
    {
        Time.timeScale = 1f;
        if (m_IsDelayFlag)
        {
            Invoke("NextScene", m_DelayTime);
        }

        else 
        {
            Invoke("NextScene", 0.0f);
        }
        
    }

    public void Change(string scenename)
    {
        Time.timeScale = 1f;
        if (m_IsDelayFlag)
        {
            nextScene = scenename;
            Invoke("NextScene", m_DelayTime);
        }

        else
        {
            nextScene = scenename;
            Invoke("NextScene", 0.0f);
        }

    }

    public void BeginGame()
    {
        SCR_GameManager.DeleteScore();
        Change();
    }
}
