using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private GameObject camera;

    void Start(){
        camera = GameObject.Find("MainCamera");
    }

    void Update() 
    {
        transform.LookAt(camera.transform.position, Vector3.up);
    }
}
