using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Establish player fields
    [SerializeField] float cash;
    [SerializeField] float goldBars;
    [SerializeField] float shotDelay;
    [SerializeField] float shotClock;
    [SerializeField] float shotAccuracy;
    [SerializeField] float score;
    [SerializeField] float timeRemaining;
    [SerializeField] float shotsTaken;
    [SerializeField] float shotsHit;
    [SerializeField] float accuracy;
    [SerializeField] int ammo;
    [SerializeField] int maxAmmo;
    [SerializeField] Image reddickle;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject manager;
    [SerializeField] float recoilCrossHairChange;
    [SerializeField] AudioSource soundEffects;
    [SerializeField] AudioClip[] sounds;
    [SerializeField] int targetsHitCount;
    [SerializeField] int targetsSeenCount;
    [SerializeField] bool hardMode;
    Ray raycast;
    public Text scoreText;
    public Text accuracyText;
    public Text ammoCount;
    public Text timeText;
    public Text gameOverText;
    public Text targetsHitText;
    public Text targetsSeenText;



    // Start is called before the first frame update
    void Start()
    {
        soundEffects.clip = sounds[0];
        soundEffects.Play();
        maxAmmo = 6;
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining > 0)
        {
            timeText.text = "Time Remaining: " + timeRemaining;
            ammoCount.text = "Ammo: " + ammo;
            if(shotsTaken > 0)
            {
                accuracy = (shotsHit / shotsTaken) * 100.0f;
                accuracyText.text = "Accuracy: " + accuracy + "%";
            }
            targetsHitText.text = "Targets Hit: " + shotsHit;
            targetsSeenCount = manager.GetComponent<EnemyManager>().targetsSpawned;
            targetsSeenText.text = "Targets Seen: " + targetsSeenCount;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            //reddickle.transform.position = worldMousePos;
            if (hardMode)
            {
                crosshair.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            }
            else
            {
                crosshair.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f); // https://www.cleanpng.com/png-reticle-clip-art-crosshair-89104/download-png.html

            }
            crosshair.transform.position = worldMousePos;
            //reddickle.transform.position = Camera.main.WorldToScreenPoint(Input.mousePosition);
            //crosshair.transform.position = Camera.main.WorldToScreenPoint(Input.mousePosition);
            shotClock += Time.deltaTime;
            scoreText.text = "Score: " + score;
            if (Input.GetMouseButtonDown(0))
            {
                if (ammo > 0)
                {
                    if (shotClock > shotDelay)
                    {
                        Shoot();
                        ammo--;
                        shotClock = 0;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
            if(Input.GetKeyDown(KeyCode.H))
            {
                hardMode = !hardMode;
            }
        }
        else
        {
            gameOverText.text = "GAME OVER";
            foreach(GameObject enemy in manager.GetComponent<EnemyManager>().staticEnemies)
            {
                enemy.SetActive(false);
            }
        }

    }

    void Shoot()
    {
        Debug.Log("TEST");
        Vector3 tempMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("PRERNG " + tempMousePos);
        //raycast = Camera.main.ScreenPointToRay(tempMousePos);
        //Debug.Log(raycast);

        if (hardMode)
        {
            float rng = Random.Range(0.7f,shotAccuracy);
            tempMousePos.x *= rng;
            rng = Random.Range(0.7f, shotAccuracy);
            tempMousePos.y *= rng;
            rng = Random.Range(0.7f, shotAccuracy);
            tempMousePos.z *= rng;
            Debug.Log(Input.mousePosition);
            Debug.Log("randomed" + tempMousePos);
        }
        //float rng = Random.Range(0.0f,shotAccuracy);
        //tempMousePos.x *= rng;
        //rng = Random.Range(0.0f, shotAccuracy);
        //tempMousePos.y *= rng;
        //rng = Random.Range(0.0f, shotAccuracy);
        //tempMousePos.z *= rng;
        //Debug.Log(Input.mousePosition);
        //Debug.Log("randomed" + tempMousePos);
        shotsTaken++;
        //raycast = Camera.main.ScreenPointToRay(tempMousePos);
        foreach (GameObject enemy in manager.GetComponent<EnemyManager>().staticEnemies)
        {
            if (enemy.activeSelf)
            {
                Debug.Log(enemy.name);
                Debug.Log("max" + enemy.GetComponent<BoxCollider2D>().bounds.max);
                Debug.Log("min" + enemy.GetComponent<BoxCollider2D>().bounds.min);
                Debug.Log("TempX " + tempMousePos.x);
                Debug.Log("TempY " + tempMousePos.y);
                if ((enemy.GetComponent<BoxCollider2D>().bounds.max.x > tempMousePos.x) && (enemy.GetComponent<BoxCollider2D>().bounds.min.x < tempMousePos.x))
                {
                    if ((enemy.GetComponent<BoxCollider2D>().bounds.max.y > tempMousePos.y) && (enemy.GetComponent<BoxCollider2D>().bounds.min.y < tempMousePos.y)){
                        enemy.SetActive(false);
                        score += 100.0f;
                        manager.GetComponent<EnemyManager>().activeStatic--;
                        enemy.GetComponent<Transform>().localScale = Vector3.one;
                        shotsHit++;
                    }
                }
            }
        }

        //Debug.Log(raycast);
        //Debug.Log(Physics.Raycast(raycast, out RaycastHit hit));
        //RaycastHit2D hit2D = Physics2D.Raycast(tempMousePos,Vector2.;
        //if (Physics.Raycast(raycast, out RaycastHit hit2))
        //{
        //    GameObject hitEnemy = hit2.collider.gameObject;
        //    Debug.Log(hitEnemy);
        //    if(hitEnemy != null )
        //    {
        //        if (hitEnemy.tag.Equals("Enemy"))
        //        {
        //            if (hitEnemy.GetComponent<StaticEnemy>() != null)
        //            {
        //                hitEnemy.SetActive(false);
        //                score += 100.0f;
        //                manager.GetComponent<EnemyManager>().activeStatic--;
        //            }
        //        }
        //    }
        //}
        // Play sound
        soundEffects.clip = sounds[1];
        soundEffects.Play();
    }
    void rapidFireShot()
    {

    }

    void Reload()
    {
        Debug.Log("RELOADED");
        ammo = maxAmmo;
        soundEffects.clip = sounds[2];
        soundEffects.Play();
    }
}
