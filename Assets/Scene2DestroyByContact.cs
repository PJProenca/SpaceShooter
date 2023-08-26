using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2DestroyByContact : MonoBehaviour
{
    private Stage2Controller stage2Controller;
    public GameObject explosion;
    public GameObject ShieldExplosion;
    public GameObject playerExplosion;
    public int scoreValues;
    void Start()
    {
        GameObject Stage2_GCObj = GameObject.FindWithTag("Stage2_GC");
        if (Stage2_GCObj != null)
        {
            stage2Controller = Stage2_GCObj.GetComponent<Stage2Controller>();
        }
        if (stage2Controller == null)
        {
            Debug.Log("Cannot Find' Stage2Controller' script");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }


     if(other.tag == "Shield")
        {
            
            Instantiate(ShieldExplosion, other.transform.position, other.transform.rotation);            
            Destroy(gameObject);
            stage2Controller.ShieldOff();
        }

     if(other.tag == "Player" && !stage2Controller.isShieldOn())
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            stage2Controller.GameOver();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if(other.tag == "PlayerShot")
        {
            if (!stage2Controller.ReturnGameOver() && stage2Controller.ReturnStage() < 5)
            {
                stage2Controller.AddScore(scoreValues);
                Destroy(gameObject);
                Destroy(other.gameObject);
                Instantiate(explosion, transform.position, transform.rotation);
            }

            if (!stage2Controller.ReturnGameOver() && stage2Controller.ReturnStage() >= 5)
            {
                stage2Controller.AddScore(scoreValues*2);
                Destroy(gameObject);
                Destroy(other.gameObject);
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }

        if(other.tag == "Asteroide1" || other.tag == "Asteroide2")
        {
            return;
        }

    }

}

