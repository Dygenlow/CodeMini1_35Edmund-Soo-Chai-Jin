using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 12.0f;

    float xLimitA = 9.7f;
    float zLimitA = 9.7f;
    float xLimitB = 4.6f;
    float zLimitB = 19.6f;

    float planeCheck = 0.0f;
    float gravityModifier = 2.5f;
    bool isGround = false;

    Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * moveSpeed);
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * moveSpeed);

        if (planeCheck == 0.0f)
        {
            //Left and Right Boundary
            if (transform.position.x < -xLimitA)
            {
                transform.position = new Vector3(-xLimitA, transform.position.y, transform.position.z);
            }

            else if (transform.position.x > xLimitA)
            {
                transform.position = new Vector3(xLimitA, transform.position.y, transform.position.z);
            }

            //Front and Back Boundary
            if (transform.position.x > -4.6f && transform.position.x < 4.6f)
            {
                
            }

            else if (transform.position.z < -zLimitA)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -zLimitA);
            }

            else if (transform.position.z > zLimitA)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zLimitA);
            }
        }

        if (planeCheck == 1.0f)
        {
            //Left and Right Boundary
            if (transform.position.x < -xLimitB)
            {
                transform.position = new Vector3(-xLimitB, transform.position.y, transform.position.z);
            }

            else if (transform.position.x > xLimitB)
            {
                transform.position = new Vector3(xLimitB, transform.position.y, transform.position.z);
            }

            //Front Boundary
            if (transform.position.z > zLimitB)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zLimitB);
            }
        }

        JumpPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlaneA"))
        {
            Debug.Log("Stepping on PlaneA");
            planeCheck = 0.0f;
            isGround = true;
        }

        else
        {
            Debug.Log("Stepping on PlaneB");
            planeCheck = 1.0f;
            isGround = true;
        }
    }

    private void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isGround = false;

            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }
}
