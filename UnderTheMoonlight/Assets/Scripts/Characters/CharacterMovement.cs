using System;
using System.Collections;
using UnityEngine;

namespace UnderTheMoonlight.Characters
{
    [RequireComponent(typeof(Animator))]
    public class CharacterMovement : MonoBehaviour
    {
        protected Animator animator = null;

        [SerializeField] protected float movementSpeed = 1f;
        [SerializeField] public LayerMask impassableLayers = 0;

        protected Vector3 previousDir = Vector3.zero;
        [HideInInspector] public Vector3 movementDir { get; protected set; } = Vector3.zero;
        protected Vector3 movementStart = Vector3.zero;
        protected Vector3 movementTarget = Vector3.zero;
        protected float movementDelta = 0f;
        protected Func<bool> PlayerIsMoving;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public bool IsPlayerMoving => movementDelta != 0;

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, movementTarget);
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();

            PlayerIsMoving = Moving;
        }

        protected virtual void Update()
        {
            if (!IsPlayerMoving && movementDir != Vector3.zero && CanMove())
                StartCoroutine(Move());
        }

        /// <summary> Sets the direction for the character to move to. </summary>
        /// <param name="newDir"> The new direction for the player to go. </param>
        public virtual void SetDirection(Vector2 newDir)
        {
            if (!IsPlayerMoving && ((newDir.x != 0 && newDir.y == 0) || (newDir.x == 0 && newDir.y != 0)))
            {
                movementDir = newDir;
                movementTarget = transform.position + movementDir;

                var transformScale = transform.localScale;
                if (movementDir.x != 0 && Mathf.Sign(movementDir.x) != Mathf.Sign(transformScale.x))
                {
                    transform.localScale = new Vector3(-transformScale.x, transformScale.y, transformScale.z);
                }
            }
        }

        /// <summary> Checks to see if a player can move in that direction. </summary>
        protected virtual bool CanMove()
        {
            var position = transform.position;
            Debug.DrawLine(position, position + movementDir, Color.white, 3f);
            return !Physics2D.Raycast(position, movementDir, 1f, impassableLayers);
        }

        /// <summary> Moves the player towards the target. </summary>
        /// <returns> Returns true if the player is currently moving. </returns>
        protected virtual bool Moving()
        {
            movementDelta += Time.deltaTime * movementSpeed;
            if (movementDelta > 1f)
                movementDelta = 1f;

            transform.position = Vector3.Lerp(movementStart, movementTarget, movementDelta);
            return movementDelta != 1;
        }

        /// <summary> Moves the character to a tile in the given direction if possible. </summary>
        protected virtual IEnumerator Move()
        {
            movementStart = transform.position;
            animator.SetBool(IsWalking, true);

            yield return new WaitWhile(PlayerIsMoving);

            animator.SetBool(IsWalking, false);
            previousDir = movementDir;
            movementDir = Vector3.zero;
            transform.position = movementTarget;
            movementDelta = 0f;
        }
    }
}