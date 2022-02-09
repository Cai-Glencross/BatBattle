using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    public float spawnRateSeconds;

    [SerializeField]
    public float spawnRateNumber;

    [SerializeField]
    GameObject worldBounds;

    [SerializeField]
    public float behindDistance;

    [SerializeField]
    public int spawnBehindFrequency;

    [SerializeField]
    public float enemySpeed;

    private IEnumerator spawnEnemiesCoroutine;

    private ArrayList currentEnemies;

    private float _maxXValue;
    private float _minXValue;
    private float _maxYValue;
    private float _minYValue;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D boxCollider = worldBounds.GetComponent<BoxCollider2D>();
        Vector2 screenBoundsMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 screenBoundsMin = Camera.main.ScreenToWorldPoint(Vector2.zero);
        float screenWidth = screenBoundsMax.x - screenBoundsMin.x;
        float screenHeight = screenBoundsMax.y - screenBoundsMin.y;

        _maxXValue = worldBounds.transform.position.x + boxCollider.offset.x + boxCollider.size.x / 2.0f - screenWidth / 2.0f;
        _minXValue = worldBounds.transform.position.x + boxCollider.offset.x - boxCollider.size.x / 2.0f + screenWidth / 2.0f;
        _maxYValue = worldBounds.transform.position.y + boxCollider.offset.y + boxCollider.size.y / 2.0f - screenHeight / 2.0f;
        _minYValue = worldBounds.transform.position.y + boxCollider.offset.y - boxCollider.size.y / 2.0f + screenHeight / 2.0f;

        _player = GameObject.Find("Player");
        currentEnemies = new ArrayList();

        spawnEnemiesCoroutine = spawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnEnemies()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnRateSeconds);
            for(int i = 0; i < spawnRateNumber; i++)
            {
                if (currentEnemies.Count % spawnBehindFrequency == 0)
                {
                    spawnEnemyBehind();
                }
                else
                {
                    spawnEnemy();
                }

            }
        }

    }

    void spawnEnemy()
    {
        float xPosition = Random.Range(_minXValue, _maxXValue);
        float yPosition = Random.Range(_minYValue, _maxYValue);

        GameObject enemy = Instantiate(enemyPrefab, 
            new Vector3(xPosition, yPosition, enemyPrefab.transform.position.z), 
            Quaternion.identity);
        enemy.GetComponent<EnemyController>().startingSpeed = enemySpeed;

        currentEnemies.Add(enemy);
    }

    void spawnEnemyBehind()
    {
        Vector3 position = _player.transform.position - _player.transform.up * behindDistance;
        GameObject enemy = Instantiate(enemyPrefab,
            position,
            Quaternion.identity);
        enemy.GetComponent<EnemyController>().startingSpeed = enemySpeed;
        currentEnemies.Add(enemy);
    }

    public void startSpawning()
    {
        StartCoroutine(spawnEnemiesCoroutine);
    }

    public void stopSpawning()
    {
        StopCoroutine(spawnEnemiesCoroutine);
    }

    public void clearEnemies()
    {
        foreach (GameObject enemy in currentEnemies)
        {
            Destroy(enemy);
        }
        currentEnemies = new ArrayList();
    }
}
