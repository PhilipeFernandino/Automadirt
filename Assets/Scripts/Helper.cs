using UnityEngine;

public static class Helper {
    public static float TransformRange(float value, float oldMin, float oldMax, float newMin, float newMax) {
        return (((value - oldMin) * (newMax - newMin)) / (oldMax - oldMin)) + newMin;
    }
    public static float TSimplexRange(float value) {
        return ((value + 1) / 2);
    }   
    public static bool IsVec2IntInBoundaries(Vector2Int pos, int xMin, int yMin, int xMax, int yMax) {
        if (pos.x < xMin || pos.x > xMax || pos.y < yMin || pos.y > yMax) return false;
        return true;
    }
}