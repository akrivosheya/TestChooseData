using UnityEngine;

using UI;

namespace Data
{
    public class LocationPresenter : IPresenter
    {
        private Vector2 _namePosition = new Vector2(-270, 145);
        private Vector2 _nameScale = new Vector2(160, 30);
        private Vector2 _idPosition = new Vector2(-130, 145);
        private Vector2 _idScale = new Vector2(160, 30);
        private Vector2 _descriptionPosition = new Vector2(-270, 100);
        private Vector2 _descriptionScale = new Vector2(160, 60);
        private Vector2 _imagePosition = new Vector2(230, 0);
        private Vector2 _imageScale = Vector2.one * 2;

        public void Present(IData dataInterface, DataUI dataUI)
        {
            var data = (LocationData)dataInterface;

            dataUI.AddText(_namePosition, _nameScale, data.Name);
            dataUI.AddText(_idPosition, _idScale, data.Id);
            dataUI.AddText(_descriptionPosition, _descriptionScale, data.Description);
            dataUI.AddImage(_imagePosition, _imageScale, data.Image);
        }
    }
}
