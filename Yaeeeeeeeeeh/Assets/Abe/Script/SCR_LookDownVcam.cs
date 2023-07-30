using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SCR_LookDownVcam : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cp_Vcam;
    void Start()
    {
        cp_Vcam.Priority = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            cp_Vcam.Priority = 100;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cp_Vcam.Priority = 0;
        }
    }
}
