using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairColors : MonoBehaviour
{
    [Header("Material")]
    public Material mat;
    public SkinnedMeshRenderer[] hairColors;

    public void HairColorBtn1()
    {
        mat = Resources.Load("Material/HairColor1", typeof(Material)) as Material;
        for (int i = 0; i < hairColors.Length; i++)
        {
            hairColors[i].material = mat;
        }
    }
    public void HairColorBtn2()
    {
        mat = Resources.Load("Material/HairColor2", typeof(Material)) as Material;
        for (int i = 0; i < hairColors.Length; i++)
        {
            hairColors[i].material = mat;
        }
    }
    public void HairColorBtn3()
    {
        mat = Resources.Load("Material/HairColor3", typeof(Material)) as Material;
        for (int i = 0; i < hairColors.Length; i++)
        {
            hairColors[i].material = mat;
        }
    }
    public void HairColorBtn4()
    {
        mat = Resources.Load("Material/HairColor4", typeof(Material)) as Material;
        for (int i = 0; i < hairColors.Length; i++)
        {
            hairColors[i].material = mat;
        }
    }
}
