using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SCR_Cursor : MonoBehaviour
{
    [SerializeField] List<RectTransform> m_PositionList;
    [SerializeField] List<UnityEvent> m_ButtonEventList;

    [SerializeField] bool m_IsVerticalStick;

    private int m_PosIndex = 0;
    private float m_Delaytime = 0.4f;
    private float m_Time = 0.0f;

    // ���t�g�X�e�B�b�N�̓��͂ɂ��I���̐���
    private float m_LeftStickSensitivity = 0.9f; // ���t�g�X�e�B�b�N�̊��x�i�l��傫������Ɗ��x��������j

    void Start()
    {
        transform.position = m_PositionList[0].position;
    }

    void Update()
    {
        m_Time += Time.unscaledDeltaTime;
        if (m_Time > m_Delaytime)
        {
            if (Gamepad.current == null) { return; }

            transform.position = m_PositionList[m_PosIndex].position;

            // ���t�g�X�e�B�b�N�̓��͎擾
            Vector2 leftStickInput = Gamepad.current.leftStick.ReadValue();

            if(m_IsVerticalStick)
            {
                if (Mathf.Abs(leftStickInput.y) > m_LeftStickSensitivity)//���x�𒴂��Ă���ꍇ
                {
                    // ���t�g�X�e�B�b�N�̏㉺�̌X���ɉ����đI������ύX
                    if (leftStickInput.y > 0) m_PosIndex += -1;
                    else if (leftStickInput.y < 0) m_PosIndex += 1;

                    m_PosIndex = Mathf.Clamp(m_PosIndex, 0, m_PositionList.Count - 1);// �I�����͈̔͂𐧌�

                    m_Time = 0.2f; //�f�B���C�����Z�b�g
                }
            }
            if (!m_IsVerticalStick)
            {
                if (Mathf.Abs(leftStickInput.x) > m_LeftStickSensitivity)//���x�𒴂��Ă���ꍇ
                {
                    // ���t�g�X�e�B�b�N�̍��E�̌X���ɉ����đI������ύX
                    if (leftStickInput.x > 0) m_PosIndex += 1;
                    else if (leftStickInput.x < 0) m_PosIndex += -1;

                    m_PosIndex = Mathf.Clamp(m_PosIndex, 0, m_PositionList.Count - 1);// �I�����͈̔͂𐧌�

                    m_Time = 0.2f; //�f�B���C�����Z�b�g
                }
            }


            // A�{�^���̓���
            if (Gamepad.current.buttonSouth.isPressed)
            {
                m_ButtonEventList[m_PosIndex].Invoke();
            }
        }
    }
}
