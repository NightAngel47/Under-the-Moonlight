using UnityEngine;
using UnityEngine.UI;

namespace UnderTheMoonlight.UI
{
    public class PauseMenuCanvases : MonoBehaviour
    {
        [SerializeField] private Canvas pauseCanvas = null;
        public Canvas PauseCanvas => pauseCanvas;

        [Space]

        [SerializeField] private Canvas howToPlayCanvas = null;
        public Canvas HowToPlayCanvas => howToPlayCanvas;

        [SerializeField] private Button howToPlayExitButton = null;
        public Button HowToPlayExitButton => howToPlayExitButton;

        [Space]

        [SerializeField] private Canvas settingsCanvas = null;
        public Canvas SettingsCanvas => settingsCanvas;

        [SerializeField] private Button settingsExitButton = null;
        public Button SettingsExitButton => settingsExitButton;
    }
}