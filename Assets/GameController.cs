using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{


    public GameObject hazard;
    public GameObject Boss;

    private HazardMovement hazardSpeed;



    public Vector3 spawnValues;
    Vector3 bossposition;


    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text gameOverText;
    public Text restartText;
    public Text StageCompleteTxt;

    private bool gameOver;
    public static int score;
    private bool restart;
    public bool returnGame;

    private int nextLevel;
    public Text LevelText;
    private int StageNum;
    private bool levelOne;
    private int stageLvl;

    private int highScore;
    public AudioClip GameOverClip;
    //public AudioClip BossAlertClip;
    private AudioSource Music;
    //AudioSource BossAlert;

    
    private bool Destroyed;
    private bool SceneOne;

    
    void Start()
    {
        PlayerPrefs.SetInt("RunScore", 0);
        levelOne = true;
        
        StartCoroutine(SpawnWaves());
        gameOver = false;
        restart = false;
        returnGame = false;
        score = 0;
        WriteScore();
        gameOverText.text = "";
        restartText.text = "";
        nextLevel = 1000;
        StageNum = 1;
        Destroyed = false;
        SceneOne = true;
        StageCompleteTxt.text = " ";
        stageLvl = 1;

    }
    private void StageUp()
    {
        stageLvl = 2;
    }

    public int ReturnStageLvl()
    {
        return stageLvl;
    }
    private void Update()
    {
        StartCoroutine(FirstLevel());
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SpaceShooter_Scene1");
                PlayerPrefs.SetInt("RunScore", 0);
            }
        }
        if (returnGame)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("Menu");
            }
        }

        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }

        if (ReturnBossDestroyed() && !gameOver)
        {
            Destroyed = false;
            PlayerPrefs.SetInt("RunScore", score);
            PlayerPrefs.SetInt("RunNextLevel", nextLevel);
            StageUp();
            StartCoroutine(WriteStageComplete());
            
        }

      
        //GameObject hazardSpeedObj = GameObject.FindWithTag("Asteroide");
        //if (hazardSpeedObj != null)
        //{
        //    hazardSpeed = hazardSpeedObj.GetComponent<HazardMovement>();
        //}
        //if (hazardSpeed == null)
        //{
        //    Debug.Log("Cannot Find 'Hazard Movement' script");
        //}
        //Debug.Log(hazardSpeedObj);


    }


    /*Game Over Code Related*/

    void SounOnGameOver()
    {
        Music = GetComponent<AudioSource>();
        Music.Stop();
        Music.clip = GameOverClip;
        Music.loop = false;
        Music.Play();

    }

  

    public void GameOver()
    {
        SounOnGameOver();
        gameOver = true;
        gameOverText.text = "Game Over";
        score = 0;
    }

    public bool ReturnGameOver()
    {
        return gameOver;
    }

    /* Add Score code Related*/
    public void AddScore(int addValues)
    {
        score += addValues;
        WriteScore();
    }




    /* Level Up code related*/

    void LevelUp()
    {
     
        nextLevel *= 2;
        
        StageNum++;
        levelOne = true;
        switch (StageNum)
        {

            case 2:
            case 4:
            case 6:
            case 8:
                hazardCount += 1;
                break;
            case 3:
            case 5:
            case 7:
            case 9:
                spawnWait -= 0.25f;
                break;
            default:
                break;
        }
    }

  
    /* Screen Text Output code related*/

  
    public void WriteLevel()
    {
        if (StageNum <= 9)
        {
            LevelText.text = "Stage 1 - " + StageNum.ToString();
        }
        else
        {
            //BossAlertSound();  
            LevelText.text = "WARNING!! BOSS LEVEL";
        }
    }

    void WriteScore()
    {
        scoreText.text = "Score:" + score.ToString();
    }

    IEnumerator WriteStageComplete()
    {
        StageCompleteTxt.text = " Stage 1 Completed!!";
        
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(2);

    }


    /* IEnumerators */

    IEnumerator SpawnWaves()
    {

        yield return new WaitForSeconds(startWait);
        while (true )
        {
          
                for (int i = 0; i < hazardCount; i++)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion SpawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, SpawnRotation);

                    yield return new WaitForSeconds(spawnWait);
                }
                yield return new WaitForSeconds(waveWait);





                //if (score == 1000)
                //{
                //    hazardSpeed.updateSpeed();
                //    Debug.Log(hazardSpeed.speed);

                //}
                if (gameOver)
                {

                    restart = true;
                    returnGame = true;
                    restartText.text = "Press <R> to Restart or <E> to Exit";
                    break;
                }



                if (score >= nextLevel && StageNum < 10)
                {
                    LevelUp();

                    yield return new WaitForSeconds(3);
                }

                //switch (StageNum)
                //{

                //    case 2:
                //    case 4:
                //    case 6:
                //    case 8:
                //        Debug.Log("wavewait update");
                //        spawnWait -= 0.5f;
                //        break;
                //    default:
                //        Debug.Log("no update");
                //        break;
                //}
                if (StageNum == 10 && !gameOver && SceneOne)
                {
                    hazardCount = 0;
                    SceneOne = false;
                    bossposition = new Vector3(0, 0, 0);
                    Quaternion BossRotation = Quaternion.identity;
                    Instantiate(Boss, bossposition, BossRotation);

                }
            
          

        }
    }

    IEnumerator FirstLevel()
    {

        if (levelOne)
        {
            WriteLevel();
            yield return new WaitForSeconds(5);
            levelOne = false;
            LevelText.text = " ";
        }
    }


    /*Boss code*/

    public void BossDestroyed()
    {
        Destroyed = true;
    }

    public bool ReturnBossDestroyed()
    {
        return Destroyed;
    }

   

    public int ReturnStage()
    {
        return StageNum;
    }






}
