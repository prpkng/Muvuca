
namespace Muvuca.Systems
{
    using UnityEngine;

#if UNITY_EDITOR

    using UnityEditor;

    public class ReadOnlyEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            base.OnGUI(position, property, label);
            GUI.enabled = true;
        }
    }

#endif


    public class ReadOnlyAttribute : PropertyAttribute
    {

    }
}