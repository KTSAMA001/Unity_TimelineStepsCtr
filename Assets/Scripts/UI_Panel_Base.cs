using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Frame.UI
{
    public class UI_Panel_Base : MonoBehaviour, IUI_Panel
    {
        public void Awake()
        {
            Debug.Log("UI_Panel_Base_Awake");
        }
        public virtual void Init(Action action)
        {
           
        }
    }
}


