using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

    // GameOver 설명에 적을 내용
    public Text explanationText;

    // 돌아가기를 누르면 이루어질 작업
    public ReactionCollection backReaction;

    public float fadeSpeed;

    private Button returnBtn;


    public void VisibleGameOver()
    {
        // 화면에 표시가 완전히 될 때까지 돌아가기 버튼 클릭 금지
        returnBtn = transform.Find("ReturnBtn").GetComponent<Button>();
        returnBtn.enabled = false;

        // Gameover Panel을 켠다.
        gameObject.SetActive(true);

        // 서서히 Visible
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        float alpha = 0;
        while(alpha < 1)
        {
            canvasGroup.alpha = alpha;
            alpha += fadeSpeed * Time.deltaTime;
        }
        canvasGroup.alpha = 1;

        // 돌아가기 버튼 클릭 가능
        returnBtn.enabled = true;
    }

    public void OnClickReturn()
    {
        backReaction.InitAndReact();


    }
}
