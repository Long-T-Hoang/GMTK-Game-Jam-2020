using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    public float speed;
    public bool isCaught;

    float minAngle;
    float maxAngle;
    float timer;

    bool isDead;
    bool turnClockwise;

    Vector3 mainCamPos;
    Vector2[] directions;
    Vector2 moveDirection;
    int currentDirection;

    Rigidbody2D rb;

    WallDetector wallDetectorScript;


    // Start is called before the first frame update
    void Start()
    {
        minAngle = 0;
        timer = 0f;
        isDead = false;
        mainCamPos = Camera.main.transform.position;
        turnClockwise = true;
        currentDirection = 0;

        directions = new Vector2[4];

        directions[0] = transform.up;
        directions[1] = transform.right;
        directions[2] = -transform.up;
        directions[3] = -transform.right;

        moveDirection = directions[0];

        rb = GetComponent<Rigidbody2D>();

        wallDetectorScript = transform.GetChild(0).GetComponent<WallDetector>();
    }

    private void Update()
    {
        // Update camera position
        if (mainCamPos.y < transform.position.y)
        {
            mainCamPos.y = transform.position.y;

            Camera.main.transform.position = mainCamPos;
        }

        // Stop update if dead
        if (isDead || isCaught)
        {
            return;
        }

        MoveForward();
    }

    void MoveForward()
    {
        transform.position = (Vector2)transform.position + moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Change direction of turning when entering special zones
        if(collision.transform.tag == "Reverse Zone")
        {
            turnClockwise = !turnClockwise;
        }

        // Die on colliding with an object with "Death" tag
        if(collision.transform.tag == "Death")
        {
            isDead = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Change direction of turning when exiting special zones
        if (collision.transform.tag == "Reverse Zone")
        {
            turnClockwise = !turnClockwise;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Wall")
        {
            currentDirection += turnClockwise ? 1 : -1;

            if(currentDirection < 0)
            {
                currentDirection = 3;
            }

            if(currentDirection > 3)
            {
                currentDirection = 0;
            }

            moveDirection = directions[currentDirection];
        }
    }
}
