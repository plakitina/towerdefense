using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int target = 0;
    public Transform exit;
    public Transform[] wayPoints;
    public float navigation;
    [SerializeField]
    int rewardAmount;
    [SerializeField]
    int health;

    Collider2D enemyCollider;


    Transform enemy;
    float navigationTime = 0;

    bool isDead = false;

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }


    void Start()
    {
        enemy = GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>();
        Manager.Instance.registerEnemy(this);
    }

    void Update()
    {
        if(wayPoints != null && isDead == false)
        {
            navigationTime += Time.deltaTime;
            if(navigationTime > navigation)
            {
                if(target < wayPoints.Length)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
                    navigationTime /=2;
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
            Manager.Instance.unregisterEnemy(this);
        }
        else if(collision.tag == "Projectile")
        {
            Projectile newP = collision.gameObject.GetComponent<Projectile>();
            EnemyHit(newP.AttackDamage);
            Destroy(collision.gameObject);
        }
    }

    public void EnemyHit(int hitPoints)
    {
        if (health - hitPoints > 0)
        {
            health -= hitPoints;
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        enemyCollider.enabled = false;
    }
}
