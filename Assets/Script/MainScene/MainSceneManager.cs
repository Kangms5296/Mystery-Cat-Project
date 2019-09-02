using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainSceneManager : MonoBehaviour {
    
    public CanvasGroup blackPanel;

    [Header("Start UI")]
    public GameObject touchToStart;

    [Header("Stage Choice UI")]
    public CanvasGroup stageUI;
    public Image stageBlack;
    public RectTransform hourHand;
    public RectTransform clock;
    public Image cantStage1;
    public Image cantStage2;
    public Image cantStage3;


    [Header("For New Game/Load Game")]
    public GameObject newLoadDisplayer;
    public GameObject loadDisplayer;

    [Header("For Developer")]
    public GameObject developerDisplayer;

    [Header("For Sound")]
    public SountScriptInMainScene soundScript;
    public AudioSource bgm;
    public AudioSource effect;
    public AudioClip uiClickClip;
    public AudioClip clockRotateClip;


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

    public void OnClickTouchToStart()
    {
        // 클릭 효과음
        effect.clip = uiClickClip;
        effect.Play();

        // 시작 터치 UI 삭제
        touchToStart.SetActive(false);

        stageUI.gameObject.SetActive(true);
        stageBlack.gameObject.SetActive(true);

        // Stage 선택 UI 화면 표시 코루틴 실행
        StartCoroutine(UIVisible());
    }

    // 게임 시작 버튼 클릭
    public void OnClickStart()
    {
        // 클릭 효과음
        effect.clip = uiClickClip;
        effect.Play();

        newLoadDisplayer.SetActive(true);
    }

    // 스토리 다시 보기 버튼 클릭
    public void OnClickStory()
    {
        // 클릭 효과음
        effect.clip = uiClickClip;
        effect.Play();

        PlayerPrefs.SetString("AfterStory", "MainScene");
        StartCoroutine(StartingReStory());
    }

    // 개발자 확인 UI 클릭
    public void OnClickDeveloper()
    {
        // 클릭 효과음
        effect.clip = uiClickClip;
        effect.Play();

        developerDisplayer.SetActive(true);
    }

    // 개발자 확인 UI 종료
    public void OnClickDeveloperClose()
    {
        // 클릭 효과음
        effect.clip = uiClickClip;
        effect.Play();

        developerDisplayer.SetActive(false);
    }

    // 사운드 UI 클릭
    public void OnClickSound()
    {
        // 클릭 효과음
        effect.clip = uiClickClip;
        effect.Play();

        soundScript.soundDisplayer.SetActive(true);
    }

    // 사운드 UI 종료
    public void OnClickSoundClose()
    {
        // 클릭 효과음
        effect.clip = uiClickClip;
        effect.Play();

        soundScript.soundDisplayer.SetActive(false);
    }
    
    public void OnClickNewGame()
    {
        // 클릭 효과음
        effect.clip = uiClickClip;
        effect.Play();

        newLoadDisplayer.SetActive(false);

        StaticInfoForSound.playingSlotIndex = 4;
        PlayerPrefs.SetString("AfterStory", "Stage01");
        StartCoroutine(StartingNewGame());
    }

    public void OnClickLoadGame()
    {
        // 클릭 효과음
        effect.clip = uiClickClip;
        effect.Play();

        newLoadDisplayer.SetActive(false);
        loadDisplayer.SetActive(true);
    }

    public void LoadGame(float rotateValue)
    {
        // 클릭 효과음
        effect.clip = uiClickClip;
        effect.Play();

        StartCoroutine(StartingLoadGame(rotateValue));
    }

    // ============================================================================== private function ==============================================================================

    private IEnumerator StartingNewGame()
    {
        // 터치를 막는다.
        blackPanel.gameObject.SetActive(true);

        // 시계가 약간 커지며 현재까지의 stage가 밝아진다.
        float conAlpha = 0;
        float maxAlpha = 0.5f;
        while (conAlpha < maxAlpha)
        {
            clock.localScale = new Vector3(1.1f + 0.05f * conAlpha * 2, 1.1f + 0.05f * conAlpha * 2, 1);
            cantStage1.color = new Color(1, 1, 1, 1 - conAlpha * 2);

            conAlpha += Time.deltaTime;
            yield return null;
        }
        clock.localScale = new Vector3(1.15f, 1.15f, 1);
        cantStage1.color = new Color(1, 1, 1, 0);

        effect.clip = clockRotateClip;

        // 잠시 대기
        yield return new WaitForSeconds(0.2f);

        // 시계 회전
        const float RotateSpeed = 60f;
        float conRotate = -115;
        float maxRotate = -205;
        float tempfloat = 0;
        while(conRotate > maxRotate)
        {
            hourHand.localEulerAngles = new Vector3(0, 0, conRotate);
            conRotate -= Time.deltaTime * RotateSpeed;
            tempfloat += Time.deltaTime * RotateSpeed;
            if(tempfloat > 10)
            {
                tempfloat = 0;
                effect.Play();
            }

            yield return null;
        }
        hourHand.localEulerAngles = new Vector3(0, 0, maxRotate);

        yield return new WaitForSeconds(1);

        // 씬 이동 간 페이드인
        float conTime = 0;
        while (conTime < 3)
        {
            // black panel의 알파값을 상승
            blackPanel.alpha = conTime / 3;
            // bgm의 크기를 낮춘다.
            bgm.volume = StaticInfoForSound.BGMSound * (3 - conTime) / 3;

            conTime += Time.deltaTime;
            yield return null;
        }

        // 씬 이동
        SceneManager.LoadScene("StoryCutScene");
    }

    private IEnumerator StartingLoadGame(float rotateValue)
    {
        // 터치를 막는다.
        blackPanel.gameObject.SetActive(true);

        // 시계가 약간 커지며 현재까지의 stage가 밝아진다.
        float conAlpha = 0;
        float maxAlpha = 0.5f;
        while (conAlpha < maxAlpha)
        {
            clock.localScale = new Vector3(1.1f + 0.05f * conAlpha, 1.1f + 0.05f * conAlpha, 1);
            cantStage1.color = new Color(1, 1, 1, 1 - conAlpha);

            conAlpha += Time.deltaTime;
            yield return null;
        }
        clock.localScale = new Vector3(1.15f, 1.15f, 1);
        cantStage1.color = new Color(1, 1, 1, 0);
        
        effect.clip = clockRotateClip;

        // 잠시 대기
        yield return new WaitForSeconds(0.2f);

        // 시계 회전
        const float RotateSpeed = 60f;
        float conRotate = -115;
        float maxRotate = conRotate - rotateValue;
        float tempfloat = 0;
        while (conRotate > maxRotate)
        {
            hourHand.localEulerAngles = new Vector3(0, 0, conRotate);
            conRotate -= Time.deltaTime * RotateSpeed;
            tempfloat += Time.deltaTime * RotateSpeed;
            if (tempfloat > 10)
            {
                tempfloat = 0;
                effect.Play();
            }

            yield return null;
        }
        hourHand.localEulerAngles = new Vector3(0, 0, maxRotate);

        yield return new WaitForSeconds(1);

        // 씬 이동 간 페이드인
        float conTime = 0;
        while (conTime < 3)
        {
            // black panel의 알파값을 상승
            blackPanel.alpha = conTime / 3;
            // bgm의 크기를 낮춘다.
            bgm.volume = StaticInfoForSound.BGMSound * (3 - conTime) / 3;

            conTime += Time.deltaTime;
            yield return null;
        }

        // 씬 이동
        SceneManager.LoadScene("Stage01");
    }


    private IEnumerator StartingReStory()
    {
        blackPanel.gameObject.SetActive(true);

        float conTime = 0;
        while (conTime < 3)
        {
            // black panel의 알파값을 상승
            blackPanel.alpha = conTime / 3;
            // bgm의 크기를 낮춘다.
            bgm.volume = StaticInfoForSound.BGMSound * (3 - conTime) / 3;

            conTime += Time.deltaTime;
            yield return null;
        }

        // 씬 이동
        SceneManager.LoadScene("StoryCutScene");
    }

    private IEnumerator UIVisible()
    {
        yield return null;

        float conTime = 0;
        float maxTime = 1f;

        while(conTime < maxTime)
        {
            stageUI.alpha = conTime;
            stageBlack.color = new Color(0, 0, 0, ( 140 * conTime )/ (255 * maxTime));
            conTime += Time.deltaTime;
            yield return null;
        }
        stageUI.alpha = 1;
        stageBlack.color = new Color32(0, 0, 0, 140);
    }
}
