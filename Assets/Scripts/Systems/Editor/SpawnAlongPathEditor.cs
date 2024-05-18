using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Muvuca.Systems.Editor
{
    [CustomEditor(typeof(SpawnAlongPathUtility))]
    public class SpawnAlongPathEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var element = new VisualElement();
            var button = new Button(() => ((SpawnAlongPathUtility)target).Spawn())
            {
                text = "Spawn!"
            };
            element.Add(button);
            button = new Button(() => ((SpawnAlongPathUtility)target).GenerateCollisions())
            {
                text = "Generate Collision!"
            };
            element.Add(button);
            InspectorElement.FillDefaultInspector(element, serializedObject, this);
            return element;
        }
    }
}