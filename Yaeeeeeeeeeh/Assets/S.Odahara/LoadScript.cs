using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Change",3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ÉVÅ[ÉìêÿÇËë÷Ç¶
    void Change()
    {
        SceneManager.LoadScene(ChangeScene.loadAfterScene);
    }
}
