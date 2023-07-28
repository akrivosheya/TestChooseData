using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ChoiceUI : MonoBehaviour
    {
        public string Name { get; set; }
        public UnityEvent<string> OnChose;

        [SerializeField] private Text _text;


        void Start()
        {
            _text.text = Name;
        }

        public void OnClick()
        {
            if(OnChose != null)
            {
                OnChose.Invoke(Name);
            }
        }
    }
}
