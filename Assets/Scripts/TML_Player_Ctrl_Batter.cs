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
        /// ������������
        /// </summary>
        [ReadOnly]
        public int playablesLength;
        /// <summary>
        /// ��ʼ״����cur_PlayableDierctor_IndexΪ-1,��ʾ����δ��ʼ
        /// </summary>
        [Header("��ǰ���ŵ�λ�ã�-1��ʾ����ǰ�ȿճ�һ��λ��")]
        [SerializeField] int cur_PlayableDirector_Index = -1;
    /*    /// <summary>
        /// ��������Ǵ�1��ʼ����֮��0��ʼ��
        /// </summary>
        [Header("������������Ǵ�1��ʼ����֮��0��ʼ��")]
        bool start1=false;*/
        [Header("�������ĸ�����")]
        [SerializeField] GameObject playableDirectorsGroup;
        /// <summary>
        /// ��ѡ����������е�TML
        /// </summary>
        [ReadOnly]
        [Header("��ѡ����������е�TML")]
        [SerializeField] GameObject[] step_gos;
        /// <summary>
        /// �Ƿ���˲��Ͳ��ŵ�һ�����������������ڵ�һ����������Ҫ���һ����һ���Ż���ֵ�һ��
        /// </summary>
        [Header("�Ƿ���˲��Ͳ��ŵ�һ������")]
        [SerializeField] bool IsFirstPlay = false;

        /// <summary>
        /// �Ƿ����л�����ʱֱ�Ӳ���TML
        /// </summary>
        [Header("�Ƿ����л�����ʱֱ�Ӳ���TML")]
        public bool playOnStepChange = false;
        /// <summary>
        /// Complete all previous steps;�Զ����ǰ�����еĲ���
        /// </summary>
        [Header("�Զ����ǰ�����еĲ���")]
        public bool completeAllPreviousSteps = true;
        /// <summary>
        /// �Ƿ����л�����ʱ������һ���Ķ���
        /// </summary>
        [Header("�Ƿ����л�����ʱ������һ���Ķ���(�ᵼ���޷���������played�Լ�stoped�¼�)")]
        public bool disEnableLastObject = false;

        /// <summary>
        /// �Ƿ���ʾ���������Լ���ǰ�������ڵ���ʾ
        /// </summary> 
        [Header("�Ƿ���ʾ���������Լ���ǰ�������ڵ���ʾ")]
        [SerializeField] private bool showStepTips = false;
        [ShowIf("showStepTips")]
        public Color curStepImageColor = new Color(0.349f, 0.145f, 0.4f, 1f);
        [ShowIf("showStepTips")]
        public Color defStepImageColor = new Color(1, 1, 1, 1);
        [ShowIf("showStepTips")]
        [Header("�����ǵ�Ԥ����")]
        [SerializeField] GameObject ImgStep;
        [ShowIf("showStepTips")]
        [Header("�����ǵĸ�����")]
        [SerializeField] GameObject Step_Group_go;
        /// <summary>
        /// �Ƿ��Զ���������
        /// </summary>
        [Header("�Ƿ��Զ���������")]
        [ShowIf("showStepTips")]
        [SerializeField] private bool isCustomStepsCount = false;
        [ShowIf("isCustomStepsCount")]
        [Header("�Զ��岽������������ڼ��ض�Ӧ�������")]
        public int CustomStepsCount = 0;
        [ShowIf("isCustomStepsCount")]
        [InfoBox("��ΪTML�������Ǹ���TML����ȷ���Ĳ������������Դ��ڶ��TML�׶�����ͬһ�������������ʱ����Ҫ������׶�ͬʱ���Ϊͬһ������")]
        public List<int> listStep = new List<int>();

        List<Image> images_Step_List = new List<Image>();

        /// <summary>
        /// �ⲿ������Ҫ����PlayableDirector���б�
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
            //�����е�TML��ʼ��׼�����ŵ�״̬
            InitSteps(0,playablesLength-1);
            Flash_Display();
            StepImageCreate();
        }
        private void Start()
        {
           // Multi_StepsPlay(new int[] { 1,3,4,5});
        }
        /// <summary>
        /// ��ʼ���Ĳ���ͳһִ�еص�
        /// </summary>
        void InitGroups()
        {
         
            //���û���ֶ���ֵȷ�ϵ�ǰ��TML�飬�Ǿ�Ĭ���Լ�����TML��
            if (playableDirectorsGroup==null)
            {
                playableDirectorsGroup = gameObject;
            }
            InitPlayableDirectors();
          
            //���в������ĵ�ע�Ὺ�������¼��Լ����������¼�
            for (int i = 0; i < step_gos.Length; i++)
            {
                int index = i;
                PlayableDirector pd = step_gos[index].GetComponent<PlayableDirector>();
                if (pd != null)//�˴�û��ʹ��ReferenceEquals(pd, null)��ԭ��pdΪ�յõ�����"null"����null
                {
                    Event_Cap_Base event_Cap_Base = step_gos[index].GetComponent<Event_Cap_Base>();
                    if (!ReferenceEquals(event_Cap_Base, null))//�����ٶȸ���
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
        /// ��ʼ��TML������
        /// </summary>
        private void InitPlayableDirectors()
        {
            Transform playableDirectorsGroup_Trans = playableDirectorsGroup.transform;
            InitPlayableDirectors(playableDirectorsGroup_Trans.gameObject);
        }
        /// <summary>
        /// ����ӵ��TML���������ӵ�е�TML��ʼ�����Ź��������
        /// </summary>
        /// <param name="_pDGroup">TML�����Ա���������GameObject��</param>
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
        /// �¼�ע���������
        /// </summary>
        public void EventRegister()
        {

        }



        /// <summary>
        /// �����һ�������Բ��裬
        /// ��ǿ�����ȫ��ǰ�ò����Լ�����ȫ�����ò���
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
        /// ָ����һ���Ĳ��裬���Բ���
        /// ��ǿ�����ȫ��ǰ�ò����Լ�����ȫ�����ò���
        /// </summary>
        /// <param name="index"></param>
        public void Next_Step_Index(int index)
        {
            if(index>=playablesLength)
            {
                Debug.LogWarning("���ڳ��Բ��ŵ�TML��Ŵ������е����ֵ" + playablesLength +"-1");
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
        /// �����һ�������Բ���
        /// ��ǿ�����ȫ��ǰ�ò����Լ�����ȫ�����ò���
        /// </summary>
        public void Last_Step()
        {
            if (cur_PlayableDirector_Index < 1)
            {
                Debug.Log("����ֹͣ");
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
        /// ָ����һ���Ĳ��裬���Բ���
        /// ��ǿ�����ȫ��ǰ�ò����Լ�����ȫ�����ò���
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
        /// ���ڲ������֮�󣬽�timeLine��ʱ����̶�������λ����
        /// </summary>
        /// <param name="pd">��Ҫ��ִ�е�pd</param>
        public void OnPlayed(PlayableDirector pd)
        {
            pd.timeUpdateMode = DirectorUpdateMode.Manual;
            pd.time = pd.duration;
            pd.Evaluate();
        }
        /// <summary>  
        /// ���ŵ�ǰ����
        /// </summary>
        public void Play()
        {
            //StartCoroutine(Play_Cor());
            OnPlay_Linear();
        }
        int cur_PlayableDirector_Index_temp = 0;
        /// <summary>
        /// ���Բ���������ͬ�����ţ�
        /// �ض���ʼ����ǰ����������Լ���ɵ�ǰ����ǰ�沽��
        /// </summary>
        public void  OnPlay_Linear()
        {
            Flash_Display();
            cur_PlayableDirector_Index_temp = cur_PlayableDirector_Index;
            PlayableDirector pd;
            //��ȡ�������ϵ�Event_Cap_Base
            //ִ��OnStartEvent
            //����������Զ���ɵ�ǰ������ǰ�����в���
            if (completeAllPreviousSteps)
            {
                //ִ����ɵ�ǰ����֮ǰȫ�����裨���������飩��ȫ������
                CompleteSteps(0, cur_PlayableDirector_Index_temp-1);
            }
            //��ʼ���Լ��Լ������ȫ�����裬�趨Ϊδִ�е�״̬
            InitSteps(cur_PlayableDirector_Index_temp, step_gos.Length-1);
            pd = step_gos[cur_PlayableDirector_Index_temp].GetComponent<PlayableDirector>();
            //�����ǰ���� ����Ϊ���裬û��TML��������������
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
            //�������ŵ�ǰ����ʱ���ָ�ΪGame Timeģʽ
            pd.timeUpdateMode = DirectorUpdateMode.GameTime;
            pd = step_gos[cur_PlayableDirector_Index_temp].GetComponent<PlayableDirector>();
            pd.time = 0;
            pd.Play();
        }
        /// <summary>
        /// ����ָ���Ĳ���
        /// </summary>
        /// <param name="startIndex">��ʼ(����)</param>
        /// <param name="endIndex">����(����)</param>
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
                        //��������Ѿ������һ�β��ţ������ٴ�ִ��
                    }
                    else
                    {
                        //�����δ���Ź��������Ҫִ��һ�β����¼�

                        if (pd.time <= 0)
                        {
                            pd.Play();
                        }
                        //pd.timeUpdateMode = DirectorUpdateMode.Manual;
                        //����Ѿ���ʼ�˲��ţ���ôִֻ�н����¼���Ȼ�󽫶������õ�����λ��
                        pd.Stop();
                        pd.time = pd.duration;
                        pd.Evaluate();
                    }
                }
                //���û�в�������ӵ��һ��TML��
                else
                {
                    TML_Player_Ctrl_Batter tPCB = step_gos[i].GetComponent<TML_Player_Ctrl_Batter>();
                    if (tPCB != null)
                    {
                        //ִ�е����˵����ǰ������һ�������飬
                        //���������в��趼��Ҫ����ɣ�
                        //������ѡ���������Ĳ���֮ǰ�Ĳ�����
                        tPCB.CompleteSteps(0, tPCB.playablesLength - 1,
                            () =>
                            {
                                //��Ȼ������������в��裬�Ǿͽ�����ĵ�ǰ�������ż�Ϊ���һ����
                                //��ʾִ�����
                                tPCB.cur_PlayableDirector_Index = tPCB.playablesLength - 1;
                            });
                    }

                }
            }
            action?.Invoke();
        }
        /// <summary>
        /// ��ʼ��������״̬Ϊδ����״̬
        /// </summary>
        /// <param name="startIndex">��ʼ�������Լ���</param>
        /// <param name="endIndex">�����������Լ���</param>
        public void InitSteps(int startIndex,int endIndex)
        {
            PlayableDirector pd;
            //��ʼ����������в���Ϊû�в��ŵ�״̬
            for (int i = startIndex; i <= endIndex; i++)
            {
                pd = step_gos[i].GetComponent<PlayableDirector>();
                if (pd != null)
                {
                    //�������playableDirector����ر� play on awake,
                    //���������play on awake�����ǹرջ���������Ϊ�����Ѿ�ִ���˲��ţ�
                    //������Ҫ�ڹر�֮���ֶ���״̬�趨Ϊδ����״̬
                    //ע�⣺play on awake���ᴥ��played�¼��������played�¼����⴦��
                    pd.playOnAwake = false;
                    pd.timeUpdateMode = DirectorUpdateMode.Manual;
                    pd.time = 0;
                    pd.Evaluate();
                    //  pd.initialTime = 0;
                }
                //����ʼ���Ĳ�����һ���������ʱ�򣬽���һ��ȫ����ʼ��
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
        /// ͬʱ���Ŷ��TML��
        /// ������û���Ⱥ�˳��Ҫ��ģ�
        /// ����֮��Ŀ�ʼ�¼��ͽ����¼����໥����ִ��
        /// </summary>
        /// <param name="callBack">�ص��õ�����������в����PlayableDirector�б�</param>
        /// <param name="stepIndexs">���������</param>
        public void Multi_StepsPlay( Action<List<PlayableDirector>> callBack=null,params int[] stepIndexs)
        {
            List<PlayableDirector> pdList = new List<PlayableDirector>(); 
            //������Ĳ���ȫ������
            for (int i=0;i<stepIndexs.Length;i++)
            {
                pdList.Add(step_gos[stepIndexs[i]].GetComponent<PlayableDirector>());
                step_gos[stepIndexs[i]].GetComponent<PlayableDirector>().Play();
                //���ӣ��ڲ�����������ִ�з�����Ȼ���ڷ����н��Լ�ж�ص����ָ�����ǰ��״̬
                //pdList[i].stopped += test;
            }
           callBack?.Invoke(pdList);
        }
        //public void test(PlayableDirector pd)
        //{
        //    pd.stopped-=test;
        //}
        /// <summary>
        /// ��������������ţ���Ӱ��ǰ����Ľ���
        /// </summary>
        /// <param name="stepIndex">�������</param>
        public void Single_StepPlay(int stepIndex,Action<PlayableDirector> callBack = null)
        {
            PlayableDirector pd = null;
            step_gos[stepIndex].GetComponent<PlayableDirector>().Play();
            callBack?.Invoke(pd);
        }
        //�Ϸ�����ı��ݼ�
        /*   IEnumerator Play_Cor()
           {
               Flash_Display();
               //����Ǵ�1��ʼ���������֣���ǰ��������-1
               if (start1)
               {
                   cur_PlayableDirector_Index -= 1;
               }
               PlayableDirector pd;
               //��ȡ�������ϵ�Event_Cap_Base
               //ִ��OnStartEvent
               //����������Զ���ɵ�ǰ������ǰ�����в���
               if (completeAllPreviousSteps)
               {

                   //����ǰ������ǰ�����в����趨Ϊ���Ž���
                   for (int i = 0; i < cur_PlayableDirector_Index; i++)
                   {
                       pd = step_gos[i].GetComponent<PlayableDirector>();
                       //�ж��Ƿ��в�����
                       if (pd != null)
                       {
                           //���ģʽ��ʾ��ǰ���ģʽֻ����������˲��ţ��򲻻��ٴ�ִ��
                           if(pd.time==pd.duration)
                           {
                               continue;
                           }
                           //��ģʽ��ʾֻҪǰ��Ĳ��迪ʼ�����ˣ��Ͳ����ٴ�ִ��
                           //if (pd.time > 0)
                           //{
                           //    continue;
                           //}
                           //��ʱ�����ڲ���������Ϲ����¼��ķ�ʽ�����ܻ���
                           pd.timeUpdateMode = DirectorUpdateMode.Manual;

                           //һ������²��趯���趨Ϊ����������Զ�ֹͣ
                           pd.Play();//���ţ�Ҳ��ʼplayed�¼�
                           pd.Stop();//���ţ�Ҳ��ʼstoped�¼�
                           pd.time = pd.duration;
                           pd.Evaluate();
                           //ͣ��һ֡�ȴ�stoped�ķ������
                           yield return null;
                       }
                       else
                       {
                           //�鿴�Ƿ�����һ��TML��
                           TML_Player_Ctrl_Batter tPCB = step_gos[i].GetComponent<TML_Player_Ctrl_Batter>();
                           if(!ReferenceEquals(tPCB,null))
                           {
                             //  tPCB.CustomStepsCount=
                           }

                       }
                       yield return null;
                   }
               }
               //��ʼ����������в���Ϊû�в��ŵ�״̬
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
               //�����ǰ���� ����Ϊ���裬û��TML��������������
               if (step_gos.Length <= 0 || pd == null)
               {
                   yield break;
               }
               //�������ŵ�ǰ����ʱ���ָ�ΪGame Timeģʽ
               pd.timeUpdateMode= DirectorUpdateMode.GameTime;
                pd = step_gos[cur_PlayableDirector_Index].GetComponent<PlayableDirector>();
               pd.time = 0;
               pd.Play();
           }*/
        /// <summary>
        /// �������������Լ���ǰ�������ڵ���ʾ���ѱ��
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
            //�������Ѿ�ӵ�е����ݣ��ٴθ��������ݴ���������ʾ
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
        /// ����UI��ˢ�²���
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
        /// �޸ĵ�ǰ�Ĳ�����ţ����Ҳ���
        /// </summary>
        /// <param name="_index">ָ�����ŵĲ�����</param>
        /// <param name="finalCallBack">���������Ҫִ�еķ���</param>
        public void ChangeCurIndexAndPlay(int _index)
        {
            cur_PlayableDirector_Index = _index;
            Play();
        }
        private void OnValidate()
        {
            //���������û�д򿪣��ӿ���Ҳ�ر�
            if (!showStepTips)
            {
                IsCustomStepsCount = showStepTips;
            }


        }
    }
}