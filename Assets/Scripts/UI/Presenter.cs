using System.Collections.Generic;
using UnityEngine;

using Data;

namespace UI
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField] private ChooseTableUI _chooseTable;
        [SerializeField] private DataUI _data;
        [SerializeField] private DataMaster _dataMaster;

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
            if(!_dataMaster.TryGetList(table, out List<string> list))
            {
                return;
            }

            _currentChooseTableList = list;
            _chooseTable.Activate(_currentChooseTableList, ShowData);
        }

        public void ShowData(string data)
        {
            if(!_dataMaster.LoadData(data))
            {
                return;
            }

            if(!_dataMaster.TryGetPresenter(data, out IPresenter presenter))
            {
                return;
            }

            _chooseTable.Deactivate();
            _data.Activate();
            presenter.Present(_dataMaster.CurrentData, _data);
        }

        private void OnCancelChooseTable()
        {
            // cancelation

            _chooseTable.Deactivate();
        }

        private void OnCancelData()
        {
            _data.Clear();
            _chooseTable.Activate(_currentChooseTableList, ShowData);
        }
    }
}
