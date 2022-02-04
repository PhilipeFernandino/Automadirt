using UnityEngine;

public static class PerlinNoise {
    public static Texture2D GenerateTexture(int width, int height, float scale) {
        Texture2D texture = new Texture2D(width, height);
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Color color = CalculateColor(x, y, scale, width, height);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    public static float GetPixel(int x, int y, int width, int height, float scale, float offsetX = 0f, float offsetY = 0f) {
        float xCoord = (float) x / width * scale + offsetX;
        float yCoord = (float) y / height * scale + offsetY;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    static Color CalculateColor(int x, int y, float scale, int width, int height) {
        float xCoord = (float) x / width * scale;
        float yCoord = (float) y / height * scale;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }

}