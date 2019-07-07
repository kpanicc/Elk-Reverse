using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightImage : MonoBehaviour {

    [SerializeField]
    public HoverableButton lastHoveredButton;
	// Use this for initialization
	void Awake () {
        gameObject.SetActive(false);
	}

    public void MoveAndShowInNewButton(GameObject destination)
    {
        Vector3 oldButtonPosition = lastHoveredButton.transform.position;
        Vector3 newButtonPosition = destination.transform.position;

        Vector3 offset = newButtonPosition - oldButtonPosition;

        Vector3 currentPosition = transform.position;
        currentPosition.y += offset.y;

        this.transform.position = currentPosition;

        lastHoveredButton = destination.GetComponent<HoverableButton>();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
