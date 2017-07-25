using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define a rectangular Game Area.
/// </summary>
[AddComponentMenu("Chazu Games/Game Area")]
public class GameArea : MonoBehaviour {

    private Rect area = new Rect(0, 0, 10, 10);
    public Rect Area
    {
        get { return area; }
        set { area = value; }
    }
    public Vector2 size;
    public Color gizmosColor = new Color(0, 0, 1, 0.2f);
    private Color gizmosWireColor;

    public void SetArea(Vector2 size)
    {
        Area = new Rect(size.x * -0.5f, size.y * -0.5f, size.x, size.y);
    }

    private void OnValidate()
    {
        SetArea(size);
        gizmosWireColor = new Color(gizmosColor.r, gizmosColor.g, gizmosColor.b, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = gizmosColor;
        Gizmos.DrawCube(new Vector3(Area.center.x, Area.center.y, 0), new Vector3(Area.width, Area.height, 0));
        Gizmos.color = gizmosWireColor;
        Gizmos.DrawWireCube(new Vector3(Area.center.x, Area.center.y, 0), new Vector3(Area.width, Area.height, 0));
    }

}
