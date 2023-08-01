using UnityEngine;
using UnityEngine.InputSystem;

namespace Complete
{
    public class InputManager : MonoBehaviour
    {
        public Vector2 Move;

        private void Update()
        {
            if (Move.magnitude >= 1)
            {
                Move = Move.normalized;
            }

            Move.x = (Keyboard.current.dKey.isPressed ? 1f : 0f) + (Keyboard.current.aKey.isPressed ? -1f : 0f);
            Move.y = (Keyboard.current.wKey.isPressed ? 1f : 0f) + (Keyboard.current.sKey.isPressed ? -1f : 0f);

        }
    }
}