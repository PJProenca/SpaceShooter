using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot_2_Destroy : MonoBehaviour
{

    Stage2Controller stage2Controller;
    public GameObject ShieldExplosion;
    public GameObject PlayerExplosion;
    private void Start()
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
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shield")
        {
            Debug.Log("teste");
            Instantiate(ShieldExplosion, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
            stage2Controller.ShieldOff();
        }
        
        if(other.tag == "Player" && !stage2Controller.isShieldOn())
        {
            Instantiate(PlayerExplosion, transform.position, transform.rotation);
            stage2Controller.GameOver();
            Destroy(other.gameObject);
        }
    }
}
