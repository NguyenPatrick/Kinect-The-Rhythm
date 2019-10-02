using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour {


    public void changeToHomeScene()
    {
        SceneManager.LoadScene("Home");
    }

    public void changeToGameScene()
    {
        SceneManager.LoadScene("Game");
    }


    public void changeToProfileScene()
    {
        SceneManager.LoadScene("Profile");
    }

    public void changeToCreateProfileScene()
    {
        SceneManager.LoadScene("CreateProfile");
    }


    public void changeToGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
