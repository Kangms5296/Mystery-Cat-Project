using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CutSceneManager : MonoBehaviour
{
    [System.Serializable]
    public struct ScriptInfo
    {
        public string scriptText;
        public Sprite scriptImg;
    }
    public ScriptInfo[] scriptInfos;

    // scriptText show Text
    public Text cutSceneScriptText;
    // scriptImg show Image
    public Image cutSceneScriptImg;

    // Sound Source(BGM)
    public AudioSource audioSource;

    // story fade in time
    public float fadeinTime;

    // story fade out time
    public float fadeoutTime;

    // 각 글자 별 Delay
    public float delayPerChar;

    // master speed
    public float speed = 1;

    // last black pannel
    public Image blackPanel;
    // last black pannel fade in time
    public float fadeTime;

    public AudioSource clickSound;
    public AudioClip SkipTouchSound;

    // script control variable
    private float conTime = 0;
    private bool isFinishLine = true;
    private bool doNextScript = true;

    // Use this for initialization
    void Start()
    {
        // 씬 Sound 지정
        audioSource.volume = StaticInfoForSound.BGMSound;
        clickSound.volume = StaticInfoForSound.EffectSound;

        // 위의 변수들을 바탕으로 스크립팅 시작
        StartCoroutine(CutSceneCheckingForSkip());
    }



    // ================================================================= public function ==================================================================

    public void OnClickTouchBtn()
    {
        clickSound.Play();

        if (!isFinishLine)
        {
            delayPerChar = 0;
            conTime = 20;
        }
        else
        {
            doNextScript = true;
        }
    }

    // skip 버튼 클릭
    public void OnClickSkip()
    {
        clickSound.clip = SkipTouchSound;
        clickSound.Play();
        
        StartCoroutine(VisibleBlackPanel());
    }

    // ================================================================= private function ==================================================================

    // skip을 누르거나, 스크립팅이 끝날 때 까지 대사를 출력
    IEnumerator CutSceneCheckingForSkip()
    {
        // 최초 대기
        yield return new WaitForSeconds(2);

        // For alpha value control 
        Color tempColor = cutSceneScriptText.color;

        // Line Control variable 
        int conLine = 0;
        int maxLine = scriptInfos.Length;

        // script를 한 줄씩 출력
        while(conLine < maxLine)
        {
            // 이전 script의 정보를 초기화
            cutSceneScriptText.text = "";
            delayPerChar = 0.12f;
            conTime = 0;

            // scriptImg를 새로운 sprite로 교체
            cutSceneScriptImg.color = new Color(1, 1, 1, 0);
            cutSceneScriptImg.sprite = scriptInfos[conLine].scriptImg;

            // 이미지를 표시
            while (conTime < fadeinTime)
            {
                cutSceneScriptImg.color = new Color(1, 1, 1, conTime / fadeinTime);
                cutSceneScriptText.color = new Color(tempColor.r, tempColor.g, tempColor.b, conTime / fadeinTime);
                conTime += Time.deltaTime;
                yield return null;
            }
            cutSceneScriptImg.color = new Color(1, 1, 1, 1);
            cutSceneScriptText.color = new Color(tempColor.r, tempColor.g, tempColor.b, 1);

            // 이제부터 화면을 터치 시 Skip or 다음 script 출력
            isFinishLine = false;
            doNextScript = false;
            conTime = 0;

            // 해당 라인의 글자들을 일정 간격(delayPerChar)으로 입력
            for (int conChar = 0; conChar < scriptInfos[conLine].scriptText.Length; conChar++)
            {
                // 현재 string의 입력값이 코드일 경우 따로 처리. 그 외엔 UI에 표시
                switch (scriptInfos[conLine].scriptText[conChar])
                {
                    case 't':
                        cutSceneScriptText.text = cutSceneScriptText.text += '\n';
                        continue;
                    case 'd':
                        float T1 = 0.8f;
                        for (float temp = 0; temp < T1;)
                        {
                            if (delayPerChar == 0)
                                T1 = 0;
                            temp += Time.deltaTime * speed;
                            yield return null;
                        }
                        continue;
                    case 'o':
                        float T2 = 0.6f;
                        if (delayPerChar == 0)
                            T2 = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            for (float temp = 0; temp < T2;)
                            {
                                temp += Time.deltaTime * speed;
                                yield return null;
                            }
                            cutSceneScriptText.text = cutSceneScriptText.text + '.';
                        }
                        continue;
                    default:
                        // 추가한 글자를 UI에 표시
                        cutSceneScriptText.text = cutSceneScriptText.text + scriptInfos[conLine].scriptText[conChar];
                        break;
                }

                // 다음 글자를 입력할 때까지 대기
                while (conTime < delayPerChar)
                {
                    conTime += Time.deltaTime * speed;
                    yield return null;
                }
                conTime = 0;
            }
            isFinishLine = true;

            // 모든 글자를 출력했으므로 화면을 터치할 때까지 대기
            while (!doNextScript)
                yield return null;

            // 만약 모든 script를 표시했다면..
            if (conLine + 1 == maxLine)
            {
                StartCoroutine(VisibleBlackPanel());
            }
            else
            {
                // 현재 표시한 내용들 제거
                conTime = fadeoutTime;
                while (conTime > 0)
                {
                    cutSceneScriptImg.color = new Color(1, 1, 1, conTime / fadeinTime);
                    cutSceneScriptText.color = new Color(tempColor.r, tempColor.g, tempColor.b, conTime / fadeinTime);
                    conTime -= Time.deltaTime;
                    yield return null;
                }
                cutSceneScriptImg.color = new Color(1, 1, 1, 0);
                cutSceneScriptText.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0);
                conTime = 0;
            }
 
            conLine++;
        }
    }

    IEnumerator VisibleBlackPanel()
    {
        blackPanel.color = new Color32(20, 20, 20, 0);
        blackPanel.gameObject.SetActive(true);

        // 검은 패널을 지정된 시간을 들여서 표시
        for (float tempTime = 0; tempTime < fadeTime; tempTime += Time.deltaTime * speed)
        {
            blackPanel.color = new Color(20.0f / 255, 20.0f / 255, 20.0f / 255, tempTime / fadeTime);
            audioSource.volume = (1 - tempTime / fadeTime) * StaticInfoForSound.BGMSound;
            yield return null;
        }
        blackPanel.color = new Color32(20, 20, 20, 255);
        audioSource.volume = 0;

        // 씬 전환
        SceneChange();

        yield return null;
    }

    private void SceneChange()
    {
        // 씬 전환
        string nextScene = PlayerPrefs.GetString("AfterStory");
        switch (nextScene)
        {
            case "Stage01":
                SceneManager.LoadScene("Stage01");
                break;
            case "MainScene":
                SceneManager.LoadScene("MainScene");
                break;
            default:
                break;
        }
    }
}
