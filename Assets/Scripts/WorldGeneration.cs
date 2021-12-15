using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGeneration : MonoBehaviour
{
    public int mapHeight = 100, mapWidth = 1000, seed = 0, ugHeight;

    public float simpFreq;
    [Range(0, 1)] public float sfStonePb = 0.2f, ugDirtPb = 0.2f;

    public Tile dirt, stone, iron;
    public Tilemap tilemap;

    void GenerateWithSimplex() {
        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        noise.SetSeed(seed);
        noise.SetFrequency(simpFreq);

        {
            for (int w = 0; w < mapWidth; w++) {
                for (int h = 0; h < mapHeight; h++) {
                    if (h > ugHeight) {
                        if ((Helper.TSimplexRange(noise.GetNoise(w, h))) > sfStonePb)
                            tilemap.SetTile(new Vector3Int(w, h, 0), dirt);
                        else  
                            tilemap.SetTile(new Vector3Int(w, h, 0), stone);
                    }
                    else {
                        if ((Helper.TSimplexRange(noise.GetNoise(w, h))) > ugDirtPb)
                            tilemap.SetTile(new Vector3Int(w, h, 0), stone);
                        else  
                            tilemap.SetTile(new Vector3Int(w, h, 0), dirt);
                    }

                }
            }
        }
    }

    IEnumerator DebugTime() {
        float time = Time.time;
        yield return 0;
        Debug.Log(Time.time - time);
    }

    void OnGUI() {
        if(GUI.Button(new Rect(10,30,100,30), "Limpar")) {
            tilemap.ClearAllTiles();
        }
        if(GUI.Button(new Rect(10,110,100,30), "Simplex")) {
            StartCoroutine(DebugTime());
            GenerateWithSimplex();
        }
    }
}
