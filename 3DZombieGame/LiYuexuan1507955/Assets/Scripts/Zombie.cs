

using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zombie : MonoBehaviour
{
    float speed = 0.01f;

    float speedCap = 3.0f;
    public Animator animator;
   

    //public Player player;

    Transform playerTarget;

    List<Transform> activatedBases = new List<Transform>();

    NavMeshAgent agent;

  

    Transform currentTarget;

    int lastCount = 1;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {


        agent = GetComponent<NavMeshAgent>();
        //agent.enabled = true;

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        currentTarget = playerTarget;

        //animator.SetBool("walk", false);   
    }

    void SlowByMoreActivated()
    {

        if (activatedBases.Count <= lastCount)
        {
            lastCount = activatedBases.Count;
            

        }

        if(activatedBases.Count > lastCount)
        {//slow down

            agent.speed = agent.speed * 0.5f;
            lastCount = activatedBases.Count;
        }
    }

    public void SlowDown()
    {

        //Debug.Log("SLowdown"+ agent.speed.ToString());
        agent.speed = agent.speed * 0.9f;
    }

    public void Pause()
    {
        isPaused = true;
        //agent.enabled = false;
        agent.isStopped = false;
    }

    public void Resume()
    {
        isPaused = false;
        //agent.enabled = true;

        agent.isStopped = true;
    }

    void ReStart() { }


    // Update is called once per frame
    void Update()
    {
        //if (isPaused == true)
        //{
        //    return;
        //}

        if (isPaused == false)
        {
            //Debug.Log("zombie speed: " + agent.speed.ToString());
            //Debug.Log("current target: " + currentTarget.tag);
            //activatedBases = new List<Transform>();
            //activatedBases.Add(playerTarget);
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Activated");
            foreach (GameObject go in gameObjects)
            {
                activatedBases.Add(go.transform);
            }

            //CheckDisappeared();
            CheckReached();

            Debug.Log(activatedBases.Count.ToString());

            FindClosest();


            animator.SetTrigger("StopAttack");
            agent.SetDestination(currentTarget.position);
            StartCoroutine("IncreaseSpeedPerSecond", 10.0f);
        }
        //else
        //{
        //    return;
        //}

        //Debug.Log("mindist: "+currentTarget.position.ToString());
        //agent.destination = player.transform.position;


        //StartCoroutine("IncreaseSpeedPerSecond", 10.0f);
    }

    IEnumerator IncreaseSpeedPerSecond(float waitTime)
    {
        //while agent's speed is less than the speedCap
        while (agent.speed < speedCap)
        {
            //wait "waitTime"
            yield return new WaitForSeconds(waitTime);
            //add 0.5f to currentSpeed every loop 
            agent.speed = agent.speed + 0.001f;
        }
    }





    void CheckReached()
    {

        float minDist = Vector3.Distance(currentTarget.position, transform.position);

            Debug.Log(minDist.ToString());
        if (minDist<1.0f) {

            //if(activatedBases.IndexOf(currentTarget) > 0)
            //{
            if (activatedBases.Contains(currentTarget))
            {
                Debug.Log("reached, base");
                SlowDown();
                //}
                activatedBases.Remove(currentTarget);
            }
            else
            {
                playerKilled = true;
                animator.SetTrigger("Attack");
                Debug.Log("reached, player");
            }

        }

        

        currentTarget = playerTarget;

      



    }

    void CheckDisappeared()
    {

        int idx = 0;
        foreach (Transform tf in activatedBases)
        {
            if(tf.tag=="Disappear")
            {
                idx = activatedBases.IndexOf(tf);
            }
        }

        if (idx> 0)
            {
                activatedBases.RemoveAt(idx);
            }
    }

   public bool playerKilled = false;

    

    //void Pause()
    //{
    //    agent.enabled = false;
    //}


    void FindClosest()
    {
        if (activatedBases.Count != 0)
        {
            float minDist = Vector3.Distance(activatedBases[0].position, transform.position);


            foreach (Transform tf in activatedBases)
            {


                float current = Vector3.Distance(tf.position, transform.position);
                if (minDist >= current)
                {
                    minDist = current;
                    currentTarget = tf;

                }


            }
        }
        else
        {
            currentTarget = playerTarget;
        }





    }

    private void FixedUpdate()
    {
        //TimeUp();
        //if (seconds >= 5.0f)
        //{
        //    //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //    animator.SetBool("walk", false);
        //    TimeRestart();
        //}
        //else
        //{
        //    animator.SetBool("walk", true);
        //}
    }

    private float seconds = 0.0f;
    private float minutes = 0.0f;
    private float hours = 0.0f;
    //private float timer = 0;

    public void TimeRestart()
    {
        seconds = 0.0f;
        minutes = 0.0f;
        hours = 0.0f;
    }

    public void TimeUp()
    {
        seconds += Time.deltaTime;
        if (seconds > 60)
        {
            minutes += 1;
            seconds = 0;
        }
        if (minutes > 60)
        {
            hours += 1;
            minutes = 0;
        }
        string min = minutes.ToString();
        string sec = seconds.ToString();

        if (minutes < 10)
        {
            min = "0" + min;
        }

        sec = sec.Split('.')[0];
        if (seconds < 10)
        {
            sec = "0" + sec;
        }


        //print("Hours: " + hours + " " + "Minutes: " + minutes + " " + "Seconds" + seconds);
    }

   


}
