using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (Input.GetButtonDown("Cancel"))
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        


        // Lantern control
        if (Input.GetButtonDown("LanternRight"))
        {
            character.LanternRight();
        }
        else if(Input.GetButtonUp("LanternRight"))
        {
            character.LanternRightUp();
        }
        if (Input.GetButtonDown("LanternLeft"))
        {
            character.LanternLeft();
        }
        else if (Input.GetButtonUp("LanternLeft"))
        {
            character.LanternLeftUp();
        }
        if (Input.GetButtonDown("LanternUp"))
        {
            character.LanternHeight(1);
        }
        else if (Input.GetButtonUp("LanternUp"))
        {
            character.LanternHeight(0);
        }
        if (Input.GetButtonDown("LanternDown"))
        {
            character.LanternHeight(-1);
        }
        else if (Input.GetButtonUp("LanternDown"))
        {
            character.LanternHeight(0);
        }


        // Jump control
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
