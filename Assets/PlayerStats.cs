using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public List<string> tasks;

    /// <summary>Awake is called when the script instance is being loaded.</summary>
    void Start()
    {
        // If the instance reference has not been set, yet, 
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(gameObject);

        tasks = ActionsMaster.instance.GetMandatoryTasks();

    }

    void Update()
    {
        tasks = ActionsMaster.instance.GetMandatoryTasks();

    }


    public List<string> MandatoryTasks()
    {

        return tasks;

    }

}
 