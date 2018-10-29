using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEngine.UI;

    public class WorldKeyboardController : MonoBehaviour
    {
        private InputField input;

        [SerializeField] GameObject dialogObject;
        TextController textController;
        

        public void ClickKey(string character)
        {
            input.text += character;
        }

        public void Backspace()
        {
            if (input.text.Length > 0)
            {
                input.text = input.text.Substring(0, input.text.Length - 1);
            }
        }

        public void Enter()
        {
            if (input.text.Length == 0)
                return;

            //VRTK_Logger.Info("You've typed [" + input.text + "]");
            textController.PlayerName = input.text;
            
            this.gameObject.SetActive(false);
            //textController.IsPlayerNameInputted = false;
            
        }

        private void Start()
        {
            textController = dialogObject.GetComponent<TextController>();

            input = GetComponentInChildren<InputField>();
        }
    }
}
