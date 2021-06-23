using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Renderer playerRenderer;
    public Vector3[] cameraPositions = new Vector3[4];
    //private int indexPosition = 0;
    //private bool cameraCanMove;

    private void Start()
    {
        StartCoroutine(Starting());
    }
    /*
    void Update()
    {
        if (!playerRenderer.isVisible && cameraCanMove)
        {
            MoveCamera();
        }
    }*/

    public void MoveCamera(int index)
    {
        this.transform.position = cameraPositions[index];
    }

    IEnumerator Starting()
    {
        yield return new WaitForSeconds(1.0f);
        //cameraCanMove = true;
    }
}
