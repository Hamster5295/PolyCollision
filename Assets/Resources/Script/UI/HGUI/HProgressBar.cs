using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HProgressBar : MonoBehaviour
{
    public HProgressBarDirection direction;
    public RectTransform bar;
    public float value = 0;
    public float trackSpeed;


    private RectTransform m_background;

    private void Start()
    {
        m_background = (RectTransform)transform;
    }

    private void Update()
    {
        switch (direction)
        {
            case HProgressBarDirection.Horizontal:
                if (Mathf.Abs(value - bar.rect.width / m_background.rect.width) >= 0.01f)
                {
                    bar.offsetMax = Vector2.Lerp(bar.offsetMax, new Vector2(m_background.rect.width * (value - 1), 0), trackSpeed);
                }
                else
                {
                    bar.offsetMax = new Vector2(m_background.rect.width * (value - 1), 0);
                }
                break;

            case HProgressBarDirection.Vertical:
                if (Mathf.Abs(value - bar.rect.height / m_background.rect.height) >= 0.01f)
                {
                    bar.offsetMax = Vector2.Lerp(bar.offsetMax, new Vector2(0, m_background.rect.height * (value - 1)), trackSpeed);
                }
                else
                {
                    bar.offsetMax = new Vector2(0, m_background.rect.height * (value - 1));
                }
                break;
        }
    }



    public void SetValue(float v)
    {
        value = v;
    }
}

public enum HProgressBarDirection
{
    Horizontal, Vertical
}
