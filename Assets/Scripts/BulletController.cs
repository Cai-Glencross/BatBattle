using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    public float speed = 5;

    [SerializeField]
    public float lifeExpectancySeconds = 10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyAfterLifespan());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    IEnumerator destroyAfterLifespan()
    {
        yield return new WaitForSeconds(lifeExpectancySeconds);
        Destroy(this.gameObject);
    }
}
