using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardUpgrade : MonoBehaviour
{
    [Header("Unity stuff")]
    private Enemy target;
    private Queue<Enemy> enemies = new Queue<Enemy>();
    public GameObject[] bulletPrefab;
    

    [Header("Specification")]
    public float magicDmg;
    public float physicalDmg;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float slow;
    public float armor_magicPenetration;
    public float explosion;
    public float poison;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        //Debug.Log(target);

    }


    private void Attack()
    {
        if (target == null && enemies.Count > 0)
        {
            target = enemies.Dequeue();
        }
        if (target != null && target.IsAlive)
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;

        }

    }

    void Shoot()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab[0], transform.position, transform.rotation);
        Bullet _bullet = bullet.GetComponent<Bullet>();

        if (_bullet != null)
        {
            _bullet.Seek(target, magicDmg, physicalDmg, explosion, slow, armor_magicPenetration, poison);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Enemy")
        {
            enemies.Enqueue(other.GetComponent<Enemy>());

        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            target = null;
        }
    }
}
