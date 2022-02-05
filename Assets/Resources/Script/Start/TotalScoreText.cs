using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScoreText : MonoBehaviour
{
    private Text t;

    private void Start()
    {
        t = GetComponent<Text>();
        t.text = "得分: " + GlobalData.score;

        if (GlobalData.score > PlayerPrefs.GetInt("Highest", 0))
        {
            t.text += "\n新纪录!";
            PlayerPrefs.SetInt("Highest", Mathf.RoundToInt(GlobalData.score));
        }
    }
}
