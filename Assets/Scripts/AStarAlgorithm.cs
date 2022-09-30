using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarAlgorithm : MonoBehaviour
{
    public static List<int> AStar(int startNodeId, int destNodeId, List<Vector3> nodes, GameObject[] edges)
    {
        // g, h, f=g+hvalues
        List<float> gCost = new List<float>();
        List<float> hCost = new List<float>();
        List<float> fCost = new List<float>();
        List <int> open = new List<int>(); // Nodes queued up for searching
        List<int> parent = new List<int>(); // Parent of current node
        int currentNodeId = startNodeId; // Initialize the first node


        // Initialize g, f values with a built-in maximum float, h with the distances between 2 nodes
        for (int n = 0; n < nodes.Count; n++)
        {
            parent.Add(-1); //node n has no parent yet
            gCost.Add(float.MaxValue);
            fCost.Add(float.MaxValue);
            hCost.Add(distanceTo(n, destNodeId));
        }

        // startNode now has been discovered
        open.Add(startNodeId);
        gCost[startNodeId] = 0f;
        fCost[startNodeId] = gCost[startNodeId] + hCost[startNodeId];

        while (open.Count > 0)
        {
            currentNodeId = findSmallestFCost(); // find the node with smallest fCost in open list

            // return if current node is the destination
            if (currentNodeId == destNodeId) return buildPath(currentNodeId);

            // if current node has not reached destination
            open.Remove(currentNodeId); // done searching current node, remove
            List<int> neighbors = findNeighbors(currentNodeId); // Find all neighbors of current node

            foreach (int neighbor in neighbors)
            {
                float potential_gCost = gCost[currentNodeId] + distanceTo(currentNodeId, neighbor);
                if (potential_gCost < gCost[neighbor])
                {
                    parent[neighbor] = currentNodeId; // update parent list
                    gCost[neighbor] = potential_gCost;
                    fCost[neighbor] = gCost[neighbor] + hCost[neighbor];
                    if (!open.Contains(neighbor))
                    {
                        open.Add(neighbor); // Add neighbor to open if not present
                    }
                }

            }
        }
        return null; // if reaching the end of the while loop, no path has found

        // Distance between two nodes
        float distanceTo(int n1, int n2)
        {
            return (nodes[n2] - nodes[n1]).magnitude;
        }

        // find the node with smallest fCost in the open list
        int findSmallestFCost()
        {
            float smallestFCost = float.MaxValue;
            int smallestFCostNodeId = -1;

            foreach (int n in open)
            {
                gCost[n] = distanceTo(n, startNodeId);
                fCost[n] = gCost[n] + hCost[n];

                if (fCost[n] < smallestFCost)
                {
                    smallestFCost = fCost[n];
                    smallestFCostNodeId = n;
                }
            }
            return smallestFCostNodeId;
        }

        // Build the path to current vertex
        List<int> buildPath(int currentId)
        {
            List<int> path = new List<int>();
            int prevNodeId = parent[currentId];
            path.Add(currentId);

            while (prevNodeId >= 0)
            {
                path.Add(prevNodeId);
                prevNodeId = parent[prevNodeId];
            }

            return path;
        }

        // Find all the neighbors of current node
        List<int> findNeighbors(int current)
        {
            List<int> neighbors = new List<int>();

            foreach(GameObject edge in edges)
            {
                GameObject[] pointObj = edge.GetComponent<LineController>().points;
                Vector3 pointStart = pointObj[0].transform.position;
                Vector3 pointEnd = pointObj[1].transform.position;
                // endpoints 1 & 2 of an edge
                int p1 = nodes.FindIndex(p => p.x == pointStart.x && p.y == pointStart.y && p.z == pointStart.z);
                int p2 = nodes.FindIndex(p => p.x == pointEnd.x && p.y == pointEnd.y && p.z == pointEnd.z);

                if (current == p1) neighbors.Add(p2);
                else if (current == p2) neighbors.Add(p1);
            }
            return neighbors;

        }
    }



}
