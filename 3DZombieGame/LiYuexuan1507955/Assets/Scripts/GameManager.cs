using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //public Bonus[] bonus_arr = new Bonus[6];
    public Bonus bonus;
    int NUM_BONUS = 15;
    float CDBONUS = 120.0f;
    float BONUS_HEIGHT = 2.0f;
    List<Bonus> bonusList = new List<Bonus>();

    public Zombie initialZombie;
    public LatentCreature latentCreature;
    int NUM_BASE = 20;
    float CDBASE = 180.0f;
    List<LatentCreature> latentCreatureList = new List<LatentCreature>();
    List<Zombie> zombieList = new List<Zombie>();


    public Player player;
    bool isPaused = false;
    public int score = -1;
    public int num_lives = 4;
    int delta_score = 1;
    float DOUBLETIME = 120.0f;
    float SCORETIME = 10.0f;

    public CoolDownTimer coolDownTimer;
    List<CoolDownTimer> cdTimerList = new List<CoolDownTimer>();


    Text text_lives;
    Text text_scores;
    Text text_enemies;
    CoolDownTimer CD_deltaScore;
    CoolDownTimer CD_score;
    Text text_time;

    private RectTransform MSG;
    Text text_fail;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 1;
        //player = transform.Find("Player").gameObject;
       
        
        Vector3 pos = new Vector3(100, 0, 100);
      
        initialZombie.transform.position = pos;
        Instantiate(initialZombie);


        zombieList.Add(initialZombie);

        SpawnBonus();
        SpawnBase();

        CD_deltaScore = Instantiate(coolDownTimer);
        CD_deltaScore.coolDown = DOUBLETIME;

        CD_deltaScore.StartCD();

        CD_score = Instantiate(coolDownTimer);
        CD_score.coolDown = SCORETIME;
        CD_score.StartCD();



        MSG = transform.Find("Canvas/MSG") as RectTransform;
        //MSG.gameObject.SetActive(false);
        text_fail = transform.Find("Canvas/MSG/Text").GetComponent<Text>();
        text_lives = transform.Find("Canvas/HUD/Left/Text").GetComponent<Text>();
        text_scores = transform.Find("Canvas/HUD/Center/Text").GetComponent<Text>();
        text_enemies = transform.Find("Canvas/HUD/Right/Text").GetComponent<Text>();
        text_time = transform.Find("Canvas/HUD/LeftColumn/Timer/Text").GetComponent<Text>();



    }

    public void RestartCurrentScene()
    {

        Debug.Log("Restart");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void DestroyAll()
    {
        //player.Pause();
        //player.enabled = false;
        //bonus.enabled = false;
        //latentCreature.enabled = false;
        foreach (Zombie z in zombieList)
        {
            //z.Pause();
            //z.enabled = false;
        }

        foreach (Bonus b in bonusList)
        {
            b.enabled = false;
        }

        foreach (LatentCreature l in latentCreatureList)
        {
            //l.enabled = false;
        }

        foreach (CoolDownTimer cdt in cdTimerList)
        {
            //cdt.Pause();
        }


        //CD_score.Pause();
        //CD_deltaScore.Pause();

    }

    // Update is called once per frame
    void Update()
    {
        //if (isPaused == false)
        //{
            CheckLive();

            SpawnZombie();
            SpawnNewBase();
            RemoveBonus();
            SpawnNewBonus();
            TimeUp();
            UpdateUI();

            if (CD_score.coolDownTimer.Equals(0))
            {
                score += delta_score;
                CD_score.StartCD();
            }

            if (CD_deltaScore.coolDownTimer.Equals(0))
            {
                delta_score *= 2;
                CD_deltaScore.StartCD();
            }



        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R");
            RestartCurrentScene();
        }
        //Pause();
        //}

        //if (Input.GetKeyDown(KeyCode.Space))

    }


    void UpdateUI()
    {
        text_lives.text = num_lives.ToString();
        text_scores.text = score.ToString();
        text_enemies.text = zombieList.Count.ToString();
        text_time.text =  min + ":" + sec;
    }

    void Resume()
    {
        player.Resume();
        player.enabled = true;
        //bonus.enabled = false;
        //latentCreature.enabled = false;
        //foreach(Zombie z in zombieList)
        //{
        //    z.Resume();
        //}
        foreach (Bonus b in bonusList)
        {
            b.enabled = true;
        }

        foreach (LatentCreature l in latentCreatureList)
        {
            l.enabled = true;
        }

        foreach (CoolDownTimer cdt in cdTimerList)
        {
            cdt.Resume();
        }

        CD_score.Resume();
        CD_deltaScore.Resume();


        //isPaused = false;
    }
    void Pause()
    {
        player.Pause();
        player.enabled = false;
        //bonus.enabled = false;
        //latentCreature.enabled = false;
        foreach (Zombie z in zombieList)
        {
            z.Pause();
            //z.enabled = false;
        }

        foreach (Bonus b in bonusList)
        {
            b.enabled = false;
        }

        foreach (LatentCreature l in latentCreatureList)
        {
            l.enabled = false;
        }

        foreach(CoolDownTimer cdt in cdTimerList)
        {
            cdt.Pause();
        }


        CD_score.Pause();
        CD_deltaScore.Pause();
        //isPaused = true;
    }

    void CheckLive()
    {

        if (player.isAttacked && num_lives>0)
        {
            num_lives -= 1;
            player.isAttacked = false;
        }

        if (num_lives == 0)
        {
            MSG.gameObject.SetActive(true);

            if (score < 50) { text_fail.text = "The zombies, don’t think you fought brave enough, you won’t be joining their ranks";
                text_fail.color = Color.red;
            
            }
            if (score >= 50)
            {
                text_fail.text ="The zombies, admired your futile efforts, you will be joining their ranks." ;
                text_fail.color = new Color(255, 215, 0);
            }


            }
        //foreach(Zombie z in zombieList)
        //{
        //    if (z.playerKilled == true)
        //    {
        //        num_lives--;
        //        z.playerKilled = false;
        //    }
        //}

    }


    void SpawnBonus()
    {


        for(int i = 0; i<NUM_BONUS; i ++)
        {

            int x= Random.Range(40, 180);
            int z = Random.Range(40, 180);


            Bonus b = Instantiate(bonus);

            //Instantiate(bonus);
            //bonus.Initialize(new Vector3(x, BONUS_HEIGHT, z));
            b.Initialize(new Vector3(x, BONUS_HEIGHT, z));

            bonusList.Add(b);

        }


      
    }

    void RemoveBonus()
    {
        int idx = -1;
        foreach (Bonus b in bonusList)
        {
            if (b.isDisappear == true)
            {
                idx = bonusList.IndexOf(b);
                Destroy(b.gameObject);

                score += 10;
            }
        }

        if (idx >= 0)
        {
            bonusList.RemoveAt(idx);
            //cdBaseCoolTimer = cdBaseCool;
            CoolDownTimer cdt = Instantiate(coolDownTimer);

            cdt.coolDown = CDBONUS;
            cdt.t = 1;
            //cdt.t = "base";
            //cdt.coolDownTimer = 20.0f;
            cdt.StartCD();
            //cdt.type = "base";
            cdTimerList.Add(cdt);
        }
    }

    void SpawnNewBonus() {
        int idx = -1;

        foreach (CoolDownTimer cdt in cdTimerList)
        {
            if (cdt.t == 1)
            {
                if (cdt.coolDownTimer.Equals(0))
                {
                    int x = Random.Range(40, 180);
                    int z = Random.Range(40, 180);
                    Bonus b = Instantiate(bonus);

                    //Instantiate(bonus);
                    //bonus.Initialize(new Vector3(x, BONUS_HEIGHT, z));
                    b.Initialize(new Vector3(x, BONUS_HEIGHT, z));

                    bonusList.Add(b);


                    idx = cdTimerList.IndexOf(cdt);


                    Destroy(cdt.gameObject);
                }
            }

        }

        if (idx >= 0)
        {
            //Destroy(base_cdTimer[idx].gameObject);
            cdTimerList.RemoveAt(idx);
        }
    }




    void SpawnBase()
    {
        for (int i = 0; i < NUM_BASE; i++)
        {
            int x = Random.Range(40, 180);
            int z = Random.Range(40, 180);

            LatentCreature c =   Instantiate(latentCreature);

            //Instantiate(latentCreature);
            //latentCreature.Initialize(new Vector3(x, 0, z));
            c.Initialize(new Vector3(x, 0, z));

            latentCreatureList.Add(c);
        }
    }


    bool needNewBase = false;
    void SpawnZombie()
    {
        int idx = -1;
        foreach(LatentCreature c in latentCreatureList)
        {
            GameObject cube = c.transform.Find("Cube").gameObject;
            if (c.spawnCreature == true)
            {
                Zombie z = Instantiate(initialZombie);

                z.transform.position = c.transform.position;

                zombieList.Add(z);
                c.spawnCreature = false;

                idx = latentCreatureList.IndexOf(c);

                Destroy(c.gameObject);


            }
        }

        if (idx >= 0)
        {
            latentCreatureList.RemoveAt(idx);

            CoolDownTimer cdt = Instantiate(coolDownTimer);
            
            cdt.coolDown = CDBASE;
            cdt.t = 0;
           
            cdt.StartCD();
          
            cdTimerList.Add(cdt);
           
        }
       




    }

    void SpawnNewBase()
    {
        int idx = -1;
        foreach(CoolDownTimer cdt in cdTimerList)
        {
            if (cdt.t==0)
            {
                if (cdt.coolDownTimer.Equals(0))
                {
                    int x = Random.Range(40, 180);
                    int z = Random.Range(40, 180);

                    LatentCreature c = Instantiate(latentCreature);

                   
                    c.Initialize(new Vector3(x, 0, z));

                    latentCreatureList.Add(c);
                    idx = cdTimerList.IndexOf(cdt);


                    Destroy(cdt.gameObject);
                }
            }

        }

        if (idx >= 0)
        {
           
            cdTimerList.RemoveAt(idx);
        }


    }



    private float seconds = 0.0f;
    private float minutes = 0.0f;
    private float hours = 0.0f;

    string sec;
    string min;
    private float num_seconds = 0.0f;
    //private float timer = 0;

    public void TimeRestart()
    {
        seconds = 0.0f;
        minutes = 0.0f;
        hours = 0.0f;
    }




    public void TimeUp()
    {

       
        num_seconds += Time.deltaTime;
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

        min = minutes.ToString();
        sec = seconds.ToString();

        if (minutes < 10)
        {
            min = "0" + min;
        }

        sec = sec.Split('.')[0];
        if (seconds < 10)
        {
            sec = "0" + sec;
        }

        Debug.Log(sec);


        //if (!num_seconds.Equals(0))
        //{
        //    //int s = (int)System.Math.Round(num_seconds, 0);
        //    int s = int.Parse(num_seconds.ToString().Split('.')[0]);
        //    string ss = s.ToString();
        //    if(s>=120&& ss.EndsWith("0", System.StringComparison.Ordinal))
        //    {
        //        delta_score *= 2;

        //    }
        //    //if (s >=120 && s % DOUBLETIME == 0)
        //    //{
        //    //    delta_score *= 2;
        //    //}
        //}

        //if (!num_seconds.Equals(0))
        //{
        //    int s = int.Parse(num_seconds.ToString().Split('.')[0]);
        //    string ss = s.ToString();

        //    Debug.Log(ss);
        //    //if (s >= 10 && ss.EndsWith("0", System.StringComparison.Ordinal))
        //    //{
        //    //    score+=delta_score;

        //    //}
        //    //if (s>=10 && s % 10 == 0)
        //    //{
        //    //    score += delta_score;
        //    //}
        //}

        //if (!min.Equals("00"))
        //{
        //    if (sec[-1].Equals( "0"))
        //    {
        //        score += delta_score;
        //    }
        //}
        //else
        //{
        //if (sec[-1].Equals("0") && !sec[0].Equals("0"))
        //{
        //    score += delta_score;
        //}
        //}

        //if (sec[2].Equals("0"))
        //{
        //score += delta_score;
        //}


        //print("Hours: " + hours + " " + "Minutes: " + minutes + " " + "Seconds" + seconds);
    }




}

