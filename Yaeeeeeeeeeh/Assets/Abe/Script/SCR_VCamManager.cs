using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SCR_VCamManager : MonoBehaviour
{
    public static SCR_VCamManager instance = null;

    [SerializeField] [Header("0:�����낵VCam�@1:�X�^�[�gVCam")] 
    private List<CinemachineVirtualCamera> m_VCamList = new List<CinemachineVirtualCamera>();

    private int m_NowVCam = 1;

    private void Awake()
    {
        instance = this;

        foreach (var cam in m_VCamList) 
        {
            cam.Priority = 0;
        }

        m_VCamList[m_NowVCam].Priority = 10;
    }

    /// <summary>
    /// �J�����ύX
    /// </summary>
    /// <param name="VCam�ԍ�"></param>
    public void SwitchVCam(int num)
    {
        m_NowVCam = num;

        foreach (var cam in m_VCamList)
        {
            cam.Priority = 0;
        }

        m_VCamList[m_NowVCam].Priority = 10;
    }

    public void OneTimeVCamOn(int num)
    {
        m_VCamList[num].Priority = 100;
    }

    public void OnTimeVCamOff()
    {
        foreach (var cam in m_VCamList)
        {
            cam.Priority = 0;
        }

        m_VCamList[m_NowVCam].Priority = 10;
    }

}
