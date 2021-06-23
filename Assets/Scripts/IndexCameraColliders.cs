using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexCameraColliders : MonoBehaviour
{
    public int index;
    public GameObject camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            camera.GetComponent<CameraController>().MoveCamera(index);
        }
    }
}
