using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenderType : MonoBehaviour
{
    public GameObject male;
    public GameObject female;
    public GameObject beard;
    public void MaleButton()
    {
        female.SetActive(false);
        male.SetActive(true);
        beard.SetActive(true);
    }
    public void FemaleButton()
    {        
        male.SetActive(false);
        beard.SetActive(false);
        female.SetActive(true);
    }
}
