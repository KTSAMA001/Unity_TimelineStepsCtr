using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Cap_Cube2 : Event_Cap_Base
{
    public GameObject[] m_Caps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnStartEvent()
    {
        //m_Capsȫ������
        foreach (var cap in m_Caps)
        {
            cap.SetActive(true);
        }
        Debug.Log("Event_Cap: OnStartEvent��" + gameObject.name + ":" + transform.parent.name);
    }
    public override void OnFinalEvent()
    {
        Debug.Log("Event_Cap: OnFinalEvent��" + gameObject.name + ":" + transform.parent.name);
    }
}
