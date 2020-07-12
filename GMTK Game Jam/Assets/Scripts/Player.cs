using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Variables
    public float speed;
    public bool isCaught;

    bool isDead;
    bool turnClockwise;
    bool playing;
    bool isWin;

    Vector2[] directions;
    Vector2 moveDirection;
    int currentDirection;

    Rigidbody2D rb;

    // Wall detector
    Ray2D ray;
    RaycastHit2D hit;

    // Animation frames
    public float FPS;

    SpriteRenderer sr;
    public Sprite[] walkUp;
    public Sprite[] walkDown;
    public Sprite[] walkSide;

    // Properties
    public bool Playing
    {
        get { return playing; }
    }

    public bool IsWin
    {
        get { return isWin; }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentDirection = 0;

        isDead = false;
        isWin = false;
        turnClockwise = true;
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
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Stop update if dead
        if (isCaught || !playing)
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
        if(collision.transform.CompareTag("Reverse Zone") && turnClockwise)
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
        if (collision.transform.CompareTag("Reverse Zone") && !turnClockwise)
        {
            turnClockwise = !turnClockwise;
        }

        //Switch tiles to wall when exiting switch tiles
        if (collision.transform.CompareTag("Switch"))
        {
            collision.transform.tag = "Wall";
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
    public void PlayGame()
    {
        playing = !playing;
        PlayAnimation();
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
            if(hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Fence"))
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

                if(currentDirection == 1)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX = false;
                }

                moveDirection = directions[currentDirection];

                PlayAnimation();
            }
        }
    }

    private void PlayAnimation()
    {
        switch (currentDirection)
        {
            case 0:
                StopAllCoroutines();
                StartCoroutine(WalkUp());
                break;

            case 1:
                StopAllCoroutines();
                StartCoroutine(WalkSide());
                break;

            case 2:
                StopAllCoroutines();
                StartCoroutine(WalkDown());
                break;

            case 3:
                StopAllCoroutines();
                StartCoroutine(WalkSide());
                break;
        }
    }

    IEnumerator WalkUp()
    {
        int i;
        i = 0;
        while (i < walkUp.Length)
        {
            sr.sprite = walkUp[i];
            i++;
            yield return new WaitForSeconds(1 / FPS);
            yield return 0;

        }
        StartCoroutine(WalkUp());
    }

    IEnumerator WalkSide()
    {
        int i;
        i = 0;
        while (i < walkSide.Length)
        {
            sr.sprite = walkSide[i];
            i++;
            yield return new WaitForSeconds(1 / FPS);
            yield return 0;

        }
        StartCoroutine(WalkSide());
    }

    IEnumerator WalkDown()
    {
        int i;
        i = 0;
        while (i < walkDown.Length)
        {
            sr.sprite = walkDown[i];
            i++;
            yield return new WaitForSeconds(1 / FPS);
            yield return 0;

        }
        StartCoroutine(WalkDown());
    }
}
