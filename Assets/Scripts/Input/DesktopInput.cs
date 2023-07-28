using UnityEngine;
using UnityEngine.Events;

namespace CrossInput
{
    public class DesktopInput : MonoBehaviour, IInput
    {
        public UnityEvent<int> OnHorizontalInput { get; }
        public UnityEvent<int> OnVerticalInput { get; }
        public UnityEvent OnEnter { get; }

        [SerializeField] private KeyCode _enterKeyCode;
        [SerializeField] private string _xAxisName = "Horizontal";
        [SerializeField] private string _yAxisName = "Vertical";


        void Update()
        {
            int xAxis = (int)Input.GetAxisRaw(_xAxisName);
            int yAxis = (int)Input.GetAxisRaw(_yAxisName);
            bool pressedEnter = Input.GetKeyDown(_enterKeyCode);

            if(xAxis != 0 && OnHorizontalInput != null)
            {
                OnHorizontalInput.Invoke(xAxis);
            }
            else if(yAxis != 0 && OnVerticalInput != null)
            {
                OnVerticalInput.Invoke(yAxis);
            }

            if(pressedEnter && OnEnter != null)
            {
                OnEnter.Invoke();
            }
        }
    }
}
