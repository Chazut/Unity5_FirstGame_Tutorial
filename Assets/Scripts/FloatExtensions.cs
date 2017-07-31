using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class FloatExtensions {

     static public float LinearToDecibel(this float linear)
    {
        float dB;
        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;
        return dB;
    }

    static public float DecibelToLinear(this float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20.0f);
        return linear;
    }

}
