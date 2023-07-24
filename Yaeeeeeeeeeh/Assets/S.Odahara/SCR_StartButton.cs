using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_StartButton : MonoBehaviour
{
    [SerializeField] String sceneName;
    [SerializeField] AudioClip touchAudio;//触ったときの音

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();//このオブジェクトについているAudioSourceを取得
    }

    // Update is called once per frame
    void Update()
    {
        bool touch = false;

        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touch = true;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            touch = true;
        }
        if (touch)
        {
            audioSource.PlayOneShot(touchAudio);//音鳴らす
            Invoke("NextScene", 1.0f);
        }
    }

    void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }

}
