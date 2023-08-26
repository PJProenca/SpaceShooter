using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_2_Boss_Control : MonoBehaviour
{
    public GameObject shot;
    public Transform ShotSpawn1;
    public Transform ShotSpawn2;
    public float fireRate;
    public Rigidbody rb;

    public float nextShot;
    private bool InitShoot;

    public GameObject canvasObj;
    public ProgressBar healthBar;

    // Start is called before the first frame update
    void Start()
    {

        canvasObj.SetActive(false);
        healthBar.BarValue = 100;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SpawnBossShots());
        InitShoot = false;
    }
    IEnumerator SpawnBossShots()
    {
        yield return new WaitUntil(() => rb.transform.position.z <= 7.22);
        canvasObj.SetActive(true);
        yield return new WaitForSeconds(3);

        InitShoot = true;
    }

    public float returnBossPosition()
    {
        return rb.transform.position.z;
    }
    // Update is called once per frame
    void Update()
    {


        if (InitShoot && Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;
            Instantiate(shot, ShotSpawn1.position, ShotSpawn1.rotation);
            Instantiate(shot, ShotSpawn2.position, ShotSpawn2.rotation);
        }
    }

    public void HealthByShot()
    {
        healthBar.BarValue -= 2.5f;
        //healthBar.BarValue -= Mathf.Min(Random.value, healthBar.BarValue / 2f);

    }
}
