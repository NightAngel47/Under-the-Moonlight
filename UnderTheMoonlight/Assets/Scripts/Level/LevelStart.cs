using UnderTheMoonlight.Characters;
using Unity.Mathematics;
using UnityEngine;

namespace UnderTheMoonlight.Level
{
    public class LevelStart : MonoBehaviour
    {
        [SerializeField] private GameObject player = null;
    
        private void Awake()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            PlayerCharacterInput playerInput = FindObjectOfType<PlayerCharacterInput>();
            if (!playerInput)
                Instantiate(player, transform.position, quaternion.identity);
            else
                playerInput.transform.position = transform.position;
        }
    }
}
