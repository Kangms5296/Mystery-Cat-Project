using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScript : MonoBehaviour {

    // Logo 표시 끝 - 씬 전환
    public void FinishedLogo()
    {
        SceneManager.LoadScene("MainScene");
    }
}
