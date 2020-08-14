using UnityEngine;
using UnityEngine.UI;

namespace UnderTheMoonlight.Managers
{
    public class EndingSceneManager : MonoBehaviour
    {
        [SerializeField] private Button mainMenuButton = null;

        private void OnEnable()
        {
            mainMenuButton.onClick.AddListener(() => GameManager.Instance.LoadMainMenu());
        }

        private void OnDisable()
        {
            mainMenuButton.onClick.RemoveListener(() => GameManager.Instance.LoadMainMenu());
        }
    }
}
