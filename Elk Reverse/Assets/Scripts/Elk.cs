using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elk : MonoBehaviour {

    private float speed = 0.0F;

    private Animator anim;
    private Rigidbody2D rb2d;
                                            // Use this for initialization
    void Start ()
    {
        //Get reference to the Animator component attached to this GameObject.
        anim = GetComponent<Animator>();
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
