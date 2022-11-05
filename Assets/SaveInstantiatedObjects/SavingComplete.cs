using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//FOKOzuynen
public class SavingComplete : MonoBehaviour
{
    [SerializeField] private Text uitext;
    
    float countTimer;
    void Update()
    {
        TextSave();
    }
    public void TextSave()
    {
        countTimer += Time.deltaTime;
        
        if(countTimer >=1f)uitext.text = "Save Complete" ;
        
        if(countTimer >=2f)
        {
            gameObject.SetActive(false) ;
            countTimer = 0;
            uitext.text = "";
        }
    }
}
