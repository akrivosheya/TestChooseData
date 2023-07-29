using UnityEngine;

using UI;

namespace Data
{
    public class PersonPresenter : IPresenter
    {
        private Vector2 _namePosition = new Vector2(-270, 145);
        private Vector2 _nameScale = new Vector2(160, 30);
        private Vector2 _levelPosition = new Vector2(-130, 145);
        private Vector2 _levelScale = new Vector2(160, 30);
        private Vector2 _imagePosition = new Vector2(-230, 0);
        private Vector2 _imageScale = Vector2.one * 2;
        private Vector3 _personPosition = new Vector3(1.3f, 0.5f, -7.4f);
        private Vector3 _personScale = Vector3.one;

        public void Present(IData dataInterface, DataUI dataUI)
        {
            var data = (PersonData)dataInterface;

            dataUI.AddText(_namePosition, _nameScale, data.Name);
            dataUI.AddText(_levelPosition, _levelScale, data.Level);
            dataUI.AddImage(_imagePosition, _imageScale, data.Image);
            dataUI.AddModel(_personPosition, _personScale, data.Model);
        }
    }
}
