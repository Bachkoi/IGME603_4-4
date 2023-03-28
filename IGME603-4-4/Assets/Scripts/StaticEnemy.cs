using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    [SerializeField] public float totalTime = 5.0f;
    [SerializeField] float scale;
    [SerializeField] bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf) // https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.vectorstock.com%2Froyalty-free-vectors%2Ftarget-shooting-cowboy-vectors&psig=AOvVaw22EbWOHYa9htWzN6XOkObd&ust=1680082248027000&source=images&cd=vfe&ved=0CA8QjRxqFwoTCJjt-56o_v0CFQAAAAAdAAAAABAI
        {
            totalTime -= Time.deltaTime;
            if(totalTime < 0)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            totalTime = 5.0f;
        }
    }
}
