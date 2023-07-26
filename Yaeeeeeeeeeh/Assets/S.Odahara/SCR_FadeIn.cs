using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_FadeIn : MonoBehaviour
{
    //FadeIn���邽�߂����̃X�N���v�g
    //Scene�ɂ���I�u�W�F�N�g�ɃA�^�b�`���邾���A�A�����ǂ���Ȗʓ|�Ȃ��Ƃ₾����v����

    [SerializeField] Color m_FadeInColor = Color.black;
    [SerializeField] float m_FadeInTime = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        SCR_FadeManager.FadeIn(m_FadeInColor, m_FadeInTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
