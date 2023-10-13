using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event_Cap_Base : MonoBehaviour
{
    public UnityEvent playedUE;
    public UnityEvent stopedUE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void OnStartEvent()
    {
        Debug.Log($"OnStartEvent:{gameObject}:{gameObject.transform.parent}");
    }
    public virtual void OnFinalEvent()
    {
        Debug.Log($"OnFinalEvent:{gameObject}:{gameObject.transform.parent}");
    }
}
