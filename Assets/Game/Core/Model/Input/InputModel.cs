using UnityInput = UnityEngine.Input;

namespace Game.Core.Model.Input
{
    public class InputModel : IInputModel
    {
        private const string MouseAxisX = "Mouse X";
        private const string MouseAxisY = "Mouse Y";

        public bool IsTouching => UnityInput.GetMouseButton(0);
        
        public float GetMouseAxis(MouseAxis mouseAxis)
        {
            return mouseAxis == MouseAxis.X ? UnityInput.GetAxis(MouseAxisX)
                                            : UnityInput.GetAxis(MouseAxisY);
        }
    }
}