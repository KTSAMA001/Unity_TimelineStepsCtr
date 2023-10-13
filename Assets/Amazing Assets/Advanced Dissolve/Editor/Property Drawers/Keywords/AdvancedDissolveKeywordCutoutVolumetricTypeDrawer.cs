using UnityEngine;
using UnityEditor;
using System.Collections;


namespace AmazingAssets.AdvancedDissolveEditor
{
    class AdvancedDissolveKeywordCutoutGeometricTypeDrawer : AdvancedDissolveKeywordsDrawer
	{
		public override void EnumToKeywords(out string[] labels, out string[] keywords)
		{
			labels = System.Enum.GetNames(typeof(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType));
			keywords = new string[labels.Length];

			for (int i = 0; i < labels.Length; i++)
			{
				labels[i] = EnumStringToUnityStyle(labels[i]).Replace("X Y Z", "XYZ");
				keywords[i] = AdvancedDissolve.AdvancedDissolveKeywords.ToString((AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType)i);
			}
		}
	}
}