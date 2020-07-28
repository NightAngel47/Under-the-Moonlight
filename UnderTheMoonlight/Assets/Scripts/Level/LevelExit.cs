using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private float loadDelay = 3;

    public UnityEvent ExitReached;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerCharacterInput player))
            StartCoroutine(ExitCoroutine());
    }

    /// <summary> Triggers the ExitReached event. </summary>
    /// <returns></returns>
    private IEnumerator ExitCoroutine()
    {
        ExitReached?.Invoke();

        yield return new WaitForSeconds(loadDelay);

        GameManager.Instance.LoadNextLevel();
    }
}
