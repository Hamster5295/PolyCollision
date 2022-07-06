using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : HBtn_Event
{
    public Animation ani_UI;

    public override void OnClick()
    {
        GlobalData.score = 0;
        GlobalData.isGameOver = false;
        GlobalData.currentEnemyCount = 0;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        ani_UI.Play("CanvasGroup_Out");
        yield return new WaitForSeconds(ani_UI["CanvasGroup_Out"].length);
        SceneManager.LoadScene("GameScene");

    }
}
