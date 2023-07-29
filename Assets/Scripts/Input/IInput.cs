using UnityEngine;
using UnityEngine.Events;

namespace CrossInput
{
    public abstract class AbstractInput : MonoBehaviour
    {
        public UnityEvent<int> OnHorizontalInput { get; } = new UnityEvent<int>();
        public UnityEvent<int> OnVerticalInput { get; } = new UnityEvent<int>();
        public UnityEvent OnEnter { get; } = new UnityEvent();
    }
}
