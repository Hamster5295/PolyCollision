using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighestScoreText : MonoBehaviour
{
    private Text t;

    private void Start()
    {
        t = GetComponent<Text>();

        var i = PlayerPrefs.GetInt("Highest", 0);
        if (i == 0) t.gameObject.SetActive(false);
        t.text = "最高分: " + i;
    }
}
