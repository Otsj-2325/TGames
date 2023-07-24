using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ChangeScene : MonoBehaviour
{

    //遷移先のシーン
    public string nextScene;

    //時間経過でシーン遷移する場合
    public bool bTimeFlag;

    public static string loadAfterScene;

 
    // Start is called before the first frame update
    void Start()
    {
        

        if (bTimeFlag)
        {
            Invoke("Change", 3.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        

       
    }

    //シーン切り替え
    public void Change()
    {
        //ロード後のシーン
        loadAfterScene = nextScene;
        SceneManager.LoadScene("Load");
    }
}
