using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Specifications")]
    public float speed;
    public float health;
    public int dmg;
    public float magic_res;
    public float armor;

    private float startHealth;
    [Header("Image")]
    public Image healthBar;

    private Transform target;
    private int wavepointIndex = 0;
    private float takeDmg;
    public bool IsAlive { get; set; }

    private float baseSpeed;
    private float slowTimer = 0f;
    private float poisonTimer = 0f;
    private float dmgTimer = 0.5f;
    
    private void Start()
    {
        target = Waypoints.points[0];
        IsAlive = true;
        startHealth = health;
        baseSpeed = speed;
    }

    private void Update()
    {
        SlowFunction();

        if(poisonTimer > 0)
        {
            if(dmgTimer <= 0)
            {
                dmgTimer = 0.5f;
                PoisonFunction();
            }
            dmgTimer -= Time.deltaTime;
            poisonTimer -= Time.deltaTime;
            
        }
        

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f )
        {
            GetNextWaypoint();
        }
    }
    private void SlowFunction()
    {
        if (slowTimer > 0)
        {
            speed = baseSpeed / 2;
            slowTimer -= Time.deltaTime;
        }
        else
        {
            speed = baseSpeed;
        }
    }
    private void PoisonFunction()
    {
        health -= 10f;
        CheckHealth();        
    }
    private void GetNextWaypoint()
    {

        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            DealDmg();
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    private void DealDmg()
    {
        if(GameManager.Instance.Lives > dmg)
        {
            GameManager.Instance.Lives -= dmg;
        }
        else
        {
            GameManager.Instance.Lives = 0;
            GameManager.Instance.GameOver();
        }
    }

    public void TakeDmg(float magicDmg, float physicalDmg, float slow, float armor_magicPenetration, float poison)
    {
        slowTimer = slow;
        poisonTimer = poison;
        if(armor_magicPenetration > 0)
        {
            if(armor_magicPenetration < armor)
                armor -= armor_magicPenetration;
            if(armor_magicPenetration < magic_res)
                magic_res -= armor_magicPenetration;
        }
        if(magicDmg < magic_res)
        {
            magicDmg = 0;
        }
        else
        {
            magicDmg = magicDmg - magic_res;
        }
        if (physicalDmg < armor)
        {
            physicalDmg = 0;
        }
        else
        {
            physicalDmg = physicalDmg - armor;
        }

        takeDmg = physicalDmg + magicDmg;
        if(takeDmg == 0)
        {
            takeDmg = 1;
        }

        if(health <= takeDmg)
        {
            Destroy(gameObject);
            GameManager.Instance.Money += dmg;
            return;
        }
        else
        {
            health = health - takeDmg;
        }

        healthBar.fillAmount = health/startHealth;

    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.Money += dmg;
            return;
        }
        else
        {
            healthBar.fillAmount = health / startHealth;
        }

    }

}
