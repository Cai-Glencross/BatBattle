using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    public float score = 0;

    [SerializeField]
    public string scorePrefix;

    [SerializeField]
    public Text scoreText;

    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = scorePrefix + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseScore(float scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = scorePrefix + score;
    }

    public void resetScore()
    {
        score = 0;
        scoreText.text = scorePrefix + score;
    }
}
