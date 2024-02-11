using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject Screen1;
    [SerializeField] GameObject Screen2;
    [SerializeField] private GameObject Screen3;
    
    
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject LevelSelectionCanvas;

    enum Screen { Screen1, Screen2, Screen3 };

    private Screen currentScreen = Screen.Screen1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLeftButtonPressed()
    {
        if (currentScreen == Screen.Screen1)
        {
            Screen1.SetActive(false);
            Screen3.SetActive(true);
            currentScreen = Screen.Screen3;
        }
        else if (currentScreen == Screen.Screen2)
        {
            Screen2.SetActive(false);
            Screen1.SetActive(true);
            currentScreen = Screen.Screen1;
        }
        else if (currentScreen == Screen.Screen3)
        {
            Screen3.SetActive(false);
            Screen2.SetActive(true);
            currentScreen = Screen.Screen2;
        }
    }

    public void OnRightButtonPressed()
    {
        if (currentScreen == Screen.Screen1)
        {
            Screen1.SetActive(false);
            Screen2.SetActive(true);
            currentScreen = Screen.Screen2;
        }
        else if (currentScreen == Screen.Screen2)
        {
            Screen2.SetActive(false);
            Screen3.SetActive(true);
            currentScreen = Screen.Screen3;
        }
        else if (currentScreen == Screen.Screen3)
        {
            Screen3.SetActive(false);
            Screen1.SetActive(true);
            currentScreen = Screen.Screen1;
        }
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }

    public void OnPlayButtonPressed()
    {
        MainMenuCanvas.SetActive(false);
        LevelSelectionCanvas.SetActive(true);
    }

    public void OnLevelSelect(int levelId)
    {
        switch (levelId)
        {
            case 0:
                SceneManager.LoadScene(0);
                break;
            case 1:
                SceneManager.LoadScene(1);
                break;
            case 2:
                SceneManager.LoadScene(2);
                break;
        }
    }
}
