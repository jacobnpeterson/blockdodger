using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 1000f;
    public float jumpForce = 500f;
    public float moveForce = 500f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        rb.MoveRotation(Quaternion.identity);

        //fall off edge
        if (rb.position.y < -.5f)
        {
            endGame();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-moveForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(moveForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
           endGame();
        }
    }

    private void endGame()
    {   
        this.enabled = false;
        FindObjectOfType<GameState>().endGame();
    }
}
