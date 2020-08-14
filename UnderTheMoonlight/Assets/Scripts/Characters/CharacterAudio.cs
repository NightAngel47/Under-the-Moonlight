using UnityEngine;

namespace UnderTheMoonlight.Characters
{
    public class CharacterAudio : MonoBehaviour
    {
        protected AudioSource source = null;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }
    }
}