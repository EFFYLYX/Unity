using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rg;

    float MAX_SPEED = 7.0f;
    float MAX_LIVES =4;
    float walk_speed = 3.0f;
    float run_speed = 6.0f;
    float RUN_TIME = 10.0f;

    private CharacterController controller;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;


    public Animator animator;
    public float jumpForce = 5.0f;

    bool isGrounded = true;

    bool isPaused = false;


    Vector3 movement;

    bool isWalking = false;
    bool isRunning = false;
    Vector3 m_EulerAngleVelocity;

    bool canRun = true;


    public CoolDownTimer coolDownTimer;
    CoolDownTimer attackTimer;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rg = GetComponent<Rigidbody>();
        m_EulerAngleVelocity = new Vector3(0, 10, 0);
        //animator.SetBool("isGrounded", true);
        //animator.SetBool("isAlive", true);

        attackTimer = Instantiate(coolDownTimer);
        attackTimer.coolDown = 3.0f;

    }


    // Update is called once per frame
    void Update()
    {
        //if (isPaused == true)
        //{
        //    return;
        //}

        //Debug.Log(rg.velocity.ToString());
        if (isPaused == false)
        {

            if (isAttacked)
            {
                
            }

            //Debug.Log(cdRunTimer.ToString()+","+cdCoolTimer.ToString());

            CDTimer();



            InputControl();


            CDRunTimer();
           
            if(cdRunTimer>0& isRunning)
            {
                Run();

            }

            if (cdRunTimer.Equals(0))
            {
                isRunning = false;
            }



            //if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            //{

            //    Run();
            //    //isRunning = true;
            //    animator.SetFloat("isRunning", rg.velocity.z);
            //}


            animator.SetBool("isGrounded", isGrounded);

            animator.SetBool("isWalking",isWalking);

            animator.SetBool("isRunning", isRunning);
        }
     
        //InputControl();
        //animator.SetBool("isGrounded", isGrounded);




    }


    public void Pause()
    {
        isPaused = true;

    }

    public void Resume()
    {
        isPaused = false;
    }

    void ReStart() { }

   


    void MoveForward()
    {
        float h = transform.position.z;
        float v =1.0f;

        //Move()
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * walk_speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        rg.MovePosition(transform.position + movement);
    }

    void MoveBackward()
    {
        float h = transform.position.z;
        float v = -1.0f;

        //Move()
        movement.Set(h, 0f, v);
        
       

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * walk_speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        rg.MovePosition(transform.position + movement);
    }
    void TurnLeft()
    {

    }


    private void FixedUpdate()

