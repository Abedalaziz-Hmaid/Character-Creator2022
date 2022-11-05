using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
//FOKOzuynen

[System.Serializable]
public class StringData
{
	public string[] valueStrings;
}


public class SaveLoadInstantiatedObj : MonoBehaviour
{
	[HideInInspector]
	public StringData[] listOffListStrings;
	public enum saveFormats { playerprefs, binaryXOR };
	public saveFormats saveAs;
	
	[SerializeField]private GameObject prefabExampleToInstantiate;
	[Tooltip("Write the unique ID name saved on prefab")]
	[SerializeField]private string uniqueIDNameOfObj ;
	[SerializeField]private bool clearSavedStrings = false;
	public bool saveData = false; // this need to be public to send message to all instantiated object to get in list for to be save
	
	[Header("Set the size of max instantiated objects to be save    ↓↓↓")]
	public string[] stringInstaObjNames;
	[HideInInspector]
	public Vector3[] vectorInstaObjPosition;
	[HideInInspector]
	public Vector3[] vectorInstaObjRotation;
	[HideInInspector]
	public Vector3[] vectorInstaObjScale;
	float timeCount;
	int key = 129;
	 InstantiatedObject[] InstantiatedObjectScript;
	
	 int[] saveInstaObjValuesLength;
	bool refreshNow = true;
	[SerializeField] private Button saveButton;
	// 
    
	void Start()
	{
		if(uniqueIDNameOfObj.Equals(""))Debug.Log("original Name string on Instantiated Manager is empty");
		vectorInstaObjPosition = new Vector3[stringInstaObjNames.Length];
		vectorInstaObjRotation = new Vector3[stringInstaObjNames.Length];
		vectorInstaObjScale = new Vector3[stringInstaObjNames.Length];
		listOffListStrings = new StringData[stringInstaObjNames.Length];
		saveInstaObjValuesLength = new int[stringInstaObjNames.Length];
		
		for(int i=0; i < stringInstaObjNames.Length; i++)
		{
			listOffListStrings[i] = new StringData();
			listOffListStrings[i].valueStrings = new string[saveInstaObjValuesLength[i]];
		}
		
		LoadSavedObjsString(); //load the names ,position,rotation and scale data of the objects
		InstantiateObjsSaved();  //Instantiate the number of object from the load list setting the names, position, rotation and scale
	   if(saveButton != null)saveButton.onClick.AddListener(delegate{saveData=true;});
	}
	

