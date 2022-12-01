using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public string menuSceneName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("pause");
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        Debug.Log("Resume");
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        //SceneManager.LoadScene(menuSceneName);
    }
}
