using Muvuca.Tools;
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
            InspectorElement.FillDefaultInspector(element, serializedObject, this);
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
            button = new Button(() => ((SpawnAlongPathUtility)target).GenerateLines())
            {
                text = "Generate Lines!"
            };
            element.Add(button);
            return element;
        }
    }
}