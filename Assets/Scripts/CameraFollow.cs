using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    [SerializeField]
    public GameObject worldBounds;

    [SerializeField]
    public float speed;

    private float _maxXValue;
    private float _minXValue;
    private float _maxYValue;
    private float _minYValue;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D boxCollider = worldBounds.GetComponent<BoxCollider2D>();
        Vector2 screenBoundsMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 screenBoundsMin = Camera.main.ScreenToWorldPoint(Vector2.zero);
        float screenWidth = screenBoundsMax.x - screenBoundsMin.x;
        float screenHeight = screenBoundsMax.y - screenBoundsMin.y;

        _maxXValue = worldBounds.transform.position.x + boxCollider.offset.x + boxCollider.size.x / 2.0f - screenWidth/2.0f;
        _minXValue = worldBounds.transform.position.x + boxCollider.offset.x - boxCollider.size.x / 2.0f + screenWidth/2.0f;
        _maxYValue = worldBounds.transform.position.y + boxCollider.offset.y + boxCollider.size.y / 2.0f - screenHeight/2.0f;
        _minYValue = worldBounds.transform.position.y + boxCollider.offset.y - boxCollider.size.y / 2.0f + screenHeight/2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player!=null)
        {
            Vector3 direction = (player.transform.position - this.transform.position);
            transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);
        }

        //don't show out of bounds stuff
        Vector2 screenBoundsMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 screenBoundsMin = Camera.main.ScreenToWorldPoint(Vector2.zero);

        if (this.transform.position.x > _maxXValue)
        {
            this.transform.position = new Vector3(_maxXValue, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x <_minXValue)
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


    }
}
