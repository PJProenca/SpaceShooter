using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage2Controller : MonoBehaviour
{
    private GameObject Hazard;
    public GameObject Hazard1;
    public GameObject Hazard2;
    public GameObject Boss;
    private GameObject[] HazardsContainer;
    public Vector3 spawnValues;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text gameOverText;
    public Text restartText;
    public Text StageCompleteTxt;
    public Text LevelText;

    private int ScoreToLevel;
    private int score;
    private bool ShieldIsOn;
    private int nextLevel;
    private int StageNum;
    private bool levelOne;

    private bool gameOver;

    public AudioClip GameOverClip;
    private AudioSource Music;

    private bool restart;
    public bool returnGame;

    public GameObject ShieldObject;

    private bool Destroyed;
    private bool SceneTwo;
    // Start is called before the first frame update
    void Start()
    {
        
        HazardsContainer = new GameObject[2];
        HazardsContainer[0] = Hazard1;
        HazardsContainer[1] = Hazard2;
        StartCoroutine(SpawnWaves());
        ShieldIsOn = true;
        score = PlayerPrefs.GetInt("RunScore");
        ScoreToLevel = 0;
        levelOne = true;
        gameOver = false;
        restart = false;
        returnGame = false;
        WriteScore();
        gameOverText.text = "";
        restartText.text = "";
        nextLevel = 1000;
        StageNum = 1;
        Destroyed = false;
        SceneTwo = true;
        StageCompleteTxt.text = " ";


    }

    public bool isShieldOn()
    {
        return ShieldIsOn;
    }

    public void ShieldOff()
    {
        ShieldIsOn = false;
        ShieldObject.SetActive(false);
    }

    public void ShieldOn()
    {
        ShieldIsOn = true;
        ShieldObject.SetActive(true);
    }
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
                if (!isShieldOn())
                {
                    ShieldOn();
                }
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


    public void AddScore(int addValues)
    {
        score += addValues;
        ScoreToLevel += addValues;
        WriteScore();
    }

    public int ReturnStage()
    {
        return StageNum;
    }

    void WriteScore()
    {
        scoreText.text = "Score:" + score.ToString();
    }

    public void WriteLevel()
    {
        if (StageNum <= 9)
        {
            LevelText.text = "Stage 2 - " + StageNum.ToString();
        }
        else
        {
            //BossAlertSound();  
            LevelText.text = "WARNING!! BOSS LEVEL";
        }
    }
    IEnumerator SpawnWaves()
    {

        yield return new WaitForSeconds(startWait);
        while (true)
        {

            for (int i = 0; i < hazardCount; i++)
            {

                int RandomIndex = Random.Range(0, 2);
                Hazard = HazardsContainer[RandomIndex];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion SpawnRotation = Quaternion.identity;
                Instantiate(Hazard, spawnPosition, SpawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                PlayerPrefs.SetInt("RunScore", 0);
                PlayerPrefs.SetInt("RunNextLevel", 0);
                restart = true;
                returnGame = true;
                restartText.text = "Press <R> to Restart or <E> to Exit";
                break;
            }

        

            if (ScoreToLevel >= nextLevel && StageNum < 10)
            {
                LevelUp();

                yield return new WaitForSeconds(3);
            }

            if (StageNum == 10 && !gameOver && SceneTwo)
            {
                hazardCount = 0;
                SceneTwo = false;
                Vector3 bossposition = new Vector3(0, 0, 0);
                Quaternion BossRotation = Quaternion.Euler(0,180,0);
                Instantiate(Boss, bossposition, BossRotation);

            }
        }

    }


    void Update()
    {
        StartCoroutine(FirstLevel());
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.SetInt("RunScore", 0);
                PlayerPrefs.SetInt("RunNextLevel", 0);
                SceneManager.LoadScene("SpaceShooter_Scene1");
                
            }
        }
        if (returnGame)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("Menu");
                PlayerPrefs.SetInt("RunScore", 0);
            }
        }

        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
            
            
        }

        if (ReturnBossDestroyed() && !gameOver)
        {
            Destroyed = false;
            
            StartCoroutine(WriteStageComplete());

        }
    }

    public void BossDestroyed()
    {
        Destroyed = true;
    }

    public bool ReturnBossDestroyed()
    {
        return Destroyed;
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

    IEnumerator WriteStageComplete()
    {
        StageCompleteTxt.text = " Congratulations!! Game Completed!!";
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Menu");

    }
}
