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
        if (Input.GetButtonDown("UseLantern"))
        {
            character.EnableLantern();
            character.SetLanternHeight(0);
        }
        else if (Input.GetButtonUp("UseLantern"))
        {
            character.DisableLantern();
        }

        if (Input.GetButtonDown("LanternUp"))
        {
            character.SetLanternHeight(1);
        }
        else if (Input.GetButtonUp("LanternUp"))
        {
            character.SetLanternHeight(0);
        }

        if (Input.GetButtonDown("LanternDown"))
        {
            character.SetLanternHeight(-1);
        }
        else if (Input.GetButtonUp("LanternDown"))
        {
            character.SetLanternHeight(0);
        }

        if (Input.GetButtonDown("GrabWagon"))
        {
            character.GrabWagon();
        }
        else if (Input.GetButtonUp("GrabWagon"))
        {
            character.ReleaseWagon();
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
