using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;


namespace AmazingAssets.AdvancedDissolveEditor
{
    class AdvancedDissolveKeywordEdgeBaseSourceDrawer : AdvancedDissolveKeywordsDrawer
	{
		public override void EnumToKeywords(out string[] labels, out string[] keywords)
		{
			labels = System.Enum.GetNames(typeof(AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource));
			keywords = new string[labels.Length];

			for (int i = 0; i < labels.Length; i++)
			{
				labels[i] = EnumStringToUnityStyle(labels[i]);
				keywords[i] = AdvancedDissolve.AdvancedDissolveKeywords.ToString((AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource)i);
			}
		}
	} 
}