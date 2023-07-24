using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_GamepadInput : MonoBehaviour
{
    Vector2 m_leftstick;        //左スティックの状態を保持する変数
    bool m_LeftStickNorthflag;
    bool m_LeftStickSouthflag;
    bool m_LeftStickEastflag;
    bool m_LeftStickWestflag;

    bool m_StartButtonPressed; // スタートボタンの状態を保持する変数
    bool m_AButtonPressed; // Aボタンの状態を保持する変数
    bool m_BButtonPressed; // Bボタンの状態を保持する変数

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

        // スタートボタンの状態を取得
        m_StartButtonPressed = Gamepad.current.startButton.isPressed;

        // Aボタンの状態を取得
        m_AButtonPressed = Gamepad.current.aButton.isPressed;

        // Bボタンの状態を取得
        m_BButtonPressed = Gamepad.current.bButton.isPressed;
    }
}
