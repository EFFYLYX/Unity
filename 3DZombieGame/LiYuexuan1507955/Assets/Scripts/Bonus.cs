using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{

    GameObject gameObject;
    float REPAWN_TIME = 2.0f;
    // Start is called before the first frame update
    public bool isDisappear = false;
    public Material[] materials;
    Renderer renderer;
    public Mesh[] meshes;
    MeshFilter meshFilter;

    float BONUS_HEIGHT = 2.0f;



    public Vector3 GetPos()
    {
        return transform.position;
    }


    void Start()
    {

       
        gameObject = transform.Find("icecream").gameObject;
        renderer = gameObject.GetComponent<Renderer>();
        meshFilter = gameObject.GetComponent<MeshFilter>();
        renderer.enabled = true;

        ChangeShape();
    }
    void Pause()
    {


    }

    void Resume()
    {

    }

    void ReStart() { }



    void ChangeShape()
    {
        int index = Random.Range(0, meshes.Length);
       

        meshFilter.sharedMesh = meshes[index];

        renderer.sharedMaterial = materials[index];
    }

    // Update is called once per frame
    void Update()
    {


        Work();

    }


    void Work() {
        transform.Rotate(Time.deltaTime, Time.deltaTime, Time.deltaTime);

        //if (isDisappear == true)
        //{
        //    TimeUp();
        //}

        //if (minutes >= REPAWN_TIME)
        //{
        //    TimeRestart();
        //    gameObject.SetActive(true);
        //    ChangePos();
        //    ChangeShape();
        //    isDisappear = false;
        //}
    }



    private void OnTriggerStay(Collider other)
    {

        isDisappear = true;
        //Debug.Log("tigger");
        //if(isDisappear == false)
        //{
          

        //    gameObject.SetActive(false);
        //    isDisappear = true;
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

    public void Initialize(Vector3 pos)
    {

        //int x = Random.Range(40, 180);
        //int z = Random.Range(40, 180);
        transform.position =pos;
    }

    void ChangePos()
    {
        int x = Random.Range(40, 180);
        int z = Random.Range(40, 180);

        transform.position = new Vector3(x, BONUS_HEIGHT, z);
    }



}
