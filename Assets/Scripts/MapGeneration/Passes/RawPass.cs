using UnityEngine;

public class RawPass : NoisePass {
    public float frequency;
    public bool inverse = false;

    public FastNoiseLite.NoiseType noiseType;
    public FastNoiseLite.FractalType fractalType;
    public FastNoiseLite.CellularDistanceFunction cellularDistanceFunction;
    public FastNoiseLite.CellularReturnType cellularReturnType;
    public FastNoiseLite.DomainWarpType domainWarpType;

    public float fractalLacunarity = 0f, fractalGain = 0f, absoluteGain = 0f;
    public int fractalOctaves = 0, seed = 0;
    
    public override void MakePass(Texture2D texture, int mapWidth, int mapHeight) {
        
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
                if (inverse) noiseValue = 1 - noiseValue;
                Color color = new Color(noiseValue, noiseValue, noiseValue);
                texture.SetPixel(w, h, color);
            }
        }
    }

    private void OnValidate() {
        GetComponent<NoiseVisualizer>().GenerateNoiseTexture(); 
    }
}