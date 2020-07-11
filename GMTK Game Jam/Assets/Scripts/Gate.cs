using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // Variables
    [SerializeField]
    public bool isVertical = false;

    float timer;
    bool isOpen;
    bool isMoving;

    Vector2 closedPos;
    Vector2 openPos;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        isOpen = false;
        isMoving = false;

        closedPos = transform.position;

        if(isVertical)
        {
            openPos = closedPos;
            openPos.y -= transform.localScale.y;
        }
        else
        {
            openPos = closedPos;
            openPos.x -= transform.localScale.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            MoveGate();
        }
    }

    private void OnMouseDown()
    {
        if(!isMoving)
        {
            isOpen = !isOpen;
            isMoving = true;
        }
    }

    private void MoveGate()
    {
        if(timer >= 1f)
        {
            timer = 0f;
            isMoving = false;
            return;
        }

        timer += Time.deltaTime;

        if (isOpen)
        {
            transform.position = Vector2.Lerp(closedPos, openPos, timer);
        }
        else
        {
            transform.position = Vector2.Lerp(openPos, closedPos, timer);
        }
    }
}
