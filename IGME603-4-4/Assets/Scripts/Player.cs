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
    [SerializeField] int ammo;
    [SerializeField] int maxAmmo;
    [SerializeField] Image reddickle;
    [SerializeField] AudioSource soundEffects;
    [SerializeField] AudioClip[] sounds;
    




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
        reddickle.transform.position = Input.mousePosition;
        shotClock += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if(ammo > 0)
            {
                if(shotClock > shotDelay)
                {
                    Shoot();
                    ammo--;
                    shotClock= 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        Debug.Log("TEST");
        Vector3 tempMousePos = Input.mousePosition;
        float rng = Random.Range(0.0f,shotAccuracy);
        tempMousePos.x *= rng;
        rng = Random.Range(0.0f, shotAccuracy);
        tempMousePos.y *= rng;
        rng = Random.Range(0.0f, shotAccuracy);
        tempMousePos.z *= rng;
        Debug.Log(Input.mousePosition);
        Debug.Log("randomed" + tempMousePos);

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
