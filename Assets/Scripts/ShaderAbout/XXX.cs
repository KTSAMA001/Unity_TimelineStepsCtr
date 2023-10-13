using System;
using UnityEngine;
using UnityEditor;

public class XXX : ShaderGUI
{
    // OnGuI ���յ��������� ��
    MaterialEditor materialEditor;//��ǰ�������
    MaterialProperty[] materialProperty;//��ǰshader��properties
    Material targetMat;//���ƶ��������

    // �۵���
    private bool m_GUITest = true;

    // ��Ҫʵ���߼�
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        this.materialEditor = materialEditor; // ��ǰ�༭��
        this.materialProperty = properties;   // �õ��ı���
        this.targetMat = materialEditor.target as Material; // ��ǰ������

        show(); // ʹ��������� show����
    }
    void show()
    {
        #region Shader����
        // Shader��������ԣ�FindProperty ���Ǵ�shader�����������
        MaterialProperty _MainTex = FindProperty("_MainTex", materialProperty);
        MaterialProperty _MainColor = FindProperty("_MainColor", materialProperty);
        MaterialProperty _Range = FindProperty("_Range", materialProperty);
        MaterialProperty _Float = FindProperty("_Float", materialProperty);
        MaterialProperty _Red = FindProperty("_Red", materialProperty);
        #endregion

        #region GUI����
        // GUI����
        GUIContent mainTex = new GUIContent("����ͼ");
        GUIContent mainColor = new GUIContent("����ͼȾɫ");
        GUIContent range = new GUIContent("������Range");
        GUIContent float1 = new GUIContent("������Float");
        GUIContent red = new GUIContent("��ɫ");
        #endregion

        #region GUI�۵�
        // ���۵�ʹ��
        m_GUITest = EditorGUILayout.BeginFoldoutHeaderGroup(m_GUITest, "GUI�۵�");
        if (m_GUITest)
        {
            // ��ʾͼƬ��
            materialEditor.TexturePropertySingleLine(mainTex, _MainTex, _MainColor);
            EditorGUI.indentLevel++;
            materialEditor.ShaderProperty(_Range, range);
            materialEditor.ShaderProperty(_Float, float1);
            EditorGUI.indentLevel--;
        }
        #endregion

        // ����
        EditorGUI.BeginChangeCheck();
        EditorGUI.showMixedValue = _Red.hasMixedValue;
        var _RED_ON = EditorGUILayout.Toggle(red, _Red.floatValue == 1);
        if (EditorGUI.EndChangeCheck())
            _Red.floatValue = _RED_ON ? 1 : 0;
        EditorGUI.showMixedValue = false;
        // �򿪿���֮���Ч��
        if (_Red.floatValue == 1)
        {
            targetMat.EnableKeyword("_RED_ON");
            EditorGUI.indentLevel++;
            GUILayout.Label("�����ñ��� _RED_ON");
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