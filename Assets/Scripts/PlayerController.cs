using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float speed;

    [SerializeField]
    public float gunCooldownTime;

    [SerializeField]
    public GameObject bulletPrefab;

    [SerializeField]
    public GameObject playerBoundsObject;

    private float _maxXValue;
    private float _minXValue;
    private float _maxYValue;
    private float _minYValue;

    private AudioSource audioSource;

    private bool gunIsReady;
    // Start is called before the first frame update
    void Start()
    {
        gunIsReady = true;

        BoxCollider2D boxCollider = playerBoundsObject.GetComponent<BoxCollider2D>();
        _maxXValue = playerBoundsObject.transform.position.x + boxCollider.offset.x + boxCollider.size.x / 2.0f;
        _minXValue = playerBoundsObject.transform.position.x + boxCollider.offset.x - boxCollider.size.x / 2.0f;
        _maxYValue = playerBoundsObject.transform.position.y + boxCollider.offset.y + boxCollider.size.y / 2.0f;
        _minYValue = playerBoundsObject.transform.position.y + boxCollider.offset.y - boxCollider.size.y / 2.0f;

        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move
        float verticalDisplacement = Input.GetAxis("Vertical")*speed*Time.deltaTime;
        float horizontalDisplacement = Input.GetAxis("Horizontal")*speed*Time.deltaTime;

        transform.position = new Vector3(transform.position.x + horizontalDisplacement,
            transform.position.y + verticalDisplacement,
            transform.position.z);

        if (this.transform.position.x > _maxXValue)
        {
            this.transform.position = new Vector3(_maxXValue, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x < _minXValue)
        {
            this.transform.position = new Vector3(_minXValue, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.y > _maxYValue)
        {
            this.transform.position = new Vector3(this.transform.position.x, _maxYValue, this.transform.position.z);
        }
        else if (this.transform.position.y < _minYValue)
        {
            this.transform.position = new Vector3(this.transform.position.x, _minYValue, this.transform.position.z);
        }

        //Rotate
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionTowardMouse = mousePosition - this.transform.position;
        transform.up = new Vector2(directionTowardMouse.x, directionTowardMouse.y);

        //Shoot
        if (Input.GetMouseButton(0))
        {
            if (gunIsReady)
            {
                GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                bullet.transform.up = this.transform.up;
                audioSource.Play();
                StartCoroutine(startCooldown());
            }
        }
    }

    IEnumerator startCooldown()
    {
        gunIsReady = false;
        yield return new WaitForSeconds(gunCooldownTime);
        gunIsReady = true;
    }

}
