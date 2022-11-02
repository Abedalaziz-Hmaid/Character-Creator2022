using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Character Assets")]
public class AssetSO : ScriptableObject
{
    public Color[] SkinColor;

    public Mesh[] Belts;
    public Mesh[] Clothes;
    public Mesh[] Gloves;
    public Mesh[] Hair;
    public Mesh[] Shose;
    public Mesh[] Faces;
    public Mesh[] ShoulderPads;
}
