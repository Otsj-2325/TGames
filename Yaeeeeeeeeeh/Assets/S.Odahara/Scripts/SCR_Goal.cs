using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Goal : MonoBehaviour
{
    private SCR_ChangeScene scr_ChangeScene;
    private bool m_IsClearflg;
    // Start is called before the first frame update
    void Start()
    {
        scr_ChangeScene = GetComponent<SCR_ChangeScene>();
        m_IsClearflg = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_IsClearflg = true;
            scr_ChangeScene.Change();
        }
    }
}
