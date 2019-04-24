using UnityEngine;
using System.Collections;

public class CoolDownTimer : MonoBehaviour
{

    public float coolDown;
    public float coolDownTimer;
    public int t;
    
    // Use this for initialization
    void Start()
    {

    }

    public void StartCD()
    {
        coolDownTimer = coolDown;
    }
    bool isPaused = false;

    public void Pause()
    {
        isPaused = true;

    }

    public void Resume()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isPaused == true)
        //{
        //    return;
        //}

        //if (isPaused == false)
        //{
            if (coolDownTimer > 0)
            {
                coolDownTimer -= Time.deltaTime;
            }

            if (coolDownTimer < 0)
            {
                coolDownTimer = 0;
            }
        //}
       
    }
}
