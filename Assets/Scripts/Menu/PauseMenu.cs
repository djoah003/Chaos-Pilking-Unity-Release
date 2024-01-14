using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


namespace menu
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;

        public GameObject PauseMenuUI;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (GameIsPaused)
                {
                    Resume();
                    
                }
                else
                {
                    Pause();
                    Cursor.lockState = CursorLockMode.None; // Tarkoitus: Hiiri on näkyvissä pausen aikana
                }
            }
        }

        public void Resume()
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            Cursor.lockState = CursorLockMode.Locked; // Hiiri lukitaan näyttöön kun painetaan resume.
        }

        void Pause()
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;

        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }



    }
}
