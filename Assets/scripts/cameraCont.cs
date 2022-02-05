using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCont : MonoBehaviour
{
    public GameObject personajeGO;
    public GameObject camaraGO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 posicionPer = personajeGO.transform.position;
        //Vector3 posicionCam = camaraGO.transform.position;
        camaraGO.transform.position = new Vector3(posicionPer.x-3, posicionPer.y+5, posicionPer.z-3);
    }
}
