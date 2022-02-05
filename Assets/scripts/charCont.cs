using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class charCont : MonoBehaviour
{
    public float vel = 6f;
    public NavMeshAgent playerNavt;
    public Camera cam;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit destino;
            if(Physics.Raycast(ray,out destino))
            {
                playerNavt.SetDestination(destino.point);
            }
        }
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0, y);
        personajeCont.Move(move * Time.deltaTime * vel);
    }*/
}
