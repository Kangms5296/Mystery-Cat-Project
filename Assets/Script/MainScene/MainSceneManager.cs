using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainSceneManager : MonoBehaviour {

    public float fadeTime;

    public CanvasGroup blackPanel;
    public AudioSource bgm;

    public void Start()
    {
        if ((1920 * Screen.height / Screen.width) > 1080)
        {
            CanvasScaler[] canvases = GameObject.FindObjectsOfType<CanvasScaler>();
            for (int i = 0; i < canvases.Length; i++)
                canvases[i].referenceResolution = new Vector2(1920, 1920 * Screen.height / Screen.width);
        }
    }


    // =============================================================================== public function ==============================================================================

    // 게임 시작 - 스토리 보기 이후 stage1 씬으로 이동
    public void OnClickStart()
    {
        PlayerPrefs.SetString("AfterStory", "Stage01");
        StartCoroutine(FadingBlack());
    }

    // 스토리 다시 보기 - 스토리 보기 이후 main 씬으로 이동
    public void OnClickStory()
    {
        PlayerPrefs.SetString("AfterStory", "MainScene");
        StartCoroutine(FadingBlack());
    }


    // ============================================================================== private function ==============================================================================

    private IEnumerator FadingBlack()
    {
        blackPanel.gameObject.SetActive(true);

        float conTime = 0;
        while (conTime < fadeTime)
        {
            // black panel의 알파값을 상승
            blackPanel.alpha = conTime / fadeTime;
            // bgm의 크기를 낮춘다.
            bgm.volume = StaticInfoForSound.BGMSound * (fadeTime - conTime) / fadeTime;

            conTime += Time.deltaTime;
            yield return null;
        }

        // 씬 이동
        SceneManager.LoadScene("TemporaryCutScene");
    }
}
