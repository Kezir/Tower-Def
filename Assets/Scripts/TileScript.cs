using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{

    public Point GridPosition { get; private set; }

    public bool IsEmpty { get; private set; }

    private Tower myTower;
    
    // Start is called before the first frame update
    void Start()
    {
        IsEmpty = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);

        LevelManager.Instance.Tiles.Add(gridPos, this);
        
    }

    private void OnMouseOver()
    {
        
        //Debug.Log(GridPosition.X + "" + GridPosition.Y);
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.Button != null)
        {
            if (Input.GetMouseButtonDown(0) && !gameObject.CompareTag("Road") && IsEmpty == true)
            {
                PlaceTower();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Hover.Instance.Deactivate();
            }
        }
        else if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.Button == null)
        {
            //Debug.Log(myTower);
            if(myTower != null)
            {
                GameManager.Instance.SelectTower(myTower);
            }
            else
            {
                GameManager.Instance.DisselectTower();
            }
        }
    }

    public void SellTower()
    {
        IsEmpty = true;
    }
    private void PlaceTower()
    {

        GameObject tower = (GameObject)Instantiate(GameManager.Instance.Button.TowerPrefab, transform.position, Quaternion.identity);
        tower.transform.SetParent(transform);
        this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();
        //Debug.Log(myTower);
        IsEmpty = false;
        GameManager.Instance.BuyTower();
        //Debug.Log(myTower);


    }
}
