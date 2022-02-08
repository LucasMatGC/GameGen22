using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    [TextArea]
	public string m_text;
	[Range(0.01f, 0.1f)]
	public float m_characterInterval; //(in secs)

	private string m_partialText;
	private float m_cumulativeDeltaTime;


	private Text m_label;

    private char separation = ';';
    private int index = 0;
    private bool waiting = false;

    void Awake () {
		m_label = GetComponent<Text> ();
	}
	
	void Start () {	
		m_partialText = "";
		m_cumulativeDeltaTime = 0;
	}
		
	void Update () {
		m_cumulativeDeltaTime += Time.deltaTime;
        //Debug.Log("m_cumulativeDeltaTime = " + m_cumulativeDeltaTime);
		while (m_cumulativeDeltaTime >= m_characterInterval && index < m_text.Length) {
            
            if (m_text[index] != separation)
            {
                if (waiting)
                {
                    m_partialText = "";
                    waiting = false;
                }
                m_partialText += m_text[index];
                m_cumulativeDeltaTime -= m_characterInterval;
            }
            else
            {
                m_cumulativeDeltaTime = -3f;
                waiting = true;
            }
            index += 1;


        }
		m_label.text = m_partialText;
	}

}
