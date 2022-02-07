using UnityEngine;

public class NoisePass : MonoBehaviour {
    public bool makePass, autoUpdate, rawPass, inverse;

    public enum Colorization {
        Gradient,
        FlatColor
    }

    public Colorization colorization = Colorization.FlatColor;
    public Gradient colorGradient = new Gradient();
    public Color flatColor = Color.white;
    
    public AnimationCurve heightCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
    
    public FastNoiseLite.NoiseType noiseType;
    public FastNoiseLite.FractalType fractalType;
    public FastNoiseLite.CellularDistanceFunction cellularDistanceFunction;
    public FastNoiseLite.CellularReturnType cellularReturnType;
    public FastNoiseLite.DomainWarpType domainWarpType;
    
    public float frequency, fractalLacunarity, fractalGain, absoluteGain, colorStep;
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

                //Calculando noiseValue
                float noiseValue = Helper.TSimplexRange(noise.GetNoise(w, h));
                noiseValue += absoluteGain;
                noiseValue = noiseValue * heightCurve.Evaluate((float) h / mapHeight);
                if (inverse) noiseValue = 1 - noiseValue;

                //Aplicando textura crua ou colorida
                if (rawPass) {
                    Color color = new Color(noiseValue, noiseValue, noiseValue);
                    texture.SetPixel(w, h, color);
                } else {
                    if (colorization == Colorization.Gradient) {
                        Color color = colorGradient.Evaluate(noiseValue);
                        if (color.a > 0) texture.SetPixel(w, h, color);
                    } else if (colorization == Colorization.FlatColor) {
                        if (noiseValue > colorStep) {
                            texture.SetPixel(w, h, flatColor);
                        }
                    }
                }
            }
        }


        //Faz uma cópia da textura e salva para não precisar calcular novamente se nenhum parâmetro for alterado
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