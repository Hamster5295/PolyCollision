using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneBtn : HBtn_Event
{
    public string target;
    public Animation ani;
    public AnimationClip clip;

    public override void OnClick()
    {
        StartCoroutine(_Ani());
    }

    private IEnumerator _Ani()
    {
        yield return 0;
        if (ani)
        {
            ani.Play(clip.name);
            yield return new WaitForSeconds(ani[clip.name].length);
        }
        SceneManager.LoadScene(target);
    }
}
