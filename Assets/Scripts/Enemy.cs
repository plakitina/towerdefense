using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int target = 0;
    public Transform exit;
    public Transform[] wayPoints;
    public float navigation;
    int health;
    [SerializeField]
    int rewardAmount;

    Transform enemy;
    float navigationTime = 0;

    void Start()
    {
        enemy = GetComponent<Transform>();
        Manager.instance.registerEnemy(this);

    }

    void Update()
    {
        if(wayPoints != null)
        {
            navigationTime += Time.deltaTime;
            if(navigationTime > navigation)
            {
                if(target < wayPoints.Length)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
                    navigationTime /=2;
                    Debug.Log(wayPoints[target].position);
                }
                else
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, exit.position, navigationTime);
                    navigationTime /= 2;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MovingPoint")
        {
            target += 1;
        }
        else if(collision.tag == "Finish")
        {
            Manager.instance.unregisterEnemy(this);
        }
    }
}
