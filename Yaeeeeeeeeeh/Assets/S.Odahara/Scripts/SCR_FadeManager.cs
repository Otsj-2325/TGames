using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCR_FadeManager : MonoBehaviour
{

    //�t�F�[�h�p��Canvas��Image
    private static Canvas m_fadeCanvas;
    private static Image m_fadeImage;

    //�t�F�[�h�pImage�̓����x
    private static float m_alpha = 0.0f;

    //�t�F�[�h�C���A�E�g�̃t���O
    public static bool m_isFadeIn = false;
    public static bool m_isFadeOut = false;

    public static bool m_isFadeInFinish = false;
    public static bool m_isFadeOutFinish = false;


    private static float m_fadeTime = 1.5f;//�t�F�[�h���������ԁi�P�ʂ͕b

    //�J�ڐ�̃V�[���ԍ�
    private static string m_nextScene;
    //�t�F�[�h�p��Canvas��Image����
    static void Init()
    {
        //�t�F�[�h�p��Canvas����
        GameObject FadeCanvasObject = new GameObject("CanvasFade");
        m_fadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<GraphicRaycaster>();
        m_fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<SCR_FadeManager>();

        //�őO�ʂɂȂ�悤�K���ȃ\�[�g�I�[�_�[�ݒ�
        m_fadeCanvas.sortingOrder = 100;

        //�t�F�[�h�p��Image����
        m_fadeImage = new GameObject("ImageFade").AddComponent<Image>();
        m_fadeImage.transform.SetParent(m_fadeCanvas.transform, false);
        m_fadeImage.rectTransform.anchoredPosition = Vector3.zero;

        //Image�T�C�Y�͓K���ɑ傫���ݒ�
        m_fadeImage.rectTransform.sizeDelta = new Vector2(9999, 9999);
    }

    //�t�F�[�h�C���J�n
    public static void FadeIn(Color col, float fadetime)
    {
        if (m_fadeImage == null) Init();
        m_fadeImage.color = col;
        m_fadeTime = fadetime;
        m_alpha = 1.0f;
        m_isFadeInFinish = false;
        m_isFadeIn = true;
        Time.timeScale = 0f;//���~�߂�
    }

    //�t�F�[�h�A�E�g�J�n
    public static void FadeOut(string scenename, Color col, float fadetime)
    {
        if (m_fadeImage == null) Init();
        m_nextScene = scenename;
        m_fadeTime = fadetime;
        m_fadeImage.color = col;     
        m_fadeCanvas.enabled = true;
        m_alpha = 0.0f;
        m_isFadeOut = true;
        m_isFadeOutFinish = false;
        Time.timeScale = 0f;//���~�߂�
    }

    void Update()
    {
        //�t���O�L���Ȃ疈�t���[���t�F�[�h�C��/�A�E�g����
        if (m_isFadeIn)
        {
            //�o�ߎ��Ԃ��瓧���x�v�Z
            m_alpha -= Time.unscaledDeltaTime / m_fadeTime;

            //�t�F�[�h�C���I������
            if (m_alpha <= 0.0f)
            {
                m_isFadeIn = false;
                m_alpha = 0.0f;
                m_fadeCanvas.enabled = false;
                Time.timeScale = 1f;//�ĊJ
                m_isFadeInFinish = true;
                
            }

            //�t�F�[�h�pImage�̐F�E�����x�ݒ�
            m_fadeImage.color = new Color(0.0f, 0.0f, 0.0f, m_alpha);
        }
        else if (m_isFadeOut)
        {
            //�o�ߎ��Ԃ��瓧���x�v�Z
            m_alpha += Time.unscaledDeltaTime / m_fadeTime;

            //�t�F�[�h�A�E�g�I������
            if (m_alpha >= 1.0f)
            {
                m_isFadeOut = false;
                m_alpha = 1.0f;
                Time.timeScale = 1f;//�ĊJ
                m_isFadeOutFinish = true;
                //���̃V�[���֑J��
                SceneManager.LoadScene(m_nextScene);
            }

            //�t�F�[�h�pImage�̐F�E�����x�ݒ�
            m_fadeImage.color = new Color(0.0f, 0.0f, 0.0f, m_alpha);
        }
    }
}