{






    }


    //private void FixedUpdate()
    //{

    //}

    public LayerMask groundLayer;
    //bool IsGrounded()
    //{

    //    //if (Physics.Raycast(this.transform.position, Vector3.down, 6.0f, groundLayer.value))
    //    if (Physics.Raycast(this.transform.position, Vector3.down, 0.1f))
    //    {

    //        return true;
    //    }
    //    Debug.Log("notGrounded");
    //    return false;
    //}
    public bool isAttacked = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            Debug.Log("Grounded");
            isGrounded = true;
        }
        //if (collision.gameObject.tag == "Zombie")
        //{
        //    Debug.Log("isAttacked:"+isAttacked);
        //    isAttacked = true;
        //}

        //if (collision.gameObject.tag == "")
        //{

        //}
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {

            Debug.Log("notGrounded");
            isGrounded =false;
        }

        //if (collision.gameObject.tag == "Zombie")
        //{
        //    isAttacked = false;
        //    Debug.Log("isAttacked:" + isAttacked);
        //}

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Zombie")
        {
            //isAttacked = false;
            Debug.Log("Ontriggerenter:" + isAttacked);
            //attackTimer.coolDownTimer = 0f;
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Zombie" && attackTimer.coolDownTimer.Equals(0))
        {
            isAttacked = true;
            Debug.Log("Ontriggerenter:" + isAttacked);
            attackTimer.StartCD();
           
        }
    }
    //void OnCollisionStay()
    //{
    //    isGrounded = true;
    //}

    void Jump()
    {

        //animation.Play("jump_pose"); 
        if (isGrounded)
        {


            //animator.SetBool("jump", true);
            rg.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("jump");
            //animator.SetBool("jump", true);
        }
    }


        private void Run()
    {

        if (rg.velocity.z < run_speed)
        {
       
            rg.velocity = transform.forward * run_speed;
       
        }
    }
    
    private void InputControl()
    {


        //if (isGrounded)
        //{
        ////moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        ////float speed = walk_speed;

        //if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        //{
        //    isWalking = true;
        //    moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        //    //moveDirection.y -= gravity * Time.deltaTime;
        //    moveDirection = transform.TransformDirection(moveDirection);
        //    moveDirection *= walk_speed;

        //    //controller.Move(moveDirection * Time.deltaTime);
        //}
        //else
        //{
        //    isWalking = false;
        //}

        //if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        //{
        //    isRunning = true;

        //    cdRunTimer = cdRun;

        //    //speed = run_speed;


        //    //isWalking = false;
        //}

        //if (cdRunTimer > 0)
        //{
        //    //moveDirection = new Vector3(0, 0, 1);
        //    ////moveDirection.y -= gravity * Time.deltaTime;
        //    //moveDirection = transform.TransformDirection(moveDirection);
        //    //moveDirection *= run_speed;

        //    //controller.Move(moveDirection * Time.deltaTime);

        //}

        //if (isWalking)
        //{

        //}

        //moveDirection.y -= gravity * Time.deltaTime;
        ////moveDirection = transform.TransformDirection(moveDirection);
        ////moveDirection *= walk_speed;
        //controller.Move(moveDirection * Time.deltaTime);


        ////if (cdRunTimer.Equals(0))
        ////{
        ////    isRunning = false;
        ////}
        ////else
        ////{
        ////    isRunning = false;
        ////}

        ////Debug.Log("vertical" + Input.GetAxis("Vertical").ToString());


        //if (Input.GetKey(KeyCode.Q))
        //{
        //    moveDirection.y = jumpForce;
        //}

        ////transform.right = Vector3.Slerp(transform.right, -Vector3.right * Input.GetAxis("Horizontal"), 0.01f);


        ////this.transform.Rotate(new Vector3(0, Input.GetAxisRaw("Horizontal") * 0.01f * Time.deltaTime, 0));
        /// 
        /// 
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {

                //transform.Rotate(-Vector3.up * speed * Time.deltaTime);
                Quaternion deltaRotation = Quaternion.Euler(-m_EulerAngleVelocity * Time.deltaTime);
                rg.MoveRotation(rg.rotation * deltaRotation);



            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {

                Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
                rg.MoveRotation(rg.rotation * deltaRotation);




                //transform.Rotate(Vector3.up * speed * Time.deltaTime);



            }


            if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                isWalking = true;
                transform.Translate(Vector3.forward*Input.GetAxis("Vertical") * walk_speed * Time.deltaTime);


            }
            else { isWalking = false; 
            
            }


            if (Input.GetKey(KeyCode.Q))
            {
                Jump();
            }

            if(cdCoolTimer.Equals(0) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                //Run();
                isRunning = true;
                cdCoolTimer = cdCool;
                cdRunTimer = cdRun;

            }



        }










    }





    float cdRun = 10.0f;
    float cdRunTimer;
    void CDRunTimer()
    {
        if (cdRunTimer > 0)
        {
            cdRunTimer -= Time.deltaTime;
        }

        if (cdRunTimer < 0)
        {
            cdRunTimer = 0;
        }

    }

    float cdCool = 60.0f;
    float cdCoolTimer;
    void CDTimer()
    {
        if (cdCoolTimer > 0)
        {
            cdCoolTimer -= Time.deltaTime;
        }

        if (cdCoolTimer < 0)
        {
            cdCoolTimer = 0;
        }

    }



    //private float seconds = 0.0f;
    //private float minutes = 0.0f;
    //private float hours = 0.0f;
    ////private float timer = 0;

    //public void TimeRestart()
    //{
    //    seconds = 0.0f;
    //    minutes = 0.0f;
    //    hours = 0.0f;
    //}




    //public void TimeUp()
    //{
    //    seconds += Time.deltaTime;
    //    if (seconds > 60)
    //    {
    //        minutes += 1;
    //        seconds = 0;
    //    }
    //    if (minutes > 60)
    //    {
    //        hours += 1;
    //        minutes = 0;
    //    }
    //    string min = minutes.ToString();
    //    string sec = seconds.ToString();

    //    if (minutes < 10)
    //    {
    //        min = "0" + min;
    //    }

    //    sec = sec.Split('.')[0];
    //    if (seconds < 10)
    //    {
    //        sec = "0" + sec;
    //    }


    //    //print("Hours: " + hours + " " + "Minutes: " + minutes + " " + "Seconds" + seconds);
    //}
}
