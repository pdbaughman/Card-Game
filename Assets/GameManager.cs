using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenu;  
    public GameObject winScreen;  
    public GameObject gameUI;    

    // Called when Start button is pressed
    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
    }

    // Called when Exit button is pressed
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game quit! (Does not close in editor)");
    }

    // Called when Reset button is pressed
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Call this when the player wins
    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
        gameUI.SetActive(false);
    }
	
	// Called when Main Menu button pressed
	public void BackToMainMenu()
	{
    winScreen.SetActive(false);
    mainMenu.SetActive(true);
    gameUI.SetActive(false);
	}

}
