using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int maxMoveFrames = 40;
    public float bounceSpeed = 0.01f;

    private int moveFrames = 0;
    private bool moveUp = true;

    private void Start()
    {
        moveFrames = maxMoveFrames;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.DoPowerup();
            Destroy(this.gameObject);
        }   
    }
    
    // Update is called once per frame
    void Update()
    {
        float delta = bounceSpeed;
        if (!moveUp)
        {
            delta *= -1;
        }
        transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + delta, transform.position.z), transform.rotation);
        moveFrames -= 1;
    
        if(moveFrames == 0)
        {
            moveUp = !moveUp;
            moveFrames = maxMoveFrames;
        }
    }
}
