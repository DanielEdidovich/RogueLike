using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Play : MonoBehaviour
{
    public AudioClip transitionMusic;
    public void SceneLoad(int index)
    {
        //MusicController.Instance.PlayMusic(transitionMusic);
        SceneManager.LoadScene(index); 
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}