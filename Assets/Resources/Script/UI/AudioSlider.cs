using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string key;
    public int rangeMin, rangeMax;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat(key, 0.5f);
        mixer.SetFloat(key, slider.value * (rangeMax - rangeMin) + rangeMin);
    }

    public void OnValueChanged()
    {
        mixer.SetFloat(key, slider.value * (rangeMax - rangeMin) + rangeMin);
        // Debug.Log("gg");
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat(key, slider.value);
    }
}
