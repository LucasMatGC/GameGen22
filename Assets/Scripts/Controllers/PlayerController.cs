using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float vel = 6f;
    public NavMeshAgent playerNavt;
    public GameObject player;
    public Camera cam;

    bool arrived = true;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetButtonDown("Movement"))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit destino;
            if(Physics.Raycast(ray,out destino))
            {
                playerNavt.SetDestination(destino.point);
                player.GetComponent<PlayerActions>().enabled = false;
                arrived = false;
            }
        }
        
        if (!arrived && Vector3.Distance(playerNavt.destination, player.transform.position) < 0.3f)
        {
            player.GetComponent<PlayerActions>().enabled = true;
            arrived = true;
        }
        
    }

   
}