  // 
    void Update()
    {
		
	    if(saveData)SaveData();
		ClearSavedStringObjs();
		
	    if(refreshNow)
	    {
			RefreshFieldsInsta();
            refreshNow = false;
		}
    }
	public void InvokeSaveData(){saveData =true;}
	public void SaveData()
	{
		
		InstantiatedObjectScript = FindObjectsOfType<InstantiatedObject>();
			
			for(int i=0; i <  InstantiatedObjectScript.Length; i++)
			{
				if( InstantiatedObjectScript[i].uniqueIDNameOfObj == uniqueIDNameOfObj )
				{
					InstantiatedObjectScript[i].CallToSave();
					for(int j =0;j<stringInstaObjNames.Length;j++)
						saveInstaObjValuesLength[j] = listOffListStrings[j].valueStrings.Length;
				}
			}
			
			timeCount += Time.deltaTime;  // this will give time to call in list all instantiated objs that need to be save
			if(timeCount >= 3) // to adjsut this and test in a scene if enaf time for processing else increase and remember to put a Save Bar progress image.
			{
			  for(int i=0; i < stringInstaObjNames.Length; i++)
			  { 
				  if(saveAs == saveFormats.playerprefs)
				  {
				  PlayerPrefs.SetString(uniqueIDNameOfObj+ i.ToString() , stringInstaObjNames[i]);
					  PlayerPrefs.SetInt(uniqueIDNameOfObj + "valueStringLength" +i.ToString(),saveInstaObjValuesLength[i]);
				  PlayerPrefs.SetFloat(uniqueIDNameOfObj + "vectorObjPositionX"+ i.ToString(),vectorInstaObjPosition[i].x);
				  PlayerPrefs.SetFloat(uniqueIDNameOfObj + "vectorObjPositionY"+ i.ToString(),vectorInstaObjPosition[i].y);
				  PlayerPrefs.SetFloat(uniqueIDNameOfObj + "vectorObjPositionZ"+ i.ToString(),vectorInstaObjPosition[i].z);
			  
				  PlayerPrefs.SetFloat(uniqueIDNameOfObj + "vectorObjRotationX"+ i.ToString(),vectorInstaObjRotation[i].x);
				  PlayerPrefs.SetFloat(uniqueIDNameOfObj + "vectorObjRotationY"+ i.ToString(),vectorInstaObjRotation[i].y);
				  PlayerPrefs.SetFloat(uniqueIDNameOfObj + "vectorObjRotationZ"+ i.ToString(),vectorInstaObjRotation[i].z);
			  
				  PlayerPrefs.SetFloat(uniqueIDNameOfObj + "vectorObjScaleX"+ i.ToString(), vectorInstaObjScale[i].x);
				  PlayerPrefs.SetFloat(uniqueIDNameOfObj + "vectorObjScaleY"+ i.ToString(), vectorInstaObjScale[i].y);
				  PlayerPrefs.SetFloat(uniqueIDNameOfObj + "vectorObjScaleZ"+ i.ToString(), vectorInstaObjScale[i].z);
					  //down here we save the values float@integer@strings that we send with the SaveInstaObValues component
					   
					  for(int j=0;j<listOffListStrings[i].valueStrings.Length;j++ )
					  {
						  PlayerPrefs.SetString(uniqueIDNameOfObj + "valueStrings"+i.ToString()+ j.ToString(),listOffListStrings[i].valueStrings[j]);
					  }
					  
				  }
			  }
				if(saveAs == saveFormats.binaryXOR)
				  {
					   File. Delete(uniqueIDNameOfObj); 
					   using FileStream fileStreamSaveBinary = File.Open(uniqueIDNameOfObj, FileMode.Create); 
					   using BinaryWriter binaryWriterSave = new(fileStreamSaveBinary); 
					   for(int i=0; i < stringInstaObjNames.Length; i++)
					   {
						  if(stringInstaObjNames[i] == null)stringInstaObjNames[i] = "";
						   binaryWriterSave.Write(EncryptDecrypt( stringInstaObjNames[i].ToString()));
						   
						   binaryWriterSave. Write (EncryptDecrypt(saveInstaObjValuesLength[i].ToString()));
						   binaryWriterSave.Write(EncryptDecrypt(vectorInstaObjPosition[i].x.ToString()));
						   binaryWriterSave.Write(EncryptDecrypt(vectorInstaObjPosition[i].y.ToString()));
						   binaryWriterSave.Write(EncryptDecrypt(vectorInstaObjPosition[i].z.ToString()));
						   
						   binaryWriterSave.Write(EncryptDecrypt(vectorInstaObjRotation[i].x.ToString())); 
						   binaryWriterSave.Write(EncryptDecrypt(vectorInstaObjRotation[i].y.ToString()));
						   binaryWriterSave.Write(EncryptDecrypt(vectorInstaObjRotation[i].z.ToString()));
						      
						   binaryWriterSave.Write(EncryptDecrypt(vectorInstaObjScale[i].x.ToString()));
						   binaryWriterSave.Write(EncryptDecrypt(vectorInstaObjScale[i].y.ToString()));
						   binaryWriterSave.Write(EncryptDecrypt(vectorInstaObjScale[i].z.ToString()));
						   //down here we save the values float@integer@strings that we send with the SaveInstaObValues component
						    for(int j=0;j<listOffListStrings[i].valueStrings.Length;j++ )
						   {
							    binaryWriterSave.Write(EncryptDecrypt(listOffListStrings[i].valueStrings[j]));
						   }  
						   
						   fileStreamSaveBinary.Flush();
						  
						  
					   }
					fileStreamSaveBinary.Close();
				  }
			  timeCount = 0;
				saveData = false;
			}
		
	}
	
	
	public void LoadSavedObjsString()
	{
		
		for(int i=0; i < stringInstaObjNames.Length; i++)
		{
			if(saveAs ==	saveFormats.playerprefs)
			{
		   stringInstaObjNames[i] = PlayerPrefs.GetString(uniqueIDNameOfObj+ i.ToString());
			
			saveInstaObjValuesLength[i]=	PlayerPrefs.GetInt(uniqueIDNameOfObj + "valueStringLength" +i.ToString());
				
			vectorInstaObjPosition[i].x = PlayerPrefs.GetFloat(uniqueIDNameOfObj + "vectorObjPositionX"+ i.ToString());
			vectorInstaObjPosition[i].y = PlayerPrefs.GetFloat(uniqueIDNameOfObj + "vectorObjPositionY"+ i.ToString());
			vectorInstaObjPosition[i].z = PlayerPrefs.GetFloat(uniqueIDNameOfObj + "vectorObjPositionZ"+ i.ToString());
			
			vectorInstaObjRotation[i].x = PlayerPrefs.GetFloat(uniqueIDNameOfObj + "vectorObjRotationX"+ i.ToString());
			vectorInstaObjRotation[i].y = PlayerPrefs.GetFloat(uniqueIDNameOfObj + "vectorObjRotationY"+ i.ToString());
			vectorInstaObjRotation[i].z = PlayerPrefs.GetFloat(uniqueIDNameOfObj + "vectorObjRotationZ"+ i.ToString());
			
			vectorInstaObjScale[i].x = PlayerPrefs.GetFloat(uniqueIDNameOfObj + "vectorObjScaleX"+ i.ToString());
			vectorInstaObjScale[i].y = PlayerPrefs.GetFloat(uniqueIDNameOfObj + "vectorObjScaleY"+ i.ToString());
			vectorInstaObjScale[i].z = PlayerPrefs.GetFloat(uniqueIDNameOfObj + "vectorObjScaleZ"+ i.ToString());
				listOffListStrings[i] = new StringData();
				listOffListStrings[i].valueStrings = new string[saveInstaObjValuesLength[i]];
				for(int j=0; j<listOffListStrings[i].valueStrings.Length;j++)
				{
				   listOffListStrings[i].valueStrings[j]=PlayerPrefs.GetString( uniqueIDNameOfObj + "valueStrings"+i.ToString()+ j.ToString());	
			
				} 
		   } 	
		}
		if(saveAs == saveFormats.binaryXOR && File.Exists(uniqueIDNameOfObj))
		   {
			using FileStream fileStreamLoad = File.Open(uniqueIDNameOfObj, FileMode.Open);
			using BinaryReader binaryReaderLoad = new(fileStreamLoad);
			  
			for(int i=0; i < stringInstaObjNames.Length; i++)
			   {
				stringInstaObjNames[i]= EncryptDecrypt( binaryReaderLoad.ReadString());
				
				saveInstaObjValuesLength[i] = int.Parse(EncryptDecrypt(binaryReaderLoad.ReadString())); //the reading is on a string then that decrypt as string and convert in Int 
				vectorInstaObjPosition[i].x = float.Parse(EncryptDecrypt(binaryReaderLoad.ReadString()));
				vectorInstaObjPosition[i].y = float.Parse(EncryptDecrypt(binaryReaderLoad.ReadString()));
				vectorInstaObjPosition[i].z = float.Parse(EncryptDecrypt(binaryReaderLoad.ReadString()));
			   
				vectorInstaObjRotation[i].x = float.Parse(EncryptDecrypt(binaryReaderLoad.ReadString()));
				vectorInstaObjRotation[i].y = float.Parse(EncryptDecrypt(binaryReaderLoad.ReadString()));
				vectorInstaObjRotation[i].z = float.Parse(EncryptDecrypt(binaryReaderLoad.ReadString()));
			   
				vectorInstaObjScale[i].x = float.Parse(EncryptDecrypt(binaryReaderLoad.ReadString()));
				vectorInstaObjScale[i].y = float.Parse(EncryptDecrypt(binaryReaderLoad.ReadString()));
				vectorInstaObjScale[i].z = float.Parse(EncryptDecrypt(binaryReaderLoad.ReadString()));
				      listOffListStrings[i] = new StringData();
					   listOffListStrings[i].valueStrings = new string[saveInstaObjValuesLength[i]];
		
					   for(int j=0; j<listOffListStrings[i].valueStrings.Length;j++)
					   {
				        listOffListStrings[i].valueStrings[j] = EncryptDecrypt(binaryReaderLoad.ReadString());
					   }
				}
				
		      
			binaryReaderLoad.Close();
		   }
		
	}
	
	
	public void InstantiateObjsSaved()
	{
		string originalName = prefabExampleToInstantiate.name.ToString();
		
		   for(int i=0; i < stringInstaObjNames.Length; i++)
		    {
		        if(!stringInstaObjNames[i].Equals(""))
		        {
			        prefabExampleToInstantiate.GetComponent<InstantiatedObject>().setName = false;
			        prefabExampleToInstantiate.name = stringInstaObjNames[i].ToString();
			        Instantiate(prefabExampleToInstantiate,vectorInstaObjPosition[i],Quaternion.Euler(vectorInstaObjRotation[i]));
			        prefabExampleToInstantiate.GetComponent<InstantiatedObject>().setName = true;
			        prefabExampleToInstantiate.name = originalName;
		        }
			 }		
	}
	
