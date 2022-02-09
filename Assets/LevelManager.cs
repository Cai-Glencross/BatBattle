using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    ScoreController scoreController;

    [SerializeField]
    EnemySpawner enemySpawner;

    [SerializeField]
    float scoreNeeded;

    [SerializeField]
    Text levelText;

    [SerializeField]
    Text scoreNeededText;

    public int currentLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeLevels());
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreController.score >= scoreNeeded)
        {
            endLevel();
            StartCoroutine(changeLevels());
        }
    }


    IEnumerator changeLevels()
    {
        levelText.text = "Level " + currentLevel;
        scoreNeededText.text = "Score Needed: " + scoreNeeded;

        yield return new WaitForSeconds(5.0f);

        levelText.text = "";
        scoreNeededText.text = "";

        enemySpawner.startSpawning();
    }

    void endLevel()
    {
        enemySpawner.stopSpawning();
        enemySpawner.clearEnemies();
        scoreController.resetScore();

        currentLevel++;
        scoreNeeded += 50;

        if (currentLevel % 5 == 0)
        {
            enemySpawner.enemySpeed = enemySpawner.enemySpeed + 1;
        }

        if (currentLevel % 3 == 0)
        {
            if (enemySpawner.spawnRateSeconds > 0.2f)
            {
                enemySpawner.spawnRateSeconds = enemySpawner.spawnRateSeconds - 0.2f;
            }
        }

        if (currentLevel % 7 == 0)
        {
            if (enemySpawner.behindDistance > 4)
            {
                enemySpawner.behindDistance = enemySpawner.behindDistance - 2;
            }

        }

        if (currentLevel % 10 == 0)
        {
            if (enemySpawner.spawnBehindFrequency > 2)
            {
                enemySpawner.spawnBehindFrequency = enemySpawner.spawnBehindFrequency - 1;
            }
        }

        StartCoroutine(changeLevels());
    }
}
