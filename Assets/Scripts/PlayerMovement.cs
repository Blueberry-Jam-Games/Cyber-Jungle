using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpVelocity = 20.0f;
    public float throwSpeed = 10.0f;

    public GameObject hookRef;
    //public LayerMask notGrappleable;

    private Rigidbody2D rb2d;

    private Vector3 targetPos;
    
    private bool onGround = false;

    private bool jumpPressed = false;
    private bool grapplePressed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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

        //Base Movement -----------------------------
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

        //Grappling Hook
        if (grapplePressed)
        {
            grapplePressed = false;
            if(GameObject.FindGameObjectWithTag("hook") == null)
            {
                Vector3 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float theta = Mathf.Atan2(delta.y, delta.x);

                float velX = Mathf.Cos(theta) * throwSpeed;
                float velY = Mathf.Sin(theta) * throwSpeed;

                Vector2 vel = new Vector2(velX, velY);
                Vector3 position = this.transform.position;

                Debug.Log("Delta: " + delta + " Theta " + theta + " Rotation " + Mathf.Rad2Deg * -theta);

                float z = Mathf.Rad2Deg * theta + 90.0f;

                GameObject currentHook = Instantiate(hookRef, position, Quaternion.Euler(new Vector3(0, 0, z)));
                GrapplingHook gh = currentHook.GetComponent<GrapplingHook>();
                
                gh.InitialVelocity(vel.x, vel.y, this.gameObject);
            }
        }
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
