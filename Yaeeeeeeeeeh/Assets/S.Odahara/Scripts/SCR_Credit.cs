using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Credit : MonoBehaviour
{
    private RectTransform m_rectTransform;
    [SerializeField] private float m_StartPosY = -200.0f;
    [SerializeField] private float m_LimitPosY = 200.0f;
    [SerializeField] private float m_MoveSpeed = 0.1f;

    private float m_MovePos;
    // Start is called before the first frame update
    void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_rectTransform.anchoredPosition = new Vector3(0.0f, m_StartPosY, 0.0f);
        m_MovePos = m_StartPosY;
    }

    // Update is called once per frame
    void Update()
    {

        //���o�[�W�����iraycast�ŉ�ʊO���肵�ď����ʒu�ɖ߂������Ƃ��������玩�R�����A�e�L�X�g�����Ȃ�Ƃނ������ȁj
        if(m_rectTransform.anchoredPosition.y < m_LimitPosY)
        {
            m_rectTransform.anchoredPosition = new Vector3(0.0f, m_MovePos, 0.0f);
        }
        else
        {
            m_rectTransform.anchoredPosition = new Vector3(0.0f, m_StartPosY, 0.0f);
            m_MovePos = m_StartPosY;
        } 
            



        m_MovePos += m_MoveSpeed;
    }
}
