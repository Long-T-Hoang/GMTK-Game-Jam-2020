using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    public float speed;

    float minAngle;
    float maxAngle;
    float timer;

    bool isTurn;
    bool turnClockwise;
    bool isDead;
    bool isCaught;

    Vector3 mainCamPos;

    Rigidbody2D rb;

    WallDetector wallDetectorScript;

    // Start is called before the first frame update
    void Start()
    {
        minAngle = 0;
        isTurn = false;
        turnClockwise = true;
        timer = 0f;
        isDead = false;
        mainCamPos = Camera.main.transform.position;

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

        if (isTurn)
        {
            timer += Time.deltaTime;
            TurnNinetyDegree();
        }
        else
        {
            MoveForward();
        }

        // Set action to turning and calculate starting and final angle
        if (wallDetectorScript.isWall && !isTurn)
        {
            minAngle = transform.eulerAngles.z;
            isTurn = true;
            wallDetectorScript.isWall = false;

            if (turnClockwise)
            {
                maxAngle = minAngle - 90f;
            }
            else
            {
                maxAngle = minAngle + 90f;
            }
        }
    }

    void MoveForward()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void TurnNinetyDegree()
    {
        float angle = Mathf.LerpAngle(minAngle, maxAngle, timer);
        transform.eulerAngles = new Vector3(0, 0, angle);

        // Stop turning and start moving once lerping is done
        if(timer >= 1f)
        {
            isTurn = false;
            timer = 0;
        }
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
}
