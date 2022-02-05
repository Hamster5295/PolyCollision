using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animation ani_UI;

    private void Start()
    {
        StartCoroutine(CheckGameover());
    }

    private IEnumerator CheckGameover()
    {
        yield return new WaitUntil(() => GlobalData.isGameOver);
        yield return new WaitForSeconds(1f);
        ani_UI.Play("UI_Out");
        yield return new WaitForSeconds(ani_UI["UI_Out"].length);
        SceneManager.LoadScene("EndScene");
    }
}
