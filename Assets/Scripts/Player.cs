using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator playerAnimator;

    private bool playerFlipped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey ("left")){
            playerAnimator.SetBool("runSide", true);

            if (playerFlipped) { 
                // Flip Player
                Vector3 locScale = transform.localScale;
		        locScale.x *= -1;
		        transform.localScale = locScale;
                playerFlipped = false;
            }

            transform.position = new Vector2(transform.position.x - 0.07f, transform.position.y);
        }
        else if(Input.GetKey ("right")){
            playerAnimator.SetBool("runSide", true);

            if(!playerFlipped){
                // Flip Player
		        Vector3 locScale = transform.localScale;
		        locScale.x *= -1;
		        transform.localScale = locScale;
                playerFlipped = true;
            }

            transform.position = new Vector2(transform.position.x + 0.07f, transform.position.y);
        }
        else{
            playerAnimator.SetBool("runSide", false);
        }

        if(Input.GetKey ("up")){
            playerAnimator.SetBool("runUp", true);
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.07f);
        }
        else{
            playerAnimator.SetBool("runUp", false);
        }

        if(Input.GetKey ("down")){
            playerAnimator.SetBool("runDown", true);
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.07f);
        }
        else{
            playerAnimator.SetBool("runDown", false);
        }

    }

}
