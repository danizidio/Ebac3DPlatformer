using TMPro;
using UnityEngine;
using System;
namespace CommonMethodsLibrary
{
    public class TextManager : MonoBehaviour
    {
        public static Action OnCallText;
        public static Action<string> OnCallMessage;

        [SerializeField] TMP_Text _txtObj;
        protected TMP_Text txtObj { get { return _txtObj; } }

        [SerializeField] float _timeToHideMessage;
        protected float timeToHideMessage { get { return _timeToHideMessage; } }

        Canvas _canvas;

        [Tooltip("ADICIONE ITENS PARA APRESENTAR VARIOS TEXTOS SEGUIDOS")]
        [SerializeField] string[] _dialogs;

        int _txtNumber = 0;

        private void Awake()
        {
            _canvas = GetComponentInChildren<Canvas>();
            _canvas.worldCamera = FindObjectOfType<Camera>();

            _txtObj = GetComponentInChildren<TMP_Text>();
        }

        void Talking()
        {
            GetComponent<Animator>().SetTrigger("SHOW");

            if (_txtNumber < _dialogs.Length)
            {
                _txtObj.GetComponentInChildren<TMP_Text>().text = _dialogs[_txtNumber];

                _txtNumber++;
            }
            else
            {
                Destroy(this.gameObject);
            }

            GetComponent<Animator>().SetTrigger("HIDE");
        }

        protected virtual void ShowMessage(string message)
        {
            _txtObj.GetComponentInChildren<TMP_Text>().text = message;
        }

        private void OnEnable()
        {
            OnCallText += Talking;
            OnCallMessage = ShowMessage;
        }
        private void OnDisable()
        {
            OnCallText -= Talking;
            OnCallMessage -= ShowMessage;
        }
    }
}
