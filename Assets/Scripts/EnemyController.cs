using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    public float startingSpeed;

    [SerializeField]
    public float scoreWorth;



    private ScoreController scoreController;

    private GameObject _player;
    private Light2D _flashlight;
    private float _speed;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _flashlight = _player.GetComponentInChildren<Light2D>();
        scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();
        _speed = startingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 differenceBetweenPlayer = (_player.transform.position - this.transform.position);
        Vector3 directionTowardPlayer = differenceBetweenPlayer.normalized;

        Vector3 directionFromPlayer = (this.transform.position - _player.transform.position).normalized;

        Vector3 displacement = directionTowardPlayer * _speed * Time.deltaTime;

        this.transform.Translate(displacement.x, displacement.y, 0);

        //speed up if behind

        if(_flashlight.pointLightOuterRadius > differenceBetweenPlayer.magnitude)
        {
            //make sure its at the right angle.
            float angle = Vector3.Angle(_player.transform.up, directionFromPlayer);
            if (angle < (_flashlight.pointLightOuterAngle / 2.0f)) {
                _speed = startingSpeed / 2.0f;
            }
            else
            {
                _speed = startingSpeed;
            }
        }
        else
        {
            _speed = startingSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger detected");
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            scoreController.increaseScore(scoreWorth);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Game Over You Die!!!!");
            scoreController.resetScore();
            SceneManager.LoadScene(2);
        }
    }
}
