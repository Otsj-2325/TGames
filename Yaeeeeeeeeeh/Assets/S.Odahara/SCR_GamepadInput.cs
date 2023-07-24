using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_GamepadInput : MonoBehaviour
{
    Vector2 m_leftstick;        //���X�e�B�b�N�̏�Ԃ�ێ�����ϐ�
    bool m_LeftStickNorthflag;
    bool m_LeftStickSouthflag;
    bool m_LeftStickEastflag;
    bool m_LeftStickWestflag;

    bool m_StartButtonPressed; // �X�^�[�g�{�^���̏�Ԃ�ێ�����ϐ�
    bool m_AButtonPressed; // A�{�^���̏�Ԃ�ێ�����ϐ�
    bool m_BButtonPressed; // B�{�^���̏�Ԃ�ێ�����ϐ�

    // Update is called once per frame
    void Update()
    {
        m_leftstick = Gamepad.current.leftStick.ReadValue();
        if (m_leftstick.y > 0.5f)
        {
            m_LeftStickNorthflag = true;
        }
        else
        {
            m_LeftStickNorthflag = false;
        }

        if (m_leftstick.y < -0.5f)
        {
            m_LeftStickSouthflag = true;
        }
        else
        {
            m_LeftStickSouthflag = false;
        }

        if (m_leftstick.x > 0.5f)
        {
            m_LeftStickEastflag = true;
        }
        else
        {
            m_LeftStickEastflag = false;
        }

        if (m_leftstick.x < -0.5f)
        {
            m_LeftStickWestflag = true;
        }
        else
        {
            m_LeftStickWestflag = false;
        }

        // �X�^�[�g�{�^���̏�Ԃ��擾
        m_StartButtonPressed = Gamepad.current.startButton.isPressed;

        // A�{�^���̏�Ԃ��擾
        m_AButtonPressed = Gamepad.current.aButton.isPressed;

        // B�{�^���̏�Ԃ��擾
        m_BButtonPressed = Gamepad.current.bButton.isPressed;
    }
}
