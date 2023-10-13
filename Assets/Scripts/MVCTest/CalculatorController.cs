// Controller层
using UnityEngine;

public class CalculatorController : MonoBehaviour
{
    // Model引用
    private CalculatorModel model;
    // View引用
    private CalculatorView view;

    // 初始化
    private void Start()
    {
        // 获取Model组件
        model = GetComponent<CalculatorModel>();
        // 获取View组件
        view = GetComponent<CalculatorView>();
        // 为Model添加数据变化监听器
        model.onDataChanged += OnDataChanged;
    }

    // 当Model数据变化时调用此方法
    private void OnDataChanged(string data)
    {
        // 更新View显示内容
        view.UpdateDisplay(data);
    }

    // 当用户输入数字时调用此方法
    public void OnNumberInput(string number)
    {
        // 调用Model的方法
        model.InputNumber(number);
    }

    // 当用户输入运算符时调用此方法
    public void OnOperatorInput(string op)
    {
        // 调用Model的方法
        model.InputOperator(op);
    }

    // 当用户输入清除时调用此方法
    public void OnClearInput()
    {
        // 调用Model的方法
        model.Clear();
    }

    // 当用户输入等号时调用此方法
    public void OnEqualInput()
    {
        // 调用Model的方法
        model.Calculate();
    }
}
