using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDestroyed : MonoBehaviour
{
    public GameObject bossExplosion;
    private GameController bossControl;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            bossControl = gameControllerObject.GetComponent<GameController>();
        }
        if (gameControllerObject == null)
        {
            Debug.Log("Cannot Find'GameController' script");
        }
    }
         private void OnTriggerEnter(Collider other)
   {
       Instantiate(bossExplosion, transform.position, transform.rotation);
      // bossControl.BossDestroyed();

        Destroy(gameObject);
    }
    

}
