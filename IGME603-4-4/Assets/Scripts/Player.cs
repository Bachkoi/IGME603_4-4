using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Establish player fields
    [SerializeField] public float cash;
    [SerializeField] float goldBars;
    [SerializeField] public float shotDelay;
    [SerializeField] float shotClock;
    [SerializeField] public float shotAccuracy;
    [SerializeField] float score;
    [SerializeField] float timeRemaining;
    [SerializeField] float shotsTaken;
    [SerializeField] float shotsHit;
    [SerializeField] float accuracy;
    [SerializeField] int ammo;
    [SerializeField] public int maxAmmo;
    [SerializeField] Image reddickle;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject manager;
    [SerializeField] float recoilCrossHairChange;
    [SerializeField] AudioSource soundEffects;
    [SerializeField] AudioClip[] sounds;
    [SerializeField] int targetsHitCount;
    [SerializeField] int targetsSeenCount;
    [SerializeField] bool hardMode;
    [SerializeField] GameObject[] bulletHoles;
    [SerializeField] GameObject[] sixShotReload;
    [SerializeField] GameObject[] eightShotReload;
    [SerializeField] GameObject[] twelveShotReload;
    [SerializeField] Button shopButton;
    [SerializeField] Button restartButton;
    [SerializeField] bool paused;
    [SerializeField] Button[] shopButtons;
    [SerializeField] Text[] shopText;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject obstaclesHolder;
    [SerializeField] GameObject revolver;
    public float fireRatePrice = 100.0f;
    public float accuracyPrice = 250.0f;
    public float reloadSpeedPrice = 1000.0f;
    public float ammoCountPrice = 1000.0f;
    bool countedCash = false;

    Ray raycast;
    public Text scoreText;
    public Text accuracyText;
    public Text ammoCount;
    public Text timeText;
    public Text gameOverText;
    public Text targetsHitText;
    public Text targetsSeenText;

    public Text cashText;

    public Text playerFireRate;
    public Text playerAmmoCount;
    public Text playerReloadSpeed;
    public Text playerAccuracy;

    public Text upgradeFireRate;
    public Text upgradeAmmoCount;
    public Text upgradeReloadSpeed;
    public Text upgradeAccuracy;




    // Start is called before the first frame update
    void Start()
    {
        soundEffects.clip = sounds[0];
        soundEffects.Play();
        maxAmmo = 6;
        ammo = maxAmmo;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            timeRemaining -= Time.deltaTime;
        }
        if(timeRemaining <= 0)
        {
            timeText.text = "Time Remaining: 0.0";
            if(countedCash == false)
            {
                CalculateCash();
                countedCash = true;
            }

        }
        if (timeRemaining > 0.0f)
        {

            timeText.text = "Time Remaining: " + timeRemaining.ToString("n2");
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
                switch (shotAccuracy)
                {
                    case 0.7f:
                        crosshair.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        break;

                    case 0.8f:
                        crosshair.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                        break;

                    case 0.9f:
                        crosshair.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                        break;

                    case 1.0f:
                        crosshair.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                        break;

                }
                //crosshair.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            }
            else
            {
                //crosshair.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f); // https://www.cleanpng.com/png-reticle-clip-art-crosshair-89104/download-png.html

            }
            crosshair.transform.position = worldMousePos;

            shotClock += Time.deltaTime;
            scoreText.text = "Score: " + score;
            switch (maxAmmo)
            {
                case 6:
                    for (int i = 6; i >= 0; i--)
                    {
                        if (i == ammo)
                        {
                            sixShotReload[i].SetActive(true);
                        }
                        else
                        {
                            sixShotReload[i].SetActive(false);

                        }
                    }
                    break;

                case 8:
                    for (int i = 8; i >= 0; i--)
                    {
                        if (i == ammo)
                        {
                            eightShotReload[i].SetActive(true);
                        }
                        else
                        {
                            eightShotReload[i].SetActive(false);

                        }
                    }
                    break;

                case 12:
                    for (int i = 12; i >= 0; i--)
                    {
                        if (i == ammo)
                        {
                            twelveShotReload[i].SetActive(true);
                        }
                        else
                        {
                            twelveShotReload[i].SetActive(false);

                        }
                    }
                    break;
            }
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
                else
                {
                    gameOverText.text = "PRESS R TO RELOAD";
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
            paused = true;
            gameOverText.text = "GAME OVER, please enter the shop or run it back!";
            cashText.text = "$" + cash;
            foreach (GameObject enemy in manager.GetComponent<EnemyManager>().staticEnemies)
            {
                enemy.SetActive(false);
            }
            manager.GetComponent<EnemyManager>().activeStatic = 0;
            shopButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            restartButton.onClick.AddListener(RestartGame);
            shopButton.onClick.AddListener(OpenShop);
        }

    }

    void RestartGame()
    {
        timeRemaining = 60.0f;
        paused = false;
        restartButton.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);
        gameOverText.text = "";
        accuracy = 0.0f;
        targetsHitCount = 0;
        targetsSeenCount = 0;
        shotsHit = 0.0f;
        shotsTaken = 0.0f;
        score = 0.0f;
        ammo = maxAmmo;
        countedCash = false;
        manager.GetComponent<EnemyManager>().targetsSpawned = 0;

        foreach(GameObject sixShot in sixShotReload)
        {
            sixShot.SetActive(false);
        }
        foreach (GameObject eightShot in eightShotReload)
        {
            eightShot.SetActive(false);

        }
        foreach (GameObject twelveShot in twelveShotReload)
        {
            twelveShot.SetActive(false);
        }
    }

    void Shoot()
    {
        Vector3 tempMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (hardMode)
        {
            //float rng = Random.Range(0.7f,shotAccuracy);
            //tempMousePos.x *= rng;
            //rng = Random.Range(0.7f, shotAccuracy);
            //tempMousePos.y *= rng;
            //rng = Random.Range(0.7f, shotAccuracy);
            //tempMousePos.z *= rng;
            float rng = Random.Range(shotAccuracy, 1.0f);
            tempMousePos.x *= rng;
            rng = Random.Range(shotAccuracy, 1.0f);
            tempMousePos.y *= rng;
            rng = Random.Range(shotAccuracy, 1.0f);
            tempMousePos.z *= rng;
        }
        shotsTaken++;

        int bulletRng = Random.Range(0, bulletHoles.Length);
        Vector3 tempVec3 = new Vector3(tempMousePos.x, tempMousePos.y, 0.0f);
        GameObject testBulletHole = Instantiate(bulletHoles[bulletRng], tempVec3, transform.rotation);
        testBulletHole.SetActive(true);
        testBulletHole.GetComponent<BulletHole>().lifeSpan = 3.0f;
        foreach (GameObject enemy in manager.GetComponent<EnemyManager>().staticEnemies)
        {
            if (enemy.activeSelf)
            {

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
        // Play sound
        soundEffects.clip = sounds[1];
        soundEffects.Play();
    }

    void rapidFireShot()
    {

    }

    void Reload()
    {
        //Debug.Log("RELOADED");
        ammo = maxAmmo;
        soundEffects.clip = sounds[2];
        soundEffects.Play();
        gameOverText.text = "";
    }
    void CalculateCash()
    {
        float tempCashGained = 0.0f;
        tempCashGained += (shotsHit * 20.0f);
        tempCashGained += (accuracy * 2.0f) / 100.0f;
        if((shotsHit / targetsSeenCount) > 0.70)
        {
            tempCashGained *= 1.5f;
        }
        cash += tempCashGained;
    }

    void OpenShop()
    {
        shopUI.SetActive(true);
        shopButtons[0].GetComponent<Button>().onClick.AddListener(BuyAccuracy);
        shopButtons[1].GetComponent<Button>().onClick.AddListener(BuyFireRate);
        shopButtons[2].GetComponent<Button>().onClick.AddListener(BuyReloadSpeed);
        shopButtons[3].GetComponent<Button>().onClick.AddListener(BuyAmmo);
        shopButtons[4].GetComponent<Button>().onClick.AddListener(CloseShop);
        foreach (GameObject sixShot in sixShotReload)
        {
            sixShot.SetActive(false);
        }
        foreach (GameObject eightShot in eightShotReload)
        {
            eightShot.SetActive(false);

        }
        foreach (GameObject twelveShot in twelveShotReload)
        {
            twelveShot.SetActive(false);
        }
        obstaclesHolder.SetActive(false);
        crosshair.SetActive(false);
        revolver.SetActive(false);
    }

    public void BuyFireRate()
    {
        if (cash > fireRatePrice)
        {
            if (shotDelay >= 0.1f)
            {
                cash -= fireRatePrice;
                shotDelay -= 0.1f;
                fireRatePrice *= 2.0f;
                cashText.text = "$" + cash;
                upgradeFireRate.text = "Cost: $ " + fireRatePrice;
                playerFireRate.text = "Your fire rate is " + shotDelay + " seconds";
            }
            else
            {
                Debug.Log("Maxed out");
            }
        }
    }

    public void BuyAccuracy()
    {
        if (cash > accuracyPrice)
        {
            if (shotAccuracy <= 0.9f)
            {
                cash -= accuracyPrice;
                shotAccuracy += 0.1f;
                accuracyPrice *= 2.0f;
                cashText.text = "$" + cash;
                upgradeAccuracy.text = "Cost: $ " + accuracyPrice; 
                playerAccuracy.text = "Your gun is now " + (shotAccuracy * 100.0f) + "% accurate";
            }
            else
            {
                Debug.Log("Maxed out");
            }
        }
    }

    public void BuyAmmo()
    {
        if (cash > ammoCountPrice)
        {
            if (maxAmmo == 6)
            {
                cash -= ammoCountPrice;
                maxAmmo = 8;
                ammoCountPrice *= 2.0f;
                cashText.text = "$" + cash;
                upgradeAmmoCount.text = "Cost: $ " + ammoCountPrice;
                playerAmmoCount.text = "Your revolver now has " + maxAmmo + " shots in it.";
            }
            else if (maxAmmo == 8)
            {
                cash -= ammoCountPrice;
                maxAmmo = 12;
                ammoCountPrice *= 2.0f;
                cashText.text = "$" + cash;
                upgradeAmmoCount.text = "Cost: $ " + ammoCountPrice;
                playerAmmoCount.text = "Your revolver now has " + maxAmmo + " shots in it.";
            }
            else
            {
                Debug.Log("Maxed out");
            }
        }
    }

    public void BuyReloadSpeed()
    {
        if (cash > reloadSpeedPrice)
        {
            if (shotDelay >= 0.1f)
            {
                // Speed up the reload
                Debug.Log("Reload sped up");
                upgradeReloadSpeed.text = "Cost: $ " + reloadSpeedPrice;
            }
            else
            {
                Debug.Log("Maxed out");
            }
        }
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        obstaclesHolder.SetActive(true);
        crosshair.SetActive(true);
        revolver.SetActive(true);
    }
}
