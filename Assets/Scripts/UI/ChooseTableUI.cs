using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ChooseTableUI : MonoBehaviour
    {
        public UnityEvent OnCancelEvent;

        [SerializeField] private ChoiceUI _choicePrefab;
        [SerializeField] private Button _cancelButtonPrefab;
        [SerializeField] private Vector2 _firstChoicePosition;
        [SerializeField] private Vector2 _cancelButtonPosition;
        [SerializeField] private float _rowsOffset = 30f;
        [SerializeField] private float _columnsOffset = 50f;
        [SerializeField] private int _rowsCount = 4;
        [SerializeField] private int _columnsCount = 4;

        private List<ChoiceUI> _choices = new List<ChoiceUI>();
        private Button _cancel;


        public void Activate(List<string> choicesParams, UnityAction<string> onChose)
        {
            Clear();

            foreach(var param in choicesParams)
            {
                if(_choices.Count == _rowsCount * _columnsCount)
                {
                    break;
                }
                var newChoice = Instantiate(_choicePrefab);
                newChoice.Name = param;
                newChoice.OnChose.AddListener(onChose);
                newChoice.transform.SetParent(transform);
                var offset = new Vector2(_rowsOffset * _choices.Count % _columnsCount,
                    _columnsOffset * _choices.Count / _columnsCount);
                var newPosition = _firstChoicePosition + offset;
                newChoice.transform.position = newPosition;
                _choices.Add(newChoice);
            }

            _cancel = Instantiate(_cancelButtonPrefab);
            _cancel.onClick.AddListener(OnCancel);
            _cancel.transform.SetParent(transform);
            _cancel.transform.position = _cancelButtonPosition;
        }

        public void OnCancel()
        {
            OnCancelEvent.Invoke();
        }

        public void Clear()
        {
            foreach(var choice in _choices)
            {
                Destroy(choice.gameObject);
            }

            _choices.Clear();

            if(_cancel != null)
            {
                Destroy(_cancel.gameObject);
            }
        }
    }
}
