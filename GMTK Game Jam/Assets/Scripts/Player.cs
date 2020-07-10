using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    public float speed;

    float minAngle;
    float maxAngle;
    bool isTurn;
    bool turnClockwise;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        minAngle = 0;
        isTurn = false;
        turnClockwise = false;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurn)
        {
            timer += Time.deltaTime;
            TurnNinetyDegree();
        }
        else
        {
            MoveForward();
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
        // Set action to turning and calculate starting and final angle
        if(collision.transform.tag == "Wall" && !isTurn)
        {
            minAngle = transform.eulerAngles.z;
            isTurn = true;

            if (turnClockwise)
            {
                maxAngle = minAngle - 90f;
            }
            else
            {
                maxAngle = minAngle + 90f;
            }
        }

        // Change direction of turning when entering special zones
        if(collision.transform.tag == "Reverse Zone")
        {
            turnClockwise = !turnClockwise;
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
