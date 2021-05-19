using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;

    public GameObject spawnPoint;
    public GameObject[] enemies;
    public int maxEnemiesOnScreen;
    public int totalEnemies; 
    public int enemiesPerSpawn;

    private int enemiesOnScreen = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Spawn()
    {
        if(enemiesPerSpawn > 0 && enemiesPerSpawn < totalEnemies)
        {
            for(int i = 0; i < enemiesPerSpawn; i++)
            {
                if (enemiesOnScreen < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScreen = enemiesOnScreen + 1;
                }
            }
        }
    }
}
