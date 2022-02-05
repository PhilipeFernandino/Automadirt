using UnityEngine;

public class NoisePass : MonoBehaviour {
    public bool makePass, autoUpdate, rawPass, inverse;

    public Gradient colorGradient, heightGradient;
    public FastNoiseLite.NoiseType noiseType;
    public FastNoiseLite.FractalType fractalType;
    public FastNoiseLite.CellularDistanceFunction cellularDistanceFunction;
    public FastNoiseLite.CellularReturnType cellularReturnType;
    public FastNoiseLite.DomainWarpType domainWarpType;
    
    public float frequency, fractalLacunarity, fractalGain, absoluteGain;
    public int fractalOctaves, seed;
    
    public bool isDirty = true;
    private NoiseVisualizer noiseVisualizer;
    private Texture2D lastTexture;

    private void OnEnable() {
        noiseVisualizer = GetComponent<NoiseVisualizer>();
        Debug.Assert(noiseVisualizer != null, "Noise visualizer não encontrado");
    }

    public Texture2D GetLastTexture() {
        Texture2D texture = new Texture2D(lastTexture.width, lastTexture.height);
        texture.SetPixels(lastTexture.GetPixels());
        return texture;
    }

    public void MakePass(Texture2D texture, int mapWidth, int mapHeight) {
        
        if (!makePass) return;

        FastNoiseLite noise = new FastNoiseLite(seed);
        noise.SetNoiseType(noiseType);
        noise.SetFractalType(fractalType);        
        noise.SetCellularDistanceFunction(cellularDistanceFunction);
        noise.SetDomainWarpType(domainWarpType);
        noise.SetFractalLacunarity(fractalLacunarity);
        noise.SetFractalGain(fractalGain);
        noise.SetFrequency(frequency);

        for (int w = 0; w < mapWidth; w++) {
            for (int h = 0; h < mapHeight; h++) {
                float noiseValue = Helper.TSimplexRange(noise.GetNoise(w, h));
                noiseValue += absoluteGain;
                noiseValue = noiseValue * heightGradient.Evaluate( (float) h / mapHeight).grayscale;
                if (inverse) noiseValue = 1 - noiseValue;

                if (rawPass) {
                    Color color = new Color(noiseValue, noiseValue, noiseValue);
                    texture.SetPixel(w, h, color);
                } else {
                    Color color = colorGradient.Evaluate(noiseValue);
                    if (color.a > 0) texture.SetPixel(w, h, color);
                }
            }
        }

        lastTexture = new Texture2D(texture.width, texture.height);
        lastTexture.SetPixels(texture.GetPixels());

        isDirty = false;
    }

    public void OnValidate() {
        isDirty = true;
        if (autoUpdate) {
            noiseVisualizer.GenerateNoiseTexture(); 
        }
    }
}