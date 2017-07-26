using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 /// <summary>
 /// Loop an Object in rectangular area.
 /// </summary>
[AddComponentMenu("Chazu Games/Transform Looper")]
public class GameAreaKeeper : MonoBehaviour {

    public GameArea gameArea;
    private Vector3 areaSpacePosition;

    public void Start()
    {
        if (!gameArea)
            gameArea = GameArea.Main;
    }

    // Update is called once per frame
    void FixedUpdate () {
        areaSpacePosition = gameArea.transform.InverseTransformPoint(transform.position);

        if (gameArea.Area.Contains(areaSpacePosition))
            return;

        if (areaSpacePosition.x < gameArea.Area.xMin)
            areaSpacePosition.x = gameArea.Area.xMax;
        else if (areaSpacePosition.x > gameArea.Area.xMax)
            areaSpacePosition.x = gameArea.Area.xMin;
        if (areaSpacePosition.y < gameArea.Area.yMin)
            areaSpacePosition.y = gameArea.Area.yMax;
        else if (areaSpacePosition.y > gameArea.Area.yMax)
            areaSpacePosition.y = gameArea.Area.yMin;

        transform.position = gameArea.transform.InverseTransformPoint(areaSpacePosition);
    }
}
