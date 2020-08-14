using UnityEngine;

namespace UnderTheMoonlight.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private VolumeManager volManager = null;

        [Space]

        [SerializeField] private GameObject quitButton = null;

        private void Start()
        {
            volManager.Initalize();

            if (Application.platform == RuntimePlatform.WebGLPlayer && quitButton != null)
                quitButton.SetActive(false);
        }

        public void StartGameOnClick()
        {
            GameManager.Instance.StartGameAtFirstLevel();
        }

        public void QuitGameOnClick()
        {
            GameManager.Instance.Quit();
        }
    }
}