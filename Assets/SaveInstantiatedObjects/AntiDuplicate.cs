using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//FOKOzuynen

	public class AntiDuplicate : MonoBehaviour
{
	
	[SerializeField] private GameObject mainObj;
	[SerializeField] private float intervalCheck = 30f;
	[SerializeField] private GameObject[] ObjsFound;
	 private int nrID;
	 float timeCount;
	void Start()
	{
		mainObj = gameObject;
	}
	
	void Update()
	{
		timeCount += Time.deltaTime;
		if(timeCount >= intervalCheck)
		{
			nrID = mainObj.GetInstanceID();
			FindGameObjectsWithName(mainObj.name);
			Destroy(ObjsFound[0]);
			timeCount = 0f;
		}
		
	}
	GameObject[] FindGameObjectsWithName(string name)
 {
	 GameObject[] gameObjects =  GameObject.FindObjectsOfType<GameObject>();
	 ObjsFound = new GameObject[gameObjects.Length];
	 int FluentNumber = 0;
	 for (int i = 0; i < gameObjects.Length; i++)
	 {
		 if (gameObjects[i].name == name && gameObjects[i].GetInstanceID() != nrID)
		 {
			 ObjsFound[FluentNumber] = gameObjects[i];
			 FluentNumber++;
		 }
	 }
	 Array.Resize(ref ObjsFound, FluentNumber);
	 return ObjsFound;
 }
}

