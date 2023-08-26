using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDestroyControl : MonoBehaviour
{
    public GameObject BossExplosion;
    public GameObject BossPlayerDestroy;
    public GameObject ShotExplosion;

    private GameController gameController;
    private BossControl bossControl;
    public int BossScoreValues;
    void Start()
    {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot Find'GameController' script");
        }

        GameObject BossControlObj = GameObject.FindWithTag("Boss");
        if (BossControlObj != null)
        {
            bossControl = BossControlObj.GetComponent<BossControl>();
        }
        if (BossControlObj == null)
        {
            Debug.Log("Cannot Find 'BossControl' script");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(BossPlayerDestroy, transform.position, transform.rotation);
            gameController.GameOver();
            Destroy(other.gameObject);
        }
        if (other.tag == "PlayerShot" && bossControl.returnBossPosition() <= 9)
        {


            bossControl.HealthByShot();
            Destroy(other.gameObject);
            Instantiate(ShotExplosion, transform.position, transform.rotation);
            if (bossControl.healthBar.BarValue <= 0)
            {
                Instantiate(BossExplosion, transform.position, transform.rotation);
                Destroy(gameObject);
                gameController.AddScore(BossScoreValues);
                gameController.BossDestroyed();
            }

        }


    }

}
