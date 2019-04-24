using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ReStartTrigger : MonoBehaviour
{
    private Scene scene;
    // Use this for initialization
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //private void OnTriggerEnter(Collider other)
    //{
        
    //}

   public void bClick()
    {

        
        SceneManager.LoadScene(scene.name);
        
        //Application.LoadScene(scene.name);
    }
}
