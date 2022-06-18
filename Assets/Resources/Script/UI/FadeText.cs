using UnityEngine;

public class FadeText : MonoBehaviour
{
    public float time;

    private Animation ani;

    private float timer = 0;

    private void Start()
    {
        ani = GetComponent<Animation>();
    }

    private void Update()
    {
        if (timer == -1) return;

        if (timer >= time)
        {
            ani.Play("Text_FadeOut");
            timer = -1;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
