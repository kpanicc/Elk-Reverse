using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour {

    public float mass;
    public RigidbodyInterpolation2D interp;

    private Rigidbody2D rb2d;
    private float SpeedX = 0.0f;

    private GameObject[] children = new GameObject[2];

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.mass = mass;
        rb2d.interpolation = interp;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            children[i] = gameObject.transform.GetChild(i).gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
        SpeedX = rb2d.velocity.x;

        foreach (GameObject child in children)
        {
            Debug.Log(SpeedX);
            Animator animator = child.GetComponent<Animator>();
            if (animator != null)
                animator.SetFloat("SpeedX", SpeedX);
        }
	}
}
