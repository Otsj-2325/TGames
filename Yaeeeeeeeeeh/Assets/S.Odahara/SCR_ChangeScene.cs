using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SCR_ChangeScene : MonoBehaviour
{

    //�J�ڐ�̃V�[��
    public string nextScene;

    //���Ԍo�߂ŃV�[���J�ڂ���ꍇ
    [SerializeField] bool DelayFlag;
    [SerializeField] float DelayTime = 2.0f;

    public static string loadAfterScene;

 
    // Start is called before the first frame update
    void Start()
    {
        if(DelayFlag)
        {
            Invoke("Change", DelayTime);
        }


    }

    // Update is called once per frame
    void Update()
    {



    }

    //�V�[���؂�ւ�
    public void Change()
    {
        //���[�h��̃V�[��
        loadAfterScene = nextScene;
        SceneManager.LoadScene("Load");
    }
}
