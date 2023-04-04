using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public float fireRatePrice = 100.0f;
    public float accuracyPrice = 500.0f;
    public float reloadSpeedPrice = 1000.0f;
    public float ammoCountPrice = 1000.0f;

    public void BuyFireRate()
    {
        if(player.GetComponent<Player>().cash > fireRatePrice)
        {
            if(player.GetComponent<Player>().shotDelay >= 0.1f)
            {
                player.GetComponent<Player>().cash -= fireRatePrice;
                player.GetComponent<Player>().shotDelay -= 0.1f;
                fireRatePrice *= 2.0f;
            }
            else
            {
                Debug.Log("Maxed out");
            }
        }
    }

    public void BuyAccuracy()
    {
        if (player.GetComponent<Player>().cash > accuracyPrice)
        {
            if (player.GetComponent<Player>().shotAccuracy <= 0.9f)
            {
                player.GetComponent<Player>().cash -= accuracyPrice;
                player.GetComponent<Player>().shotAccuracy += 0.1f;
                accuracyPrice *= 2.0f;
            }
            else
            {
                Debug.Log("Maxed out");
            }
        }
    }

    public void BuyAmmo()
    {
        if (player.GetComponent<Player>().cash > ammoCountPrice)
        {
            if (player.GetComponent<Player>().maxAmmo == 6)
            {
                player.GetComponent<Player>().cash -= ammoCountPrice;
                player.GetComponent<Player>().maxAmmo = 8;
                ammoCountPrice *= 2.0f;
            }
            if(player.GetComponent<Player>().maxAmmo == 8)
            {
                player.GetComponent<Player>().cash -= ammoCountPrice;
                player.GetComponent<Player>().maxAmmo = 12;
                ammoCountPrice *= 2.0f;
            }
            else
            {
                Debug.Log("Maxed out");
            }
        }
    }

    public void BuyReloadSpeed()
    {
        if (player.GetComponent<Player>().cash > reloadSpeedPrice)
        {
            if (player.GetComponent<Player>().shotDelay >= 0.1f)
            {
                // Speed up the reload
                Debug.Log("Reload sped up");
            }
            else
            {
                Debug.Log("Maxed out");
            }
        }
    }

}
