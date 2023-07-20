using UnityEngine;

namespace DefaultNamespace.Services.InputService
{
    public class InputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string Button = "Fire";

        public NetworkInputData GetNetworkInput()
        {
            var data = new NetworkInputData();
            data.Direction = SimpleInputAxis();
            data.IsFireUp  = IsAttackButtonUp();

            return data;
        }

        public bool IsAttackButtonUp() =>
            SimpleInput.GetButtonUp(Button);

        private Vector2 SimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}