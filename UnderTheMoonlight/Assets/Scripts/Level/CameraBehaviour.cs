using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBehaviour : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private Transform _playerTransform = null;
    private Transform _exitTransform = null;
    [SerializeField] private float transitionSpeed = 0;
    private float movementDelta = 0.0f;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _virtualCamera.Follow = FindObjectOfType<PlayerCharacterInput>().transform;
    }

    private void LevelStart()
    {
        _virtualCamera.Follow = FindObjectOfType<LevelExit>().transform;
        

    }

    bool LevelIntro()
    {
        movementDelta += Time.deltaTime * transitionSpeed;
        if (movementDelta > 1f)
            movementDelta = 1f;

        transform.position = Vector3.Lerp(_exitTransform.position, _playerTransform.position, movementDelta);
        return movementDelta != 1;
    }
}
