using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButton : MonoBehaviour
{
    //�J�ڐ�̃V�[��
    [SerializeField] string nextScene;

    //���Ԍo�߂ŃV�[���J�ڂ���ꍇ
    [SerializeField] float delayTime;

    public static string loadAfterScene;

    [SerializeField] AudioClip touchAudio;//�G�����Ƃ��̉�

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();//���̃I�u�W�F�N�g�ɂ��Ă���AudioSource���擾
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
    }

    public void NextScene()
    {
        audioSource.PlayOneShot(touchAudio);//���炷
        Invoke("Change", delayTime);
        Time.timeScale = 1f;
    }


}
