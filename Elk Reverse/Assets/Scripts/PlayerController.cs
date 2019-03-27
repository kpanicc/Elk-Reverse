using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character2D))]
public class PlayerController : MonoBehaviour {

    private Character2D character;

	// Use this for initialization
	void Awake ()
    {
        character = GetComponent<Character2D>();	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float direction = Input.GetAxis("Horizontal");
        character.Move(direction);
    }
}
