using UnityEngine;
using UnityEngine.InputSystem;

namespace UnderTheMoonlight.Characters
{
    [RequireComponent(typeof(WerewolfMovement))]
    public class PlayerCharacterInput : MonoBehaviour
    {
        private CharacterMovement character = null;

        private Vector2 input = Vector2.zero;

        private void Awake()
        {
            character = GetComponent<CharacterMovement>();
        }

        private void Update()
        {
            //SetDirection();
            character.SetDirection(input);
        }

        /// <summary>  </summary>
        //public void SetDirection()
        //{
        //    input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Horizontal") == 0f ? Input.GetAxis("Vertical") : 0f).normalized;
        //}

        /// <summary> Tells the character to move base on the players input. </summary>
        /// <param name="context"> CallbackContext of the input. </param>
        public void SetDirection(InputAction.CallbackContext context)
        {
            input = context.ReadValue<Vector2>();
        }
    }
}