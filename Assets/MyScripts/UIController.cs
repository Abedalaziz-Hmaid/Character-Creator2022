using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    
    Transform camHolder;

    private Camera ZoomCamera;

    public float ScrollSpeed = 5f ;
    // cam rotation x
    float x = 16;

    // cam rotation y
    float y = -30;

    // randomize character creating button
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontStyle = FontStyle.Bold;
        style.fontSize = 24;
        GUI.Label(new Rect(10, 10, 150, 50), "Hold Right Mouse Button Down\nor use W A S D To Rotate.", style);
    }
    private void Start()
    {
        ZoomCamera = Camera.main;
        // setting up the camera position, rotation, and reference for use
        Transform cam = Camera.main.transform;
        if (cam)
        {
            cam.position = transform.position + new Vector3(0, 0.3f, 2);
            cam.rotation = Quaternion.Euler(0, -180, 0);
            camHolder = new GameObject().transform;
            camHolder.position = transform.position + new Vector3(0, 1, 0);
            cam.LookAt(camHolder);
            cam.SetParent(camHolder);
        }
    }

    private void Update()
    {
        if (ZoomCamera.orthographic)
        {
            ZoomCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        }
        else if (ZoomCamera.fieldOfView <= 65 && ZoomCamera.fieldOfView >= 35)
        {
            ZoomCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
            if (ZoomCamera.fieldOfView < 45)
            {
                camHolder.position = transform.position + new Vector3(0, 1.25f, 0);
            }
            else
            {
                camHolder.position = transform.position + new Vector3(0, 1, 0);
            }
        }
        else if (ZoomCamera.fieldOfView <= 35)
        {
            ZoomCamera.fieldOfView = 35;
        }else if(ZoomCamera.fieldOfView >= 65)
        {
            ZoomCamera.fieldOfView = 65;
        }
        
        if (camHolder)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                x += 1 * Input.GetAxis("Mouse X");
                y -= 1 * Input.GetAxis("Mouse Y");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                x -= 1 * Input.GetAxis("Horizontal");
                y -= 1 * Input.GetAxis("Vertical");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    void LateUpdate()
    {
        // method for handling the camera rotation around the character
        if (camHolder)
        {
            y = Mathf.Clamp(y, -45, 15);
            camHolder.eulerAngles = new Vector3(y, x, 0.0f);
        }
    }

    public static UIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

}
