using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Frame.UI;

public class TML_Player_Panel : UI_Panel_Base
{
//auto
  Transform _transform;
   public void Start()
  {
		
  }
	Button btn_Last_KT = null;
	Button btn_Next_KT = null;
	Button btn_Play_KT = null;
	Button btn_Focus_KT = null;
	[SerializeField]Transform step_Group_trans;
    [SerializeField]private GameObject imgStep;

    public Button Btn_Last_KT{get => btn_Last_KT; set => btn_Last_KT = value;} 
	public Button Btn_Next_KT{get => btn_Next_KT; set => btn_Next_KT = value;} 
	public Button Btn_Play_KT{get => btn_Play_KT; set => btn_Play_KT = value;}
    public GameObject ImgStep { get => imgStep; set => imgStep = value; }
    public Transform Step_Group_trans { get => step_Group_trans; set => step_Group_trans = value; }
    public Button Btn_Focus_KT { get => btn_Focus_KT; set => btn_Focus_KT = value; }

    public void Init(Action action)
  {
        _transform=transform;
        Btn_Last_KT = _transform.Find("Panel_TML_Player_Group_KT/InteractionGroup_KT/Btn_Last_KT").GetComponent<Button>();
		Btn_Next_KT = _transform.Find("Panel_TML_Player_Group_KT/InteractionGroup_KT/Btn_Next_KT").GetComponent<Button>();
		Btn_Play_KT = _transform.Find("Panel_TML_Player_Group_KT/InteractionGroup_KT/Btn_Play_KT").GetComponent<Button>();
        Btn_Focus_KT = _transform.Find("Panel_TML_Player_Group_KT/InteractionGroup_KT/Btn_Focus_KT").GetComponent<Button>();
		
     action?.Invoke();
  }
}
