using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Ranges
    private Vector2 xPosRange = new Vector2(-8.5f,8.5f);
    private Vector2 yPosRange = new Vector2(-4.5f,4.5f);

    private Vector3 screenPoint;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
        void OnMouseDrag()
    {
        Vector3 currScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currPosition = Camera.main.ScreenToWorldPoint(currScreenPoint) + offset;
        transform.position = currPosition;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Node") || other.gameObject.CompareTag("Obstacle")){
            this.transform.position = new Vector2(Random.Range(xPosRange.x, xPosRange.y), Random.Range(yPosRange.x, yPosRange.y));
        }
    }
}
