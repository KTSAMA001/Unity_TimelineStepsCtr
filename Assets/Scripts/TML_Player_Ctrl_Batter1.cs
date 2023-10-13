using Frame.UI;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class TML_Player_Ctrl_Batter1 : MonoBehaviour
    {        /// <summary>
             /// 初始状况下cur_PlayableDierctor_Index为-1,表示操作未开始
             /// </summary>
        [Header("当前播放的位置，-1表示播放前先空出一个位置")]
        [SerializeField] int cur_PlayableDirector_Index = -1;
        /// <summary>
        /// 播放序号是从1开始（反之从0开始）
        /// </summary>
        [Header("播放设置序号是从1开始（反之从0开始）")]
        public bool start1=true;
        [Header("多个步骤的父物体")]
        [SerializeField] GameObject playableDirectorsGroup;
        /// <summary>
        /// 所选择物体的所有的TML
        /// </summary>
        [ReadOnly]
        [Header("所选择物体的所有的TML")]
        [SerializeField] PlayableDirector[] playableDirectors;
        /// <summary>
        /// 是否开启瞬间就播放第一步操作，是则进入就在第一步，否则需要点击一下下一步才会出现第一步
        /// </summary>
        [Header("是否开启瞬间就播放第一步操作")]
        [SerializeField] bool IsFirstPlay = false;

        /// <summary>
        /// 是否在切换步骤时直接播放TML
        /// </summary>
        [Header("是否在切换步骤时直接播放TML")]
        public bool playOnStepChange = false;
        /// <summary>
        /// Complete all previous steps;自动完成前面所有的步骤
        /// </summary>
        [Header("自动完成前面所有的步骤")]
        public bool completeAllPreviousSteps = true;
        /// <summary>
        /// 是否在切换步骤时隐藏上一步的对象
        /// </summary>
        [Header("是否在切换步骤时隐藏上一步的对象")]
        public bool disEnableLastObject = false;

        /// <summary>
        /// 是否显示步骤数量以及当前步骤所在的提示
        /// </summary> 
        [Header("是否显示步骤数量以及当前步骤所在的提示")]
        [SerializeField] private bool showStepTips = true;
        [ShowIf("showStepTips")]
        public Color curStepImageColor = new Color(0.349f, 0.145f, 0.4f, 1f);
        [ShowIf("showStepTips")]
        public Color defStepImageColor = new Color(1, 1, 1, 1);
        [ShowIf("showStepTips")]
        [Header("步骤标记的预制体")]
        [SerializeField] GameObject ImgStep;
        [ShowIf("showStepTips")]
        [Header("步骤标记的父物体")]
        [SerializeField] GameObject Step_Group_go;
        [ShowIf("showStepTips")]
        [SerializeField] private bool isCustomStepsCount = false;
        [ShowIf("isCustomStepsCount")]
        [Header("自定义步骤的数量，用于加载对应数量标记")]
        public int CustomStepsCount = 0;
        [ShowIf("isCustomStepsCount")]
        [InfoBox("因为TML控制器是根据TML数量确定的步骤数量，所以存在多个TML阶段属于同一步骤的情况，这个时候需要给多个阶段同时标记为同一个步骤")]
        public List<int> listStep = new List<int>();

        List<Image> images_Step_List = new List<Image>();

        /// <summary>
        /// 外部调用需要传入PlayableDirector的列表
        /// </summary>
        public PlayableDirector[] PlayableDirectors { get => playableDirectors; set => playableDirectors = value; }


        public bool ShowStepTips { get => showStepTips; set => showStepTips = value; }
        public bool IsCustomStepsCount { get => isCustomStepsCount; set => isCustomStepsCount = value; }
 
        //public PlayableDirector Cur_PlayableDiector { get => cur_PlayableDiector; set => cur_PlayableDiector = value; }
        private void Awake()
        {
            InitGroups();
            EventRegister();
            if (IsFirstPlay)
            {
                Next_Step();
            }
            Flash_Display();
            StepImageCreate();
        }
        /// <summary>
        /// 初始化的步骤统一执行地点
        /// </summary>
        void InitGroups()
        {
            InitPlayableDirectors();


       
            //给有播放器的的注册开启播放事件以及结束播放事件
            for (int i = 0; i < playableDirectors.Length; i++)
            {
                int index = i;
                if (playableDirectors[i] != null)
                {
                    Event_Cap_Base event_Cap_Base = playableDirectors[index].GetComponent<Event_Cap_Base>();
                    if (!ReferenceEquals(event_Cap_Base, null))
                    {
                        playableDirectors[index].played += (PlayableDirector pd) => { event_Cap_Base.OnStartEvent(); };
                        playableDirectors[index].stopped += (PlayableDirector pd) => { event_Cap_Base.OnFinalEvent(); };
                    }
                }
            }
        }

        /// <summary>
        /// 初始化TML播放器
        /// </summary>
        private void InitPlayableDirectors()
        {
            Transform playableDirectorsGroup_Trans = playableDirectorsGroup.transform;
            InitPlayableDirectors(playableDirectorsGroup_Trans.gameObject);
        }
        /// <summary>
        /// 根据拥有TML的组对下面拥有的TML初始化播放轨道数量；
        /// </summary>
        /// <param name="_pDGroup">TML组根成员（父物体的GameObject）</param>
        public void InitPlayableDirectors(GameObject _pDGroup)
        {
            int length = _pDGroup.transform.childCount;
            playableDirectors = new PlayableDirector[length];
            for (int i = 0; i < length; i++)
            {
                playableDirectors[i] = _pDGroup.transform.GetChild(i).GetComponent<PlayableDirector>();

            }
            StepImageCreate();
        }
        /// <summary>
        /// 事件注册管理区域
        /// </summary>
        public void EventRegister()
        {

        }



        /// <summary>
        /// 点击下一步
        /// </summary>
        public void Next_Step()
        {
            if (cur_PlayableDirector_Index >= playableDirectors.Length - 1)
                return;
            cur_PlayableDirector_Index += 1;
            Flash_Display();
            if (playOnStepChange)
            {
                Play();
            }
        }
        /// <summary>
        /// 指定下一步的步骤
        /// </summary>
        /// <param name="index"></param>
        public void Next_Step_Index(int index)
        {
            if (index == -1)
            {
                Next_Step();
            }
            else
            {
                cur_PlayableDirector_Index = index;
                Flash_Display();
                if (playOnStepChange)
                {
                    Play();
                }
            }
        }
        /// <summary>
        /// 点击上一步
        /// </summary>
        public void Last_Step()
        {
            if (cur_PlayableDirector_Index < 1)
            {
                Debug.Log("步骤停止");
                return;
            }
            cur_PlayableDirector_Index -= 1;
            Flash_Display();
            if (playOnStepChange)
            {
                Play();
            }
        }
        /// <summary>
        /// 指定上一步的步骤
        /// </summary>
        public void Last_Step_Index(int index)
        {
            if (index == -1)
            {
                Last_Step();
            }
            else
            {
                cur_PlayableDirector_Index = index;
                Flash_Display();
                if (playOnStepChange)
                {
                    Play();
                }
            }
        }
        /// <summary>  
        /// 播放当前步骤
        /// </summary>
        public void Play()
        {
            StartCoroutine(Play_Cor());
        }

        IEnumerator Play_Cor()
        {
            Flash_Display();
            //获取对象身上的Event_Cap_Base
            //执行OnStartEvent
            //如果设置了自动完成当前步骤以前的所有步骤
            if (completeAllPreviousSteps)
            {
                //将当前步骤以前的所有步骤设定为播放结束
                for (int i = 0; i < cur_PlayableDirector_Index; i++)
                {
                    //判断是否有播放器
                    if (playableDirectors[i] != null)
                    {
                        //将前面的动画都设定播放完成的状态,
                        //-0.01的目的是为了让动画最后一帧的TML上挂载的事件正常工作
                       // playableDirectors[i].time = playableDirectors[i].duration - Time.deltaTime/2;
                       //暂时放弃在播放器面板上挂载事件的方式，这会很混乱
                        playableDirectors[i].time = playableDirectors[i].duration;
                        //停顿一帧等待Play
                        yield return null;
                        //一般情况下步骤动画设定为播放完成则自动停止
                        playableDirectors[i].Play();//播放，也开始played事件
                        //停顿一帧等待stoped的方法完成
                       yield return null;
                    }
                    else
                    {
                        //查看是否这是一个TML组
                        TML_Player_Ctrl_Batter tPCB = playableDirectors[i].GetComponent<TML_Player_Ctrl_Batter>();
                        if(!ReferenceEquals(tPCB,null))
                        {
                          //  tPCB.CustomStepsCount=
                        }
                    }
                    
                }
            }
            //初始化后面的所有步骤为没有播放的状态
            for(int i= cur_PlayableDirector_Index; i<playableDirectors.Length;i++)
            {
                playableDirectors[i].initialTime = 0;
            }
            //如果当前步骤 单纯为步骤，没有TML，则跳过不播放
            if (playableDirectors.Length <= 0 || playableDirectors[cur_PlayableDirector_Index] == null)
            {
                yield break;
            }
            //如果是从1开始，则标记数字（当前步骤数）-1
            if (start1)
            {
                cur_PlayableDirector_Index -= 1;
            }

            playableDirectors[cur_PlayableDirector_Index].time = 0;
            playableDirectors[cur_PlayableDirector_Index].Play();
        }

        /*     /// <summary>
       /// 修改当前的播放序号，并且播放
       /// </summary>
       public void ChangeCurIndexAndPlay(int _index)
       {
           Cur_PlayableDirector_Index = _index;
       }*/


        /// <summary>
        /// 创建步骤数量以及当前步骤所在的显示提醒标记
        /// </summary>
        public void StepImageCreate()
        {
            if (Step_Group_go.Equals(null))
            {
                return;
            }
            //先清理已经拥有的数据，再次根据新数据创建步骤显示
            for (int i = 0; i < Step_Group_go.transform.childCount; i++)
            {
                Destroy(Step_Group_go.transform.GetChild(i).gameObject);
                images_Step_List.Clear();
            }
            if (!ShowStepTips|| ImgStep==null)
            {
                return;
            }
            if (IsCustomStepsCount)
            {
                for (int i = 0; i < CustomStepsCount; i++)
                {
                    GameObject go = Instantiate(ImgStep, Step_Group_go.transform);
                    images_Step_List.Add(go.GetComponent<Image>());
                    images_Step_List[i].color = new Color(1, 1, 1, 0.2f);
                    //go.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                }
            }
            else
            {
                for (int i = 0; i < playableDirectors.Length; i++)
                {
                    GameObject go = Instantiate(ImgStep, Step_Group_go.transform);
                    images_Step_List.Add(go.GetComponent<Image>());
                    images_Step_List[i].color = new Color(1, 1, 1, 0.2f);
                    //go.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                }
            }
            Flash_Display();
        }
        /// <summary>
        /// 界面UI的刷新操作
        /// </summary>
        void Flash_Display()
        {
            if (disEnableLastObject)
            {
                for (int i = 0; i < playableDirectors.Length; i++)
                {

                    if (playableDirectors[i] != null)
                    {
                        playableDirectors[i].gameObject.SetActive(i == cur_PlayableDirector_Index);

                    }
                }
            }
            if (cur_PlayableDirector_Index >= 0 && cur_PlayableDirector_Index < playableDirectors.Length)
            {
                if (IsCustomStepsCount)
                {

                    if (images_Step_List.Count >= 0)
                    {
                        for (int i = 0; i < images_Step_List.Count; i++)
                        {
                            if (i == listStep[cur_PlayableDirector_Index])
                            {
                                images_Step_List[i].color = curStepImageColor;

                            }
                            else
                            {
                                images_Step_List[i].color = defStepImageColor;
                            }
                            if (images_Step_List[i].transform.Find("Image") != null)
                            {
                                images_Step_List[i].transform.Find("Image").gameObject.SetActive(i == listStep[cur_PlayableDirector_Index]);
                            }
                        }
                    }
                }
                else
                {
                    if (images_Step_List.Count >= 0)
                    {
                        for (int i = 0; i < images_Step_List.Count; i++)
                        {
                            if (i == cur_PlayableDirector_Index)
                            {
                                images_Step_List[i].color = curStepImageColor;

                            }
                            else
                            {
                                images_Step_List[i].color = defStepImageColor;
                            }
                            if (images_Step_List[i].transform.Find("Image") != null)
                            {
                                images_Step_List[i].transform.Find("Image").gameObject.SetActive(i == cur_PlayableDirector_Index);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 修改当前的播放序号，并且播放
        /// </summary>
        /// <param name="_index">指定播放的步骤数</param>
        /// <param name="finalCallBack">播放完成需要执行的方法</param>
        public void ChangeCurIndexAndPlay(int _index)
        {
            cur_PlayableDirector_Index = _index;
            Play();
        }
        private void OnValidate()
        {
            //如果主开关没有打开，子开关也关闭
            if (!showStepTips)
            {
                IsCustomStepsCount = showStepTips;
            }


        }
    }
}