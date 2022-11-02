using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Customiser : MonoBehaviour
{
    public AssetSO Assets;

    public SkinnedMeshRenderer Belt; 
    public SkinnedMeshRenderer Clothes;
    public SkinnedMeshRenderer Gloves;
    public SkinnedMeshRenderer Hair;
    public SkinnedMeshRenderer Shose;
    public SkinnedMeshRenderer Faces;
    public SkinnedMeshRenderer ShoulderPads;

    public void Randomise()
    {
        Belt.sharedMesh = Assets.Belts[Random.Range(0, Assets.Belts.Length)];
        Clothes.sharedMesh = Assets.Clothes[Random.Range(0, Assets.Clothes.Length)];
        Gloves.sharedMesh = Assets.Gloves[Random.Range(0, Assets.Gloves.Length)];
        Hair.sharedMesh = Assets.Hair[Random.Range(0, Assets.Hair.Length)];
        Shose.sharedMesh = Assets.Shose[Random.Range(0, Assets.Shose.Length)];
        Faces.sharedMesh = Assets.Faces[Random.Range(0, Assets.Faces.Length)];
        ShoulderPads.sharedMesh = Assets.ShoulderPads[Random.Range(0, Assets.ShoulderPads.Length)];
    }

}

[CustomEditor(typeof(Customiser))]
public class CustomiserEditor : Editor 
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        var customiser = (Customiser)target;

        if (customiser != null)
        {
            if (GUILayout.Button("Randomise"))
            {
                customiser.Randomise();
            }
        }
    }
}
