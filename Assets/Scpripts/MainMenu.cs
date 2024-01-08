using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void OnLevel1ButtonPressed()
    {
        SceneManager.LoadScene("Level1");

    }

    public void OnLevel2ButtonPressed()
    {
        SceneManager.LoadScene("Level2");

    }

    public void OnExitToDesktopButtonPressed()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
