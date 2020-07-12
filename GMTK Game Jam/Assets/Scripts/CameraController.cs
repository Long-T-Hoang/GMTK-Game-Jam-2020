using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variables
    public float sensitivity;
    public Grid tileGrid;

    float halfMapSize;
    Player playerScript;
    Vector3 mainCamPos;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mainCamPos = transform.position;

        halfMapSize = tileGrid.GetComponent<RectTransform>().rect.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerScript.Playing)
        {
            Vector3 cameraPos = transform.position;
            cameraPos.y += Input.GetAxis("Mouse ScrollWheel") * sensitivity;

            if(!ClampCamera(cameraPos))
            {
                transform.position = cameraPos;
            }
        }
        else
        {
            mainCamPos.y = playerScript.transform.position.y;

            if (!ClampCamera(mainCamPos))
            {
                transform.position = mainCamPos;
            }
        }
    }

    private bool ClampCamera(Vector3 cameraPos)
    {
        if (cameraPos.y + Camera.main.orthographicSize > halfMapSize)
        {
            return true;
        }

        if (cameraPos.y - Camera.main.orthographicSize < -halfMapSize)
        {
            return true;
        }

        return false;
    }
}
