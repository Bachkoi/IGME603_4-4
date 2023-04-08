using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeepPlayer : MonoBehaviour
{

    public static KeepPlayer instance;
    public Player player;
    public GameObject playerAccuracy;
    public GameObject playerCompletion;
    public GameObject playerCash;
    public GameObject rustyRevolver;
    public GameObject contestShooter;
    public GameObject theGoldenGun;
    public Button previousArrow;
    public Button nextArrow;
    public Button buyCashButton;
    public Button buyGoldButton;
    public int currentIndex = 0;


    public void Start()
    {
        player = Player.Instance;
        float accuracy = ((player.shotsHit / player.shotsTaken) * 100.0f);
        float completion = (player.shotsHit / player.targetsSeenCount);
        playerAccuracy.GetComponent<TextMeshProUGUI>().text = "Accuracy: " + accuracy.ToString("n2") + "%";
        playerCompletion.GetComponent<TextMeshProUGUI>().text = "Completion: " + completion.ToString("n2") + "%";
        playerCash.GetComponent<TextMeshProUGUI>().text = "" + player.cash.ToString("n2");
        previousArrow.onClick.AddListener(PreviousGuns);
        nextArrow.onClick.AddListener(AdvanceGuns);
        buyCashButton.onClick.AddListener(PayWithCash);
        buyGoldButton.onClick.AddListener(PayWithGold);
    }
    public void Update()
    {

    }

    public void PayWithCash()
    {
        float cost = 0;
        switch (currentIndex)
        {
            case (0):
                cost = 0.0f;
                break;
            case (1):
                cost = 700.0f;
                break;
            case (2):
                cost = 10000.0f;
                break;

        }
        if (player.cash > cost)
        {
            player.cash -= cost;
            switch (currentIndex)
            {
                case (0):
                    player.startingGun = 0;
                    break;
                case (1):
                    player.startingGun = 1;
                    break;
                case (2):
                    player.startingGun = 2;
                    break;

            }
        }
    }
    public void PayWithGold()
    {
        float cost = 0;
        switch (currentIndex)
        {
            case (0):
                cost = 0.0f;
                break;
            case (1):
                cost = 5.0f;
                break;
            case (2):
                cost = 50.0f;
                break;

        }
        if (player.gold > cost)
        {
            player.gold -= cost;
            switch (currentIndex)
            {
                case (0):
                    player.startingGun = 0;
                    break;
                case (1):
                    player.startingGun = 1;
                    break;
                case (2):
                    player.startingGun = 2;
                    break;

            }
        }
    }

    public void AdvanceGuns()
    {
        switch (currentIndex)
        {
            case (0):
                rustyRevolver.SetActive(false);
                contestShooter.SetActive(true);
                currentIndex = 1;
                break;
            case (1):
                contestShooter.SetActive(false);
                theGoldenGun.SetActive(true);
                currentIndex = 2;
                break;
            case (2):
                theGoldenGun.SetActive(false);
                rustyRevolver.SetActive(true);
                currentIndex = 0;
                break;

        }
    }
    public void PreviousGuns()
    {
        switch (currentIndex)
        {
            case (0):
                rustyRevolver.SetActive(false);
                theGoldenGun.SetActive(true);
                currentIndex = 2;
                break;
            case (1):
                contestShooter.SetActive(false);
                rustyRevolver.SetActive(true);
                currentIndex = 0;
                break;
            case (2):
                theGoldenGun.SetActive(false);
                contestShooter.SetActive(true);
                currentIndex = 1;
                break;

        }
    }


}
