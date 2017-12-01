using Assets.Scripts.World;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

    Rigidbody rb; //rigid body for the player character
    Mesh me; //character mesh

    [SerializeField]
    Vector3 _velocity;
    Vector3 lastPos;

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

    public Vector3 Velocity {get {return _velocity;}}

    

    // Use this for initialization
    void Start ()
    {
        if(WorldStateManager.Instance.positionSet)
        {
            transform.position = WorldStateManager.Instance.Position;
        }

        rb = GetComponent<Rigidbody>();
        me = GetComponent<Mesh>();
        print(me);
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        source = GetComponent<AudioSource>();
        Time.timeScale = 1;

        PauseMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; //locks mouse so it's not in the way

        lastPos = transform.position;
    }

    void OnCollisionStay() //If object collides with something, it is grounded
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        //WorldStateManager.Instance.SetPosition(transform.position);
        //float translation = Input.GetAxis("Vertical") * speed; //gets input forwards and backwards
        //float straffe = Input.GetAxis("Horizontal") * speed; //gets input left and right
        //translation *= Time.deltaTime; //keeps movements smooth and in time with update
        //straffe *= Time.deltaTime; //keeps movements smooth and in time with update

        //transform.Translate(new Vector3(straffe /*z-axis*/, 0, translation/*x-axis*/));
        

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
    }


    private void FixedUpdate()
    {
         float translation = Input.GetAxis("Vertical") * speed; //gets input forwards and backwards
        float straffe = Input.GetAxis("Horizontal") * speed; //gets input left and right
        translation *= Time.fixedDeltaTime; //keeps movements smooth and in time with update
        straffe *= Time.fixedDeltaTime; //keeps movements smooth and in time with update

        transform.Translate(new Vector3(straffe /*z-axis*/, 0, translation/*x-axis*/));


        _velocity = transform.position - lastPos;
        lastPos = transform.position;

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