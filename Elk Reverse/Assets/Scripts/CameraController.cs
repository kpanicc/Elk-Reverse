using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    public bool m_followPlayerYAxis;
    public bool m_associatedObjectsFollowPlayerYAxis;
    public GameObject[] associatedObjects;
    public Vector3[] associatedObjectsOffset;
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;

        associatedObjectsOffset = new Vector3[associatedObjects.Length];
        for (int i = 0; i < associatedObjects.Length; i++)
        {
            associatedObjectsOffset[i] = associatedObjects[i].transform.position - player.transform.position;
        }
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        Vector3 cameraPosition = transform.position;
        Vector3 playerPosition = player.transform.position;

        cameraPosition.x = player.transform.position.x + offset.x;
        cameraPosition.z = player.transform.position.z + offset.z;

        if (m_followPlayerYAxis)
            cameraPosition.y = player.transform.position.y + offset.y;

        transform.position = cameraPosition;

        for (int i = 0; i < associatedObjects.Length; i++)
        {
            Vector3 associatedObjectPosition = associatedObjects[i].transform.position;

            associatedObjectPosition.x = playerPosition.x + associatedObjectsOffset[i].x;
            associatedObjectPosition.z = playerPosition.z + associatedObjectsOffset[i].z;

            if (m_associatedObjectsFollowPlayerYAxis)
                associatedObjectPosition.y = playerPosition.y + associatedObjectsOffset[i].y;

            associatedObjects[i].transform.position = associatedObjectPosition;
        }
    }
    private void OnGUI()
    {

    }
}
