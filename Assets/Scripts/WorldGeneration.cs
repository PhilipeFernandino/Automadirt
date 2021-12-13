using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGeneration : MonoBehaviour
{
    public int mapHeight = 100, mapWidth = 1000, seed = 0;

    public float basicTerrainScale = 20f, stoneHeight = 1;
    public float ironScale = 20f, ironHeight = 1f;
    public float simplexFrequency;
    [Range(0, 1)] public float ironProbability;

    public Tile dirt, stone, iron;
    public Tilemap tilemap;
    public BoundsInt area;

    void GenerateWithNoise() {
        Random.InitState(seed);
        int maxAxis = Mathf.Max(mapHeight, mapWidth);
        
        //Gerar o mapa básico        
        {
            float offsetX = Random.Range(-1000000, 1000000);
            float offsetY = Random.Range(-1000000, 1000000);
            
            for (int w = 0; w < mapWidth; w++) {
                for (int h = 0; h < mapHeight; h++) {
                    if (PerlinNoise.GetPixel(w, h, maxAxis, maxAxis, basicTerrainScale, offsetX, offsetY) 
                    > (float) h * stoneHeight / mapHeight) 
                        tilemap.SetTile(new Vector3Int(w, h, 0), stone);
                    else  
                        tilemap.SetTile(new Vector3Int(w, h, 0), dirt);
                }
            }
        }

        //Adicionar ores
        {
            float offsetX = Random.Range(-1000000, 1000000);
            float offsetY = Random.Range(-1000000, 1000000);
            
            for (int w = 0; w < mapWidth; w++) {               
                for (int h = 0; h < mapHeight; h++) {
                    if (PerlinNoise.GetPixel(w, h, maxAxis, maxAxis, ironScale, offsetX, offsetY) 
                    > ((float) h / mapHeight * ironHeight + (1 - ironProbability))) 
                        tilemap.SetTile(new Vector3Int(w, h, 0), iron);
                }
            }
        }

    }

    void GenerateWithSimplex() {
        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        noise.SetSeed(seed);
        noise.SetFrequency(simplexFrequency);
        {
            float offsetX = Random.Range(-1000000, 1000000);
            float offsetY = Random.Range(-1000000, 1000000);
            
            for (int w = 0; w < mapWidth; w++) {
                for (int h = 0; h < mapHeight; h++) {
                    if (noise.GetNoise(w, h) > (float) h * stoneHeight / mapHeight) 
                        tilemap.SetTile(new Vector3Int(w, h, 0), stone);
                    else  
                        tilemap.SetTile(new Vector3Int(w, h, 0), dirt);
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
        if(GUI.Button(new Rect(10,70,100,30), "Perlin")) {
            StartCoroutine(DebugTime());
            GenerateWithNoise();
        }
        if(GUI.Button(new Rect(10,110,100,30), "Simplex")) {
            StartCoroutine(DebugTime());
            GenerateWithSimplex();
        }
    }
}
