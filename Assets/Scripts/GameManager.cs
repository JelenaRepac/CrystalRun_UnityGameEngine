using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool gameStarted;

  
    public void StartGame(){
        gameStarted= true;
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            StartGame();
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
