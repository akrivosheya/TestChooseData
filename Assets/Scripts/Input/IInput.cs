using UnityEngine.Events;

namespace CrossInput
{
    public interface IInput
    {
        public UnityEvent<int> OnHorizontalInput { get; }
        public UnityEvent<int> OnVerticalInput { get; }
        public UnityEvent OnEnter { get; }
    }
}
