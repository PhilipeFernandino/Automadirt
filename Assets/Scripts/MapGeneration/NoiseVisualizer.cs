using UnityEngine;
public class NoiseVisualizer : MonoBehaviour {
    public int mapHeight, mapWidth;

    private Renderer meshRenderer;  

    private void Start() {
        meshRenderer = GetComponent<Renderer>();
    }

    public void GenerateNoiseTexture() {
        Texture2D texture = new Texture2D(mapWidth, mapHeight);
        
        bool noisePassQueueIsDirty = false;

        int i = 0;
        foreach(var noisePass in GetComponents<NoisePass>()) {

            if (noisePass.isDirty)  noisePassQueueIsDirty = true;
            
            if (noisePassQueueIsDirty) noisePass.MakePass(texture, mapWidth, mapHeight);
            else texture = noisePass.GetLastTexture();
            i++;
        }

        texture.filterMode = FilterMode.Point;
        texture.Apply();
        meshRenderer.material.mainTexture = texture;
    }
}
