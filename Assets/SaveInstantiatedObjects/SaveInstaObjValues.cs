using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using	UnityEditor;
using System.Reflection;
//FOKOzuynen
[ExecuteInEditMode]
public class SaveInstaObjValues : MonoBehaviour
{
	
	public enum saveValueType { m_String,m_Float,m_Integer,m_Bool };
	public saveValueType typeOfValue;
	[HideInInspector]
	public bool boolSaved;
	[HideInInspector]
	public int intValueIndexGet;
	[HideInInspector]
	public int intValueIndexSet;
	[HideInInspector]
	public string[] stringValueFieldGet; 
	[HideInInspector]
	public string[] stringValueGet; 
	[HideInInspector]
	public string[] stringValueSet; 
	[HideInInspector]
	public string[] stringCloneValueSet;
	[HideInInspector]
	public int intCloneValueIndexSet;
	[HideInInspector]
	public string valueToSave;  
	[HideInInspector]
	public float  floatSaved;
	[HideInInspector]
	public int  integerSaved;
	[HideInInspector]
	public string stringSaved;
	[Tooltip("Drag here the Script from where to get the Values to be save")]
	public Component m_ComponentValueGet ;
	[Tooltip("Drag here the Script where to be set the Values saved")]
	public Component m_ComponentValueSet ;
	
	[Space(5)]
	
	[HideInInspector]
	public bool soloMode;
	[Header("This save option is to be used as optional for personal use")]
	[HideInInspector]
	public bool savetheValue = false;
	[HideInInspector]
	public bool loadtheValue = false;
	[Tooltip("this BOOL prevent canceling fields while choose the desired field since the method is on update")]
	[HideInInspector]
	public bool setFieldReady = false;  //=> this BOOL prevent canceling fields while choose the desired field since the method is on update
	
	[HideInInspector]
	public string uniqueID;
	
	string valueSavedField;
	void Update()
	{
		 #if UNITY_EDITOR
         ProcessValues();
         #endif
		
		if(setFieldReady)ProcessValues();
		
       iModeSave();
	   iModeLoad();
	}
	void iModeSave()
	{
       if(savetheValue)
	   {
          if(typeOfValue == saveValueType.m_Float)
		  {
			PlayerPrefs.SetString("savetheFloat"+uniqueID,valueToSave);
			savetheValue = false;
		  }
		  else if(typeOfValue == saveValueType.m_Integer)
		  {
			PlayerPrefs.SetString("savetheInteger"+uniqueID,valueToSave);
			savetheValue = false;
		  }
		  else if(typeOfValue == saveValueType.m_String)
		  {
			PlayerPrefs.SetString("savetheString"+uniqueID,valueToSave);
			savetheValue = false;
		  }
		  else if(typeOfValue == saveValueType.m_Bool)
		  {
			PlayerPrefs.SetString("savetheBool"+uniqueID,valueToSave);
			savetheValue = false;
		  }
	   }
	}
	  void iModeLoad()
	  { 
		if(loadtheValue)
		{
		  if(typeOfValue == saveValueType.m_Float)
		  {
			setFieldReady = true;
			floatSaved = float.Parse(PlayerPrefs.GetString("savetheFloat"+uniqueID));
			loadtheValue = false;
		  }
          else if(typeOfValue == saveValueType.m_Integer)
		  {
            setFieldReady = true;
			integerSaved =  Convert.ToInt32(PlayerPrefs.GetString("savetheInteger"+uniqueID));
			loadtheValue = false;
		  }
          else if(typeOfValue == saveValueType.m_String)
		  {
            setFieldReady = true;
			stringSaved =  PlayerPrefs.GetString("savetheString"+uniqueID);
			loadtheValue = false;
		  }
		  else if(typeOfValue == saveValueType.m_Bool)
		  {
            setFieldReady = true;
			stringSaved =  PlayerPrefs.GetString("savetheBool"+uniqueID);
			if(stringSaved == "False")boolSaved = false;
			if(stringSaved == "True")boolSaved = true;
			loadtheValue = false;
		  }
		}
		
	
	}
	
