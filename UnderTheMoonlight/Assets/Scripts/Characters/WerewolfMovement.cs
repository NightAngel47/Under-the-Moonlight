using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WerewolfMovement : CharacterMovement
{
    public bool InMoonlight { get; private set; } = false;
    public bool IsTransforming { get; private set; } = false;

    [Header("Werewolf Fields")]
    [SerializeField] private float speedMultiplier = 2;
    [SerializeField] private float transformationTime = 2f;

    public UnityEvent TransformToWerewolf;
    public UnityEvent TransformToHuman;

    private void OnEnable()
    {
        foreach (MoonlightBehaviour moonlight in FindObjectsOfType<MoonlightBehaviour>())
        {
            moonlight.EnterMoonlight.AddListener((werewolf) => WerewolfInMoonlight(werewolf));
            moonlight.ExitMoonlight.AddListener((werewolf) => WerewolfOutOfMoonlight(werewolf));
        }
    }

    protected override void Update()
    {
        if (!IsPlayerMoving && InMoonlight && !IsTransforming)
            movementDir = previousDir;
        if (!IsPlayerMoving && !IsTransforming && movementDir != Vector3.zero && CanMove())
            StartCoroutine(Move());
    }

    /// <summary> A werewolf has entered the moonlight. </summary>
    /// <param name="werewolf"> The werewolf in question. </param>
    public void WerewolfInMoonlight(WerewolfMovement werewolf)
    {
        if (werewolf == this)
        {
            InMoonlight = true;

            TransformToWerewolf?.Invoke();
            StartCoroutine(Transform());
        }
    }

    /// <summary> A werewolf has left the moonlight. </summary>
    /// <param name="werewolf"> The werewolf in question. </param>
    public void WerewolfOutOfMoonlight(WerewolfMovement werewolf)
    {
        if (werewolf == this)
        {
            InMoonlight = false;

            TransformToHuman?.Invoke();
            StartCoroutine(Transform());
        }
    }

    /// <summary> Transforms this werewolf character into the correct state. </summary>
    /// <returns></returns>
    private IEnumerator Transform()
    {
        IsTransforming = true;
        animator.SetBool("IsHuman", !InMoonlight);
        animator.SetTrigger("IsTransforming");

        yield return new WaitForSeconds(transformationTime);

        IsTransforming = false;
    }

    /// <summary> Sets the direction for the character to move to if the werewolf isn't transforming. </summary>
    /// <param name="newDir"> The new direction for the player to go. </param>
    public override void SetDirection(Vector2 newDir)
    {
        if (!IsTransforming)
            base.SetDirection(newDir);
    }

    /// <summary> Checks to see if a player can move in that direction. </summary>
    protected override bool CanMove()
    {
        if (!InMoonlight)
            return base.CanMove();
        
        if (!base.CanMove())
            SetDirection(movementDir * -1f);

        return base.CanMove();
    }

    /// <summary> Moves the player towards the target. </summary>
    /// <returns> Returns true if the player is currently moving. </returns>
    protected override bool Moving()
    {
        if (!InMoonlight)
            return base.Moving();

        movementDelta += Time.deltaTime * movementSpeed * speedMultiplier;
        if (movementDelta > 1f)
            movementDelta = 1f;

        transform.position = Vector3.Lerp(movementStart, movementTarget, movementDelta);
        return movementDelta != 1;
    }

    /// <summary> Moves the character to a tile in the given direction if possible. </summary>
    protected override IEnumerator Move()
    {
        if (!InMoonlight)
            StartCoroutine(base.Move());
        else
        {
            movementStart = transform.position;
            animator.SetBool("IsWalking", true);

            yield return new WaitWhile(PlayerIsMoving);

            animator.SetBool("IsWalking", false);
            transform.position = movementTarget;
            movementDelta = 0f;

            if (InMoonlight)
            {
                SetDirection(movementDir);
                CanMove();
                StartCoroutine(Move());
            }
            else
            {
                previousDir = movementDir;
                movementDir = Vector3.zero;
            }
        }
    }
}