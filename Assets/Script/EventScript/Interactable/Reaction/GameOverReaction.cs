using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverReaction : DelayedReaction
{
    public CanvasGroup blackPanel;
    public GameOver gameOverPanel;

    private GameObject myCorotine;

    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(BlackFadeIn()).gameObject;
    }

    IEnumerator BlackFadeIn()
    {
        blackPanel.alpha = 0;
        blackPanel.gameObject.SetActive(true);

        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            blackPanel.alpha = alpha;
            yield return null;
        }
        blackPanel.alpha = 1;

        yield return new WaitForSeconds(0.5f);

        gameOverPanel.InitGameOver();

        FSLocator.textDisplayer.reactionButton.enabled = true;
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();

        Destroy(myCorotine);
    }
}
