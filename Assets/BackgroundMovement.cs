using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float speed = 4f;
    private Vector3 StartPosition;
    public float loopPosition;
    void Start()
    {
        StartPosition = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        transform.Translate(translation: Vector3.down * speed * Time.deltaTime);
        if (transform.position.z < -loopPosition)
        {
            transform.position = StartPosition;
        }
    }
}
