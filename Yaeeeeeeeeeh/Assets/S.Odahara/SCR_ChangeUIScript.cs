using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

//�{�^����OnClick���X�N���v�g����쓮�����AUI�\��
public class SCR_ChangeUIScript : MonoBehaviour
{
    //����UI
    [SerializeField] GameObject m_UI;
    //�O�ɕ\�����Ă���UI
    [SerializeField] GameObject m_PreviousUI;
    //���\������UI
    [SerializeField] GameObject m_NextUI;


    [Header("�߂�UI�����邩")]
    [SerializeField] bool m_Returnflg;//�߂邱�Ƃ��ł��邩
    [Header("�|�[�Y�Ɏg�p���邩")]
    [SerializeField] bool m_Pauseflg;//�|�[�Y��
    [Header("A�{�^���ō쓮���邩")]
    [SerializeField] bool m_BottonAflg;//A�{�^��

    private Button m_Button;


    // Start is called before the first frame update
    void Start()
    {
        m_Button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Returnflg)
        {
            if(Gamepad.current.bButton.isPressed)//B�{�^���L�[�{�[�h�Ŗ߂鏈������Œǉ�
            {
                Return();
            }
        }

        if(m_Pauseflg)
        {
            if(Input.GetKeyDown(KeyCode.Tab) || Gamepad.current.startButton.wasPressedThisFrame)
            {
                Pause();
            }
        }

        if (m_BottonAflg)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Gamepad.current.buttonSouth.isPressed)
            {
                m_Button.onClick.Invoke();
            }
        }
    }


    //����UI�؂�ւ�
    public void Next()
    {

        //��\��
        if (m_UI != null) m_UI.SetActive(false);

        //����UI��\��
        if (m_NextUI != null) m_NextUI.SetActive(true);

        ////�C�x���g�V�X�e���ɍŏ��ɑI�����Ă���{�^����ݒ�
        //GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(m_nextButton);
    }

    //�O��UI�؂�ւ�
    public void Return()
    {
        if(m_Pauseflg) Time.timeScale = 1f;//�ĊJ

        //��\��
        if (m_UI != null) m_UI.SetActive(false);

        //����UI��\��
        if (m_PreviousUI != null) m_PreviousUI.SetActive(true);


    }
    public void Pause()
    {

        //��\��
        if(m_UI != null) m_UI.SetActive(false);

        //����UI��\��
        if (m_NextUI != null) m_NextUI.SetActive(true);

        Time.timeScale = 0f;//���~�߂�
    }
}
