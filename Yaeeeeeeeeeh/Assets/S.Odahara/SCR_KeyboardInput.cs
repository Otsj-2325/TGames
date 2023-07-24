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
        // Enter�L�[�̏�Ԃ��擾
        m_EnterKeyPressed = Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter);

        // Tab�L�[�̏�Ԃ��擾
        m_TabKeyPressed = Input.GetKey(KeyCode.Tab);

        // Space�L�[�̏�Ԃ��擾
        m_SpaceKeyPressed = Input.GetKey(KeyCode.Space);

        // W�L�[�̏�Ԃ��擾
        m_WKeyPressed = Input.GetKey(KeyCode.W);

        // A�L�[�̏�Ԃ��擾
        m_AKeyPressed = Input.GetKey(KeyCode.A);

        // S�L�[�̏�Ԃ��擾
        m_SKeyPressed = Input.GetKey(KeyCode.S);

        // D�L�[�̏�Ԃ��擾
        m_DKeyPressed = Input.GetKey(KeyCode.D);

        // R�L�[�̏�Ԃ��擾
        m_RKeyPressed = Input.GetKey(KeyCode.R);

        // E�L�[�̏�Ԃ��擾
        m_EKeyPressed = Input.GetKey(KeyCode.E);
    }
}
