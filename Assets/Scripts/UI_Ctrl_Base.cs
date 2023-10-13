using Scripts.Frame_General;
using Scripts.UI;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Frame.UI
{
    public class UI_Ctrl_Base : SerializedMonoBehaviour, IUI_Ctrl
    {
        //  General_Panel panel_Global;
        //Global_UI_Panel global_UI_Panel;
        //private Object panel;

        //public Object Panel { get => panel; set => panel = value; }

        /// <summary>
        /// 进入游戏
        /// </summary>
        public virtual void DoOnEnter()
        {
            gameObject.SetActive(true);
        }
        /// <summary>
        /// 暂停游戏
        /// </summary>
        public virtual void DoOnPause()
        {
            gameObject.SetActive(false);
        }
        /// <summary>
        /// 恢复游戏
        /// </summary>
        public virtual void DoOnRestar()
        {
            gameObject.SetActive(true);
        }
        /// <summary>
        /// 退出游戏
        /// </summary>
        public virtual void DoOnExit()
        {
            Destroy(gameObject);
        }
        protected virtual void Awake()
        {
            Debug.Log($"{ToString()}:Awake标记");
        }
        public virtual void EventRegister()
        {
            //Debug.Log($"事件注册");
            Debug.Log($"{ToString()}:事件注册标记");
        }

        //目前并没有对场景或者是UI进行等级划分，
        //无法做到一次绑定，全局自动通过当前场景、当前UI层级自动的进行上级UI返回或者是自动跳转到上一级场景
        /// <summary>
        /// 有返回按钮以及主页按钮的行为
        /// </summary> 
        //public void HasBackAndHome(Action _back,Action _home)
        //{
        //    if (GameObject.Find("Canvas_Global_KT") != null)
        //    {
        //        global_UI_Panel= GameObject.Find("Canvas_Global_KT").GetComponent<Global_UI_Panel>();
        //        global_UI_Panel.Init(() => 
        //        {
        //            if (global_UI_Panel != null)
        //            {
        //                global_UI_Panel.Btn_Back.onClick.RemoveAllListeners();
        //                global_UI_Panel.Btn_Home.onClick.RemoveAllListeners();
        //                EventRegisterCenter.Inst.ButtonEventRegisterOnClick(global_UI_Panel.Btn_Back, _back);
        //                EventRegisterCenter.Inst.ButtonEventRegisterOnClick(global_UI_Panel.Btn_Home, _home);
        //            }
        //            else
        //            {
        //                Debug.LogError("全局面板的Panel为空，无法为其注册Home以及Back方法。");
        //            }
        //        });
        //        //panel_Global = GameObject.Find("Canvas_Global_KT").GetComponent<General_Panel>();
               
        //    }
        //}


        //public virtual void GetPanel<T>(T _panel) where T:MonoBehaviour
        //{
        //   panel = FindObjectOfType(typeof(T)) as T;
        //}
        //public  void Home()
        //{
        //    Global_Info.Inst.EnterMainMenuState = EnterMainMenuState.Home;
        //    SceneLoaderTool.Inst.AsyncLoadScene(Scene_Name.Scene_1.ToString());
        //    Debug.Log("UI_Ctrl_Base.Home");
        //}

    }

}


