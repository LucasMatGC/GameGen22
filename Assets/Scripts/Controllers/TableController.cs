using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public bool busy = false;
    public GameObject tableGeneral;
    public GameObject table;
    public GameObject openBook;
    public GameObject closeBook;
    public GameObject clonedBooksStorage;
    public AudioSource sfx;

    private void Start()
    {
        tableGeneral.GetComponent<ActionsGeneralController>().addActionToList(table);
    }

    public void read(float booking)
    {
        if (busy == false)
        {
            busy = true;
            tableGeneral.GetComponent<ActionsGeneralController>().markActionTaken(table); 
            //tableGeneral.GetComponent<ActionsGeneralController>().getFreeAction();
            StartCoroutine(MyCoroutine(booking));
        }

    }

    IEnumerator MyCoroutine(float booking)
    {
        //Debug.Log("Leyendo");
        Vector3 tablePos = table.transform.position;
        Quaternion tableRotation = table.transform.rotation;
        Vector3 bookPos;
        Quaternion bookRotation = tableRotation; 
        if (table.transform.localPosition.x < 0)
        {
            bookPos = new Vector3(tablePos.x + 1f, tablePos.y + 0.3f, tablePos.z);
        }
        else
        {
            bookPos = new Vector3(tablePos.x - 1f, tablePos.y + 0.3f, tablePos.z);
            bookRotation = Quaternion.Euler(bookRotation.x, bookRotation.y+180, bookRotation.z);
        }        
        GameObject clonedCloseBook = Instantiate(closeBook, bookPos, bookRotation);
        clonedCloseBook.transform.SetParent(clonedBooksStorage.transform);
        clonedCloseBook.SetActive(true);
        yield return new WaitForSeconds(booking / 4);

        GameObject opendCloseBook = Instantiate(openBook, bookPos, bookRotation);
        opendCloseBook.transform.SetParent(clonedBooksStorage.transform);
        clonedCloseBook.SetActive(false);
        opendCloseBook.SetActive(true);
        sfx.Play();
        yield return new WaitForSeconds(booking / 2);

        clonedCloseBook.SetActive(true);
        opendCloseBook.SetActive(false);
        yield return new WaitForSeconds(booking / 4);

        Destroy(clonedCloseBook);
        Destroy(opendCloseBook);
        //Debug.Log("Accion terminada");
        tableGeneral.GetComponent<ActionsGeneralController>().markActionFree(table);
        busy = false;
    }
}
