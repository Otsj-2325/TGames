using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButton : MonoBehaviour
{
    //遷移先のシーン
    [SerializeField] string nextScene;

    //時間経過でシーン遷移する場合
    [SerializeField] float delayTime;

    public static string loadAfterScene;

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




    }
    //シーン切り替え
    public void Change()
    {
        //ロード後のシーン
        loadAfterScene = nextScene;
    }

    public void NextScene()
    {
        audioSource.PlayOneShot(touchAudio);//音鳴らす
        Invoke("Change", delayTime);
        Time.timeScale = 1f;
    }


}
