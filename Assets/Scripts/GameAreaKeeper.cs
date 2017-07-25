using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Objectif:
 * Faire boucler la position de l'objet dans une zone rectangulaire
 */

[AddComponentMenu("Chazu Games/Transform Looper")]
public class GameAreaKeeper : MonoBehaviour {

    public GameArea gameArea;
    private Vector3 areaSpacePosition;

    // Update is called once per frame
    void Update () {
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
