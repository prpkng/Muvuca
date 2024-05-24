using FMODUnity;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

namespace Muvuca.Systems.DialogueSystem
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue Data", order = 0)]
    public class DialogueData : ScriptableObject
    {
        public LocalizedString  dialogueId;
        public string speakerName;
        public Sprite speakerSprite;
        public EventReference speakerVoice;
        public bool finalize;
        public DialogueData next;
    }
}