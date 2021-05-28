using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum gameStatus
{
    next,play,gameover,win
}

public class Manager : MonoBehaviour
{
    [SerializeField]
    public static Manager instance = null;
    [SerializeField]
    public GameObject spawnPoint;
    [SerializeField]
    Text totalMoneyLabel;
    [SerializeField]
    Text currentWave;
    [SerializeField]
    Text totalEscapeLabel;
    [SerializeField]
    Text playBtnLabel;
    [SerializeField]
    Button playBtn;
    [SerializeField]
    public GameObject[] enemies;
    [SerializeField]
    public int maxEnemiesOnScreen;
    [SerializeField]
    public int totalEnemies;
    [SerializeField]
    public int enemiesPerSpawn;

    gameStatus currentStatus = gameStatus.play;

    int waveNumber;
    int totalMoney = 10;
    int totalEscaped = 0;
    int roundEscaped = 0;
    int totalKill = 0;
    int whichEnemiesToSpawn = 0;

    public int TotalMoney
    {
        get{
            return totalMoney;
        }
        set{
            totalMoney = value;
            totalMoneyLabel.text = totalMoney.ToString();
        }
    }


    public List<Enemy> EnemyList = new List<Enemy>(); 


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
        if(enemiesPerSpawn > 0 && EnemyList.Count <= totalEnemies)
        {
            for(int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }
        }
    }

    public void registerEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void unregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void destroyEnemies()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);

        }
        EnemyList.Clear();
    }

    public void addMoney(int amount)
    {
        TotalMoney += amount;
    }

    public void subtractMoney(int amount)
    {
        TotalMoney -= amount; 
    }
}
