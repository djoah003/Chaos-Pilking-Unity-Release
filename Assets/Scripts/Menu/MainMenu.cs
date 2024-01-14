using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Main menun index on 0 joten lis‰t‰‰n 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit"); // Vaan, ett‰ n‰hd‰‰n toimiiko asetettu nappi.
        Application.Quit(); // Toimii buildatussa versiossa
    }
}
