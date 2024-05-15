using TMPro;
using UnityEngine;

namespace Muvuca.UI.Menu
{
    public class Credits : MonoBehaviour
    {
        public string programming;
        public void SetProgramming(string text) => programming = text;
        public string artist2D;
        public void SetArtist2D(string text) => artist2D = text;
        public string animator;
        public void SetAnimator(string text) => animator = text;
        public string soundDesign;
        public void SetSoundDesign(string text) => soundDesign = text;
        public string gameDesign;
        public void SetGameDesign(string text) => gameDesign = text;



        private TMP_Text tmp;
        private void Awake() => tmp = GetComponent<TMP_Text>();

        public void UpdatedText()
        {
            tmp.text = $@"Guilherme Silva - {programming}
Bruno Nascimento - UI/UX
Samuel Figueiredo - {soundDesign} / OST
Brendo Saldanha - {artist2D} & {gameDesign}
Gabriel Dias - {artist2D}
Murilo Malsan - {animator}";
        }

    }
}