using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gravity : MonoBehaviour
{
    public bool groundedPlayer;
    private Vector3 playerVelocity;
    private float gravityValue = -99.81f;
    private float distToGround;
    public bool Active = true;



    // Start is called before the first frame update
    void Start()
    {
        distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
    }
    private void FixedUpdate()
    {
        if (Active)
        {
            //check if player is grounded
            groundedPlayer = IsGrounded();
            //if the grav is not active  player is grounded and and velocity is more than 0
            if (groundedPlayer && playerVelocity.y < 0)
            {
                //set the player velocity to zero
                playerVelocity.y = 0f;
            }
            //if not grounded and gravity is active, fall!
            if (!groundedPlayer)
            {
                //apply gravity
                playerVelocity.y += gravityValue * Time.deltaTime;
                gameObject.transform.Translate(playerVelocity * Time.deltaTime);
            }
        }



    }

    // Update is called once per frame
    void Update()
    {

    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
