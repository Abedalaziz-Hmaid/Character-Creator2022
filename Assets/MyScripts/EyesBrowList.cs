using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyesBrowList : MonoBehaviour
{
    public List<GameObject> eyesBrowList;
    public Button leftButton;
    public Button rihtButton;
    private GameObject rightEyesBrow;
    private GameObject leftEyesBrow;

    public void CheckActiveEyesBrowNext()
    {
        for (int i = 0; i < eyesBrowList.Capacity; i++)
        {
            if (eyesBrowList[i].activeSelf)
            {
                if (i + 1 == eyesBrowList.Capacity)
                {
                    eyesBrowList[i].SetActive(false);
                    leftEyesBrow = eyesBrowList[0];
                    return;
                }
                leftEyesBrow = eyesBrowList[i + 1];
            }
        }
        for (int i = 0; i < eyesBrowList.Capacity; i++)
        {
            eyesBrowList[i].SetActive(false);
        }
    }

    public void CheckActiveEyesBrowPrevious()
    {
        for (int i = 0; i < eyesBrowList.Capacity; i++)
        {
            if (eyesBrowList[i].activeSelf)
            {
                if (i - 1 == -1)
                {
                    eyesBrowList[i].SetActive(false);
                    rightEyesBrow = eyesBrowList[eyesBrowList.Capacity - 1];
                    return;
                }
                rightEyesBrow = eyesBrowList[i - 1];
            }
        }
        for (int i = 0; i < eyesBrowList.Capacity; i++)
        {
            eyesBrowList[i].SetActive(false);
        }
    }

    public void RightButton()
    {
        CheckActiveEyesBrowPrevious();
        rightEyesBrow.SetActive(true);
    }

    public void LeftButton()
    {
        CheckActiveEyesBrowNext();
        leftEyesBrow.SetActive(true);
    }
}
