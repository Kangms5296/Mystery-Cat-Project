using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TemporaryCutSceneManager : MonoBehaviour {

    // 대사
    [TextArea]
    public string cutSceneScript;

    // 대사의 Canvas 내 Text
    public Text cutSceneScriptText;

    // Sound Source(불륨 제어)
    public AudioSource audioSource;
    
    // 시작 Delay
    public float startDelay;

    // 각 단락 별 Delay
    public float delayPerStr;

    // 각 글자 별 Delay
    public float delayPerChar;

    public float speed = 1;

    private float conTime = 0;

    // Use this for initialization
    void Start() {
        // 씬 BGM 관리
        audioSource.volume = StaticInfoForSound.BGMSound;

        // 위의 변수들을 바탕으로 스크립팅 시작
        StartCoroutine(CutSceneCheckingForSkip());
    }




    // ========================================================== public funtion ============================================================

    // 검은 패널
    public Image blackPanel;
    // 검은 패널을 시각화하는 시간
    public float fadeTime;
    
    // skip 버튼 클릭
    public void OnClickSkip()
    {
        StartCoroutine(VisibleBlackPanel());
    }

    // =========================================================== private function ========================================================

    // skip을 누르거나, 스크립팅이 끝날 때 까지 대사를 출력
    IEnumerator CutSceneCheckingForSkip()
    {
        // 사용전 미리 글자 색 캐싱
        Color tempColor = cutSceneScriptText.color;

        // 시작 Delay 간 대기
        while (conTime <= startDelay)
        {
            conTime += Time.deltaTime * speed;
            yield return null;
        }
        conTime = 0;

        // 대사를 문자열 별로 나눈다.
        string[] lines = cutSceneScript.Split('\n');
        // 나눈 각 문자열을 한 글자씩 출력
        for(int conLine = 0; conLine < lines.Length; conLine++)
        {
            cutSceneScriptText.text = "";
            cutSceneScriptText.color = new Color(tempColor.r, tempColor.g, tempColor.b, 1);
            
            // 만약 이전의 문자열을 즉시 출력하였으면, 다시 문자 간 delay를 세팅
            delayPerChar = 0.14f;

            // 해당 라인의 글자들을 일정 간격(delayPerChar)으로 입력
            for (int conChar = 0; conChar < lines[conLine].Length; conChar++)
            {
                // 다음 글자를 입력할 때까지 대기
                while (conTime < delayPerChar)
                {
                    conTime += Time.deltaTime * speed;
                    yield return null;
                }
                conTime = 0;

                // 현재 string의 입력값이 코드일 경우 따로 처리. 그 외엔 UI에 표시
                switch(lines[conLine][conChar])
                {
                    case 't':
                        cutSceneScriptText.text = cutSceneScriptText.text += '\n';
                        continue;
                    case 'd':
                        float T1 = 0.5f;
                        if (delayPerChar == 0)
                            T1 = 0;

                        for(float temp = 0; temp < T1;)
                        {
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
                        cutSceneScriptText.text = cutSceneScriptText.text + lines[conLine][conChar];
                        break;
                }
            }

            if(conLine + 1 == lines.Length)
            {
                StartCoroutine(VisibleBlackPanel());
            }
            else
            {
                // 잠시 대기
                for (float temp = 0; temp < 0.3f;)
                {
                    temp += Time.deltaTime * speed;
                    yield return null;
                }

                // 현재 줄 페이드아웃
                while (conTime <= delayPerStr)
                {
                    conTime += Time.deltaTime * speed;
                    cutSceneScriptText.color = new Color(tempColor.r, tempColor.g, tempColor.b, 1 - (conTime / delayPerStr));
                    yield return null;
                }
                cutSceneScriptText.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0);
                conTime = 0;

                // 잠시 대기
                for (float temp = 0; temp < 0.5f;)
                {
                    temp += Time.deltaTime * speed;
                    yield return null;
                }
            }
        }

    }

    IEnumerator VisibleBlackPanel()
    {
        blackPanel.color = new Color(0, 0, 0, 0);
        blackPanel.gameObject.SetActive(true);

        // 검은 패널을 지정된 시간을 들여서 표시
        for (float tempTime = 0; tempTime < fadeTime; tempTime += Time.deltaTime * speed)
        {
            blackPanel.color = new Color(0, 0, 0, tempTime / fadeTime);
            audioSource.volume = (1 - tempTime / fadeTime) * StaticInfoForSound.BGMSound;
            yield return null;
        }
        blackPanel.color = new Color(0, 0, 0, 1);
        audioSource.volume = 0;

        // 씬 전환
        SceneChange();

        yield return null;
    }

    public void SpeedUp()
    {
        delayPerChar = 0;

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
