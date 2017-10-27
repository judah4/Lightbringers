using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

    Rigidbody rb; //rigid body for the player character
    Mesh me; //character mesh
    public Transform PauseMenu;

    //public to allow speed to be changed
    public float speed = 10.0F;
    //controls sprint and crouch speed
    public float speedMult = 1.5f;
    //change jumpspeed
    public float jumpForce = 2.0f;
    //checks to see if player is on the ground
    public bool isGrounded;
    //max speed (fixes super jump)
    public float maxSpeed =10;
    //hold jump transform
    private Vector3 jump;
    

    public AudioClip[] AudioEffect;
    public AudioSource source;


    // Use this for initialization
    void Start ()
    {
        //turns off cursor so it is not seen during gameplay
        rb = GetComponent<Rigidbody>();
        me = GetComponent<Mesh>();
        print(me);
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        source = GetComponent<AudioSource>();
        Time.timeScale = 1;
        PauseMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnCollisionStay() //If object collides with something, it is grounded
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed; //gets input forwards and backwards
        float straffe = Input.GetAxis("Horizontal") * speed; //gets input left and right
        translation *= Time.deltaTime; //keeps movements smooth and in time with update
        straffe *= Time.deltaTime; //keeps movements smooth and in time with update

        transform.Translate(straffe /*z-axis*/, 0, translation/*x-axis*/);

        //half speed to crouch or return back to normal after sprint
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed / speedMult;
        }

        //double speed to normal after crouch or to sprint 
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= speedMult;
        }

        //adds jump function if player is in the ground
        if ((Input.GetButton("Jump")) && isGrounded)
        {
            isGrounded = false; //for some reason grounded isn't being updated until a second jump is completed ??????!?!?!???
            isGrounded = false;

            //adds upward force, using gravity to mke jump smooth 
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            isGrounded = false;
        }
        /*
        if (Input.GetButtonUp("Pause")) //unlocks mouse after pressing escape key
        {
            if (PauseMenu.gameObject.activeInHierarchy == false)
            {
                //if pause menu is active
                Cursor.lockState = CursorLockMode.None;
                PauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }

            else
            {
                //if pause menu is not active
                Cursor.lockState = CursorLockMode.Locked;
                PauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }

        }
        */



    }


    void OnTriggerEnter(Collider other) //Called when object touches trigger collider
    {
        print("checkpoint");
        if (other.gameObject.CompareTag("CP")) //if object has pickup tag
        {
            other.gameObject.SetActive(false); //deactivates object            
        }
    }



    private void FixedUpdate()
    {
        //keeps player from going crazy fast
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
            //print(rb.velocity);
        }

        //checks to see if player has fallen out of the map
        if (transform.position.y <= -10)
        {
            transform.position = new Vector3(transform.position.x, 3.0f, transform.position.z);
            print("Out of bounds");
        }
    }
}