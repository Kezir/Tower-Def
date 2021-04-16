using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{

    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int cost;

    [SerializeField]
    private Text costText;

    public GameObject TowerPrefab { get => towerPrefab; }
    public Sprite Sprite { get => sprite; }
    public int Cost { get => cost; }

    private void Start()
    {
        costText.text = cost + "$";
    }
}
