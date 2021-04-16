using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Enemy target;
    private float explosion;
    public GameObject ExplosionAnim;
    private float slow;
    private float armor_magicPenetration;
    private float poison;

    private float magicDmg;
    private float physicalDmg;

    public float speed;

    public void Seek( Enemy _target, float _magicDmg, float _physicalDmg, float _explosion, float _slow, float _armor_magicPenatration, float _poison)
    {
        target = _target;
        magicDmg = _magicDmg;
        physicalDmg = _physicalDmg;
        explosion = _explosion;
        slow = _slow;
        armor_magicPenetration = _armor_magicPenatration;
        poison = _poison;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Move();
    }

    void Move()
    {
        Vector2 dir = target.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    void HitTarget()
    {
        if(explosion > 0f)
        {
            Instantiate(ExplosionAnim, gameObject.transform.position, Quaternion.identity);
            Explode();
        }
        else
        {
            target.TakeDmg(magicDmg, physicalDmg, slow, armor_magicPenetration, poison);
        }
  

        Destroy(gameObject);
        
    }

    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2);

        foreach(Collider2D collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Enemy e = collider.gameObject.GetComponent<Enemy>();
                e.TakeDmg(magicDmg, physicalDmg, slow, armor_magicPenetration, poison);
            }
        }
        
    }

}
