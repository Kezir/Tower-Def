using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : Singletone<LevelManager>
{
    [SerializeField]
    private GameObject[] tiles;

    [SerializeField]
    private CameraMovement cameraMovement;


    [SerializeField]
    private Transform map;
   
    public Dictionary<Point, TileScript> Tiles { get; set; }

    public float TileSize
    {
        get { return tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    void Start()
    {
        CreateLevel();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        string[] mapData = ReadLevelText();
        //var count = 0;

        //for (int i = 0; i<mapData.Length;i++)
        //{
        //    count = mapData[i].Count(x => x == '4') + count;
        //}
        //waypoints = new GameObject[count];

        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapYSize; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for(int x = 0; x < mapXSize; x++)
            {
                PlaceTile(newTiles[x].ToString(),x, y, worldStart);
            }
        }

        maxTile = Tiles[new Point(mapXSize - 1, mapYSize - 1)].transform.position;

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
    }

    private void PlaceTile(string tileType, int x,int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);

        TileScript newTile = Instantiate(tiles[tileIndex]).GetComponent<TileScript>();

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map);

        //if(tileIndex == 4)
        //{
        //    Instantiate(waypoint,new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), Quaternion.identity);
        //}
        
        
    }

    private string[] ReadLevelText()
    {
        TextAsset binddata = Resources.Load("Level1") as TextAsset;
        string data = binddata.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-'); 
    }
}
