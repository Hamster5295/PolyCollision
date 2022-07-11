using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckAndJump : MonoBehaviour
{
    public string target;

    private void Start()
    {
        if (PlayerPrefs.GetInt("NewToGame", 0) == 1)
        {
            SceneManager.LoadScene(target);
        }
        else
        {
            PlayerPrefs.SetInt("NewToGame", 1);
        }
    }
}
