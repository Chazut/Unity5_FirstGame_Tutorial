using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define a rectangular Game Area.
/// </summary>
[AddComponentMenu("Chazu Games/Game Area")]
public class GameArea : MonoBehaviour {

    [HideInInspector]
    [SerializeField]
    private Rect area = new Rect(0, 0, 10, 10);
    public Rect Area
    {
        get { return area; }
        set { area = value; }
    }
    public Color gizmosColor = new Color(0, 0, 1, 0.2f);
    private Color gizmosWireColor;

    static private GameArea _main;
    static public GameArea Main
    {
        get
        {
            if (!_main)
            {
                _main = (GameArea) GameObject.FindObjectOfType(typeof (GameArea));
                if (!_main)
                {
                    GameObject go = new GameObject("Game Area : Main");
                    _main = go.AddComponent<GameArea>();
                    go.AddComponent<FitToCamera>();
                }
            }
            return _main;
        }
        set
        {
            _main = value;
        }
    }

    public Vector2 size;
    public Vector2 Size
    {
        get
        {
            return Area.size;
        }
        set
        {
            Area = new Rect(value.x * -0.5f, value.y * -0.5f, value.x, value.y);
        }
    }

    private void OnValidate()
    {
        Size = size;
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

    public Vector3 GetRandomPosition()
    {
        Vector3 vector3 = Vector3.zero;
        vector3.x = Random.Range(Area.xMin, Area.xMax);
        vector3.y = Random.Range(Area.yMin, Area.yMax);
        vector3 = transform.TransformPoint(vector3);
        return vector3;
    }

}
