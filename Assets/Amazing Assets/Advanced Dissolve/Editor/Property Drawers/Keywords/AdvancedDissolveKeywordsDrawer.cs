using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

namespace AmazingAssets.AdvancedDissolveEditor
{
    abstract class AdvancedDissolveKeywordsDrawer : MaterialPropertyDrawer
    {
		readonly string[] labels;
		readonly string[] keywords;

		public AdvancedDissolveKeywordsDrawer()
        {
			EnumToKeywords(out this.labels, out this.keywords);			
		}

        public abstract void EnumToKeywords(out string[] labels, out string[] keywords);

		public void SetKeyword(MaterialProperty prop, int index)
		{
			for (int i = 0; i < keywords.Length; i++)
			{
				Object[] targets = prop.targets;
				for (int j = 0; j < targets.Length; j++)
				{
					Material material = (Material)targets[j];
					if (index == i)
					{
						material.EnableKeyword(keywords[i]);
					}
					else
					{
						material.DisableKeyword(keywords[i]);
					}
				}
			}
		}

		public override void OnGUI(Rect position, UnityEditor.MaterialProperty prop, GUIContent label, UnityEditor.MaterialEditor editor)
		{
			EditorGUI.BeginChangeCheck();
			EditorGUI.showMixedValue = prop.hasMixedValue;
			int selectedIndex = (int)prop.floatValue;
			selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, labels);
			EditorGUI.showMixedValue = false;
			if (EditorGUI.EndChangeCheck())
			{
				prop.floatValue = selectedIndex;
				SetKeyword(prop, selectedIndex);
			}


			//Copy keyword on right mouse down
			if (Event.current != null && Event.current.type == EventType.MouseDown && Event.current.button == 1 && position.Contains(Event.current.mousePosition))   //Right click
			{
				TextEditor te = new TextEditor();
				te.text = keywords[selectedIndex];
				te.SelectAll();
				te.Copy();

				Debug.Log(te.text);

			}
		}

		public string EnumStringToUnityStyle(string label)
        {
			return string.Concat(label.Select(x => System.Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
		}
	}
}