	public void RefreshFieldsInsta()
	{
	   InstantiatedObjectScript = FindObjectsOfType<InstantiatedObject>();
		   
		for(int i=0; i <  InstantiatedObjectScript.Length; i++)
		   {
			  if( InstantiatedObjectScript[i].uniqueIDNameOfObj.Equals(uniqueIDNameOfObj)) 
			  {
				InstantiatedObjectScript[i].AfterManagerStart(); 
				InstantiatedObjectScript[i].LoadValues();
			  }
		   }
	}
	
	public string EncryptDecrypt(string textToEncrypt)
	{
		StringBuilder inSb = new StringBuilder(textToEncrypt);
		StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
		char c;
		for (int i = 0; i < textToEncrypt.Length; i++)
		{
			c = inSb[i];
			c = (char)(c ^ key);
			outSb.Append(c);
		}
		return outSb.ToString();
	}
	
	
	public void InvokeClearSavedObjects(){clearSavedStrings = true;}
	public void ClearSavedStringObjs()
	{
		if(clearSavedStrings)
		{
			if(saveAs == saveFormats.playerprefs)
			{
				for(int i=0; i < stringInstaObjNames.Length; i++)
				{ 
					PlayerPrefs.DeleteKey (uniqueIDNameOfObj+ i.ToString());
					PlayerPrefs.DeleteKey(uniqueIDNameOfObj + "valueStringLength" +i.ToString());
					stringInstaObjNames[i] = null;
				}
			}
			
			if(saveAs ==	saveFormats.binaryXOR)
			{
				File. Delete(uniqueIDNameOfObj); 
				for(int i=0; i < stringInstaObjNames.Length; i++)
				{ 
					stringInstaObjNames[i] = null;
				}
			}
			
			clearSavedStrings = false;
		}
	}
	
}
