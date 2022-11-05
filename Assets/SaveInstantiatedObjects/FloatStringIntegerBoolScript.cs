using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;
//FOKOzuynen
public class FloatStringIntegerBoolScript : MonoBehaviour
{
   
	
	[SerializeField] private string string1  ;
	[SerializeField] private Text text1;
	[SerializeField] private string string2 ;
	[SerializeField] private Text text2;
	[SerializeField] private float healthFloat1 = 100f;
	[SerializeField] private float healthFloat2 ;
	[SerializeField] private int manaInt1;
	[SerializeField] private int manaInt2;
	[SerializeField] private bool bool1;
	[SerializeField] private bool bool2;
	[SerializeField] private Slider healthSlider;
	
	void Update()
	{
      
	   if(healthSlider != null) healthSlider.value = healthFloat1/100;
	    if(string1 != "" && text1 != null) 
		 text1.text = string1;
        
		if(text2 != null)
	    text2.text = string2; 
	  
	  
	}
	
}
