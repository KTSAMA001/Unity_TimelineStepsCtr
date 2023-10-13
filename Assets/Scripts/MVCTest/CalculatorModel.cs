// Model��
using System;
using UnityEngine;

public class CalculatorModel : MonoBehaviour
{
    // ���ݱ仯�¼�
    public event Action<string> onDataChanged;

    // ��ǰ���������
    private string currentNumber;
    // ��һ�����������
    private string previousNumber;
    // ��ǰ����������
    private string currentOperator;
    // ������
    private string result;

    // ��ʼ��
    private void Start()
    {
        // �������
        Clear();
        // �������ݱ仯�¼�
        onDataChanged?.Invoke(result);
    }

    // �������ֵķ���
    public void InputNumber(string number)
    {
        // �����ǰ�������0�����������Ҳ��0�������
        if (currentNumber == "0" && number == "0")
            return;
        // �����ǰ�������0����������Ĳ���0�����滻
        if (currentNumber == "0" && number != "0")
            currentNumber = number;
        else // ����׷�������������
            currentNumber += number;
        // ���½��Ϊ��ǰ���������
        result = currentNumber;
        // �������ݱ仯�¼�
        onDataChanged?.Invoke(result);
    }

    // ����������ķ���
    public void InputOperator(string op)
    {
        // �����ǰû���������֣������
        if (string.IsNullOrEmpty(currentNumber))
            return;
        // �����һ��������������Ϊ�գ����Ƚ��м���
        if (!string.IsNullOrEmpty(currentOperator))
            Calculate();
        else // ���򣬽���ǰ��������ָ�ֵ����һ�����������
            previousNumber = currentNumber;
        // ��յ�ǰ���������
        currentNumber = "";
        // ����������������ֵ����ǰ����������
        currentOperator = op;
        // ���½��Ϊ��һ����������ֺ͵�ǰ���������������
        result = previousNumber + " " + currentOperator;
        // �������ݱ仯�¼�
        onDataChanged?.Invoke(result);
    }

    // ������ݵķ���
    public void Clear()
    {
        // �����������
        currentNumber = "";
        previousNumber = "";
        currentOperator = "";
        result = "0";
        // �������ݱ仯�¼�
        onDataChanged?.Invoke(result);
    }

    // �������ķ���
    public void Calculate()
    {
        // �����ǰû���������ֻ�������������
        if (string.IsNullOrEmpty(currentNumber) || string.IsNullOrEmpty(currentOperator))
            return;
        
// ���ݲ�ͬ����������в�ͬ�ļ����߼�

        switch (currentOperator)

        {

            case "+": // �ӷ�

                result = (float.Parse(previousNumber) + float.Parse(currentNumber)).ToString();

                break;

            case "-": // ����

                result = (float.Parse(previousNumber) - float.Parse(currentNumber)).ToString();

                break;

            case "*": // �˷�

                result = (float.Parse(previousNumber) * float.Parse(currentNumber)).ToString();

                break;

            case "/": // ����

                // �������Ϊ0������ʾ������Ϣ

                if (currentNumber == "0")

                {

                    result = "Error";

                }

                else

                {

                    result = (float.Parse(previousNumber) / float.Parse(currentNumber)).ToString();

                }

                break;

        }

        // ��յ�ǰ��������ֺ������

        currentNumber = "";

        currentOperator = "";

        // �������ݱ仯�¼�

        onDataChanged?.Invoke(result);

    }

}

