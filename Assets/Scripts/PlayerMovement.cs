using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float runSpeed = 5.0f;
    public float jumpVelocity = 20.0f;
    public float throwSpeed = 10.0f;

    public GameObject hookRef;
    //public LayerMask notGrappleable;

    private Rigidbody2D rb2d;
    private Animator anim;

    private Vector3 targetPos;
    
    private bool onGround = false;

    private bool grappleOut = false;
    private int frame = 0;
    private int stepNumber = 0;

    private bool jumpPressed = false;
    private bool grapplePressed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        float xMove = BaseMovement();

        CheckRotation(xMove);
        InformAnimations(xMove);

        CheckGrapplingHook();
    }

    private float BaseMovement()
    {
        //Base Movement -----------------------------
        float deltaYvel = 0;
        //Only do jump first frame
        if (jumpPressed)
        {
            jumpPressed = false;
            //Debug.Log("Key down");
            if (onGround)
            {
                //Debug.Log("On ground");
                deltaYvel = jumpVelocity;
                anim.SetTrigger("Jumped");
            }
        }

        float multiplier = speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            multiplier = runSpeed;
            //Debug.Log("Run");
        }

        float xMove = Input.GetAxisRaw("Horizontal") * multiplier;
        rb2d.velocity = new Vector2(xMove, rb2d.velocity.y + deltaYvel);
        if (xMove != 0 && onGround == true)
            makeFootNoises();
        return xMove;
    }

    private void InformAnimations(float xMove)
    {
        anim.SetFloat("Speed", Mathf.Abs(xMove / 5));
        anim.SetBool("InAir", !onGround);
    }

    private void CheckRotation(float xMove)
    {
        //Debug.Log("XMOVE: " + xMove / 5);

        Vector3 rotation = transform.eulerAngles;
        if (xMove < 0.0f)
        {
            rotation.y = 180.0f;
        }
        else if (xMove > 0.0f)
        {
            rotation.y = 0.0f;
        }
        transform.rotation = Quaternion.Euler(rotation);
    }
    
    private void makeFootNoises() 
    {   
        
        if (stepNumber == 15)
            SoundManagerScript.PlaySound("step1");
        else if (stepNumber == 30)
            SoundManagerScript.PlaySound("step2");
        else if (stepNumber == 45)
            SoundManagerScript.PlaySound("step3");
            
        stepNumber += 1;
        if (stepNumber == 46)
            stepNumber = 1;
        
    }

    private void CheckGrapplingHook()
    {
        //Grappling Hook
        if (grapplePressed)
        {
            grapplePressed = false;
            if (grappleOut = false){
                SoundManagerScript.PlaySound ("woosh");
                grappleOut = true;
            }
            if (GameObject.FindGameObjectWithTag("hook") == null)
            {
                Vector3 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float theta = Mathf.Atan2(delta.y, delta.x);

                float velX = Mathf.Cos(theta) * throwSpeed;
                float velY = Mathf.Sin(theta) * throwSpeed;

                Vector2 vel = new Vector2(velX, velY);
                Vector3 position = this.transform.position;

                //Debug.Log("Delta: " + delta + " Theta " + theta + " Rotation " + Mathf.Rad2Deg * -theta);

                float z = Mathf.Rad2Deg * theta + 90.0f;

                GameObject currentHook = Instantiate(hookRef, position, Quaternion.Euler(new Vector3(0, 0, z)));
                GrapplingHook gh = currentHook.GetComponent<GrapplingHook>();

                gh.InitialVelocity(vel.x, vel.y, this.gameObject);
            }
        }
    }

    public void NotifyGrappleSuccess()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onGround = true;
        grappleOut = false;
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
