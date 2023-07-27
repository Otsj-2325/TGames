using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_TuraraFall : MonoBehaviour
{
    private Rigidbody cp_Rigidbody;
    [SerializeField]public float m_Speed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        cp_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" )
        {
            //Debug.Log("���������痎�Ƃ���");
            cp_Rigidbody.velocity = Vector3.down * m_Speed; 
        }
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == ("Ground"))
        {
            //Debug.Log("���ɓ����������������");
            Destroy(this.gameObject, 0.1f);
        }
        if (collider.gameObject.tag == ("Player"))
        {
            //Debug.Log("�v���C���[�ɓ����������������");
            Destroy(this.gameObject, 0.1f);
        }
    }
}