	public void ProcessValues()
	{
		////////////////////STRING///////////////////
		if(m_ComponentValueGet != null)
			{
				FieldInfo[] fieldInfos = m_ComponentValueGet.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			
				//set the array size equals to the one of the fields - dynamic to avoid errors of outside of array size
				stringValueGet = new string[fieldInfos.Length];
				stringValueFieldGet = new string[fieldInfos.Length];
				//we get only the fields that are strings and avoid the array strings then list them in a array with the name of the field and the value for our dropdown that will be created  later
				for(int i= 0; i<fieldInfos.Length ;i++)
				{
					
					if(typeOfValue == saveValueType.m_String && fieldInfos[i].FieldType == typeof(System.String)) 
					{
						stringValueGet[i] = fieldInfos[i].Name + "=" +fieldInfos[i].GetValue(m_ComponentValueGet).ToString()  ;
						stringValueFieldGet[i]= fieldInfos[i].GetValue(m_ComponentValueGet).ToString();
					}
					else if(typeOfValue == saveValueType.m_Integer && fieldInfos[i].FieldType == typeof(System.Int32)) 
					{
						stringValueGet[i] = fieldInfos[i].Name + "=" +fieldInfos[i].GetValue(m_ComponentValueGet).ToString()  ;
						stringValueFieldGet[i]= fieldInfos[i].GetValue(m_ComponentValueGet).ToString();
					}
					else if(typeOfValue == saveValueType.m_Float &&  fieldInfos[i].FieldType == typeof(System.Single))
				    {
					    stringValueGet[i] = fieldInfos[i].Name + "=" +fieldInfos[i].GetValue(m_ComponentValueGet).ToString()  ;
					    stringValueFieldGet[i]= fieldInfos[i].GetValue(m_ComponentValueGet).ToString();
				    }
					else if(typeOfValue == saveValueType.m_Bool &&  fieldInfos[i].FieldType == typeof(System.Boolean))
					{
						stringValueGet[i] = fieldInfos[i].Name + "=" +fieldInfos[i].GetValue(m_ComponentValueGet).ToString()  ;
						stringValueFieldGet[i]= fieldInfos[i].GetValue(m_ComponentValueGet).ToString();
						
					}
				}
			
				//convert array to list clean empty spaces and convert back to array	
				List<string> stringList = new List<string>(stringValueGet);
				stringList .RemoveAll(x => x == null);
				stringValueGet = stringList.ToArray();
			
				List<string> stringLista = new List<string>(stringValueFieldGet);
				stringLista .RemoveAll(i => i == null);
				stringValueFieldGet = stringLista.ToArray();
			
				//get the field name from which need to extract the value bellow in fieldInfoSetB by getting the name only the first part from our list with split
				string[] x = stringValueGet[intValueIndexGet].Split('=');
			
				if(m_ComponentValueSet != null)
				{
					//get all fields from the component we need to set the value to
					FieldInfo[] fieldStringInfoSet = m_ComponentValueSet.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public| BindingFlags.Instance);
					
					stringValueSet = new string[fieldStringInfoSet.Length];
				   stringCloneValueSet = new string[fieldStringInfoSet.Length];
					for(int i= 0; i<fieldStringInfoSet.Length ;i++)
					{
						if(typeOfValue == saveValueType.m_String && fieldStringInfoSet[i].FieldType == typeof(System.String))
						{
							stringValueSet[i] = fieldStringInfoSet[i].Name +"=" +fieldStringInfoSet[i].GetValue(m_ComponentValueSet).ToString()  ;
							//we create a clone as above only that this array will not get other transforms
							stringCloneValueSet[i] = fieldStringInfoSet[i].Name +"=" +fieldStringInfoSet[i].GetValue(m_ComponentValueSet).ToString();
						}
						else if(typeOfValue == saveValueType.m_Integer && fieldStringInfoSet[i].FieldType == typeof(System.Int32))
						{
							stringValueSet[i] = fieldStringInfoSet[i].Name +"=" +fieldStringInfoSet[i].GetValue(m_ComponentValueSet).ToString()  ;
							//we create a clone as above only that this array will not get other transforms
							stringCloneValueSet[i] = fieldStringInfoSet[i].Name +"=" +fieldStringInfoSet[i].GetValue(m_ComponentValueSet).ToString()  ;
						}
						else if(typeOfValue == saveValueType.m_Float && fieldStringInfoSet[i].FieldType == typeof(System.Single))
					    {
						    stringValueSet[i] = fieldStringInfoSet[i].Name +"=" +fieldStringInfoSet[i].GetValue(m_ComponentValueSet).ToString()  ;
						    //we create a clone as above only that this array will not get other transforms
						    stringCloneValueSet[i] = fieldStringInfoSet[i].Name +"=" +fieldStringInfoSet[i].GetValue(m_ComponentValueSet).ToString()  ;
					    }
						else if(typeOfValue == saveValueType.m_Bool && fieldStringInfoSet[i].FieldType == typeof(System.Boolean))
						{
							stringValueSet[i] = fieldStringInfoSet[i].Name +"=" +fieldStringInfoSet[i].GetValue(m_ComponentValueSet).ToString()  ;
							//we create a clone as above only that this array will not get other transforms
							stringCloneValueSet[i] = fieldStringInfoSet[i].Name +"=" +fieldStringInfoSet[i].GetValue(m_ComponentValueSet).ToString()  ;
							
						}
							
					}
			
			       //convert array to list clean empty spaces and convert back to array	
					List<string> stringListSet = new List<string>(stringValueSet);
					stringListSet .RemoveAll(x => x == null);
					stringValueSet = stringListSet.ToArray();
			
			      //we compare the choosed value from dropdown and get the index since the clone have the real index of the script field row
					for(int i=0; i< stringCloneValueSet.Length; i++)
					{
						if(stringCloneValueSet[i] ==  stringValueSet[intValueIndexSet])
						{
							intCloneValueIndexSet = i;
						}
					}
				
					//get the float value field from the Component we choosed and set to be save
					FieldInfo fieldInfoSetB = m_ComponentValueGet.GetType().GetField(x[0], BindingFlags.NonPublic | BindingFlags.Public| BindingFlags.Instance);
					object value = fieldInfoSetB.GetValue(m_ComponentValueGet);
					//we save object value in a string value
					valueToSave = value.ToString();
			
					//get the value from the saved string
					if(typeOfValue == saveValueType.m_String)valueSavedField = "stringSaved";
					else if(typeOfValue == saveValueType.m_Integer)valueSavedField = "integerSaved";
					else if(typeOfValue == saveValueType.m_Float)valueSavedField = "floatSaved";
					else if(typeOfValue == saveValueType.m_Bool)valueSavedField = "boolSaved";
					
					FieldInfo fieldInfoSetSaved = GetType().GetField(valueSavedField, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
					object valueSavedTransObj = fieldInfoSetSaved.GetValue(this);
					
					if(setFieldReady )
					{  // we assign to the desired Component Field
					   fieldStringInfoSet[intCloneValueIndexSet].SetValue(m_ComponentValueSet, valueSavedTransObj);
						setFieldReady = false;
					}
					
			      
				}
			}
	}
	
   
}	
	


