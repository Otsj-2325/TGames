using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_KeyboardInput : MonoBehaviour
{
    bool m_EnterKeyPressed;
    bool m_TabKeyPressed;
    bool m_SpaceKeyPressed;
    bool m_WKeyPressed;
    bool m_AKeyPressed;
    bool m_SKeyPressed;
    bool m_DKeyPressed;
    bool m_RKeyPressed;
    bool m_EKeyPressed;

    // Update is called once per frame
    void Update()
    {
        // Enterキーの状態を取得
        m_EnterKeyPressed = Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter);

        // Tabキーの状態を取得
        m_TabKeyPressed = Input.GetKey(KeyCode.Tab);

        // Spaceキーの状態を取得
        m_SpaceKeyPressed = Input.GetKey(KeyCode.Space);

        // Wキーの状態を取得
        m_WKeyPressed = Input.GetKey(KeyCode.W);

        // Aキーの状態を取得
        m_AKeyPressed = Input.GetKey(KeyCode.A);

        // Sキーの状態を取得
        m_SKeyPressed = Input.GetKey(KeyCode.S);

        // Dキーの状態を取得
        m_DKeyPressed = Input.GetKey(KeyCode.D);

        // Rキーの状態を取得
        m_RKeyPressed = Input.GetKey(KeyCode.R);

        // Eキーの状態を取得
        m_EKeyPressed = Input.GetKey(KeyCode.E);
    }
}
