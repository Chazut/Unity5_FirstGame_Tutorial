using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fit area to Camera.
/// </summary>
[AddComponentMenu("Chazu Games/Fit Area to Camera")]
[RequireComponent(typeof (GameArea))]
public class FitToCamera : MonoBehaviour {

    private GameArea Area
    {
        get { return GetComponent<GameArea>(); }
    }

    private void Awake()
    {
        FitToMainCamera();
    }

    private void FitToACamera (Camera cam)
    {
        Area.SetArea(new Vector2(cam.aspect * cam.orthographicSize * 2, cam.orthographicSize * 2));
        transform.position = cam.transform.position;
        transform.rotation = cam.transform.rotation;
    }

    private void FitToMainCamera()
    {
        FitToACamera(Camera.main);
    }

    private void Reset()
    {
        FitToMainCamera();
    }
}
