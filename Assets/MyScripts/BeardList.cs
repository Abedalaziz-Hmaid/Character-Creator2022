using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeardList : MonoBehaviour
{
    public List<GameObject> beardList;
    public Button leftButton;
    public Button rihtButton;
    private GameObject rightBeard;
    private GameObject leftBeard;

    public void CheckActiveBeardNext()
    {
        for (int i = 0; i < beardList.Capacity; i++)
        {
            if (beardList[i].activeSelf)
            {
                if (i + 1 == beardList.Capacity)
                {
                    beardList[i].SetActive(false);
                    leftBeard = beardList[0];
                    return;
                }
                leftBeard = beardList[i + 1];
            }
        }
        for (int i = 0; i < beardList.Capacity; i++)
        {
            beardList[i].SetActive(false);
        }
    }

    public void CheckActiveBeardPrevious()
    {
        for (int i = 0; i < beardList.Capacity; i++)
        {
            if (beardList[i].activeSelf)
            {
                if (i - 1 == -1)
                {
                    beardList[i].SetActive(false);
                    rightBeard = beardList[beardList.Capacity - 1];
                    return;
                }
                rightBeard = beardList[i - 1];
            }
        }
        for (int i = 0; i < beardList.Capacity; i++)
        {
            beardList[i].SetActive(false);
        }
    }

    public void RightButton()
    {
        CheckActiveBeardPrevious();
        rightBeard.SetActive(true);
    }

    public void LeftButton()
    {
        CheckActiveBeardNext();
        leftBeard.SetActive(true);
    }
}
