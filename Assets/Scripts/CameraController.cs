using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private GameObject player;
    public float maxXOffset = 2.5f;
    public float maxYOffset = 4.0f;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            offset = transform.position - player.transform.position;
        }

        float newX = transform.position.x;
        float newY = transform.position.y;

        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //transform.position = player.transform.position + offset;
        if (Mathf.Abs(player.transform.position.x - transform.position.x) > maxXOffset)
        {
            if (player.transform.position.x > transform.position.x)
            {
                newX = player.transform.position.x - maxXOffset;
            }
            else
            {
                newX = player.transform.position.x + maxXOffset;
            }
        }

        if (Mathf.Abs(player.transform.position.y - transform.position.y) > maxYOffset)
        {
            if (player.transform.position.y > transform.position.y)
            {
                newY = player.transform.position.y - maxYOffset;
            }
            else
            {
                newY = player.transform.position.y + maxYOffset;
            }
        }

        transform.SetPositionAndRotation(new Vector3(newX, newY, transform.position.z), transform.rotation);
    }
}
