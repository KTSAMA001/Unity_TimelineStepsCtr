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
             /// ��ʼ״����cur_PlayableDierctor_IndexΪ-1,��ʾ����δ��ʼ
             /// </summary>
        [Header("��ǰ���ŵ�λ�ã�-1��ʾ����ǰ�ȿճ�һ��λ��")]
        [SerializeField] int cur_PlayableDirector_Index = -1;
        /// <summary>
        /// ��������Ǵ�1��ʼ����֮��0��ʼ��
        /// </summary>
        [Header("������������Ǵ�1��ʼ����֮��0��ʼ��")]
        public bool start1=true;
        [Header("�������ĸ�����")]
        [SerializeField] GameObject playableDirectorsGroup;
        /// <summary>
        /// ��ѡ����������е�TML
        /// </summary>
        [ReadOnly]
        [Header("��ѡ����������е�TML")]
        [SerializeField] PlayableDirector[] playableDirectors;
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
        [Header("�Ƿ����л�����ʱ������һ���Ķ���")]
        public bool disEnableLastObject = false;

        /// <summary>
        /// �Ƿ���ʾ���������Լ���ǰ�������ڵ���ʾ
        /// </summary> 
        [Header("�Ƿ���ʾ���������Լ���ǰ�������ڵ���ʾ")]
        [SerializeField] private bool showStepTips = true;
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
        /// ��ʼ���Ĳ���ͳһִ�еص�
        /// </summary>
        void InitGroups()
        {
            InitPlayableDirectors();


       
            //���в������ĵ�ע�Ὺ�������¼��Լ����������¼�
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
            playableDirectors = new PlayableDirector[length];
            for (int i = 0; i < length; i++)
            {
                playableDirectors[i] = _pDGroup.transform.GetChild(i).GetComponent<PlayableDirector>();

            }
            StepImageCreate();
        }
        /// <summary>
        /// �¼�ע���������
        /// </summary>
        public void EventRegister()
        {

        }



        /// <summary>
        /// �����һ��
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
        /// ָ����һ���Ĳ���
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
        /// �����һ��
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
        /// ָ����һ���Ĳ���
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
        /// ���ŵ�ǰ����
        /// </summary>
        public void Play()
        {
            StartCoroutine(Play_Cor());
        }

        IEnumerator Play_Cor()
        {
            Flash_Display();
            //��ȡ�������ϵ�Event_Cap_Base
            //ִ��OnStartEvent
            //����������Զ���ɵ�ǰ������ǰ�����в���
            if (completeAllPreviousSteps)
            {
                //����ǰ������ǰ�����в����趨Ϊ���Ž���
                for (int i = 0; i < cur_PlayableDirector_Index; i++)
                {
                    //�ж��Ƿ��в�����
                    if (playableDirectors[i] != null)
                    {
                        //��ǰ��Ķ������趨������ɵ�״̬,
                        //-0.01��Ŀ����Ϊ���ö������һ֡��TML�Ϲ��ص��¼���������
                       // playableDirectors[i].time = playableDirectors[i].duration - Time.deltaTime/2;
                       //��ʱ�����ڲ���������Ϲ����¼��ķ�ʽ�����ܻ���
                        playableDirectors[i].time = playableDirectors[i].duration;
                        //ͣ��һ֡�ȴ�Play
                        yield return null;
                        //һ������²��趯���趨Ϊ����������Զ�ֹͣ
                        playableDirectors[i].Play();//���ţ�Ҳ��ʼplayed�¼�
                        //ͣ��һ֡�ȴ�stoped�ķ������
                       yield return null;
                    }
                    else
                    {
                        //�鿴�Ƿ�����һ��TML��
                        TML_Player_Ctrl_Batter tPCB = playableDirectors[i].GetComponent<TML_Player_Ctrl_Batter>();
                        if(!ReferenceEquals(tPCB,null))
                        {
                          //  tPCB.CustomStepsCount=
                        }
                    }
                    
                }
            }
            //��ʼ����������в���Ϊû�в��ŵ�״̬
            for(int i= cur_PlayableDirector_Index; i<playableDirectors.Length;i++)
            {
                playableDirectors[i].initialTime = 0;
            }
            //�����ǰ���� ����Ϊ���裬û��TML��������������
            if (playableDirectors.Length <= 0 || playableDirectors[cur_PlayableDirector_Index] == null)
            {
                yield break;
            }
            //����Ǵ�1��ʼ���������֣���ǰ��������-1
            if (start1)
            {
                cur_PlayableDirector_Index -= 1;
            }

            playableDirectors[cur_PlayableDirector_Index].time = 0;
            playableDirectors[cur_PlayableDirector_Index].Play();
        }

        /*     /// <summary>
       /// �޸ĵ�ǰ�Ĳ�����ţ����Ҳ���
       /// </summary>
       public void ChangeCurIndexAndPlay(int _index)
       {
           Cur_PlayableDirector_Index = _index;
       }*/


        /// <summary>
        /// �������������Լ���ǰ�������ڵ���ʾ���ѱ��
        /// </summary>
        public void StepImageCreate()
        {
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
        /// ����UI��ˢ�²���
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