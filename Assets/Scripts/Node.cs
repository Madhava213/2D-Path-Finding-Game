using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private LineController line;
    public LayerMask mask;
    // Ranges
    private Vector2 xPosRange = new Vector2(-10.5f,10.5f);
    private Vector2 yPosRange = new Vector2(-4.5f,4.5f);
    private Dictionary<GameObject,LineController> lineConnected = new Dictionary<GameObject,LineController>();

    // public Dictionary<GameObject,LineController> connected;

    // Update is called once per frame
    void Update()
    {
        GameObject[] allNodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject item in allNodes)
        {
            Vector3 destination = item.transform.position - transform.position;
            if(this.gameObject != item){
                RaycastHit2D nodeCast = Physics2D.Raycast(transform.position,destination,20,mask);
                // Debug.DrawLine(transform.position,new Vector3(nodeCast.point.x,nodeCast.point.y,0.0f),Color.red,5);
                // Debug.DrawLine(transform.position, destination,Color.blue,5);
                if(nodeCast.collider != null && nodeCast.collider.gameObject.CompareTag("Obstacle")){
                    if(lineConnected != null && lineConnected.ContainsKey(item) && item.gameObject.GetComponent<Node>().lineConnected.ContainsKey(this.gameObject)){
                        GameObject.Destroy(lineConnected[item]);
                        item.gameObject.GetComponent<Node>().lineConnected.Remove(this.gameObject);
                        lineConnected.Remove(item);
                    }
                }
                else if (nodeCast.collider != null && nodeCast.collider.gameObject.CompareTag("Node"))
                {
                    if(lineConnected == null || (!lineConnected.ContainsKey(item) && !item.gameObject.GetComponent<Node>().lineConnected.ContainsKey(this.gameObject))){
                        line.addPoints(new Transform[2] { this.transform, item.transform });
                        LineController newLine = Instantiate(line, transform.position, Quaternion.identity);
                        lineConnected.Add(item, newLine);
                        item.gameObject.GetComponent<Node>().lineConnected.Add(this.gameObject, newLine);
                    }
                }
            }
        }
        // Debug.Log(transform.position + " -+-+-+- " + nodeCast.point +  " ++++++++++++++ " + Vector2.up);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Node") || other.gameObject.CompareTag("Obstacle")){
            Vector2 newPos = new Vector2(Random.Range(xPosRange.x, xPosRange.y), Random.Range(yPosRange.x, yPosRange.y));
            this.transform.position = newPos;
        }
    }
}
