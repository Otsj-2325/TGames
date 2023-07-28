using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class SCR_PlayerController : MonoBehaviour
{
    // �ړ��֘A�̃p�����[�^
    [Header("�����̍Œ�l")]
    [SerializeField]
    private float m_MinSpeed = 1.0f;
    [Header("�����̍ō��l")]
    [SerializeField]
    private float m_MaxSpeed = 3.0f;
    // �ړ����x
    private Vector3 m_Velocity;
    float m_Speed = 3.0f;
    // �W�����v�֌W
    public float m_JumpPower;
    [SerializeField] private float m_JumpVelocity = 0.0f;

    private Rigidbody cp_Rigidbody;
    [SerializeField] private bool m_IsJumping = false;

    private float m_Glavity = 9.8f;

    public float knockBackPower;   // �m�b�N�o�b�N�������

    // Start is called before the first frame update
    void Start()
    {
        cp_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���p�b�h���ڑ�����Ă��Ȃ���null�ɂȂ�B
        if (Gamepad.current == null) return;

        MoveProc();
        JumpProc();
        ActionProc();
        CutProc();
        PasteProc();

        m_Velocity.y = m_JumpVelocity;
        cp_Rigidbody.velocity = m_Velocity;
    }

    void MoveProc()
    {
        // ������
        m_Velocity = Vector3.zero;

        // �R���g���[���[
        Vector2 leftStick = Gamepad.current.leftStick.ReadValue();
        if (leftStick.x > 0.1f || leftStick.x < -0.1f)
        {
            m_Velocity.x = m_Speed * leftStick.x;
        }
        if (leftStick.y > 0.1f || leftStick.y < -0.1f)
        {
            m_Velocity.z = m_Speed * leftStick.y;
        }


        // �L�[�{�[�h
        // W�L�[�i�O���ړ��j
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += m_Speed * transform.forward * Time.deltaTime;
        }
        // S�L�[�i����ړ��j
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= m_Speed * transform.forward * Time.deltaTime;
        }
        // D�L�[�i�E�ړ��j
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += m_Speed * transform.right * Time.deltaTime;
        }
        // A�L�[�i���ړ��j
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= m_Speed * transform.right * Time.deltaTime;
        }


        Quaternion Rotation = Quaternion.LookRotation(m_Velocity.normalized, Vector3.up);
        if(m_Velocity.magnitude != 0)
        {
            this.transform.rotation = Rotation;
        }

       
    }
    void JumpProc()
    {
        if (Gamepad.current.buttonSouth.isPressed && !m_IsJumping)
        {
            m_JumpVelocity = m_JumpPower;
            //Debug.Log("�W�����v");
            m_IsJumping = true;
        }
        
        if (Input.GetKey(KeyCode.Space) && !m_IsJumping)
        {
            cp_Rigidbody.velocity = Vector3.up * m_JumpPower;
            //Debug.Log("�W�����v");
            m_IsJumping = true;
        }

        // ����
        if (m_IsJumping == true)
        {
            m_JumpVelocity -= m_Glavity * Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_IsJumping = false;
            m_JumpVelocity = 0.0f;
        }

        // �G�ɐG�ꂽ��m�b�N�o�b�N
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("��������");
            cp_Rigidbody.velocity = Vector3.zero;

            // �����̈ʒu�ƐڐG���Ă����I�u�W�F�N�g�̈ʒu�Ƃ��v�Z���āA�����ƕ������o���Đ��K��(���x�x�N�g�����Z�o)
            Vector3 distination = (transform.position - collision.transform.position).normalized;

            cp_Rigidbody.AddForce(distination * knockBackPower, ForceMode.VelocityChange);
        }

    }
    void ActionProc()
    {
        if (Gamepad.current.buttonEast.isPressed)
        {           
            Debug.Log("�A�N�V������");
        }

        if (Input.GetKey(KeyCode.J))
        {       
            Debug.Log("�A�N�V������");
        }

    }
    void CutProc()
    {
        if (Gamepad.current.buttonNorth.isPressed)
        {
            Debug.Log("�J�b�g��");
        }

        if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("�J�b�g��");
        }
    }
    void PasteProc()
    {
        if (Gamepad.current.buttonWest.isPressed)
        {
            Debug.Log("�y�[�X�g��");
        }

        if (Input.GetKey(KeyCode.P))
        {
            Debug.Log("�y�[�X�g��");
        }
    }

}

