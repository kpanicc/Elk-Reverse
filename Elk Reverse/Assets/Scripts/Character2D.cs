using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : MonoBehaviour {

    private float maxSpeed = 8f;

    private Animator animator;
    private Rigidbody2D rb2d;
    private bool facingLeft = true;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(float direction)
    {
        animator.SetFloat("SpeedX", Mathf.Abs(direction));
        //Output the current Animation name and length to the screen
        rb2d.velocity = new Vector2(direction * maxSpeed, rb2d.velocity.y);

        if (facingLeft && direction > 0)
        {
            Flip();
        }
        else if (!facingLeft && direction < 0)
        {
            Flip();
        }
    }

    private void OnGUI()
    {

    }

    public void Flip()
    {
        facingLeft = !facingLeft;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
