using System.Linq;
using Muvuca.Core;
using UnityEditor;
using UnityEngine;

namespace Muvuca.Systems.Editor
{
    public class SaveSystemEditor : EditorWindow
    {
        [MenuItem("Utilities/Save Data")]
        public static void ShowWindow()
        {
            var window = CreateInstance<SaveSystemEditor>();
            window.titleContent = new GUIContent("Settings");
            window.Show();
        }

        private bool wantToReset;
        private void OnEnable()
        {
            wantToReset = false;
        }

        private void OnValidate()
        {
            wantToReset = false;
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Save to Disk"))
                SaveSystem.SaveToDisk();
            if (GUILayout.Button("Load from Disk"))
                SaveSystem.LoadFromDisk();
            if (wantToReset)
            {
                var bgColor = GUI.backgroundColor;
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Sure?"))
                {
                    SaveSystem.Reset();
                    wantToReset = false;
                }
                GUI.backgroundColor = bgColor;
            }
            else if (GUILayout.Button("Reset"))
                wantToReset = true;

            var keys = SaveSystem.SaveData.Keys.ToArray();
            var values = SaveSystem.SaveData.Values.ToArray();
            for (int index = 0; index < SaveSystem.SaveData.Count; index++)
            {
                var key = keys[index];
                var value = values[index];
                EditorGUILayout.LabelField(key, EditorStyles.helpBox);
                if (value.TryGetFloat(out var f))
                    SaveSystem.SaveData[key] = new FloatEntry { value = EditorGUILayout.FloatField(f, EditorStyles.helpBox) };
                else if (value.TryGetInt(out var i))
                    SaveSystem.SaveData[key] = new IntEntry { value = EditorGUILayout.IntField(i, EditorStyles.helpBox) };
                else if (value.TryGetString(out var str))
                    SaveSystem.SaveData[key] = new StringEntry { value = EditorGUILayout.TextField(str, EditorStyles.helpBox) };

                EditorGUILayout.Space();
            }
        }
    }
}