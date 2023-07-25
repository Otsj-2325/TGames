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
        // Enterキーの状態を取得
        EnterKeyPressed = Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter);

        // Tabキーの状態を取得
        TabKeyPressed = Input.GetKey(KeyCode.Tab);

        // Spaceキーの状態を取得
        SpaceKeyPressed = Input.GetKey(KeyCode.Space);

        // Wキーの状態を取得
        WKeyPressed = Input.GetKey(KeyCode.W);

        // Aキーの状態を取得
        AKeyPressed = Input.GetKey(KeyCode.A);

        // Sキーの状態を取得
        SKeyPressed = Input.GetKey(KeyCode.S);

        // Dキーの状態を取得
        DKeyPressed = Input.GetKey(KeyCode.D);

        // Rキーの状態を取得
        RKeyPressed = Input.GetKey(KeyCode.R);

        // Eキーの状態を取得
        EKeyPressed = Input.GetKey(KeyCode.E);
    }
}
