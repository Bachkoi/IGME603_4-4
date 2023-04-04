using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    //the First 4 variables didn't use now
    [SerializeField] private bool isStatic;
    [SerializeField] private float patrolInterval;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private GameObject[] patrolPos;
    [SerializeField] private GameObject[] opponentSpawn;
    [SerializeField] private int availableSpawnSpot;

    private GameObject targetPos;
    private GameObject attachedObj;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnOpponent(GameObject opponent)
    {       
        opponent.SetActive(true);
        int i = Random.Range(0, availableSpawnSpot);
        opponent.transform.position = opponentSpawn[i].transform.position;
        opponent.transform.localScale = this.transform.localScale;
    }

    public void DestroyOpponent(GameObject opponent)
    {
        
    }
}
