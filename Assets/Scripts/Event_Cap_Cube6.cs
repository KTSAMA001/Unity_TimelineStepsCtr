using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Cap_Cube6 : Event_Cap_Base
{
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
        Debug.Log("Event_Cap: OnStartEvent£º" + gameObject.name + ":" + transform.parent.name);
    }
    public override void OnFinalEvent()
    {
        Debug.Log("Event_Cap: OnFinalEvent£º" + gameObject.name + ":" + transform.parent.name);
    }
}
