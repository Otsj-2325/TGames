using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PauseButton : MonoBehaviour
{
    //�|�[�Y���
    [SerializeField]
    private GameObject PausePanel;

    [SerializeField] AudioClip PauseAudio;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();//���̃I�u�W�F�N�g�ɂ��Ă���AudioSource���擾
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StopGame()
    {
        audioSource.PlayOneShot(PauseAudio);//���炷
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
    }

    public void ReStartGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        audioSource.PlayOneShot(PauseAudio);//���炷
    }
}
