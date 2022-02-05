using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleController : MonoBehaviour
{
    public bool velaEncendida = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void encenderVela()
    {
        if (velaEncendida == false)
        {
            velaEncendida = true;
            StartCoroutine(MyCoroutine());
        }
    
    }

    IEnumerator MyCoroutine()
    {
        Debug.Log("Vela encendida");
        var rand = new System.Random();
        int time = rand.Next(7, 13);
        yield return new WaitForSeconds(time);
        Debug.Log("Vela apagada");
        velaEncendida = false;
    }
}
