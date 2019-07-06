using UnityEngine;
using UnityEngine.UI;

public class MissionScript : UIScript {

    // Click animation
    private Animation anim;

    // 표시할 UI
    private Text textUI;

    [HideInInspector]
    public bool isClicked = false;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animation>();
        textUI = transform.Find("Text").GetComponent<Text>();
        
    }

    private void OnEnable()
    {
        textUI.color = new Color(textUI.color.r, textUI.color.g, textUI.color.b, 0);
    }

    // Mission UI Click 시 행동 기술
    public override void OnClickUI()
    {
        // 절반 이상을 Play 했으면 다시 되돌아가면서 재생
        if (anim["MissionClick"].normalizedTime > 0.5f)
            anim["MissionClick"].speed = -1.0f;
        else
            anim["MissionClick"].speed = 1.0f;

        anim.Play("MissionClick");
        isClicked = true;
    }

    public void SetMission(string temp)
    {
        textUI.text = temp;
    }

    public string GetMission()
    {
        return textUI.text;
    }

    public void SaveMission()
    {
        // 현재 미션이 앱 종료 후에도 유지되도록 변경
        PlayerPrefs.SetString("ConMission", textUI.text);
    }

    public void LoadMission()
    {
        // 전에 저장한 미션을 Load
        textUI.text = PlayerPrefs.GetString("ConMission");
    }
}
