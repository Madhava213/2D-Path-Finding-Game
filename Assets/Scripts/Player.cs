using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator playerAnimator;

    private bool playerFlipped = false;
    public bool changing = false;
    public GameObject gameManager;
    GameManager gameManagerScript;
    GameObject[] allNodes;

    public int nextNode = 1;
    private Vector3 nextNodePos;
    private void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
            allNodes = GameObject.FindGameObjectsWithTag("Node");
        if (gameManagerScript.startNode != gameManagerScript.goalNode)
        {

            if (nextNodePos != null && !changing)
            {
                if (nextNodePos.x < transform.position.x)
                {
                    playerAnimator.SetBool("runSide", true);
                    if (playerFlipped)
                    {
                        // Flip Player
                        Vector3 locScale = transform.localScale;
                        locScale.x *= -1;
                        transform.localScale = locScale;
                        playerFlipped = false;
                    }
                }
                else if (nextNodePos.x > transform.position.x)
                {
                    playerAnimator.SetBool("runSide", true);
                    if(!playerFlipped){
                        // Flip Player
                        Vector3 locScale = transform.localScale;
                        locScale.x *= -1;
                        transform.localScale = locScale;
                        playerFlipped = true;
                    }
                }
                else
                {
                    if (playerFlipped)
                    {
                        // Flip Player
                        Vector3 locScale = transform.localScale;
                        locScale.x *= -1;
                        transform.localScale = locScale;
                        playerFlipped = false;
                    }
                    playerAnimator.SetBool("runSide", false);
                }

                if (nextNodePos.y > transform.position.y)
                {
                    playerAnimator.SetBool("runUp", true);
                }
                else
                {
                    playerAnimator.SetBool("runUp", false);
                }

                if (nextNodePos.y < transform.position.y)
                {
                    playerAnimator.SetBool("runDown", true);
                }
                else
                {
                    playerAnimator.SetBool("runDown", false);
                }

                if(nextNodePos == transform.position){
                    playerAnimator.SetBool("runSide", false);
                    playerAnimator.SetBool("runUp", false);
                    playerAnimator.SetBool("runDown", false);
                }

                // Player Movement
                if (nextNode == 1)
                {
                    nextNodePos = allNodes[gameManagerScript.pathNodes[nextNode]].transform.position;
                }
                if (nextNode < gameManagerScript.pathNodes.Count - 1 && transform.position == nextNodePos)
                {
                    nextNode++;
                    nextNodePos = allNodes[gameManagerScript.pathNodes[nextNode]].transform.position;
                }
                transform.position = Vector3.MoveTowards(transform.position, nextNodePos, 1.0f * Time.deltaTime);
            }
        }

    }

}
