using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairList : MonoBehaviour
{
    public List<GameObject> hairList;
    public Button leftButton;
    public Button rihtButton;
    private GameObject rightHair;
    private GameObject leftHair;

    public void CheckActiveHairNext()
    {
        for (int i = 0; i < hairList.Capacity; i++)
        {
            if (hairList[i].activeSelf)
            {
                if (i + 1 == hairList.Capacity)
                {
                    hairList[i].SetActive(false);
                    leftHair = hairList[0];
                    return;
                }
                leftHair = hairList[i + 1];
            }
        }
        for (int i = 0; i < hairList.Capacity; i++)
        {
            hairList[i].SetActive(false);
        }
    }

    public void CheckActiveHairPrevious()
    {
        for (int i = 0; i < hairList.Capacity; i++)
        {
            if (hairList[i].activeSelf)
            {
                if (i - 1 == -1)
                {
                    hairList[i].SetActive(false);
                    rightHair = hairList[hairList.Capacity - 1];
                    return;
                }
                rightHair = hairList[i - 1];
            }
        }
        for (int i = 0; i < hairList.Capacity; i++)
        {
            hairList[i].SetActive(false);
        }
    }

    public void RightButton()
    {
        CheckActiveHairPrevious();
        rightHair.SetActive(true);
    }

    public void LeftButton()
    {
        CheckActiveHairNext();
        leftHair.SetActive(true);
    }
}