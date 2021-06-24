using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Renderer playerRenderer;
    public Vector3[] cameraPositions = new Vector3[4];
    private void Start()
    {
        StartCoroutine(Starting());
    }

    /// <summary>
    /// Se mueve la cámara a la posición indicada dentro del array
    /// </summary>
    /// <param name="index">indice del array que moverá la cámara</param>
    public void MoveCamera(int index)
    {
        this.transform.position = cameraPositions[index];
    }

    IEnumerator Starting()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
