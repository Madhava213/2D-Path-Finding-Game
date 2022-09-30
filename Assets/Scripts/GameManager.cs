using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameObjects
    public GameObject Agent;
    public GameObject Node;
    public GameObject Obstacle;

    // Algorithm Variables
    public AStarAlgorithm AStarAlgorithm;
    private List<int> pathNodes;
    private int startNode;
    private int goalNode;

    // Ranges
    private Vector2 xPosRange = new Vector2(-8.5f,8.5f);
    private Vector2 yPosRange = new Vector2(-4.5f,4.5f);

    // Quantities
    private int numObstacles = 10;
    private int numNodes = 5;

    // Start is called before the first frame update
    void Start()
    {
        AStarAlgorithm = this.gameObject.GetComponent<AStarAlgorithm>();
        InstantiateScene();
    }

    void InstantiateScene(){
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
        // RESET GAME
        if (Input.GetKeyDown(KeyCode.R)){
            Reset();
        }

        GameObject[] allNodes = GameObject.FindGameObjectsWithTag("Node");
        List<Vector3> allNodePositions = new List<Vector3>();
        GameObject[] allLines = GameObject.FindGameObjectsWithTag("Line");

        for (int i = 0; i < allNodes.Length; i++)
        {
            allNodePositions.Add(allNodes[i].transform.position);
            if(allNodes[i].gameObject.GetComponent<Node>().start){
                startNode = i;
            }
            else if(allNodes[i].gameObject.GetComponent<Node>().goal)
                goalNode = i;
        }

        if(startNode != goalNode){
            Debug.Log(startNode + " " + goalNode);
            pathNodes = AStarAlgorithm.AStar(startNode, goalNode, allNodePositions, allLines);

            foreach (GameObject node in allNodes)
            {
                node.GetComponent<Node>().path = false;
            }
            foreach (int nodeIndex in pathNodes)
            {
                Debug.Log(nodeIndex);
                allNodes[nodeIndex].GetComponent<Node>().path = true;
            }
        }
    }

    public void Reset(){
        GameObject[] allNodes = GameObject.FindGameObjectsWithTag("Node");
        GameObject[] allLines = GameObject.FindGameObjectsWithTag("Line");
        GameObject[] allObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject item in allNodes)
        {
            GameObject.Destroy(item);
        }
        foreach (GameObject item in allLines)
        {
            GameObject.Destroy(item);
        }
        foreach (GameObject item in allObstacles)
        {
            GameObject.Destroy(item);
        }
        foreach (GameObject item in allPlayers)
        {
            GameObject.Destroy(item);
        }
        InstantiateScene();
    }
}
