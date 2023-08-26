using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private int HighScore;   
    public Text HighScoreText;

    AudioSource OpenClip;
    void Start()
    {
        OpenClip = GetComponent<AudioSource>();
        HighScoreText.text = "Highscore: ";
        HighScore = PlayerPrefs.GetInt("Highscore");
       
       
    }

    // Update is called once per frame
    void Update()
    {
      
        HighScoreText.text = "Highscore: " + HighScore.ToString();
    }

    public void StartGame()
    {
       
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void PlayOpenClip()
    {
        OpenClip.loop = false;
        OpenClip.Play();
    }
  
}
