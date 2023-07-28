using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class DataMaster : ScriptableObject
    {
        [SerializeField] private string _folder = "Data";
        [SerializeField] private string _datasFile = "Types";
        [SerializeField] private char _elementsDelim = ';';
        [SerializeField] private char _keyValueDelim = ':';

        public IData CurrentData { get; set; }
        
        private Dictionary<string, string> _dataTypes = new Dictionary<string, string>();
        private Dictionary<string, string> _presentersTypes = new Dictionary<string, string>();
        private object[] _emptyArray = new object[0];
        private int _keyValuesCount = 3;
        private int _keyIndex = 0;
        private int _dataValueIndex = 1;
        private int _presenterValueIndex = 2;


        void OnEnable()
        {
            try
            {
                string dataString = Resources.Load<TextAsset>(System.IO.Path.Join(_folder, _datasFile)).ToString();
                var elements = dataString.Split(_elementsDelim);

                foreach(var element in elements)
                {
                    var keyValues = element.Split(_keyValueDelim);
                    if(keyValues.Length < _keyValuesCount)
                    {
                        Debug.LogError("Can't parse " + element + " to key-value-value");
                    }
                    else
                    {
                        _dataTypes[keyValues[_keyIndex]] = keyValues[_dataValueIndex];
                        _presentersTypes[keyValues[_keyIndex]] = keyValues[_presenterValueIndex];
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.LogError("Can't init DataMaster: " + ex);
            }
        }

        public bool LoadData(string data)
        {
            if(!_dataTypes.ContainsKey(data))
            {
                Debug.LogError("There is not data " + data);
                return false;
            }

            try
            {
                var type = Type.GetType(_dataTypes[data]);
                string dataString = Resources.Load<TextAsset>(System.IO.Path.Join(_folder, data)).ToString();
                CurrentData = (IData)JsonUtility.FromJson(dataString, type);
            }
            catch(Exception ex)
            {
                Debug.LogError("Can't get data " + data + ": " + ex);
                return false;
            }

            return true;
        }

        public bool TryGetPresenter(string data, out IPresenter presenter)
        {
            if(!_presentersTypes.ContainsKey(data))
            {
                Debug.LogError("There is not presenter for " + data);
                presenter = default(IPresenter);
                return false;
            }

            try
            {
                var type = Type.GetType(_presentersTypes[data]);
                presenter = type.GetConstructors()[0].Invoke(_emptyArray) as IPresenter;
                return true;
            }
            catch(Exception ex)
            {
                Debug.LogError("Can't get presenter for " + data + ": " + ex);
                presenter = default(IPresenter);
                return false;
            }
        }

        public bool TryGetList(string table, out List<string> list)
        {
            try
            {
                string dataString = Resources.Load<TextAsset>(System.IO.Path.Join(_folder, table)).ToString();
                list = new List<string>(dataString.Split(_elementsDelim));
                return true;
            }
            catch(Exception ex)
            {
                Debug.LogError("Can't get table data " + table + ": " + ex);
                list = default(List<string>);
                return false;
            }
        }
    }
}
