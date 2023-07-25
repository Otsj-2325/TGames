using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_GamepadInput : MonoBehaviour
{
    Vector2 m_leftstick;        //���X�e�B�b�N�̏�Ԃ�ێ�����ϐ�
    public bool LeftStickNorthflag;
    public bool LeftStickSouthflag;
    public bool LeftStickEastflag;
    public bool LeftStickWestflag;

    public bool StartButtonPressed; // �X�^�[�g�{�^���̏�Ԃ�ێ�����ϐ�
    public bool AButtonPressed; // A�{�^���̏�Ԃ�ێ�����ϐ�
    public bool BButtonPressed; // B�{�^���̏�Ԃ�ێ�����ϐ�

    // Update is called once per frame
    void Update()
    {
        m_leftstick = Gamepad.current.leftStick.ReadValue();
        if (m_leftstick.y > 0.5f)
        {
            LeftStickNorthflag = true;
        }
        else
        {
            LeftStickNorthflag = false;
        }

        if (m_leftstick.y < -0.5f)
        {
            LeftStickSouthflag = true;
        }
        else
        {
            LeftStickSouthflag = false;
        }

        if (m_leftstick.x > 0.5f)
        {
            LeftStickEastflag = true;
        }
        else
        {
            LeftStickEastflag = false;
        }

        if (m_leftstick.x < -0.5f)
        {
            LeftStickWestflag = true;
        }
        else
        {
            LeftStickWestflag = false;
        }

        // �X�^�[�g�{�^���̏�Ԃ��擾
        StartButtonPressed = Gamepad.current.startButton.isPressed;

        // A�{�^���̏�Ԃ��擾
        AButtonPressed = Gamepad.current.aButton.isPressed;

        // B�{�^���̏�Ԃ��擾
        BButtonPressed = Gamepad.current.bButton.isPressed;
    }
}
