using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManager : MonoBehaviour
{
    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game..."); 
        Application.Quit();
    } 
}