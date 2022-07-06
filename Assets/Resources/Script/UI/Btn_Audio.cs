using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Btn_Audio : HBtn_Event
{
    public GameObject audioPad;
    public AudioMixer mixer;


    private bool isShowing = false;

    private IEnumerator Start()
    {
        yield return 0;
        audioPad.SetActive(isShowing);
    }

    public override void OnClick()
    {
        isShowing = !isShowing;
        audioPad.SetActive(isShowing);
    }
}
