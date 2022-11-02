using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinColors : MonoBehaviour
{
    [Header("Material")]
    public Material mat;
    public SkinnedMeshRenderer[] skinColor;

    public void SkinMat1Btn()
    {
       mat =  Resources.Load("Material/SkinMat1", typeof(Material)) as Material;
        for (int i = 0; i < skinColor.Length; i++)
        {
            skinColor[i].material = mat;
        }
    }
    public void SkinMat2Btn()
    {
       mat =  Resources.Load("Material/SkinMat2", typeof(Material)) as Material;
        for (int i = 0; i < skinColor.Length; i++)
        {
            skinColor[i].material = mat;
        }
    }
    public void SkinMat3Btn()
    {
       mat =  Resources.Load("Material/SkinMat3", typeof(Material)) as Material;
        for (int i = 0; i < skinColor.Length; i++)
        {
            skinColor[i].material = mat;
        }
    }
}
