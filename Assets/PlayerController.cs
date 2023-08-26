using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float xmin, xmax, zmin, zmax;
    public float tilt;

    public GameObject playerExplosion;
    public GameObject shot;
    public Transform ShotSpawn;
    
    private GameController gameController;
    public float fireRate;
    private float nextFire;

    AudioSource shootSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shootSound = GetComponent<AudioSource>();



            GameObject gameControllerObject = GameObject.FindWithTag("GameController");
            if (gameControllerObject != null)
            {
                gameController = gameControllerObject.GetComponent<GameController>();
            }
            if (gameControllerObject == null)
            {
                Debug.Log("Cannot Find'GameController' script");
            }

       



    }

    // Update is called once per frame
    private void FixedUpdate()    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.velocity = movement * speed;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, xmin, xmax), 0, Mathf.Clamp(rb.position.z, zmin, zmax));
        rb.rotation = Quaternion.Euler(0.0f, 180.0f, rb.velocity.x * tilt);
    }

    void Update()
    {
        //StopPlayer();
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, ShotSpawn.position, ShotSpawn.rotation);
            if (shootSound != null)
            {
                shootSound.Play();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BossShot")
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);

            gameController.GameOver();
            Destroy(gameObject);
        }

    }
}

