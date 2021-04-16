using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;
    private float x;
    private float y;
   

    private void Awake()
    {

        Camera camera = Camera.main;
        float Height = camera.orthographicSize * 2f;
        float Width = camera.aspect * Height;

        y = 10f - Height;
        x = 17.72f - Width;
        
        

        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
        for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.position += new Vector3(x/2, y, 0);
            
        }
    }

}