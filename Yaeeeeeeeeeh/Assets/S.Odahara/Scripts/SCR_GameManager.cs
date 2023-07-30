using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_GameManager : MonoBehaviour
{
    public static SCR_GameManager scr_GameManager;
    public static float[] m_StageScorenum = new float[5];
    public static string[] m_StageScore = new string[5];

    void Awake()
    {
        if (scr_GameManager == null)
        {
            scr_GameManager = new SCR_GameManager();

            DontDestroyOnLoad(scr_GameManager);
        }

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //�X�R�A��ۑ�
    public static void SaveScore(float score)
    {

        if (SceneManager.GetActiveScene().name == "Stage1Scene")// �X�e�[�W1�V�[��
        {
            JudgeScore(score, 0, "Stage1Score");
            PlayerPrefs.SetFloat("Stage1Time", score);
            m_StageScorenum[0] = score;
        }
        else//�X�e�[�W2�Ƃ����o���オ�����珈���ǉ�
        {

        }
    }

    //Rank����
    public static void JudgeScore(float score,  int stagenum, string stagename)
    {
        if (score <= 100)//S
        {
            m_StageScore[stagenum] = "S";
            PlayerPrefs.SetString(stagename, "S");
        }
        else if(score > 100 && score <= 200)//A
        {
            m_StageScore[stagenum] = "A";
            PlayerPrefs.SetString(stagename, "A");
        }
        else//b
        {
            m_StageScore[stagenum] = "B";
            PlayerPrefs.SetString(stagename, "B");
        }
    }
}
