using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceList : MonoBehaviour
{
    public List<GameObject> faceList;
    public List<GameObject> faceListFemale;
    public Button leftButton;
    public Button rihtButton;
    private GameObject rightFace;
    private GameObject rightFaceFemale;   
    private GameObject leftFace;
    private GameObject leftFaceFemale;
    public void CheckActiveFaceNext()
    {
        for ( int i = 0 ; i < faceList.Capacity; i++)
        {
            if (faceList[i].activeSelf)
            {
                if(i + 1 == faceList.Capacity) {
                    faceList[i].SetActive(false);
                    faceListFemale[i].SetActive(false);
                    leftFace = faceList[0];
                    leftFaceFemale = faceListFemale[0];
                    return ; 
                }
                    leftFace = faceList[i + 1];
                    leftFaceFemale = faceListFemale[i + 1];
            }
        }
        for (int i = 0; i < faceList.Capacity; i++)
        {
            faceList[i].SetActive(false);
            faceListFemale[i].SetActive(false);
        }  
    }
    
    public void CheckActiveFacePrevious()
    {
        for ( int i = 0 ; i < faceList.Capacity; i++)
        {
            if (faceList[i].activeSelf)
            {
                if (i - 1 == -1 ) {
                    faceList[i].SetActive(false);
                    faceListFemale[i].SetActive(false);
                    rightFace = faceList[faceList.Capacity-1];
                    rightFaceFemale = faceListFemale[faceListFemale.Capacity-1];
                    return; 
                }
                    rightFace = faceList[i - 1];
                rightFaceFemale = faceListFemale[i - 1];
            }
        }
        for (int i = 0; i < faceList.Capacity; i++)
        {
            faceList[i].SetActive(false);
            faceListFemale[i].SetActive(false);
        }  
    }

    public void RightButton()
    {
        CheckActiveFacePrevious();
        rightFace.SetActive(true);
        rightFaceFemale.SetActive(true);
    }

    public void LeftButton()
    {
        CheckActiveFaceNext();
        leftFace.SetActive(true);
        leftFaceFemale.SetActive(true);
    }
}
