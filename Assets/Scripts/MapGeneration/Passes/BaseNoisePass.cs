using UnityEngine;
using System.Collections.Generic;

public class BaseNoisePass : NoisePass {
    public float simplexFrequency;
    public List<ColorBetween> colorList;
    public FastNoiseLite.NoiseType noiseType = FastNoiseLite.NoiseType.OpenSimplex2;
    public int seed;
    
    public override void MakePass(Texture2D texture, int mapWidth, int mapHeight) {
        FastNoiseLite noise = new FastNoiseLite(seed);
        noise.SetNoiseType(noiseType);
        noise.SetFrequency(simplexFrequency);

        for (int w = 0; w < mapWidth; w++) {
            for (int h = 0; h < mapHeight; h++) {
                for (int cl = 0; cl < colorList.Count; cl ++) {
                    float noiseValue = Helper.TSimplexRange(noise.GetNoise(w, h));
                    if (noiseValue > colorList[cl].min && noiseValue < colorList[cl].max) {
                        texture.SetPixel(w, h, colorList[cl].color);
                    }
                }
            }
        }
    }
}
