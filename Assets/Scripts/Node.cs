using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private LineController line;
    public GameObject gameManager;
    public LayerMask mask;
    public bool start;
    public bool goal;

    public bool path;
    // Ranges
    private Vector2 xPosRange = new Vector2(-8.5f,8.5f);
    private Vector2 yPosRange = new Vector2(-4.5f,4.5f);
    private Dictionary<string,bool> connected = new Dictionary<string,bool>();

    // Update is called once per frame
    void Update()
    {
        GameObject[] allNodes = GameObject.FindGameObjectsWithTag("Node");
        GameObject[] allLines = GameObject.FindGameObjectsWithTag("Line");

        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
            if(hit.collider != null  && hit.collider.transform == this.transform)
            {
                // raycast hit this gameobject
                foreach (GameObject item in allNodes){
                    if(item.gameObject.GetComponent<Node>().start){
                        item.gameObject.GetComponent<Node>().start = false;
                        item.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
                    }
                }
                goal = false;
                start = true;
                this.GetComponent<SpriteRenderer>().color = new Color(0.4823529f,1,1);
            }
        }
        else if(Input.GetMouseButtonDown(1)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
            if(hit.collider != null  && hit.collider.transform == this.transform)
            {
                foreach (GameObject item in allNodes)
                {
                    if (item.gameObject.GetComponent<Node>().goal)
                    {
                        item.gameObject.GetComponent<Node>().goal = false;
                        item.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                    }
                }
                start = false;
                goal = true;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 0.3176471f, 0);
            }
        }

        foreach (GameObject item in allNodes)
        {
            Vector3 destination = item.transform.position - transform.position;
            if(this.gameObject != item){
                RaycastHit2D[] nodeCast = Physics2D.RaycastAll(transform.position,destination,destination.magnitude,mask);
                // Debug.DrawLine(transform.position, destination,Color.blue,5);
                bool blocked = false;
                foreach (RaycastHit2D ray in nodeCast){
                    // Debug.DrawLine(transform.position,new Vector3(ray.point.x,ray.point.y,0.0f),Color.red,5);
                    if(ray.collider.gameObject.CompareTag("Obstacle")){
                        blocked = true;
                    }
                }
                if(blocked){
                    foreach (GameObject line in allLines){
                        if((line.GetComponent<LineController>().points[0] == this.gameObject && line.GetComponent<LineController>().points[1] == item)
                        || (line.GetComponent<LineController>().points[0] == item && line.GetComponent<LineController>().points[1] == this.gameObject)
                        ){
                            GameObject.Destroy(line);
                            connected[item.gameObject.name] = false;
                        }
                    }
                }
                else
                {
                    if(!connected.ContainsKey(item.gameObject.name) || !connected[item.gameObject.name]){
                        line.addPoints(new GameObject[2] { this.gameObject, item });
                        LineController newLine = Instantiate(line, transform.position, Quaternion.identity);
                        newLine.name = this.gameObject.name + " to " + item.name;
                        connected[item.gameObject.name] = true;
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
