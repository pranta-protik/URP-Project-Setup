using System.IO;
using UnityEditor;
using UnityEngine;

namespace MyTools.EditorScript
{
	public static class OpenSaveDirectory
	{
		[MenuItem("Edit/Open Save Directory")]
		private static void OpenDirectory()
		{
			EditorUtility.RevealInFinder(Path.Combine(Application.persistentDataPath, "data.sav"));
		}
	}
}