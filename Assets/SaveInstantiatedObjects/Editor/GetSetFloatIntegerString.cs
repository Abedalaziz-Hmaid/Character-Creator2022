using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
//FOKOzuynen
[ExecuteAlways]
[CustomEditor (typeof(SaveInstaObjValues),true)]
public class GetSetFloatIntegerString : Editor
{
	string fieldGet;
	string fieldSet;
	
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI(); // this is need to see the main script for not creating other drawers and not making more fields public
		SaveInstaObjValues script =  (SaveInstaObjValues)target ;
		
		GUILayout.Label("If used independent then use iMode");
		script.soloMode = GUILayout.Toggle(script.soloMode, "iMode");
     
        if(script.soloMode)
		{
			script.savetheValue = EditorGUILayout. Toggle ("Save the Value",false);
	        script.loadtheValue = EditorGUILayout. Toggle ("Load the Value",script.loadtheValue.Equals(true));
			script.uniqueID =  EditorGUILayout.TextField("Unique ID","edcba");
		}
		
		
		if(script.typeOfValue == SaveInstaObjValues.saveValueType.m_Bool && script.m_ComponentValueGet != null)
		{
		    fieldGet = "Get Bool";
			fieldSet = "Set Bool";
		}
		if(script.typeOfValue == SaveInstaObjValues.saveValueType.m_Float && script.m_ComponentValueGet != null)
		{
		   fieldGet = "Get Float";
		   fieldSet = "Set Float";
		}
		if(script.typeOfValue == SaveInstaObjValues.saveValueType.m_Integer && script.m_ComponentValueGet != null)
		{
		    fieldGet = "Get Integer";
			fieldSet = "Set Integer";
		}
		if(script.typeOfValue == SaveInstaObjValues.saveValueType.m_String && script.m_ComponentValueGet != null)
		{
		   fieldGet = "Get String";
		   fieldSet = "Set String";
		}
			if(script.m_ComponentValueGet != null)
			{
				//create the dropdown Get
				GUILayout.BeginHorizontal();	
				GUIContent comGet = new	GUIContent(fieldGet);
				script.intValueIndexGet = EditorGUILayout.Popup(comGet,script.intValueIndexGet,script.stringValueGet);
				GUILayout.EndHorizontal();
			
			    //create the dropdown Set
				GUILayout.BeginHorizontal();	
				GUIContent comSet = new	GUIContent(fieldSet);
				script.intValueIndexSet = EditorGUILayout.Popup(comSet,script.intValueIndexSet,script.stringValueSet);
				GUILayout.EndHorizontal();
			}	
	}
	
}




