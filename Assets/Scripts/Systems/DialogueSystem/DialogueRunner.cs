using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool isLast = false;
        public void SetLast(bool last) => isLast = true;
        public UnityEvent onLast;
        
        [SerializeField] private DialogueData start;

        public float lettersPerSecond = 6;
        public float mouseDownMultiplier = .5f;

        public CharacterToStop[] charactersToStop;

        public Image portrait;
        public TMP_Text speakerText;
        public TMP_Text tmp;
        
        public bool runOnAwake = true;

        public UnityEvent onFinished;

        public UnityEvent runOnlyIfPlayedDialogue;

        public StudioEventEmitter voiceEmitter;
        
        private void Start()
        {
            switch (runOnAwake)
            {
                case true when AlreadyPlayed:
                    onFinished.Invoke();
                    return;
                case true:
                    RunDialogue(start);
                    break;
            }

            AlreadyPlayed = true;

        }


        public async void RunDialogue(DialogueData current)
        {

            voiceEmitter.EventReference = current.speakerVoice;
            
            Time.timeScale = 0;

            InputManager.IgnoringMouse = true;
            var text = current.dialogueId.GetLocalizedString();
            speakerText.text = current.speakerName;
            portrait.sprite = current.speakerSprite;

            var textLetterCount = text.Length;
            var letters = 0;

            
            while (letters < textLetterCount)
            {
                voiceEmitter.Stop();
                voiceEmitter.Play();
                letters++;
                tmp.text = text[..letters];
                var currentChar = text[letters - 1];
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
                RunDialogue(current.next);
                return;
            }

            InputManager.IgnoringMouse = false;
            Time.timeScale = 1f;
            onFinished.Invoke();
            runOnlyIfPlayedDialogue.Invoke();
            runOnlyIfPlayedDialogue = new UnityEvent();
            if (isLast) onLast.Invoke();
        }
    }
}