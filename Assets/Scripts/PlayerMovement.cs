using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private DistanceJoint2D grappeling;

    private Vector3 targetPos;

    public float speed = 5.0f;
    public float jumpVelocity = 20.0f;

    public LayerMask notGrappleable;
    
    private bool onGround = false;

    private bool jumpPressed = false;
    private bool grapplePressed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        grappeling = GetComponent<DistanceJoint2D>();
        grappeling.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            jumpPressed = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            grapplePressed = true;
        }
    }

    private void FixedUpdate()
    {

        //Base Movement
        float deltaYvel = 0;
        //Only do jump first frame
        if(jumpPressed)
        {
            jumpPressed = false;
            //Debug.Log("Key down");
            if (onGround)
            {
                //Debug.Log("On ground");
                deltaYvel = jumpVelocity;
            }
        }

        float xMove = Input.GetAxisRaw("Horizontal") * speed;
        rb2d.velocity = new Vector2(xMove, rb2d.velocity.y + deltaYvel);

        //Grappeling Hook
        /*if (grapplePressed)
        {
            Debug.Log("Start grapple");
            grapplePressed = false;
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos - transform.position, 7, notGrappleable);

            if(hit.collider != null)
            {
                if(hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    grappeling.enabled = true;
                    grappeling.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    grappeling.distance = Vector2.Distance(transform.position, hit.point);
                }
            }
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onGround = true;
        //Debug.Log("On Ground");
    }

    void OnTriggerStay2D(Collider2D Other)
    {
        onGround = true;
    }

    void OnTriggerExit2D(Collider2D Other)
    {
        onGround = false;
        //Debug.Log("Not ground");
    }
}
