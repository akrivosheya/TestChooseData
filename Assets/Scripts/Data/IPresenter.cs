using UI;

namespace Data
{
    public interface IPresenter
    {
        public void Present(IData data, DataUI dataUI);
    }
}
