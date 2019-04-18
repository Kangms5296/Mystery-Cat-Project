using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    // return 버튼
    public Button returnBtn;
    // return 이후 처리할 이벤트들
    public ReactionCollection reaction;


    public void InitGameOver()
    {
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        StartCoroutine(Init());
    }

    public void OnclickReturn()
    {
        returnBtn.enabled = false;

        StartCoroutine(End());
    }

    private IEnumerator Init()
    {
        CanvasGroup canvasComponent = GetComponent<CanvasGroup>();

        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            canvasComponent.alpha = alpha;
            yield return null;
        }
        alpha = 1;
        canvasComponent.alpha = alpha;

        returnBtn.enabled = true;
    }

    private IEnumerator End()
    {
        CanvasGroup canvasComponent = GetComponent<CanvasGroup>();

        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            canvasComponent.alpha = alpha;
            yield return null;
        }
        alpha = 0;
        canvasComponent.alpha = alpha;

        reaction.InitAndReact();

        gameObject.SetActive(false);
    }
}
