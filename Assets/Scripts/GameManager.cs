using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singletone<GameManager>
{

    public GameObject GameOverUI;

    private Tower selectedTower;
    public Panel Button { get; private set; }

    private int money;

    private int lives;

    [SerializeField]
    private Text moneyText;

    [SerializeField]
    private Text livesText;

    public int Money
    {
        get => money;
        set 
        { 
            this.money = value;
            this.moneyText.text = value.ToString() + "$";
        }
    }

    public int Lives
    {
        get => lives;
        set
        {
            this.lives = value;
            this.livesText.text = value.ToString();
        }
    }

    void Start()
    {

        Money = 15;
        Lives = 20;
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    public void PickTower(Panel panelButton)
    {
        if(Money >= panelButton.Cost)
        {
            this.Button = panelButton;
            Hover.Instance.Activate(Button.Sprite);
        }
       
    }

    public void SelectTower(Tower tower)
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }

        selectedTower = tower;
        selectedTower.Select();
    }

    public void DisselectTower()
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = null;
    }

    public void BuyTower()
    {
        if(Money >= Button.Cost)
        {
            Money -= Button.Cost;
            Hover.Instance.Deactivate();
            Button = null;          
        }
        
    }

    public void ReleaseButton()
    {
        Button = null;
    }

    public void GameOver()
    {
        GameOverUI.SetActive(true);
    }
}
