using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeSpan = 3.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if(lifeSpan <= 0)
        {
            //this.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
