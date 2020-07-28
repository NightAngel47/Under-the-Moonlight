using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoonlightBehaviour : MonoBehaviour
{
    // Components
    protected Animator animator = null;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider2D;

    // Events
    public WerewolfTransformation EnterMoonlight;
    public WerewolfTransformation ExitMoonlight;

    // Params
    [SerializeField] private bool isMoonlightActive = true;
    [SerializeField] private bool useTimer = false;

    [SerializeField, Range(0.5f, 120f)] private float moonlightActiveTime = 1f;
    [SerializeField, Range(0.5f, 120f)] private float moonlightInactiveTime = 1f;

    // Variables
    private float _currentTimerCount = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsActive", isMoonlightActive);
        animator.SetBool("StartActive", isMoonlightActive);
        animator.SetTrigger("GoToStart");
    }

    private void Start()
    {
        //_collider2D = GetComponent<BoxCollider2D>();
        //_spriteRenderer = GetComponent<SpriteRenderer>();

        //_spriteRenderer.enabled = isMoonlightActive;
        //_collider2D.enabled = isMoonlightActive;

        if (useTimer)
        {
            StartCoroutine(MoonlightTimer());
        }
    }

    private IEnumerator MoonlightTimer()
    {
        _currentTimerCount = isMoonlightActive ? moonlightActiveTime : moonlightInactiveTime;

        yield return new WaitForSeconds(_currentTimerCount);

        ToggleMoonlight();
        StartCoroutine(MoonlightTimer());
    }
    
    private void ToggleMoonlight()
    {
        isMoonlightActive = !isMoonlightActive;
        animator.SetBool("IsActive", isMoonlightActive);
        //_spriteRenderer.enabled = isMoonlightActive;
        //_collider2D.enabled = isMoonlightActive;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isMoonlightActive && other.TryGetComponent(out WerewolfMovement werewolf))
            EnterMoonlight?.Invoke(werewolf);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out WerewolfMovement werewolf))
            ExitMoonlight?.Invoke(werewolf);
    }
}

[Serializable]
public class WerewolfTransformation : UnityEvent<WerewolfMovement> { }