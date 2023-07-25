using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_GamepadInput : MonoBehaviour
{
    Vector2 m_leftstick;        //左スティックの状態を保持する変数
    public bool LeftStickNorthflag;
    public bool LeftStickSouthflag;
    public bool LeftStickEastflag;
    public bool LeftStickWestflag;

    public bool StartButtonPressed; // スタートボタンの状態を保持する変数
    public bool AButtonPressed; // Aボタンの状態を保持する変数
    public bool BButtonPressed; // Bボタンの状態を保持する変数

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

        // スタートボタンの状態を取得
        StartButtonPressed = Gamepad.current.startButton.isPressed;

        // Aボタンの状態を取得
        AButtonPressed = Gamepad.current.aButton.isPressed;

        // Bボタンの状態を取得
        BButtonPressed = Gamepad.current.bButton.isPressed;
    }
}
