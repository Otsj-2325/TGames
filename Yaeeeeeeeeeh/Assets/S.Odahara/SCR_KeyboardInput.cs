using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_KeyboardInput : MonoBehaviour
{
    public bool EnterKeyPressed;
    public bool TabKeyPressed;
    public bool SpaceKeyPressed;
    public bool WKeyPressed;
    public bool AKeyPressed;
    public bool SKeyPressed;
    public bool DKeyPressed;
    public bool RKeyPressed;
    public bool EKeyPressed;

    // Update is called once per frame
    void Update()
    {
        // Enter�L�[�̏�Ԃ��擾
        EnterKeyPressed = Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter);

        // Tab�L�[�̏�Ԃ��擾
        TabKeyPressed = Input.GetKey(KeyCode.Tab);

        // Space�L�[�̏�Ԃ��擾
        SpaceKeyPressed = Input.GetKey(KeyCode.Space);

        // W�L�[�̏�Ԃ��擾
        WKeyPressed = Input.GetKey(KeyCode.W);

        // A�L�[�̏�Ԃ��擾
        AKeyPressed = Input.GetKey(KeyCode.A);

        // S�L�[�̏�Ԃ��擾
        SKeyPressed = Input.GetKey(KeyCode.S);

        // D�L�[�̏�Ԃ��擾
        DKeyPressed = Input.GetKey(KeyCode.D);

        // R�L�[�̏�Ԃ��擾
        RKeyPressed = Input.GetKey(KeyCode.R);

        // E�L�[�̏�Ԃ��擾
        EKeyPressed = Input.GetKey(KeyCode.E);
    }
}
