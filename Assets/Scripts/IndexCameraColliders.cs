using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexCameraColliders : MonoBehaviour
{
    public int index;
    public GameObject camera;

    /// <summary>
    /// Cuando el jugador entra en el collider la cámara se posiciona según el index asignado
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            camera.GetComponent<CameraController>().MoveCamera(index);
        }
    }
}
