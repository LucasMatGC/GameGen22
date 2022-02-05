using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charCont : MonoBehaviour
{
    public float vel = 6f;
    public CharacterController personajeCont;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0, y);
        personajeCont.Move(move * Time.deltaTime * vel);
    }
}
