using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public float breakDistance = 0.6f;
    public float maxDistance = 7.0f;

    private Rigidbody2D rb2d;
    private DistanceJoint2D dj2d;
    private LineRenderer chain;

    private float dx, dy;
    private GameObject playerRef;

    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        dj2d = GetComponent<DistanceJoint2D>();
        dj2d.enabled = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        dj2d.autoConfigureDistance = false;
        rb2d.velocity = new Vector3(dx, dy, 0f);
        chain = GetComponent<LineRenderer>();
    }

    public void InitialVelocity(float dx, float dy, GameObject playerRef)
    {
        this.dx = dx;
        this.dy = dy;
        this.playerRef = playerRef;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision Enter");
        if(collision.gameObject.tag == "grapple")
        {
            rb2d.bodyType = RigidbodyType2D.Static;
            dj2d.enabled = true;
            dj2d.distance = 0.25f;
            dj2d.connectedBody = playerRef.GetComponent<Rigidbody2D>();
            active = true;
        }
        else if(active == false && collision.gameObject.tag == "Player")
        {
            //No
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Update()
    {
        chain.SetPosition(0, this.transform.position);
        chain.SetPosition(1, playerRef.transform.position);
    }

    private void FixedUpdate()
    {
        if (!active)
        {
            Vector2 tgt = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
            //Vector3 tgt = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            float z = Mathf.Atan2(tgt.y, tgt.x) * Mathf.Rad2Deg - 90.0f;
            //Debug.Log("Target is " + tgt);

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, z));

            float distance = Vector3.Distance(transform.position, playerRef.transform.position);
            if(distance >= maxDistance)
            {
                Debug.Log("Gravity");
                rb2d.gravityScale = 0.7f;
            }

        }
        else
        {
            //Debug.Log("Active");
            float distance = Vector3.Distance(transform.position, playerRef.transform.position);
            //Debug.Log("Distance is " + distance);
            if(distance <= breakDistance)
            {
                playerRef.gameObject.GetComponent<PlayerMovement>().NotifyGrappleSuccess();
                Destroy(this.gameObject);
            }
        }
    }
}
