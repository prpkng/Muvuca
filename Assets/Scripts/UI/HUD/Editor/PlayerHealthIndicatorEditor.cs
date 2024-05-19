using Codice.Client.BaseCommands;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Muvuca.UI.HUD.Editor
{
    [CustomEditor(typeof(PlayerHealthIndicator))]      
    public class PlayerHealthIndicatorEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var plr = (PlayerHealthIndicator)target;
            var element = new VisualElement();
            element.Add(new Button(async () =>
            {
                var container = plr.healthIndicatorsParent;
                for(var i = container.childCount - 1; i >= 0; i--)
                    DestroyImmediate(container.GetChild(i).gameObject);
                plr.SetMaxHealth(plr.maxHealth);
            })
            {
                text = "Regenerate!"
            });
            InspectorElement.FillDefaultInspector(element, serializedObject, this);
            return element;

        }
    }
}