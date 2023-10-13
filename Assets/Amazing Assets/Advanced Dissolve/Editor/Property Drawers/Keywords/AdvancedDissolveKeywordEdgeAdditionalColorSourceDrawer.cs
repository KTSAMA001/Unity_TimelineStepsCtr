using UnityEngine;
using UnityEditor;
using System.Collections;


namespace AmazingAssets.AdvancedDissolveEditor
{
    class AdvancedDissolveKeywordEdgeAdditionalColorSourceDrawer : AdvancedDissolveKeywordsDrawer
	{
		public override void EnumToKeywords(out string[] labels, out string[] keywords)
		{
			labels = System.Enum.GetNames(typeof(AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource));
			keywords = new string[labels.Length];

			for (int i = 0; i < labels.Length; i++)
			{
				labels[i] = EnumStringToUnityStyle(labels[i]);
				keywords[i] = AdvancedDissolve.AdvancedDissolveKeywords.ToString((AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource)i);
			}
		}
	}
}