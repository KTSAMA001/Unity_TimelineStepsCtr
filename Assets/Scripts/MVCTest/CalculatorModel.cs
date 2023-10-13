// Model层
using System;
using UnityEngine;

public class CalculatorModel : MonoBehaviour
{
    // 数据变化事件
    public event Action<string> onDataChanged;

    // 当前输入的数字
    private string currentNumber;
    // 上一个输入的数字
    private string previousNumber;
    // 当前输入的运算符
    private string currentOperator;
    // 计算结果
    private string result;

    // 初始化
    private void Start()
    {
        // 清空数据
        Clear();
        // 触发数据变化事件
        onDataChanged?.Invoke(result);
    }

    // 输入数字的方法
    public void InputNumber(string number)
    {
        // 如果当前输入的是0，且新输入的也是0，则忽略
        if (currentNumber == "0" && number == "0")
            return;
        // 如果当前输入的是0，且新输入的不是0，则替换
        if (currentNumber == "0" && number != "0")
            currentNumber = number;
        else // 否则，追加新输入的数字
            currentNumber += number;
        // 更新结果为当前输入的数字
        result = currentNumber;
        // 触发数据变化事件
        onDataChanged?.Invoke(result);
    }

    // 输入运算符的方法
    public void InputOperator(string op)
    {
        // 如果当前没有输入数字，则忽略
        if (string.IsNullOrEmpty(currentNumber))
            return;
        // 如果上一个输入的运算符不为空，则先进行计算
        if (!string.IsNullOrEmpty(currentOperator))
            Calculate();
        else // 否则，将当前输入的数字赋值给上一个输入的数字
            previousNumber = currentNumber;
        // 清空当前输入的数字
        currentNumber = "";
        // 将新输入的运算符赋值给当前输入的运算符
        currentOperator = op;
        // 更新结果为上一个输入的数字和当前输入的运算符的组合
        result = previousNumber + " " + currentOperator;
        // 触发数据变化事件
        onDataChanged?.Invoke(result);
    }

    // 清除数据的方法
    public void Clear()
    {
        // 清空所有数据
        currentNumber = "";
        previousNumber = "";
        currentOperator = "";
        result = "0";
        // 触发数据变化事件
        onDataChanged?.Invoke(result);
    }

    // 计算结果的方法
    public void Calculate()
    {
        // 如果当前没有输入数字或运算符，则忽略
        if (string.IsNullOrEmpty(currentNumber) || string.IsNullOrEmpty(currentOperator))
            return;
        
// 根据不同的运算符进行不同的计算逻辑

        switch (currentOperator)

        {

            case "+": // 加法

                result = (float.Parse(previousNumber) + float.Parse(currentNumber)).ToString();

                break;

            case "-": // 减法

                result = (float.Parse(previousNumber) - float.Parse(currentNumber)).ToString();

                break;

            case "*": // 乘法

                result = (float.Parse(previousNumber) * float.Parse(currentNumber)).ToString();

                break;

            case "/": // 除法

                // 如果除数为0，则显示错误信息

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

        // 清空当前输入的数字和运算符

        currentNumber = "";

        currentOperator = "";

        // 触发数据变化事件

        onDataChanged?.Invoke(result);

    }

}

