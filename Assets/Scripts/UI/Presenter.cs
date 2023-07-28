using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField] private ChooseTableUI _chooseTable;
        [SerializeField] private DataUI _data;

        private List<string> _currentChooseTableList;


        void Awake()
        {
            _chooseTable.OnCancelEvent.AddListener(OnCancelChooseTable);
            _data.OnCancelEvent.AddListener(OnCancelData);
        }

        void OnDestroy()
        {
            _chooseTable.OnCancelEvent.RemoveListener(OnCancelChooseTable);
            _data.OnCancelEvent.RemoveListener(OnCancelData);
        }

        public void ShowTable(string table)
        {
            // get table data

            _chooseTable.Activate(_currentChooseTableList, ShowData);
        }

        public void ShowData(string data)
        {
            // get data

            _data.Activate();
        }

        private void OnCancelChooseTable()
        {
            // cancelation

            _chooseTable.Clear();
        }

        private void OnCancelData()
        {
            _data.Clear();
            _chooseTable.Activate(_currentChooseTableList, ShowData);
        }
    }
}
