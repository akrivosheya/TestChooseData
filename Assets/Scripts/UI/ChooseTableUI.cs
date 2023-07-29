using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using CrossInput;

namespace UI
{
    public class ChooseTableUI : MonoBehaviour
    {
        public UnityEvent OnCancelEvent = new UnityEvent();

        [SerializeField] private ChoiceUI _choicePrefab;
        [SerializeField] private Button _cancelButtonPrefab;
        [SerializeField] private GameObject _selectedChoicePrefab;
        [SerializeField] private AbstractInput _input;
        [SerializeField] private Vector2 _firstChoicePosition;
        [SerializeField] private Vector2 _cancelButtonPosition;
        [SerializeField] private float _rowsOffset = 30f;
        [SerializeField] private float _columnsOffset = 50f;
        [SerializeField] private int _rowsCount = 4;
        [SerializeField] private int _columnsCount = 4;

        private List<ChoiceUI> _choices = new List<ChoiceUI>();
        private Button _cancel;
        private GameObject _selectedChoice;
        private int _currentChoiceIndex;

        void OnDestroy()
        {
            RemoveListeners();
        }

        public void OnEnter()
        {
            _choices[_currentChoiceIndex].OnClick();
        }

        public void OnHorizontalInput(int offset)
        {
            Debug.Log(offset);
            int newIndex = _currentChoiceIndex + offset;
            if(newIndex >= 0 && newIndex < _choices.Count)
            {
                _currentChoiceIndex = newIndex;
                _selectedChoice.transform.position = _choices[_currentChoiceIndex].transform.position;
            }
        }

        public void OnVerticalInput(int offset)
        {
            int newIndex = _currentChoiceIndex + _rowsCount * offset;
            if(newIndex >= 0 && newIndex < _choices.Count)
            {
                _currentChoiceIndex += newIndex;
                _selectedChoice.transform.position = _choices[_currentChoiceIndex].transform.position;
            }
        }

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
                var offset = new Vector2(_rowsOffset * (_choices.Count % _columnsCount),
                    _columnsOffset * (_choices.Count / _columnsCount));
                var newPosition = _firstChoicePosition + offset;
                newChoice.transform.localPosition = newPosition;
                _choices.Add(newChoice);
            }
            _currentChoiceIndex = 0;

            _selectedChoice = Instantiate(_selectedChoicePrefab);
            _selectedChoice.transform.SetParent(transform);
            _selectedChoice.transform.localPosition = _firstChoicePosition;

            _cancel = Instantiate(_cancelButtonPrefab);
            _cancel.onClick.AddListener(OnCancel);
            _cancel.transform.SetParent(transform);
            _cancel.transform.localPosition = _cancelButtonPosition;

            AddListeners();
        }

        public void Deactivate()
        {
            Clear();
            RemoveListeners();
        }

        public void OnCancel()
        {
            OnCancelEvent.Invoke();
        }

        private void Clear()
        {
            foreach(var choice in _choices)
            {
                Destroy(choice.gameObject);
            }

            _choices.Clear();
            _currentChoiceIndex = 0;

            if(_cancel != null)
            {
                Destroy(_cancel.gameObject);
            }

            if(_selectedChoice != null)
            {
                Destroy(_selectedChoice);
            }
        }

        private void AddListeners()
        {
            _input.OnEnter.AddListener(OnEnter);
            _input.OnHorizontalInput.AddListener(OnHorizontalInput);
            _input.OnVerticalInput.AddListener(OnVerticalInput);
        }

        private void RemoveListeners()
        {
            _input.OnEnter.RemoveListener(OnEnter);
            _input.OnHorizontalInput.RemoveListener(OnHorizontalInput);
            _input.OnVerticalInput.RemoveListener(OnVerticalInput);
        }
    }
}
