using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // Variables
    [SerializeField]
    public bool isVertical = false;

    public float speed;

    float timer;
    bool isOpen;
    bool isMoving;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        isOpen = false;
        isMoving = false;

        moveDirection = isVertical ? transform.up : transform.right;
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
            transform.position = (Vector2)transform.position + moveDirection * speed * Time.deltaTime;
        }
        else
        {
            transform.position = (Vector2)transform.position - moveDirection * speed * Time.deltaTime;
        }
    }
}
