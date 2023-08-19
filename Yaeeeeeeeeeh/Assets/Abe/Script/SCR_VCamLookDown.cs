using UnityEngine;

public class SCR_VCamLookDown : MonoBehaviour
{
    [SerializeField] private int m_VCamNum;

    private SCR_VCamManager scr_VM = null;

    void Start()
    {
        scr_VM = FindObjectOfType<SCR_VCamManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            scr_VM.OneTimeVCamOn(m_VCamNum);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            scr_VM.OnTimeVCamOff();
        }
    }
}
