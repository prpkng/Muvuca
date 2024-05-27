using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using FMODUnity;
using Muvuca.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Muvuca.Systems.DialogueSystem
{
    public class DialogueRunner : MonoBehaviour
    {
        public static bool AlreadyPlayed;
        // public static List<DialogueRunner> PlayedDialogues = new();
        [Serializable]
        public class CharacterToStop
        {
            public char character;
            public float seconds;
        }
        
        [SerializeField] private DialogueData start;

        public float lettersPerSecond = 6;
        public float mouseDownMultiplier = .5f;

        public CharacterToStop[] charactersToStop;

        public Image portrait;
        public TMP_Text speakerText;
        public TMP_Text tmp;

        public StudioEventEmitter voiceEmitter;

        public UnityEvent onFinished;
        

        public async Task RunDialogue(DialogueData current)
        {
            while (true)
            {
                voiceEmitter.EventReference = current.speakerVoice;

                Time.timeScale = 0;

                InputManager.IgnoringMouse = true;
                var text = $"\t{current.dialogueId.GetLocalizedString()}";
                speakerText.text = current.speakerName;
                portrait.sprite = current.speakerSprite;

                var textLetterCount = text.Length;
                var letters = 0;


                bool isRichText = false;
                
                while (letters < textLetterCount)
                {
                    letters++;
                    tmp.text = text[..letters];
                    var currentChar = text[letters - 1];

                    if (isRichText && currentChar != '>')
                        continue;
                    
                    isRichText = false;
                    
                    if (currentChar == '<')
                    {
                        isRichText = true;
                        continue;
                    }

                    
                    voiceEmitter.Stop();
                    voiceEmitter.Play();
                    
                    var multiplier = Mouse.current.leftButton.isPressed ? mouseDownMultiplier : 1;
                    if (charactersToStop.Select(c => c.character).Contains(currentChar))
                    {
                        var timeToWait = charactersToStop.FirstOrDefault(c => c.character == currentChar)?.seconds;
                        if (timeToWait.HasValue) await UniTask.WaitForSeconds(timeToWait.Value * multiplier, true);
                    }

                    await UniTask.WaitForSeconds(1f / lettersPerSecond * multiplier, true);
                }

                await UniTask.WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame);

                if (!current.finalize)
                {
                    current = current.next;
                    continue;
                }

                InputManager.IgnoringMouse = false;
                Time.timeScale = 1f;
                onFinished.Invoke();
                
                break;
            }
        }
    }
}