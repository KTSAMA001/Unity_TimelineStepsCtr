// View层
using UnityEngine;
using UnityEngine.UI;

public class CalculatorView : MonoBehaviour
{
    // UI元素
    public Text displayText;
    public Button[] numberButtons;
    public Button[] operatorButtons;
    public Button clearButton;
    public Button equalButton;

    // Controller引用
    private CalculatorController controller;

    // 初始化
    private void Start()
    {
        // 获取Controller组件
        controller = GetComponent<CalculatorController>();
        // 为每个按钮添加点击事件
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

    // 数字按钮点击事件
    private void OnNumberButtonClick(Button button)
    {
        // 获取按钮上的数字
        string number = button.GetComponentInChildren<Text>().text;
        // 调用Controller的方法
        controller.OnNumberInput(number);
    }

    // 运算符按钮点击事件
    private void OnOperatorButtonClick(Button button)
    {
        // 获取按钮上的运算符
        string op = button.GetComponentInChildren<Text>().text;
        // 调用Controller的方法
        controller.OnOperatorInput(op);
    }

    // 清除按钮点击事件
    private void OnClearButtonClick()
    {
        // 调用Controller的方法
        controller.OnClearInput();
    }

    // 等号按钮点击事件
    private void OnEqualButtonClick()
    {
        // 调用Controller的方法
        controller.OnEqualInput();
    }

    // 更新显示屏内容
    public void UpdateDisplay(string content)
    {
        displayText.text = content;
    }
}
