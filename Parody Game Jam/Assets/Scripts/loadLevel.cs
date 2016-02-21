using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour {

	public string levelToLoad;

	public void LoadLeveL() {
		SceneManager.LoadScene(levelToLoad);
	}
}
