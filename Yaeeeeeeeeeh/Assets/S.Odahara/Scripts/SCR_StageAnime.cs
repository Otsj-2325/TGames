using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_StageAnime : MonoBehaviour
{
    private Image image;

    private float m_Time;

    private Vector2 initialPosition;
    private Vector2 initialSize;

    [SerializeField] private float m_AnimeSpeed;
    [SerializeField] private float m_DelayTime = 1.0f;
    [SerializeField]Vector3 m_TargetPos = new Vector3(500.0f, 600.0f, 0.0f);
    [SerializeField] Vector2 m_TargetScale = new Vector2(800.0f, 1600.0f);

    public bool m_IsAnimeStart;
    public bool m_IsAnimeFin;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        initialPosition = image.rectTransform.anchoredPosition;
        initialSize = new Vector2(image.rectTransform.rect.width, image.rectTransform.rect.height);
        m_IsAnimeFin = false;
        m_IsAnimeStart = true;
        m_Time = 0.0f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_Time += Time.deltaTime;

        if(m_Time >= m_DelayTime)
        {
            if (m_IsAnimeStart)
            {
                float Percentage = (m_Time - m_DelayTime) / m_AnimeSpeed;


                if (Percentage >= 1.0f)
                {
                    // Animation is complete, set final values
                    image.rectTransform.anchoredPosition = m_TargetPos;

                    image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_TargetScale.x);

                    image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_TargetScale.y);

                    m_IsAnimeStart = false;
                    m_IsAnimeFin = true;
                }
                else
                {
                    // Calculate interpolated values for position and size
                    Vector2 newPosition = Vector2.Lerp(initialPosition, m_TargetPos, Percentage);
                    Vector2 newSize = Vector2.Lerp(initialSize, m_TargetScale, Percentage);

                    // Apply interpolated values to the RectTransform
                    image.rectTransform.anchoredPosition = newPosition;
                    image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSize.x);

                    image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newSize.y);
                }
            }
        }

    }
}
