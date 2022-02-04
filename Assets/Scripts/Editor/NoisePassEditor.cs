using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(NoisePass))]
public class NoisePassEditor : Editor {
    public override void OnInspectorGUI() {
        NoisePass noisePass = (NoisePass) target;
        noisePass.makePass = EditorGUILayout.Toggle("Make Pass", noisePass.makePass);

        using (new EditorGUI.DisabledScope(!noisePass.makePass)) {

            noisePass.autoUpdate = EditorGUILayout.Toggle("Auto Update", noisePass.autoUpdate);
            noisePass.rawPass = EditorGUILayout.Toggle("Raw Pass" , noisePass.rawPass);
            noisePass.inverse = EditorGUILayout.Toggle("Inverse" , noisePass.inverse);
            
            EditorGUILayout.Space();

            noisePass.seed = EditorGUILayout.IntField("Seed", noisePass.seed);
            noisePass.frequency = EditorGUILayout.FloatField("Frequency", noisePass.frequency);
            noisePass.absoluteGain = EditorGUILayout.FloatField("Absolute Gain", noisePass.absoluteGain);
            noisePass.heightGradient = EditorGUILayout.GradientField("Height Gradient", noisePass.heightGradient);

            using (new EditorGUI.DisabledScope(noisePass.rawPass)) {
                noisePass.colorGradient = EditorGUILayout.GradientField("Color Gradient", noisePass.colorGradient);
            }

            EditorGUILayout.Space();
            
            noisePass.noiseType = (FastNoiseLite.NoiseType) EditorGUILayout.EnumPopup("Noise Type", noisePass.noiseType);
            if (noisePass.noiseType == FastNoiseLite.NoiseType.Cellular) {
                noisePass.cellularDistanceFunction = (FastNoiseLite.CellularDistanceFunction) EditorGUILayout.EnumPopup("Cellular Distance Function",  noisePass.cellularDistanceFunction);
                noisePass.cellularReturnType = (FastNoiseLite.CellularReturnType) EditorGUILayout.EnumPopup("Cellular Return Type", noisePass.cellularReturnType);
            }

            EditorGUILayout.Space();


            noisePass.fractalType = (FastNoiseLite.FractalType) EditorGUILayout.EnumPopup("Fractal Type", noisePass.fractalType);

            if (noisePass.fractalType == FastNoiseLite.FractalType.DomainWarpProgressive 
                || noisePass.fractalType == FastNoiseLite.FractalType.DomainWarpProgressive) {
                    noisePass.domainWarpType = (FastNoiseLite.DomainWarpType) EditorGUILayout.EnumPopup("Domain Warp Type", noisePass.domainWarpType);
            }

            if (noisePass.fractalType != FastNoiseLite.FractalType.None) {
                noisePass.fractalOctaves = EditorGUILayout.IntField("Fractal Octaves", noisePass.fractalOctaves);
                noisePass.fractalGain = EditorGUILayout.FloatField("Fractal Gain", noisePass.fractalGain);
                noisePass.fractalLacunarity = EditorGUILayout.FloatField("Fractal Lacunarity", noisePass.fractalLacunarity);
            }
        }
        if(GUI.changed) noisePass.OnValidate();
    }
}
