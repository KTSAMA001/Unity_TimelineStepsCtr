// View��
using UnityEngine;
using UnityEngine.UI;

public class CalculatorView : MonoBehaviour
{
    // UIԪ��
    public Text displayText;
    public Button[] numberButtons;
    public Button[] operatorButtons;
    public Button clearButton;
    public Button equalButton;

    // Controller����
    private CalculatorController controller;

    // ��ʼ��
    private void Start()
    {
        // ��ȡController���
        controller = GetComponent<CalculatorController>();
        // Ϊÿ����ť��ӵ���¼�
        foreach (var button in numberButtons)
        {
            button.onClick.AddListener(() => OnNumberButtonClick(button));
        }
        foreach (var button in operatorButtons)
        {
            button.onClick.AddListener(() => OnOperatorButtonClick(button));
        }
        clearButton.onClick.AddListener(OnClearButtonClick);
        equalButton.onClick.AddListener(OnEqualButtonClick);
    }

    // ���ְ�ť����¼�
    private void OnNumberButtonClick(Button button)
    {
        // ��ȡ��ť�ϵ�����
        string number = button.GetComponentInChildren<Text>().text;
        // ����Controller�ķ���
        controller.OnNumberInput(number);
    }

    // �������ť����¼�
    private void OnOperatorButtonClick(Button button)
    {
        // ��ȡ��ť�ϵ������
        string op = button.GetComponentInChildren<Text>().text;
        // ����Controller�ķ���
        controller.OnOperatorInput(op);
    }

    // �����ť����¼�
    private void OnClearButtonClick()
    {
        // ����Controller�ķ���
        controller.OnClearInput();
    }

    // �ȺŰ�ť����¼�
    private void OnEqualButtonClick()
    {
        // ����Controller�ķ���
        controller.OnEqualInput();
    }

    // ������ʾ������
    public void UpdateDisplay(string content)
    {
        displayText.text = content;
    }
}
