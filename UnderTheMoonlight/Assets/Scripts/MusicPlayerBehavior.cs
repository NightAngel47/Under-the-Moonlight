using UnderTheMoonlight.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnderTheMoonlight
{
    public class MusicPlayerBehavior : MonoBehaviour
    {
        public static MusicPlayerBehavior Instance { get; private set; } = null;

        [SerializeField] private PauseMenuManager pauseManager = null;
        private AudioSource source = null;

        [Space]

        [Min(1f)]
        [SerializeField] private float volDividerWhilePaused = 2f;
        private float startVol = 1f;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            source = GetComponent<AudioSource>();
            startVol = source.volume;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += CheckNewScene;

            pauseManager.PauseMenuStateChanged.AddListener((isPaused) => ChangeVolumeForPauseMenu(isPaused));
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= CheckNewScene;

            pauseManager.PauseMenuStateChanged.RemoveListener((isPaused) => ChangeVolumeForPauseMenu(isPaused));
        }

        private void Start()
        {
            CheckNewScene(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }

        /// <summary> Check when a new scene is loaded. </summary>
        /// <param name="scene"> Scene loaded. </param>
        /// <param name="mode"> Mode the scene was loaded using. </param>
        private void CheckNewScene(Scene scene, LoadSceneMode mode)
        {
            if (scene.name.Contains("Ending"))
                source.Stop();
            else if (!source.isPlaying)
                source.Play();
        }

        /// <summary> Changes the volume of the game when the game is paused or unpaused. </summary>
        /// <param name="isPaused"> Is the game paused? </param>
        private void ChangeVolumeForPauseMenu(bool isPaused)
        {
            source.volume = isPaused ? startVol / volDividerWhilePaused : startVol;
        }
    }
}