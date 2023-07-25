using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PauseButton : MonoBehaviour
{
    //ポーズ画面
    [SerializeField]
    private GameObject PausePanel;

    [SerializeField] AudioClip PauseAudio;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();//このオブジェクトについているAudioSourceを取得
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StopGame()
    {
        audioSource.PlayOneShot(PauseAudio);//音鳴らす
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
    }

    public void ReStartGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        audioSource.PlayOneShot(PauseAudio);//音鳴らす
    }
}
