using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_2_Boss_Destroy_Control : MonoBehaviour
{
    public GameObject BossExplosion;
    public GameObject BossPlayerDestroy;
    public GameObject ShotExplosion;
    public GameObject ShieldExplosion;

    private Stage2Controller stage2Controller;
    private Scene_2_Boss_Control Scene_2_Boss_Control;
    public int BossScoreValues;
    void Start()
    {

        GameObject stage2ControllerObject = GameObject.FindWithTag("Stage2_GC");
        if (stage2ControllerObject != null)
        {
            stage2Controller = stage2ControllerObject.GetComponent<Stage2Controller>();
        }
        if (stage2Controller == null)
        {
            Debug.Log("Cannot Find'GameController' script");
        }

        GameObject Scene_2_Boss_ControlObj = GameObject.FindWithTag("FinalBoss");
        if (Scene_2_Boss_ControlObj != null)
        {
            Scene_2_Boss_Control = Scene_2_Boss_ControlObj.GetComponent<Scene_2_Boss_Control>();
        }
        if (Scene_2_Boss_Control == null)
        {
            Debug.Log("Cannot Find 'BossControl' script");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !stage2Controller.isShieldOn())
        {
            Instantiate(BossPlayerDestroy, transform.position, transform.rotation);
            stage2Controller.GameOver();
            Destroy(other.gameObject);
        }

        if (other.tag == "Shield")
        {

            Instantiate(ShieldExplosion, other.transform.position, other.transform.rotation);
            
            stage2Controller.ShieldOff();
        }
        if (other.tag == "PlayerShot" && Scene_2_Boss_Control.returnBossPosition() <= 9)
        {


            Scene_2_Boss_Control.HealthByShot();
            Destroy(other.gameObject);
            Instantiate(ShotExplosion, transform.position, transform.rotation);
            if (Scene_2_Boss_Control.healthBar.BarValue <= 0)
            {
                Instantiate(BossExplosion, transform.position, transform.rotation);
                Destroy(gameObject);
                stage2Controller.AddScore(BossScoreValues);
                stage2Controller.BossDestroyed();
            }

        }
    }
}
