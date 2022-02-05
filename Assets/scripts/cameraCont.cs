using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCont : MonoBehaviour
{
    public GameObject personajeGO;
    public float MovementSpeed = 5f;
    public GameObject camaraGO;

    private void Start()
    {
        camaraGO.transform.rotation = Quaternion.Euler(35,35,0);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 posicionPer = personajeGO.transform.position;
        Vector3 newPos = new Vector3(posicionPer.x - 5, posicionPer.y + 5, posicionPer.z - 5);
        camaraGO.transform.position = Vector3.Lerp(camaraGO.transform.position, newPos, MovementSpeed * Time.deltaTime);
    }
}
