using UnityEngine;
using UnityEditor;
using System.Collections;


namespace AmazingAssets.AdvancedDissolveEditor
{
    class AdvancedDissolveKeywordCutoutGeometricCountDrawer : AdvancedDissolveKeywordsDrawer
	{
		public override void EnumToKeywords(out string[] labels, out string[] keywords)
		{
			labels = System.Enum.GetNames(typeof(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount));
			keywords = new string[labels.Length];

			for (int i = 0; i < labels.Length; i++)
			{
				keywords[i] = AdvancedDissolve.AdvancedDissolveKeywords.ToString((AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount)i);
			}
		}
	}
}