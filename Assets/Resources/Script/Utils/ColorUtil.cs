using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorUtil
{
    public static void SetColorAlpha(Image img, float alpha)
    {
        alpha = Mathf.Max(alpha, 0);
        alpha = Mathf.Min(alpha, 1);

        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
    public static void SetColorAlpha(SpriteRenderer img, float alpha)
    {
        alpha = Mathf.Max(alpha, 0);
        alpha = Mathf.Min(alpha, 1);

        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}
