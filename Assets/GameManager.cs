using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenu;  
    public GameObject winScreen;  
    public GameObject gameUI;    

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game quit! (Does not close in editor)");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
        gameUI.SetActive(false);
    }
	
	public void BackToMainMenu()
	{
    winScreen.SetActive(false);
    mainMenu.SetActive(true);
    gameUI.SetActive(false);
	}

}
