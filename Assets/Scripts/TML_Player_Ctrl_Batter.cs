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
    public class TML_Player_Ctrl_Batter : MonoBehaviour
    {
        /// <summary>
        /// 播放器的数量
        /// </summary>
        [ReadOnly]
        public int playablesLength;
        /// <summary>
        /// 初始状况下cur_PlayableDierctor_Index为-1,表示操作未开始
        /// </summary>
        [Header("当前播放的位置，-1表示播放前先空出一个位置")]
        [SerializeField] int cur_PlayableDirector_Index = -1;
    /*    /// <summary>
        /// 播放序号是从1开始（反之从0开始）
        /// </summary>
        [Header("播放设置序号是从1开始（反之从0开始）")]
        bool start1=false;*/
        [Header("多个步骤的父物体")]
        [SerializeField] GameObject playableDirectorsGroup;
        /// <summary>
        /// 所选择物体的所有的TML
        /// </summary>
        [ReadOnly]
        [Header("所选择物体的所有的TML")]
        [SerializeField] GameObject[] step_gos;
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
        [Header("是否在切换步骤时隐藏上一步的对象(会导致无法正常调用played以及stoped事件)")]
        public bool disEnableLastObject = false;

        /// <summary>
        /// 是否显示步骤数量以及当前步骤所在的提示
        /// </summary> 
        [Header("是否显示步骤数量以及当前步骤所在的提示")]
        [SerializeField] private bool showStepTips = false;
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
        /// <summary>
        /// 是否自定义标记数量
        /// </summary>
        [Header("是否自定义标记数量")]
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
        public GameObject[] Step_Gos { get => step_gos; set => step_gos = value; }


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
            //将所有的TML初始化准备播放的状态
            InitSteps(0,playablesLength-1);
            Flash_Display();
            StepImageCreate();
        }
        private void Start()
        {
           // Multi_StepsPlay(new int[] { 1,3,4,5});
        }
        /// <summary>
        /// 初始化的步骤统一执行地点
        /// </summary>
        void InitGroups()
        {
         
            //如果没有手动赋值确认当前的TML组，那就默认自己就是TML组
            if (playableDirectorsGroup==null)
            {
                playableDirectorsGroup = gameObject;
            }
            InitPlayableDirectors();
          
            //给有播放器的的注册开启播放事件以及结束播放事件
            for (int i = 0; i < step_gos.Length; i++)
            {
                int index = i;
                PlayableDirector pd = step_gos[index].GetComponent<PlayableDirector>();
                if (pd != null)//此处没有使用ReferenceEquals(pd, null)的原因：pd为空得到的是"null"不是null
                {
                    Event_Cap_Base event_Cap_Base = step_gos[index].GetComponent<Event_Cap_Base>();
                    if (!ReferenceEquals(event_Cap_Base, null))//处理速度更快
                    {
                        pd.played +=
                            (PlayableDirector pd) => 
                            { 
                                event_Cap_Base.OnStartEvent();
                                event_Cap_Base.playedUE?.Invoke();
                            };
                        pd.stopped += 
                            (PlayableDirector pd) =>
                            {
                                event_Cap_Base.OnFinalEvent();
                                event_Cap_Base.stopedUE?.Invoke();
                                OnPlayed(pd);
                            };
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
            step_gos = new GameObject[length];
            for (int i = 0; i < length; i++)
            {
                step_gos[i] = _pDGroup.transform.GetChild(i).gameObject;
               
            }

            playablesLength = step_gos.Length;
            
            
            StepImageCreate();
        }
        /// <summary>
        /// 事件注册管理区域
        /// </summary>
        public void EventRegister()
        {

        }



        /// <summary>
        /// 点击下一步，线性步骤，
        /// 会强制完成全部前置步骤以及重置全部后置步骤
        /// </summary>
        public void Next_Step()
        {
            if (cur_PlayableDirector_Index >= step_gos.Length - 1)
                return;
            cur_PlayableDirector_Index += 1;
            Flash_Display();
            if (playOnStepChange)
            {
                Play();
            }
        }
        /// <summary>
        /// 指定下一步的步骤，线性步骤
        /// 会强制完成全部前置步骤以及重置全部后置步骤
        /// </summary>
        /// <param name="index"></param>
        public void Next_Step_Index(int index)
        {
            if(index>=playablesLength)
            {
                Debug.LogWarning("正在尝试播放的TML序号大于已有的最大值" + playablesLength +"-1");
            }
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
        /// 点击上一步，线性步骤
        /// 会强制完成全部前置步骤以及重置全部后置步骤
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
        /// 指定上一步的步骤，线性步骤
        /// 会强制完成全部前置步骤以及重置全部后置步骤
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
        /// 用于播放完成之后，将timeLine的时间轴固定到最后的位置上
        /// </summary>
        /// <param name="pd">需要被执行的pd</param>
        public void OnPlayed(PlayableDirector pd)
        {
            pd.timeUpdateMode = DirectorUpdateMode.Manual;
            pd.time = pd.duration;
            pd.Evaluate();
        }
        /// <summary>  
        /// 播放当前步骤
        /// </summary>
        public void Play()
        {
            //StartCoroutine(Play_Cor());
            OnPlay_Linear();
        }
        int cur_PlayableDirector_Index_temp = 0;
        /// <summary>
        /// 线性操作，不会同步播放，
        /// 必定初始化当前组后续步骤以及完成当前步骤前面步骤
        /// </summary>
        public void  OnPlay_Linear()
        {
            Flash_Display();
            cur_PlayableDirector_Index_temp = cur_PlayableDirector_Index;
            PlayableDirector pd;
            //获取对象身上的Event_Cap_Base
            //执行OnStartEvent
            //如果设置了自动完成当前步骤以前的所有步骤
            if (completeAllPreviousSteps)
            {
                //执行完成当前步骤之前全部步骤（包括步骤组）的全部步骤
                CompleteSteps(0, cur_PlayableDirector_Index_temp-1);
            }
            //初始化自己以及后面的全部步骤，设定为未执行的状态
            InitSteps(cur_PlayableDirector_Index_temp, step_gos.Length-1);
            pd = step_gos[cur_PlayableDirector_Index_temp].GetComponent<PlayableDirector>();
            //如果当前步骤 单纯为步骤，没有TML，则跳过不播放
            if (step_gos.Length <= 0|| cur_PlayableDirector_Index_temp>= step_gos.Length)
            {
                return;
            }
            else if (pd == null)
            {
                TML_Player_Ctrl_Batter tPCB = step_gos[cur_PlayableDirector_Index_temp].GetComponent<TML_Player_Ctrl_Batter>();
                if(tPCB==null)
                {
                    return;
                }
                tPCB.Next_Step_Index(0);
                return;
            }
            //正常播放当前步骤时，恢复为Game Time模式
            pd.timeUpdateMode = DirectorUpdateMode.GameTime;
            pd = step_gos[cur_PlayableDirector_Index_temp].GetComponent<PlayableDirector>();
            pd.time = 0;
            pd.Play();
        }
        /// <summary>
        /// 结束指定的步骤
        /// </summary>
        /// <param name="startIndex">起始(包含)</param>
        /// <param name="endIndex">结束(包含)</param>
        private void CompleteSteps(int startIndex, int endIndex,Action action=null)
        {
            PlayableDirector pd;
            for (int i = startIndex; i <= endIndex; i++)
            {
                pd = step_gos[i].GetComponent<PlayableDirector>();
                if (pd != null)
                {
                    if (pd.time == pd.duration)
                    {
                        //这个步骤已经完成了一次播放，不必再次执行
                    }
                    else
                    {
                        //代表从未播放过，因此需要执行一次播放事件

                        if (pd.time <= 0)
                        {
                            pd.Play();
                        }
                        //pd.timeUpdateMode = DirectorUpdateMode.Manual;
                        //如果已经开始了播放，那么只执行结束事件，然后将动画设置到结束位置
                        pd.Stop();
                        pd.time = pd.duration;
                        pd.Evaluate();
                    }
                }
                //如果没有播放器，拥有一个TML组
                else
                {
                    TML_Player_Ctrl_Batter tPCB = step_gos[i].GetComponent<TML_Player_Ctrl_Batter>();
                    if (tPCB != null)
                    {
                        //执行到这里，说明当前步骤是一个步骤组，
                        //且组内所有步骤都需要被完成，
                        //此组是选定被操作的步骤之前的步骤组
                        tPCB.CompleteSteps(0, tPCB.playablesLength - 1,
                            () =>
                            {
                                //既然是完成组内所有步骤，那就将此组的当前步骤标序号记为最后一步，
                                //表示执行完毕
                                tPCB.cur_PlayableDirector_Index = tPCB.playablesLength - 1;
                            });
                    }

                }
            }
            action?.Invoke();
        }
        /// <summary>
        /// 初始化播放器状态为未播放状态
        /// </summary>
        /// <param name="startIndex">起始（包含自己）</param>
        /// <param name="endIndex">结束（包含自己）</param>
        public void InitSteps(int startIndex,int endIndex)
        {
            PlayableDirector pd;
            //初始化后面的所有步骤为没有播放的状态
            for (int i = startIndex; i <= endIndex; i++)
            {
                pd = step_gos[i].GetComponent<PlayableDirector>();
                if (pd != null)
                {
                    //如果存在playableDirector，则关闭 play on awake,
                    //如果存在有play on awake，光是关闭还不够，因为可能已经执行了播放，
                    //所以需要在关闭之后手动将状态设定为未播放状态
                    //注意：play on awake不会触发played事件，无需对played事件特殊处理
                    pd.playOnAwake = false;
                    pd.timeUpdateMode = DirectorUpdateMode.Manual;
                    pd.time = 0;
                    pd.Evaluate();
                    //  pd.initialTime = 0;
                }
                //被初始化的步骤是一个步骤组的时候，将这一组全部初始化
                else
                {
                    TML_Player_Ctrl_Batter tPCB = step_gos[i].GetComponent<TML_Player_Ctrl_Batter>();
                    if (tPCB != null)
                    {
                        tPCB.InitSteps(0, tPCB.playablesLength-1);
                    }
                }
            }
        }
        /// <summary>
        /// 同时播放多个TML，
        /// 必须是没有先后顺序要求的，
        /// 他们之间的开始事件和结束事件会相互交叉执行
        /// </summary>
        /// <param name="callBack">回调得到序号组中所有步骤的PlayableDirector列表</param>
        /// <param name="stepIndexs">步骤序号组</param>
        public void Multi_StepsPlay( Action<List<PlayableDirector>> callBack=null,params int[] stepIndexs)
        {
            List<PlayableDirector> pdList = new List<PlayableDirector>(); 
            //将输入的步骤全部播放
            for (int i=0;i<stepIndexs.Length;i++)
            {
                pdList.Add(step_gos[stepIndexs[i]].GetComponent<PlayableDirector>());
                step_gos[stepIndexs[i]].GetComponent<PlayableDirector>().Play();
                //例子，在播放器结束后执行方法，然后在方法中将自己卸载掉，恢复到先前的状态
                //pdList[i].stopped += test;
            }
           callBack?.Invoke(pdList);
        }
        //public void test(PlayableDirector pd)
        //{
        //    pd.stopped-=test;
        //}
        /// <summary>
        /// 单个步骤独立播放，不影响前后步骤的进度
        /// </summary>
        /// <param name="stepIndex">步骤序号</param>
        public void Single_StepPlay(int stepIndex,Action<PlayableDirector> callBack = null)
        {
            PlayableDirector pd = null;
            step_gos[stepIndex].GetComponent<PlayableDirector>().Play();
            callBack?.Invoke(pd);
        }
        //上方代码的备份件
        /*   IEnumerator Play_Cor()
           {
               Flash_Display();
               //如果是从1开始，则标记数字（当前步骤数）-1
               if (start1)
               {
                   cur_PlayableDirector_Index -= 1;
               }
               PlayableDirector pd;
               //获取对象身上的Event_Cap_Base
               //执行OnStartEvent
               //如果设置了自动完成当前步骤以前的所有步骤
               if (completeAllPreviousSteps)
               {

                   //将当前步骤以前的所有步骤设定为播放结束
                   for (int i = 0; i < cur_PlayableDirector_Index; i++)
                   {
                       pd = step_gos[i].GetComponent<PlayableDirector>();
                       //判断是否有播放器
                       if (pd != null)
                       {
                           //这个模式表示，前面的模式只有正常完成了播放，则不会再次执行
                           if(pd.time==pd.duration)
                           {
                               continue;
                           }
                           //此模式表示只要前面的步骤开始播放了，就不会再次执行
                           //if (pd.time > 0)
                           //{
                           //    continue;
                           //}
                           //暂时放弃在播放器面板上挂载事件的方式，这会很混乱
                           pd.timeUpdateMode = DirectorUpdateMode.Manual;

                           //一般情况下步骤动画设定为播放完成则自动停止
                           pd.Play();//播放，也开始played事件
                           pd.Stop();//播放，也开始stoped事件
                           pd.time = pd.duration;
                           pd.Evaluate();
                           //停顿一帧等待stoped的方法完成
                           yield return null;
                       }
                       else
                       {
                           //查看是否这是一个TML组
                           TML_Player_Ctrl_Batter tPCB = step_gos[i].GetComponent<TML_Player_Ctrl_Batter>();
                           if(!ReferenceEquals(tPCB,null))
                           {
                             //  tPCB.CustomStepsCount=
                           }

                       }
                       yield return null;
                   }
               }
               //初始化后面的所有步骤为没有播放的状态
               for (int i= cur_PlayableDirector_Index; i<step_gos.Length;i++)
               {
                   pd = step_gos[i].GetComponent<PlayableDirector>();
                   if(pd!=null)
                   {
                       pd.time = 0;
                       pd.Evaluate();
                     //  pd.initialTime = 0;
                   }
               }
               pd= step_gos[cur_PlayableDirector_Index].GetComponent<PlayableDirector>();
               //如果当前步骤 单纯为步骤，没有TML，则跳过不播放
               if (step_gos.Length <= 0 || pd == null)
               {
                   yield break;
               }
               //正常播放当前步骤时，恢复为Game Time模式
               pd.timeUpdateMode= DirectorUpdateMode.GameTime;
                pd = step_gos[cur_PlayableDirector_Index].GetComponent<PlayableDirector>();
               pd.time = 0;
               pd.Play();
           }*/
        /// <summary>
        /// 创建步骤数量以及当前步骤所在的显示提醒标记
        /// </summary>
        public void StepImageCreate()
        {
            //if(!IsCustomStepsCount)
            //{
            //    for (int i = 0; i < playablesLength; i++)
            //    {
            //        listStep.Add(i);
            //    }
            //    CustomStepsCount=listStep.Count;
            //}
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
                for (int i = 0; i < step_gos.Length; i++)
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
                for (int i = 0; i < step_gos.Length; i++)
                {

                    if (step_gos[i] != null)
                    {
                        step_gos[i].gameObject.SetActive(i == cur_PlayableDirector_Index);

                    }
                }
            }
            if (cur_PlayableDirector_Index >= 0 && cur_PlayableDirector_Index < step_gos.Length)
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