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

    // Start is called before the first frame update
    void Start()
    {
        minAngle = 0;
        isTurn = true;
        turnClockwise = true;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveForward();

        if (isTurn)
        {
            TurnNinetyDegree(turnClockwise);
        }
    }

    void MoveForward()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void TurnNinetyDegree(bool isClockwise)
    {
        if (isClockwise)
        {
            maxAngle = minAngle - 90f;
        }
        else
        {
            maxAngle = minAngle + 90f;
        }

        float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
        transform.eulerAngles = new Vector3(0, 0, angle);

        if(transform.eulerAngles.z == maxAngle)
        {
            isTurn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Wall")
        {
            minAngle = transform.eulerAngles.z;
            isTurn = true;
        }

        if(collision.transform.tag == "Reverse Zone")
        {
            turnClockwise = !turnClockwise;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Reverse Zone")
        {
            turnClockwise = !turnClockwise;
        }
    }
}
