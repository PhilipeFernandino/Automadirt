using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {
    public static float TransformRange(float value, float oldMin, float oldMax, float newMin, float newMax) {
        return (((value - oldMin) * (newMax - newMin)) / (oldMax - oldMin)) + newMin;
    }
    public static float TSimplexRange(float value) {
        return ((value + 1) / 2);
    }   
}