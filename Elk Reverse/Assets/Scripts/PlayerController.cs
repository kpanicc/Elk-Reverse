using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character2D))]
public class PlayerController : MonoBehaviour {

    private Character2D character;
    private bool m_Jump = false;

	// Use this for initialization
	void Awake ()
    {
        character = GetComponent<Character2D>();	
	}
    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = Input.GetButtonDown("Jump");
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        float direction = Input.GetAxis("Horizontal");
        character.Move(direction);
        if (m_Jump)
        {
            character.Jump();
            m_Jump = false;
        }
    }
}
