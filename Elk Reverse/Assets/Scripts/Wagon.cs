using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour {

    public float mass;
    public RigidbodyInterpolation2D interp;
	// Use this for initialization
	void Start () {
        var rb2d = GetComponent<Rigidbody2D>();
        rb2d.mass = mass;
        rb2d.interpolation = interp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
