using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private float currentValue = 0;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        currentValue = Mathf.Lerp(currentValue, GlobalData.score, 0.1f);
        text.text = Mathf.RoundToInt(currentValue).ToString();
    }
}
