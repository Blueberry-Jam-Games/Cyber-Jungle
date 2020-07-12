using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

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
        gameObject.transform.Rotate(new Vector3(0, 0, -1.0f));
    }
}
