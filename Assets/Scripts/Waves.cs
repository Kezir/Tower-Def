using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public GameObject startButton;

    public GameObject win;

    private GameObject[] respawns;
    [SerializeField]
    private Transform[] enemies;

    [SerializeField]
    private Transform spawnPoint;

    public GameObject instruction;

    int[,] wave = new int[11, 4] { { 10, 0, 0, 0 }, { 10, 5, 0, 0 }, { 10, 10, 0, 0 }, { 10, 10, 5, 0 }, { 5, 5, 10, 0 }, { 10, 10, 10, 0 }, { 5, 5, 5, 1 }, { 10, 15, 10, 0 }, { 10, 10, 10, 1 }, { 15, 15, 15, 1 },{ 0,0,0,0} };

    public float timeBetweenWaves = 10f;
    private float countdown = 2f;

    private bool start;

    [SerializeField]
    private int waveNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (waveNumber < 11 && GameManager.Instance.Lives > 0 && start == true)
        {
            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }
            countdown -= Time.deltaTime;
        }
        else if(waveNumber == 11 && Clear())
        {
            win.SetActive(true);
        }
    }

    IEnumerator SpawnWave()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int i = 0; i < wave[waveNumber, x]; i++)
            {
                SpawnEnemy(x);
                yield return new WaitForSeconds(0.3f);
            }
        }

        waveNumber++;
    }

    private bool Clear()
    {
        respawns = GameObject.FindGameObjectsWithTag("Enemy");
        if(respawns.Length == 0)
        {
            return true;
        }
        return false;
    }
    private void SpawnEnemy(int enemy)
    {
        Instantiate(enemies[enemy], spawnPoint.position, spawnPoint.rotation);
    }

    public void StartWave()
    {
        start = !start;
        startButton.SetActive(false);
        instruction.SetActive(false);
    }
}
