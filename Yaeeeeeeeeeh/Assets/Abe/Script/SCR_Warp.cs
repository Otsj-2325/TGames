using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Warp : MonoBehaviour
{
    [SerializeField] private GameObject m_DetnWarp;

    private Transform m_WarpPos;
    private GameObject m_Player;
    private SCR_WarpTrigger scr_Wt;
    private bool m_OnWarp;

    private void Start()
    {
        m_WarpPos = m_DetnWarp.transform.Find("WarpPoint").transform;
        m_Player = FindObjectOfType<SCR_PlayerController>().gameObject;
        scr_Wt = this.transform.GetChild(0).gameObject.GetComponent<SCR_WarpTrigger>();
    }
    
    private void FixedUpdate()
    {
        var contact = scr_Wt.m_Contact;

        var canwarp = m_OnWarp && contact;
        if (canwarp)
        {
            m_Player.transform.position =  m_WarpPos.transform.position;
            m_DetnWarp.GetComponent<SCR_Warp>().Arrival();
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_OnWarp = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_OnWarp = false;
            if (!this.transform.Find("WarpTrigger").gameObject.activeSelf)
            {
                this.transform.Find("WarpTrigger").gameObject.SetActive(true);
            }
        }
    }

    public void Arrival()
    {
        this.transform.Find("WarpTrigger").gameObject.SetActive(false);
    }
}
