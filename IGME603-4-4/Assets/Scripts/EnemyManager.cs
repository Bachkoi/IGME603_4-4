using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Establish necessary fields
    [SerializeField] public GameObject[] staticEnemies;
    [SerializeField] GameObject[] moving;
    [SerializeField] public int activeStatic;
    [SerializeField] Canvas canvas;
    [SerializeField] public int targetsSpawned;
    [SerializeField] public GameObject leoTarget;
    [SerializeField] bool ifLeoSpawned = false;
    //New variables for obstacles
    [SerializeField] public GameObject[] obstacles;




    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;  i < staticEnemies.Length; i++)
        {
            staticEnemies[i].SetActive(false);
        }
        for(int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ifLeoSpawned == false)
        {
            if(Random.Range(0.0f, 1.0f) > 0.03f)
            {
                leoTarget.SetActive(true);
                targetsSpawned++;
                ifLeoSpawned = true;
                float scalar = Random.Range(0.2f, 1.0f);
                leoTarget.transform.localScale *= scalar;
                //enemy.transform.position = new Vector3(canvas.GetComponent<RectTransform>().rect.width * Random.Range(-0.7f, 0.7f), canvas.GetComponent<RectTransform>().rect.height * scalar, enemy.transform.position.z);
                if (scalar < 0.5f)
                {
                    leoTarget.transform.position = new Vector3(9.9f * Random.Range(-0.7f, 0.7f), 3.98f * (1.0f - scalar), leoTarget.transform.position.z);


                }
                else
                {
                    leoTarget.transform.position = new Vector3(9.9f * Random.Range(-0.7f, 0.7f), -3.98f * scalar, leoTarget.transform.position.z);
                }
            }
        }
        if(leoTarget.GetComponent<StaticEnemy>().totalTime < 0.0f)
        {
            leoTarget.SetActive(false);
        }
        foreach(GameObject enemy in staticEnemies)
        {
            if (activeStatic < 3)
            {
                if (enemy.activeSelf == false)
                {
                    targetsSpawned++;
                    //enemy.SetActive(true);
                    //float scalar = Random.Range(0.2f, 1.0f);
                    //enemy.transform.localScale *= scalar;
                    ////enemy.transform.position = new Vector3(canvas.GetComponent<RectTransform>().rect.width * Random.Range(-0.7f, 0.7f), canvas.GetComponent<RectTransform>().rect.height * scalar, enemy.transform.position.z);
                    //if(scalar < 0.5f)
                    //{
                    //    enemy.transform.position = new Vector3(9.9f * Random.Range(-0.7f, 0.7f), 3.98f * (1.0f-scalar), enemy.transform.position.z);
                    //    
                    //
                    //}
                    //else
                    //{
                    //    enemy.transform.position = new Vector3(9.9f * Random.Range(-0.7f, 0.7f), -3.98f * scalar, enemy.transform.position.z);
                    //}

                    //Find a randam obstacle and generate an enemy in the available end
                    int targetObstacles = Random.Range(0, obstacles.Length);
                    //obstacles[targetObstacles].GetComponent<Obstacles>().SpawnOpponent(enemy);
                    //obstacles[targetObstacles].SetActive(true);
                    enemy.SetActive(true);
                    int i = Random.Range(0, 3);
                    enemy.transform.position = obstacles[targetObstacles].GetComponent<Obstacles>().opponentSpawn[i].transform.position;
                    enemy.transform.localScale = obstacles[targetObstacles].transform.localScale;

                    activeStatic++;
                }
            }
            if(enemy.GetComponent<StaticEnemy>().totalTime < 0.0f)
            {
                enemy.SetActive(false);
                activeStatic--;
                enemy.GetComponent<StaticEnemy>().totalTime = 5.0f;
                enemy.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }

    void SpawnEnemy(GameObject[] obj, GameObject enemy)
    {
        int targetObstacles = Random.Range(0, obj.Length);
        obj[targetObstacles].GetComponent<Obstacles>().SpawnOpponent(enemy);
    }
}
