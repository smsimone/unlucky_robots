using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerMainMenu : MonoBehaviour
{
    public Button PlayButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {

        SoundManager.instance.ToGameplay();
        SceneManager.LoadScene("OverRobot");
    }

    public void back()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Commands");
    }


    public void quit()
    {
        Application.Quit();
    }


}
