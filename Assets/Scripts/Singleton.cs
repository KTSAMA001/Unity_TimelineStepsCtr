using UnityEngine;
using System.Collections;
/// <summary>
/// 单例类
/// </summary>
/// <typeparam name="T"></typeparam>

namespace Scripts.Frame_General
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        static T _inst;
        public static T Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = FindObjectOfType(typeof(T)) as T;
                    if (_inst == null)
                    {
                        GameObject managers = GameObject.Find("Managers");
                        if (managers != null)
                        {
                            _inst = managers.AddComponent<T>();
                        }
                        else
                        {
                            managers = new GameObject("Managers");
                            _inst = managers.AddComponent<T>();
                        }
                    }
                }
                return _inst;
            }
            private set { }
        }
        void Awake()
        {
            if (_inst == null)
            {
                _inst = GetComponent<T>();
                InitAwake();
            }
            else
            {
                //Destroy(this);
            }
        }
        protected virtual void InitAwake()
        {

        }
    }
}