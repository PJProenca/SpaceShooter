using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValues;


    private GameController gameController;
    


    void Start()
    {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot Find' GameController' script");
        }

       



    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }



        //Instantiate(explosion, transform.position, transform.rotation);
        
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }



        if (other.tag == "PlayerShot")
        {
            if (!gameController.ReturnGameOver())
            {
                gameController.AddScore(scoreValues);
                Destroy(gameObject);
                Destroy(other.gameObject);
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
        if (other.tag == "Asteroide")
        {
            return;
        }






    }




}
