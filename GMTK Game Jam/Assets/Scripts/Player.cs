using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    public float speed;
    public bool isCaught;
    public float sensitivity;

    bool isDead;
    bool turnClockwise;
    bool playing;
    bool isWin;

    Vector3 mainCamPos;
    Vector2[] directions;
    Vector2 moveDirection;
    int currentDirection;

    Rigidbody2D rb;

    // Wall detector
    Ray2D ray;
    RaycastHit2D hit;

    // Properties
    public bool Playing
    {
        get { return playing; }
    }

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        isWin = false;
        mainCamPos = Camera.main.transform.position;
        turnClockwise = true;
        currentDirection = 0;
        playing = false;

        directions = new Vector2[4];

        directions[0] = transform.up;
        directions[1] = transform.right;
        directions[2] = -transform.up;
        directions[3] = -transform.right;

        moveDirection = directions[0];

        ray.origin = transform.position;
        ray.direction = moveDirection;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!playing)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            cameraPos.y += Input.GetAxis("Mouse ScrollWheel") * sensitivity;

            Camera.main.transform.position = cameraPos;
        }
        else
        {
            mainCamPos.y = transform.position.y;

            Camera.main.transform.position = mainCamPos;
        }

        // Stop update if dead
        if (isDead || isCaught || !playing)
        {
            return;
        }

        MoveForward();

        WallDetection();
    }

    void MoveForward()
    {
        transform.position = (Vector2)transform.position + moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Change direction of turning when entering special zones
        if(collision.transform.CompareTag("Reverse Zone"))
        {
            turnClockwise = !turnClockwise;
        }

        if(collision.transform.CompareTag("Finish"))
        {
            isWin = true;
            playing = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Change direction of turning when exiting special zones
        if (collision.transform.CompareTag("Reverse Zone"))
        {
            turnClockwise = !turnClockwise;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Death"))
        {
            isDead = true;
        }
    }

    // Start player movement
    public void playGame()
    {
        playing = !playing;
    }

    // Detect walls
    private void WallDetection()
    {
        // Update ray
        ray.origin = transform.position;
        ray.direction = moveDirection;

        hit = Physics2D.Raycast(ray.origin, ray.direction, 0.7f);

        if(hit.collider != null)
        {
            if(hit.collider.CompareTag("Wall"))
            {
                currentDirection += turnClockwise ? 1 : -1;

                if (currentDirection < 0)
                {
                    currentDirection = 3;
                }

                if (currentDirection > 3)
                {
                    currentDirection = 0;
                }

                moveDirection = directions[currentDirection];
            }
        }
    }
}
