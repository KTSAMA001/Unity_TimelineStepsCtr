
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using General_Enum;
using Scripts.Frame_General;
using Unity.VisualScripting;

namespace Frame.UI
{
    /// <summary>
    /// 事件注册中心
    /// </summary>
    public class EventRegisterCenter : Scripts.Frame_General.Singleton<EventRegisterCenter>
    {
        /// <summary>
        /// 只注册一个OnClick事件
        /// </summary>
        /// <param name="_button"></param>
        /// <param name="_actionClick"></param>
        public void ButtonEventRegisterOnClick(Button _button, Action _actionClick)
        {
            ButtonEventRegister(_button, _actionClick, null, null, false, CustomAudioType.Defuse);
        }
        /// <summary>
        /// 只注册一个按下事件
        /// </summary>
        /// <param name="_button"></param>
        /// <param name="_actionClick"></param>
        public void ButtonEventRegisterOnDown(Button _button, Action _actionDown)
        {
            ButtonEventRegister(_button, null, _actionDown, null, false, CustomAudioType.Defuse);
        }
        /// <summary>
        /// 只注册一个抬起事件
        /// </summary>
        /// <param name="_button"></param>
        /// <param name="_actionClick"></param>
        public void ButtonEventRegisterOnUp(Button _button, Action _actionUp)
        {
            ButtonEventRegister(_button, null, null, _actionUp, false, CustomAudioType.Defuse);
        }
        /// <summary>
        /// 简易的统一管理SliderValueChange事件注册；
        /// </summary>
        /// <param name="_slider"></param>
        /// <param name="_action"></param>
        public void SliderEventRegisterOnValueChange(Slider _slider, Action<float> _action)
        {
            _slider.onValueChanged.AddListener((value) =>
            {
                _action?.Invoke(value);
            });
        }
        /// <summary>
        /// 注册按钮事件，可以同时注册 OnClick、OnPointerDown 和 OnPointerUp 事件
        /// </summary>
        /// <param name="button">目标按钮</param>
        /// <param name="actionClick">OnClick 事件回调</param>
        /// <param name="actionDown">OnPointerDown 事件回调</param>
        /// <param name="actionUp">OnPointerUp 事件回调</param>
        /// <param name="isPlayAudio">是否播放音频</param>
        /// <param name="musicType">音频类型</param>
        public void ButtonEventRegister(Button button, Action actionClick, Action actionDown, Action actionUp, bool isPlayAudio = false, CustomAudioType musicType = CustomAudioType.Defuse)
        {
            if (actionClick != null)
            {
                button.onClick.AddListener(() => { actionClick(); });
            }
            if (isPlayAudio)
            {
                actionDown +=
                    () =>
                    {
                        //此处 为音乐播放的位置
                    };
            }
            // 注册 OnPointerDown 和 OnPointerUp 事件
            if (actionDown != null || actionUp != null)
            {
                EventTrigger eventTrigger = button.AddComponent<EventTrigger>();
                // 注册 OnPointerDown 事件
                if (actionDown != null)
                {
                    // 创建一个 EventTrigger.Entry 实例，设置其事件类型为 PointerDown
                    EventTrigger.Entry entryDown = new EventTrigger.Entry();
                    entryDown.eventID = EventTriggerType.PointerDown;
                    // 为事件添加一个监听器，该监听器调用 OnPointerDownEvent 方法，并将 actionDown 作为参数传入
                    entryDown.callback.AddListener((data) => { OnPointerDownEvent((PointerEventData)data, actionDown); });
                    // 将该 Entry 添加到 EventTrigger 的 triggers 列表中
                    eventTrigger.triggers.Add(entryDown);
                }
                // 注册 OnPointerUp 事件
                if (actionUp != null)
                {
                    // 创建一个 EventTrigger.Entry 实例，设置其事件类型为 PointerUp
                    EventTrigger.Entry entryUp = new EventTrigger.Entry();
                    entryUp.eventID = EventTriggerType.PointerUp;
                    // 为事件添加一个监听器，该监听器调用 OnPointerUpEvent 方法，并将 actionUp 作为参数传入
                    entryUp.callback.AddListener((data) => { OnPointerUpEvent((PointerEventData)data, actionUp); });
                    // 将该 Entry 添加到 EventTrigger 的 triggers 列表中
                    eventTrigger.triggers.Add(entryUp);
                }
            }
        }
        /// <summary>
        /// 统一的管理Toggle事件注册的位置
        /// </summary>
        /// <param name="_toggle"></param>
        /// <param name="_action"></param>
        /// <param name="isPlayAudio"></param>
        public void ToggleEventRegister(Toggle _toggle,Action<bool> _action,bool isPlayAudio=false)
        {
            if (isPlayAudio)
            {
                _action +=
                    (isOn) =>
                    {
                        //此处 为音乐播放的位置
                    };
            }
            _toggle.onValueChanged.AddListener
                (
                (isOn)
                => 
                { 
                    _action(isOn);
                });
        }
        /// <summary>
        /// 记录按下操作
        /// </summary>
        bool isDown = false;
        /// <summary>
        /// 按下按钮触发
        /// </summary>
        /// <param name="data"></param>
        public void OnPointerDownEvent(PointerEventData data, Action action)
        {
            if (!isDown)
            {
                action?.Invoke();
                isDown = true;
                Debug.Log("按下");
            }

        }
        /// <summary>
        /// 抬起按钮触发
        /// </summary>
        /// <param name="data"></param>
        private void OnPointerUpEvent(PointerEventData data, Action action)
        {
            if (isDown)
            {
                action?.Invoke();
                isDown = false;
                Debug.Log("抬起");
            }
        }


    }


}
