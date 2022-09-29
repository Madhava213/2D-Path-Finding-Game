using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Agent;
    public GameObject Node;
    public GameObject Obstacle;

    // Ranges
    private Vector2 xPosRange = new Vector2(-8.5f,8.5f);
    private Vector2 yPosRange = new Vector2(-4.5f,4.5f);

    // Quantities
    private int numObstacles = 10;
    private int numNodes = 5;

    // Start is called before the first frame update
    void Start()
    {
        numObstacles = Random.Range(20, 25);
        numNodes = Random.Range(15, 20);

        // Generate Sprites
        for (int i = 0; i < numObstacles; i++)
        {
            Vector2 obstaclePos = new Vector2(Random.Range(xPosRange.x, xPosRange.y), Random.Range(yPosRange.x, yPosRange.y));
            GameObject obstacleObj = Instantiate(Obstacle,obstaclePos, Quaternion.identity);
            obstacleObj.name = "Obstacle " + i;
        }

        for (int i = 0; i < numNodes; i++)
        {
            Vector2 nodePos = new Vector2(Random.Range(xPosRange.x, xPosRange.y),Random.Range(yPosRange.x,yPosRange.y));
            GameObject nodeObj = Instantiate(Node,nodePos, Quaternion.identity);
            nodeObj.name = "Node " + i;
        }
        Instantiate(Agent,new Vector2(0.0f,0.0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset(){

    }
}
