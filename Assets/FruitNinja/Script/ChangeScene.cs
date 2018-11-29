using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour {


    public void changeToGameScene()
    {
        SceneManager.LoadScene("Game");
    }


    public void changeToHomeScene()
    {
        Application.LoadLevel("Game");
    }

}
