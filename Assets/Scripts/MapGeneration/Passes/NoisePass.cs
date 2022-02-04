using UnityEngine;

public abstract class NoisePass : MonoBehaviour {
    public bool makePass;
    public abstract void MakePass(Texture2D texture, int mapWidth, int mapHeight);
}