using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{	
	public void StartButton()
    {
			SceneManager.LoadScene(1);
    }

	public void OptionsButton()
    {
			//Need code to enable Options canvas overlay. Will contain Audio controls at least, can also put Credits in here.
    }
    
	public void QuitButton()
    {
			Application.Quit();
    }
		
	
}
