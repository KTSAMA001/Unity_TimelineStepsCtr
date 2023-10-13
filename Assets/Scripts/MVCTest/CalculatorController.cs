// Controller��
using UnityEngine;

public class CalculatorController : MonoBehaviour
{
    // Model����
    private CalculatorModel model;
    // View����
    private CalculatorView view;

    // ��ʼ��
    private void Start()
    {
        // ��ȡModel���
        model = GetComponent<CalculatorModel>();
        // ��ȡView���
        view = GetComponent<CalculatorView>();
        // ΪModel������ݱ仯������
        model.onDataChanged += OnDataChanged;
    }

    // ��Model���ݱ仯ʱ���ô˷���
    private void OnDataChanged(string data)
    {
        // ����View��ʾ����
        view.UpdateDisplay(data);
    }

    // ���û���������ʱ���ô˷���
    public void OnNumberInput(string number)
    {
        // ����Model�ķ���
        model.InputNumber(number);
    }

    // ���û����������ʱ���ô˷���
    public void OnOperatorInput(string op)
    {
        // ����Model�ķ���
        model.InputOperator(op);
    }

    // ���û��������ʱ���ô˷���
    public void OnClearInput()
    {
        // ����Model�ķ���
        model.Clear();
    }

    // ���û�����Ⱥ�ʱ���ô˷���
    public void OnEqualInput()
    {
        // ����Model�ķ���
        model.Calculate();
    }
}
