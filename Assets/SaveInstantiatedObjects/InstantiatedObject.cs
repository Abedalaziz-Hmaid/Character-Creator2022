using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
//FOKOzuynen    

public class InstantiatedObject : MonoBehaviour
{
	
	[Tooltip("Write the unique ID name that will reffer to this prefab")]
	public string uniqueIDNameOfObj ;
	[HideInInspector]
	[SerializeField] private string[] theConnectString;
	[HideInInspector]
	[SerializeField] private Vector3[] theConnectPosition;
	[HideInInspector]
	[SerializeField] private Vector3[] theConnectRotation;
	[HideInInspector]
	[SerializeField] private Vector3[] theConnectScale;
	
	[Tooltip("Write the name of reference that hold SaveLoadInstantiatedObj script")]
	[SerializeField] private string managerObjInstantiated; //write the name of the GameObject tat hold the script that handle the save load
	 
	 GameObject managerObj;
	[HideInInspector]
	public bool setName = true;
	bool hasQuitGame = false;
	
	[SerializeField]private SaveInstaObjValues[] saveInstaObjValues;
	[HideInInspector]
	public  StringData[] listOfListStrings;
	bool objectIsNew = false;
	bool objectExist = false;
	
	
	void Start()
	{
		SetObjName();
		gameObject.name = gameObject.name.Replace("(Clone)","");
		managerObj = GameObject.Find(managerObjInstantiated);
		
	} 
	
	
	public void AfterManagerStart()
	{
		theConnectString =  managerObj.GetComponent<SaveLoadInstantiatedObj>().stringInstaObjNames;
		theConnectPosition = managerObj.GetComponent<SaveLoadInstantiatedObj>().vectorInstaObjPosition;
		theConnectRotation = managerObj.GetComponent<SaveLoadInstantiatedObj>().vectorInstaObjRotation;
		theConnectScale = managerObj.GetComponent<SaveLoadInstantiatedObj>().vectorInstaObjScale;
		
	}
	
	
     public void LoadValues()
	{
		
		for(int i=0; i < theConnectString.Length; i++)
		{
			if(gameObject.name == theConnectString[i] )
			{
				gameObject.transform.localScale = theConnectScale[i];
			}
		}
       
		string saveFormat = managerObj.GetComponent<SaveLoadInstantiatedObj>().saveAs.ToString();
		for(int i=0; i < theConnectString.Length; i++)
		{
			if(gameObject.name == theConnectString[i] )
			{
				
				listOfListStrings = managerObj.GetComponent<SaveLoadInstantiatedObj>().listOffListStrings;
				
					if(gameObject.name == theConnectString[i] )
					{
						
						listOfListStrings[i].valueStrings = managerObj.GetComponent<SaveLoadInstantiatedObj>().listOffListStrings[i].valueStrings;
		         }
				
				if(saveFormat == "playerprefs")
				{
					
				for(int j=0; j<listOfListStrings[i].valueStrings.Length;j++)
				{
					listOfListStrings[i].valueStrings[j]=PlayerPrefs.GetString( uniqueIDNameOfObj + "valueStrings"+i.ToString()+ j.ToString());
				
					if(saveInstaObjValues[j].typeOfValue == SaveInstaObjValues.saveValueType.m_Integer)
					{
						saveInstaObjValues[j].setFieldReady = true;
						saveInstaObjValues[j].integerSaved = Int32.Parse(listOfListStrings[i].valueStrings[j] );
					}
					else if(saveInstaObjValues[j].typeOfValue == SaveInstaObjValues.saveValueType.m_Float)
					{
						saveInstaObjValues[j].setFieldReady = true;
						saveInstaObjValues[j].floatSaved = float.Parse(listOfListStrings[i].valueStrings[j] );
					}
					else if(saveInstaObjValues[j].typeOfValue == SaveInstaObjValues.saveValueType.m_String)
					{
						saveInstaObjValues[j].setFieldReady = true;
						saveInstaObjValues[j].stringSaved = listOfListStrings[i].valueStrings[j] ;
					}
					else if(saveInstaObjValues[j].typeOfValue == SaveInstaObjValues.saveValueType.m_Bool)
					{
						saveInstaObjValues[j].setFieldReady = true;
						saveInstaObjValues[j].stringSaved = listOfListStrings[i].valueStrings[j] ;
						if(saveInstaObjValues[j].stringSaved == "False")saveInstaObjValues[j].boolSaved = false;
						if(saveInstaObjValues[j].stringSaved == "True")saveInstaObjValues[j].boolSaved = true;
					}
				}
				}
				if(saveFormat == "binaryXOR")
					{
						for(int j=0; j<listOfListStrings[i].valueStrings.Length;j++)
						{
							
							if(saveInstaObjValues[j].typeOfValue == SaveInstaObjValues.saveValueType.m_Integer)
							{
								saveInstaObjValues[j].setFieldReady = true;
								saveInstaObjValues[j].integerSaved = Int32.Parse(listOfListStrings[i].valueStrings[j] );
							}
							else if(saveInstaObjValues[j].typeOfValue == SaveInstaObjValues.saveValueType.m_Float)
							{
								saveInstaObjValues[j].setFieldReady = true;
								saveInstaObjValues[j].floatSaved = float.Parse(listOfListStrings[i].valueStrings[j] );
							}
							else if(saveInstaObjValues[j].typeOfValue == SaveInstaObjValues.saveValueType.m_String)
							{
								saveInstaObjValues[j].setFieldReady = true;
								saveInstaObjValues[j].stringSaved = listOfListStrings[i].valueStrings[j] ;
							}
							else if(saveInstaObjValues[j].typeOfValue == SaveInstaObjValues.saveValueType.m_Bool)
					        {
						       saveInstaObjValues[j].setFieldReady = true;
						       saveInstaObjValues[j].stringSaved = listOfListStrings[i].valueStrings[j] ;
						       if(saveInstaObjValues[j].stringSaved == "False")saveInstaObjValues[j].boolSaved = false;
						       if(saveInstaObjValues[j].stringSaved == "True")saveInstaObjValues[j].boolSaved = true;
					        }
						}
						
					}
				}
		}
		
	}
	

