using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatentCreature : MonoBehaviour
{

    public Zombie zombie;
    GameObject cube;
    float CREATE_TIME;
    float SPAWN_TIME = 3.0f;

    public Material[] cubeMaterials;
    Renderer renderer;

    public bool activated = false;
   
    BoxCollider lc;

    public bool spawnCreature = false;

    List<GameObject> zombieList = new List<GameObject>();




    // Start is called before the first frame update
    void Start()
    {
        //zombie = transform.Find("Zombie").gameObject;
        //zombie.SetActive(false);

        this.tag = "Untagged";


        cube = transform.Find("Cube").gameObject;
        //col = transform.Find("Box Collider").gameObject;
        //cube.SetActive(false);
        //lc = transform.Find("LatentCreature").gameObject;

        lc = GetComponent<BoxCollider>();
        //renderer = GetComponent<Renderer>();
        renderer = cube.GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = cubeMaterials[0];

      


        //cdCoolTimer = 0;



    }

 
    private void OnTriggerStay(Collider other)
    {
       
        //if (other.tag == "Player" && activated == false)
        //{
        //    renderer.sharedMaterial = cubeMaterials[1];

        //    activated = true;
        //    this.tag = "Activated";

        //    //other.GetComponentInParent<Zombie>().SlowDown();
        //    //foreach(GameObject go in zombieList)
        //    //{
        //    //go.GetComponent<Zombie>().SlowDown();

        //    //}



        //}



    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.tag);

        if (other.tag == "Player" && activated == false)
        {
            renderer.sharedMaterial = cubeMaterials[1];

            activated = true;
            this.tag = "Activated";

            //other.GetComponentInParent<Zombie>().SlowDown();
            //foreach(GameObject go in zombieList)
            //{
            //go.GetComponent<Zombie>().SlowDown();

            //}



        }

        if (other.tag == "Zombie" && activated == true)
        {
            //Debug.Log("zombie touch base");
            //Instantiate(zombie);
            //zombie.transform.position = transform.position;

            this.tag = "Disappear";
            //cube.SetActive(false);

            activated = false;
            spawnCreature = true;

            //lc.enabled = false;
            Debug.Log("activated"+activated);
            //cdCoolTimer = cdCool;
        }

    }


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Zombie" && activated == true)
    //    {
    //        if (this.tag == "Disappear")
    //        {
    //            //Instantiate(zombie);
    //            //zombie.transform.position = transform.position;

    //        }
    //    }
    //}

    

    // Update is called once per frame
    void Update()
    {

        zombieList = new List<GameObject>();

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject go in gameObjects)
        {
            //zombieList.Add(go);

            zombieList.Add(go);

            //go.GetComponent<Zombie>().SlowDown();

        
        }

        //CDTimer();

        //if(activated == false)
        //{
        //    renderer.sharedMaterial = cubeMaterials[0];
        //}




        //if (cdCoolTimer.Equals(0)&& this.tag=="Disappear")
        //{
        //    cube.SetActive(true);

        //    this.tag = "Untagged";
        //    //this.tag = "Untagged";
        //    //spawnCreature = false;

        //}


      

    }
    public void Initialize(Vector3 pos)
    {
        //renderer.sharedMaterial = cubeMaterials[1];
        //this.tag = "Activated";

        transform.position =pos;
    }




    void ChangePos()
    {
        int x = Random.Range(40, 180);
        int z = Random.Range(40, 180);

        transform.position = new Vector3(x, 0, z);
    }


}
