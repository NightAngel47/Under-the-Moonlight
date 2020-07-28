using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    
    private void Awake()
    {
        PlayerCharacterInput playerInput = FindObjectOfType<PlayerCharacterInput>();
        if (!playerInput)
            Instantiate(player, transform.position, quaternion.identity);
        else
            playerInput.transform.position = transform.position;
    }
}
