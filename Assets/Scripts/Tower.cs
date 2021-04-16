using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{ 
    [Header("Unity stuff")]
    private SpriteRenderer mySpriteRenderer;
    private Enemy target;
    private Queue<Enemy> enemies = new Queue<Enemy>();
    public GameObject[] bulletPrefab;
    public GameObject UpgradeCanvas;
    public GameObject wizardUpgrade;

    [Header("Specification")]
    public string name;
    public float magicDmg;
    public float physicalDmg;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float slow;
    public float armor_magicPenetration;
    public float poison;
    public float explosion;
    private bool UIActive;
    public int cost;
    private int upgradeLvl;
    private int weapon = 0;
    public Text[] upgradeCost;
    public int[] upgradeCostText;
    public GameObject[] upgradeUI;

    // Start is called before the first frame update
    void Start()
    {
        UIActive = false;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        UpgradeCanvas.SetActive(UIActive);
        upgradeLvl = 0;
        for(int i = 0; i<upgradeCost.Length;i++)
        {
            upgradeCost[i].text = upgradeCostText[i].ToString() + "$"; 
        }

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        //Debug.Log(target);
       
    }

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
        UIActive = !UIActive;
        UpgradeCanvas.SetActive(UIActive);
    }

    private void Attack()
    {
        if(target == null && enemies.Count > 0)
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
        GameObject bullet = (GameObject)Instantiate(bulletPrefab[weapon], transform.position, transform.rotation);
        Bullet _bullet = bullet.GetComponent<Bullet>();

        if(_bullet != null)
        {
            _bullet.Seek(target, magicDmg, physicalDmg, explosion, slow, armor_magicPenetration, poison);
        }
    }

    public void Sell(GameObject tower)
    {
        GameManager.Instance.Money += cost / 2;
        tower.GetComponentInParent<TileScript>().SellTower();
        Destroy(tower);
    }

    private bool BuyUpgrade(int costUpgrade)
    {
        if(GameManager.Instance.Money >= costUpgrade)
        {
            GameManager.Instance.Money -= costUpgrade;
            return true;
        }
        return false;
    }

    public void Upgrade1()
    {
        // castle
        if(name == "Castle")
        {
            if (upgradeLvl == 0 && BuyUpgrade(upgradeCostText[0]))
            {
                upgradeUI[1].SetActive(false);
                cost += upgradeCostText[0];
                upgradeCostText[0] *= 2;
                upgradeCost[0].text = upgradeCostText[0].ToString() + "$";
                gameObject.transform.localScale += new Vector3(1,1,0);
                fireRate += 1f;
                physicalDmg += 10;
                upgradeLvl += 1;
            }
            else if (upgradeLvl == 1 && BuyUpgrade(upgradeCostText[0]))
            {
                upgradeUI[0].SetActive(false);
                cost += upgradeCostText[0];
                gameObject.transform.localScale += new Vector3(1, 1, 0);
                fireRate += 1f;
                upgradeLvl += 1;
                physicalDmg += 15;
                
            }
        }
        // wizard
        else if(name == "Wizard")
        {
            if (upgradeLvl == 0 && BuyUpgrade(upgradeCostText[0]))
            {
                cost += upgradeCostText[0];
                upgradeCostText[0] *= 2;
                upgradeCost[0].text = upgradeCostText[0].ToString() + "$";
                gameObject.transform.localScale += new Vector3(1, 1, 0);
                upgradeLvl += 1;
                magicDmg += 20;
                upgradeUI[1].SetActive(false);
            }
            else if (upgradeLvl == 1 && BuyUpgrade(upgradeCostText[0]))
            {
                cost += upgradeCostText[0];
                gameObject.transform.localScale += new Vector3(1, 1, 0);
                wizardUpgrade.GetComponent<WizardUpgrade>().enabled = true;
                upgradeLvl += 1;
                magicDmg += 20;
                upgradeUI[0].SetActive(false);
            }
        }
        else if(name == "Unknown")
        {
            if (upgradeLvl == 0 && BuyUpgrade(upgradeCostText[0]))
            {
                cost += upgradeCostText[0];
                
                for (int i = 0; i < upgradeCost.Length; i++)
                {
                    upgradeCostText[i] *= 2;
                    upgradeCost[i].text = upgradeCostText[i].ToString() + "$";
                }
                slow += 1;
                upgradeLvl += 1;
                upgradeUI[0].SetActive(false);
            }
            else if (upgradeLvl == 1 && BuyUpgrade(upgradeCostText[0]))
            {
                slow += 1;
                foreach(GameObject i in upgradeUI)
                {
                    i.SetActive(false);
                }
            }
        }
        
        //Debug.Log("Update1");
    }

    public void Upgrade2()
    {
        // castle
        if (name == "Castle")
        {
            if (upgradeLvl == 0 && BuyUpgrade(upgradeCostText[1]))
            {
                
                cost += upgradeCostText[1];
                upgradeCostText[1] *= 2;
                upgradeCost[1].text = upgradeCostText[1].ToString() + "$";
                upgradeUI[0].SetActive(false);
                gameObject.transform.localScale += new Vector3(1, 1, 0);
                physicalDmg += 30;
                upgradeLvl += 1;
                
            }
            else if (upgradeLvl == 1 && BuyUpgrade(upgradeCostText[1]))
            {
                cost += upgradeCostText[1];
                upgradeUI[1].SetActive(false);
                physicalDmg -= 15;
                magicDmg += 10;
                upgradeLvl += 1;
                weapon += 1;
                explosion += 2;
            }
        }
        // wizard
        else if (name == "Wizard")
        {
            if (upgradeLvl == 0 && BuyUpgrade(upgradeCostText[1]))
            {
                cost += upgradeCostText[1];
                upgradeCostText[1] *= 2;
                upgradeCost[1].text = upgradeCostText[1].ToString() + "$";
                upgradeUI[0].SetActive(false);
                fireRate += 2;
                upgradeLvl += 1;

            }
            else if (upgradeLvl == 1 && BuyUpgrade(upgradeCostText[1]))
            {
                cost += upgradeCostText[1];
                upgradeUI[1].SetActive(false);
                fireRate += 3;
                
            }
        }
        else if(name == "Unknown")
        {
            if (upgradeLvl == 0 && BuyUpgrade(upgradeCostText[1]))
            {
                cost += upgradeCostText[1];

                for (int i = 0; i < upgradeCost.Length; i++)
                {
                    upgradeCostText[i] *= 2;
                    upgradeCost[i].text = upgradeCostText[i].ToString() + "$";
                }

                explosion += 1;
                fireRate -= 2;
                upgradeLvl += 1;
                upgradeUI[1].SetActive(false);
            }
            else if (upgradeLvl == 1 && BuyUpgrade(upgradeCostText[1]))
            {
                explosion += 1;
                fireRate -= 2;
                cost += upgradeCostText[3];
                foreach (GameObject i in upgradeUI)
                {
                    i.SetActive(false);
                }
            }
        }
       
        //Debug.Log("Update2");
    }
    public void Upgrade3()
    {
        if (upgradeLvl == 0 && BuyUpgrade(upgradeCostText[2]))
        {
            cost += upgradeCostText[2];

            for (int i = 0; i < upgradeCost.Length; i++)
            {
                upgradeCostText[i] *= 2;
                upgradeCost[i].text = upgradeCostText[i].ToString() + "$";
            }
            armor_magicPenetration += 5;
            upgradeLvl += 1;
            upgradeUI[2].SetActive(false);
        }
        else if (upgradeLvl == 1 && BuyUpgrade(upgradeCostText[2]))
        {
            armor_magicPenetration += 5;
            cost += upgradeCostText[3];
            foreach (GameObject i in upgradeUI)
            {
                i.SetActive(false);
            }
        }
    }

    public void Upgrade4()
    {
        if (upgradeLvl == 0 && BuyUpgrade(upgradeCostText[3]))
        {
            cost += upgradeCostText[3];

            for (int i = 0; i < upgradeCost.Length; i++)
            {
                upgradeCostText[i] *= 2;
                upgradeCost[i].text = upgradeCostText[i].ToString() + "$";
            }

            poison += 2;
            upgradeLvl += 1;
            upgradeUI[3].SetActive(false);
        }
        else if (upgradeLvl == 1 && BuyUpgrade(upgradeCostText[3]))
        {
            poison += 2;
            cost += upgradeCostText[3];
            foreach (GameObject i in upgradeUI)
            {
                i.SetActive(false);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.tag);
        if(other.tag == "Enemy")
        {
            enemies.Enqueue(other.GetComponent<Enemy>());
            
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            target = null;
        }
    }

}