	public void CallToSave()
	{
		AfterManagerStart();
		
		listOfListStrings = managerObj.GetComponent<SaveLoadInstantiatedObj>().listOffListStrings;
		for(int i=0; i < theConnectString.Length; i++)
		{
			if(gameObject.name == theConnectString[i] )
			{
				listOfListStrings[i].valueStrings = managerObj.GetComponent<SaveLoadInstantiatedObj>().listOffListStrings[i].valueStrings;
		   }
		}
          if(objectExist == false)
		  {
            
		   for(int i=0; i < theConnectString.Length; i++)
			{
				if(theConnectString[i] == gameObject.name.ToString())
				{
					objectExist= true;
					return ; 
				}
			    if(theConnectString.Length == i+1)
				{
					objectIsNew =true;
					AddNewObject();
					break;
				}
			}
			
		  }
		  


		if(objectExist)
		{
		for(int i=0; i < theConnectString.Length; i++)
			{
				if(theConnectString[i] == gameObject.name.ToString())
				{
					theConnectPosition[i] = gameObject.transform.position;
					theConnectRotation[i] = gameObject.transform.localRotation.eulerAngles;
					theConnectScale[i] = gameObject.transform.localScale;
					
					listOfListStrings[i] = new StringData();
					listOfListStrings[i].valueStrings = new string[saveInstaObjValues.Length];//this will create the size in array for values to be save
					for(int j=0 ; j < saveInstaObjValues.Length; j++ )
					{
						saveInstaObjValues[j].setFieldReady = true;
						listOfListStrings[i].valueStrings[j] = saveInstaObjValues[j].valueToSave.ToString(); //here we send the values in string type then to be saved
					}
					for(int j=0; j<listOfListStrings[i].valueStrings.Length;j++)
					{
						if(saveInstaObjValues[j].typeOfValue == SaveInstaObjValues.saveValueType.m_Integer)
						{
							saveInstaObjValues[j].setFieldReady = true;
							saveInstaObjValues[j].integerSaved = Int32.Parse(listOfListStrings[i].valueStrings[j] );
						}
						else if(saveInstaObjValues[j].typeOfValue == SaveInstaObjValues.saveValueType.m_Float)
						{
							saveInstaObjValues[j].setFieldReady = true;
							saveInstaObjValues[j].floatSaved = float.Parse(listOfListStrings[i].valueStrings[j] );
						}
						else if(saveInstaObjValues[j].typeOfValue == SaveInstaObjValues.saveValueType.m_String)
						{
							saveInstaObjValues[j].setFieldReady = true;
							saveInstaObjValues[j].stringSaved = listOfListStrings[i].valueStrings[j] ;
						}
						else if(saveInstaObjValues[j].typeOfValue == SaveInstaObjValues.saveValueType.m_Bool)
						{
							saveInstaObjValues[j].setFieldReady = true;
							saveInstaObjValues[j].stringSaved = listOfListStrings[i].valueStrings[j] ;
							if(saveInstaObjValues[j].stringSaved == "False")saveInstaObjValues[j].boolSaved = false;
						    if(saveInstaObjValues[j].stringSaved == "True")saveInstaObjValues[j].boolSaved = true;
						}
					}
					return;
				}
				
			}
			objectExist= false;
		}


	}
	void AddNewObject()
	{
         if(objectIsNew)
		 {
		 for(int i=0; i < theConnectString.Length; i++)
			{ 
		     if(theConnectString[i] == "" )    
				{
					theConnectString[i] = gameObject.name.ToString();
					theConnectPosition[i] = gameObject.transform.position;
					theConnectRotation[i] = gameObject.transform.localRotation.eulerAngles;
					theConnectScale[i] = gameObject.transform.localScale;
					listOfListStrings[i].valueStrings = new string[saveInstaObjValues.Length];
					objectIsNew = false;
					return;
				}
				
			}
		 }
	}
	
   public void SetObjName()
	{
		if(setName)
		{
			Guid IDcode = Guid. NewGuid();
			gameObject.name = gameObject.name + IDcode.ToString() +"nameSet";
			setName = false;
		}
		
	}
	
	void OnApplicationQuit()
	{
		hasQuitGame = true;
	}
	void OnDestroy()
	{
		if(hasQuitGame == false)
		{
		  for(int i=0; i < theConnectString.Length; i++)
		  {
			  if(theConnectString[i] == gameObject.name)
			  {
				 theConnectString[i] = null;
		      }
		  }
		}
	}
	
}
