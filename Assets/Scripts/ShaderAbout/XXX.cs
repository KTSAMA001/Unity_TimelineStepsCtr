using System;
using UnityEngine;
using UnityEditor;

public class XXX : ShaderGUI
{
    // OnGuI 接收的两个参数 ：
    MaterialEditor materialEditor;//当前材质面板
    MaterialProperty[] materialProperty;//当前shader的properties
    Material targetMat;//绘制对象材质球

    // 折叠栏
    private bool m_GUITest = true;

    // 主要实现逻辑
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        this.materialEditor = materialEditor; // 当前编辑器
        this.materialProperty = properties;   // 用到的变量
        this.targetMat = materialEditor.target as Material; // 当前材质球

        show(); // 使用下面这个 show函数
    }
    void show()
    {
        #region Shader属性
        // Shader里面的属性，FindProperty 就是从shader里找这个属性
        MaterialProperty _MainTex = FindProperty("_MainTex", materialProperty);
        MaterialProperty _MainColor = FindProperty("_MainColor", materialProperty);
        MaterialProperty _Range = FindProperty("_Range", materialProperty);
        MaterialProperty _Float = FindProperty("_Float", materialProperty);
        MaterialProperty _Red = FindProperty("_Red", materialProperty);
        #endregion

        #region GUI名称
        // GUI名称
        GUIContent mainTex = new GUIContent("主贴图");
        GUIContent mainColor = new GUIContent("主贴图染色");
        GUIContent range = new GUIContent("测试用Range");
        GUIContent float1 = new GUIContent("测试用Float");
        GUIContent red = new GUIContent("红色");
        #endregion

        #region GUI折叠
        // 供折叠使用
        m_GUITest = EditorGUILayout.BeginFoldoutHeaderGroup(m_GUITest, "GUI折叠");
        if (m_GUITest)
        {
            // 显示图片用
            materialEditor.TexturePropertySingleLine(mainTex, _MainTex, _MainColor);
            EditorGUI.indentLevel++;
            materialEditor.ShaderProperty(_Range, range);
            materialEditor.ShaderProperty(_Float, float1);
            EditorGUI.indentLevel--;
        }
        #endregion

        // 开关
        EditorGUI.BeginChangeCheck();
        EditorGUI.showMixedValue = _Red.hasMixedValue;
        var _RED_ON = EditorGUILayout.Toggle(red, _Red.floatValue == 1);
        if (EditorGUI.EndChangeCheck())
            _Red.floatValue = _RED_ON ? 1 : 0;
        EditorGUI.showMixedValue = false;
        // 打开开关之后的效果
        if (_Red.floatValue == 1)
        {
            targetMat.EnableKeyword("_RED_ON");
            EditorGUI.indentLevel++;
            GUILayout.Label("已启用变体 _RED_ON");
            EditorGUI.indentLevel--;
        }
        else
        {
            targetMat.DisableKeyword("_RED_ON");
        }

        EditorGUILayout.Space(20);
        // Render Queue
        materialEditor.RenderQueueField();
    }
}