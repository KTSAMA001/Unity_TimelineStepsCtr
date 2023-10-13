
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
    /// �¼�ע������
    /// </summary>
    public class EventRegisterCenter : Scripts.Frame_General.Singleton<EventRegisterCenter>
    {
        /// <summary>
        /// ֻע��һ��OnClick�¼�
        /// </summary>
        /// <param name="_button"></param>
        /// <param name="_actionClick"></param>
        public void ButtonEventRegisterOnClick(Button _button, Action _actionClick)
        {
            ButtonEventRegister(_button, _actionClick, null, null, false, CustomAudioType.Defuse);
        }
        /// <summary>
        /// ֻע��һ�������¼�
        /// </summary>
        /// <param name="_button"></param>
        /// <param name="_actionClick"></param>
        public void ButtonEventRegisterOnDown(Button _button, Action _actionDown)
        {
            ButtonEventRegister(_button, null, _actionDown, null, false, CustomAudioType.Defuse);
        }
        /// <summary>
        /// ֻע��һ��̧���¼�
        /// </summary>
        /// <param name="_button"></param>
        /// <param name="_actionClick"></param>
        public void ButtonEventRegisterOnUp(Button _button, Action _actionUp)
        {
            ButtonEventRegister(_button, null, null, _actionUp, false, CustomAudioType.Defuse);
        }
        /// <summary>
        /// ���׵�ͳһ����SliderValueChange�¼�ע�᣻
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
        /// ע�ᰴť�¼�������ͬʱע�� OnClick��OnPointerDown �� OnPointerUp �¼�
        /// </summary>
        /// <param name="button">Ŀ�갴ť</param>
        /// <param name="actionClick">OnClick �¼��ص�</param>
        /// <param name="actionDown">OnPointerDown �¼��ص�</param>
        /// <param name="actionUp">OnPointerUp �¼��ص�</param>
        /// <param name="isPlayAudio">�Ƿ񲥷���Ƶ</param>
        /// <param name="musicType">��Ƶ����</param>
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
                        //�˴� Ϊ���ֲ��ŵ�λ��
                    };
            }
            // ע�� OnPointerDown �� OnPointerUp �¼�
            if (actionDown != null || actionUp != null)
            {
                EventTrigger eventTrigger = button.AddComponent<EventTrigger>();
                // ע�� OnPointerDown �¼�
                if (actionDown != null)
                {
                    // ����һ�� EventTrigger.Entry ʵ�����������¼�����Ϊ PointerDown
                    EventTrigger.Entry entryDown = new EventTrigger.Entry();
                    entryDown.eventID = EventTriggerType.PointerDown;
                    // Ϊ�¼����һ�����������ü��������� OnPointerDownEvent ���������� actionDown ��Ϊ��������
                    entryDown.callback.AddListener((data) => { OnPointerDownEvent((PointerEventData)data, actionDown); });
                    // ���� Entry ��ӵ� EventTrigger �� triggers �б���
                    eventTrigger.triggers.Add(entryDown);
                }
                // ע�� OnPointerUp �¼�
                if (actionUp != null)
                {
                    // ����һ�� EventTrigger.Entry ʵ�����������¼�����Ϊ PointerUp
                    EventTrigger.Entry entryUp = new EventTrigger.Entry();
                    entryUp.eventID = EventTriggerType.PointerUp;
                    // Ϊ�¼����һ�����������ü��������� OnPointerUpEvent ���������� actionUp ��Ϊ��������
                    entryUp.callback.AddListener((data) => { OnPointerUpEvent((PointerEventData)data, actionUp); });
                    // ���� Entry ��ӵ� EventTrigger �� triggers �б���
                    eventTrigger.triggers.Add(entryUp);
                }
            }
        }
        /// <summary>
        /// ͳһ�Ĺ���Toggle�¼�ע���λ��
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
                        //�˴� Ϊ���ֲ��ŵ�λ��
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
        /// ��¼���²���
        /// </summary>
        bool isDown = false;
        /// <summary>
        /// ���°�ť����
        /// </summary>
        /// <param name="data"></param>
        public void OnPointerDownEvent(PointerEventData data, Action action)
        {
            if (!isDown)
            {
                action?.Invoke();
                isDown = true;
                Debug.Log("����");
            }

        }
        /// <summary>
        /// ̧��ť����
        /// </summary>
        /// <param name="data"></param>
        private void OnPointerUpEvent(PointerEventData data, Action action)
        {
            if (isDown)
            {
                action?.Invoke();
                isDown = false;
                Debug.Log("̧��");
            }
        }


    }


}
