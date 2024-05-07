using UnityEngine;
using UnityEditor;

namespace Muvuca.Tools.Editor {
    
    [CustomEditor(typeof(RythmPositioner))]
    public class RythmPositionerCE : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Move!")) {
                var obj = ((RythmPositioner)target);
                Undo.RecordObject(obj.transform, "Moved to the rythm position");
                obj.Move();
                Debug.Log("Moved!");
            }
        }
    }
}