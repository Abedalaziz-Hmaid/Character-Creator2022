using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//FOKOzuynen
public class InstantiateAtMousePosition : MonoBehaviour
{
    [SerializeField] private LayerMask m_layerMask ; // don't set to Everything Layer
	 
	[SerializeField]private GameObject Obj1 ;
	
   RaycastHit m_hitData;
	Vector3 m_worldPosition;
    Vector3 cloneHit;
	Vector2 m_pointerPosition;
	Ray m_ray;
    [SerializeField] private string keyInstaObj = "1";
    
    [SerializeField] private float yDelta = 0.5f;
    [SerializeField] private float xDelta = 0.0f;
    [SerializeField] private float zDelta = 0.0f;
    
    void Update()
    {
       // if(Input.GetMouseButtonDown(0))InstaThisObj(Obj1);
        if(Input.GetKeyDown(keyInstaObj))InstaThisObj(Obj1);
        
        
        
    }
    public void InstaThisObj(GameObject Obj)
    {
	   m_pointerPosition = Input. mousePosition;
	   m_ray = Camera.main.ScreenPointToRay( m_pointerPosition);
            
		    if (Physics.Raycast(m_ray, out m_hitData, 1000, m_layerMask))
		    {
			   cloneHit = m_hitData.point;
               cloneHit.y = m_hitData.point.y + yDelta;
               cloneHit.x = cloneHit.x + xDelta;
               cloneHit.z = cloneHit.z + zDelta;

                m_worldPosition = cloneHit;
                
            }
		    
		  Instantiate (Obj,m_worldPosition, Quaternion.identity);
        
        
    }
    public void QuitApp()
    {
        Application.Quit();
         #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
         #endif
    }
}

