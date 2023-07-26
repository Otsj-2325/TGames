using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class SCR_ChangeScene : MonoBehaviour
{

    //遷移先のシーン
    public string nextScene;

    //時間経過でシーン遷移する場合
    [Header("画面遷移の前に時間を作るか")]
    [SerializeField] bool m_IsDelayFlag;
    [SerializeField] float m_DelayTime = 1.0f;
    [Header("Aボタンを使用するか")]
    [SerializeField] bool m_IsAbottonflg = false;
    [Header("Bボタンを使用するか")]
    [SerializeField] bool m_IsBbottonflg = false;


    [SerializeField] Color m_FadeOutColor = Color.black;
    [SerializeField] float m_FadeOutTime = 0.4f;

    public static string loadAfterScene;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsAbottonflg)
        {
            if(Gamepad.current.buttonSouth.isPressed || Input.GetKey(KeyCode.Return))
            {
                if (m_IsDelayFlag) Invoke("Change", m_DelayTime);
                else Invoke("Change", 0.0f);
            }
        }
        if (m_IsBbottonflg)
        {
            if (Gamepad.current.bButton.isPressed || Input.GetKeyDown(KeyCode.Escape))
                {
                if (m_IsDelayFlag) Invoke("Change", m_DelayTime);
                else Invoke("Change", 0.0f);
            }
        }

    }

    //シーン切り替え
    public void NextScene()
    {        
        //ロード後のシーン
        loadAfterScene = nextScene;
        SCR_FadeManager.FadeOut("LoadScene", m_FadeOutColor, m_FadeOutTime);

    }

    public void Change()
    {
        Time.timeScale = 1f;
        if (m_IsDelayFlag)
        {
            Invoke("NextScene", m_DelayTime);
        }

        else 
        {
            Invoke("NextScene", 0.0f);
        }
        
    }
}
