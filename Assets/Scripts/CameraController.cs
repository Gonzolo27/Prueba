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
    /// Se mueve la c�mara a la posici�n indicada dentro del array
    /// </summary>
    /// <param name="index">indice del array que mover� la c�mara</param>
    public void MoveCamera(int index)
    {
        this.transform.position = cameraPositions[index];
    }

    IEnumerator Starting()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
