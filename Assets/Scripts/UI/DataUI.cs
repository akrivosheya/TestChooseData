using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class DataUI : MonoBehaviour
    {
        public UnityEvent OnCancelEvent = new UnityEvent();

        [SerializeField] private Text _textPrefab;
        [SerializeField] private Image _imagePrefab;
        [SerializeField] private Button _cancelButtonPrefab;
        [SerializeField] private Vector2 _cancelButtonPosition;

        private List<GameObject> _elements = new List<GameObject>();


        public void Activate()
        {
            Clear();

            var cancel = Instantiate(_cancelButtonPrefab);
            cancel.onClick.AddListener(OnCancel);
            cancel.transform.SetParent(transform);
            cancel.transform.localPosition = _cancelButtonPosition;

            _elements.Add(cancel.gameObject);
        }

        public void AddText(Vector2 position, Vector2 scale, string text)
        {
            var newText = Instantiate(_textPrefab);
            newText.text = text;
            ConfigureTransform(newText.transform, position, Vector2.one);
            newText.rectTransform.sizeDelta = scale;

            _elements.Add(newText.gameObject);
        }

        public void AddImage(Vector2 position, Vector2 scale, string spriteName)
        {
            var sprite = Resources.Load<Sprite>(spriteName);
            if(sprite is null)
            {
                Debug.LogError("Can't find " + spriteName + " sprite.");
                return;
            }

            var newImage = Instantiate(_imagePrefab);
            newImage.sprite = sprite;
            ConfigureTransform(newImage.transform, position, scale);

            _elements.Add(newImage.gameObject);
        }

        public void AddModel(Vector3 position, Vector3 scale, string modelName)
        {
            var model = Resources.Load<GameObject>(modelName);
            if(model is null)
            {
                Debug.LogError("Can't find " + modelName + " model.");
                return;
            }

            var element = Instantiate(model);
            element.transform.localPosition = position;
            element.transform.localScale = scale;

            _elements.Add(element);
        }

        public void Clear()
        {
            foreach(var element in _elements)
            {
                Destroy(element);
            }
        }

        public void OnCancel()
        {
            OnCancelEvent.Invoke();
        }

        private void ConfigureTransform(Transform configuringTransform, Vector2 position, Vector2 scale)
        {
            configuringTransform.SetParent(transform);
            configuringTransform.localPosition = position;
            configuringTransform.localScale = scale;
        }
    }
}
