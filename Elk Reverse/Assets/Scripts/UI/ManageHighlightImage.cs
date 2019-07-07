using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageHighlightImage : MonoBehaviour {

	// Use this for initialization
	public void UpdateHightlightImagePosition(HighlightImage highlightImage, GameObject destination)
    {
        highlightImage.MoveAndShowInNewButton(destination);
    }

    public void HideHightlight(HighlightImage highlightImage)
    {
        highlightImage.Hide();
    }
}
