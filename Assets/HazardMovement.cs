using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardMovement : MonoBehaviour
{
    
    public Rigidbody rb;
    public float speed;
    
   
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;





    }

    //public void updateSpeed()
    //{
    //    speed -= 3;
    //}


    // Update is called once per frame
    void Update()
    {
       

    }
}
