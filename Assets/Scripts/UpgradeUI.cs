using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Text upgradeText;
    public GameObject upgradeUI;
    private int upgradeCount = 0;
    private string name;


    public void UpgradeCount()
    {
        upgradeCount += 1;
        if (upgradeCount == 2)
        {
            upgradeUI.SetActive(false);
        }
        else if (name == "Castle_1")
        {
            Castle_1();
        }
        else if(name == "Castle_2")
        {
            Castle_2();
        }
        else if(name == "Wizard_1")
        {
            Wizard_1();
        }
        else if(name == "Wizard_2")
        {
            Wizard_2();
        }
        
    }
    public void Castle_1()
    {
        if(upgradeCount == 0)
        {
            upgradeText.text = "Fire rate: +1" + "\n" + "Physical dmg: +5" + "\n" + "Range: +1";
            name = "Castle_1";
        }
        else if(upgradeCount == 1)
        {
            upgradeText.text = "Fire rate: +1" + "\n" + "Physical dmg: +15" + "\n" + "Range: +1";
        }

            
    }
    public void Castle_2()
    {
        if (upgradeCount == 0)
        {
            upgradeText.text = "Physical dmg: +30" + "\n" + "Range: +1";
            name = "Castle_2";
        }
        else if (upgradeCount == 1)
        {
            upgradeText.text = "Physical dmg: -15" + "\n" + "Magic dmg: +10" +"\n"+ "Explosion";
        }

    }

    public void Wizard_1()
    {
        if (upgradeCount == 0)
        {
            upgradeText.text = "Magic dmg: +20" + "\n" + "Range: +1";
            name = "Wizard_1";
        }
        else if (upgradeCount == 1)
        {
            upgradeText.text = "Magic dmg: +20" + "\n" + "Range: +1" +"\n"+"Double shot + Armor breaker";
        }

    }

    public void Wizard_2()
    {
        if (upgradeCount == 0)
        {
            upgradeText.text = "Fire rate: +2";
            name = "Wizard_2";
        }
        else if (upgradeCount == 1)
        {
            upgradeText.text = "Fire rate: +3";
        }

    }

    public void Unkown_0()
    {
        upgradeText.text = "Slow";

    }

    public void Unkown_1()
    {
        upgradeText.text = "Fire rate: -2" +"\n" + "Small explosion";

    }

    public void Unkown_2()
    {
        upgradeText.text = "Armor breaker";

    }

    public void Unkown_3()
    {
        upgradeText.text = "Poison";

    }

    public void exit()
    {
        upgradeText.text = "";
    }
}
