using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerGameObject;
    public float movementSpeed = 5f;
    public GameObject cameraGameOnject;

    private void Start()
    {
        cameraGameOnject.transform.rotation = Quaternion.Euler(35,45,0);
    }
    // Update is called once per frame
    void Update()
    {
        //Rotation
        Quaternion newRotation = Quaternion.Euler(35, 45, 0);
        cameraGameOnject.transform.rotation = Quaternion.Lerp(cameraGameOnject.transform.rotation, newRotation, movementSpeed * Time.deltaTime);
        //Position
        Vector3 posicionPer = playerGameObject.transform.position;
        Vector3 newPos = new Vector3(posicionPer.x - 5, posicionPer.y + 5, posicionPer.z - 5);
        cameraGameOnject.transform.position = Vector3.Lerp(cameraGameOnject.transform.position, newPos, movementSpeed * Time.deltaTime);
    }
}
