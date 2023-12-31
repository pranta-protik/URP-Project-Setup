using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MyTools.EditorScript.HotKeys
{
	public static class DeleteAllSaveFiles
	{
		[MenuItem("Edit/Delete All Save Files")]
		private static void DeleteFiles()
		{
			if (EditorUtils.DisplayDialogBoxWithOptions("Delete All Save Files", "Are you sure you want to delete all save files? This action cannot be undone."))
			{
				var saveDir = new DirectoryInfo(Application.persistentDataPath);

				foreach (var file in saveDir.GetFiles())
				{
					try
					{
						file.Delete();
					}
					catch (Exception e)
					{
						EditorUtils.DisplayDialogBox("Error!", $"Could not delete the file {file}\n{e}");
					}
				}
			}
		}
	}
}