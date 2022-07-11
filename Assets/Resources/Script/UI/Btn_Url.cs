using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Url : HBtn_Event
{
    public string url;

    public override void OnClick()
    {
        Application.OpenURL(url);
    }
}
