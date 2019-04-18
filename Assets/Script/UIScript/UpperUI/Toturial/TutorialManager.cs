using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialManager : MonoBehaviour {

    public GameObject tutorial;

    public Sprite activated;
    public Sprite deActivated;

    // 현재 눌러져 있는 dev 버튼 index
    private int conDev = 0;
    // 좌상단, 현재의 dev 표시 text
    public Text devText;

    // 중앙, tuto Image
    public Image tutoImage;
    // 현재 표시된 tuto Image
    private int conImage = 1;
    // 각 dev 버튼마다 가지는 image 정보
    [System.Serializable]
    public class TutoInfo
    {
        public GameObject devObject;
        public List<Sprite> sprites;
    }
    // 각 dev가 표시할 이미지들
    public List<TutoInfo> tutoImages;

    // 하단, tuto index
    public Text tutoIndex;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // =========================================================== public function =================================================
    
    // UI visible
    public void OnClickUI()
    {
        // UI 초기화
        InitUI();

        // UI 표시
        tutorial.SetActive(true);
    }

    // UI UnVisible
    public void OnClickExit()
    {
        // UI 감추기
        tutorial.SetActive(false);
    }

    public void InitUI()
    {
        // 좌측 dev 구분을 0로..
        ChangeDev(0);
    }

    public void ChangeDev(int index)
    {
        // 클릭한 dev 버튼을 활성화 표시
        tutoImages[conDev].devObject.GetComponent<Image>().sprite = deActivated;
        conDev = index;
        tutoImages[conDev].devObject.GetComponent<Image>().sprite = activated;

        // tuto Image를 해당 index의 이미지 그룹의 첫 이미지로 지정
        conImage = 0;
        tutoImage.sprite = tutoImages[conDev].sprites[conImage];
        TutoTextChanging();

        // tuto Text를 현재 dev의 내용으로 수정
        devText.text = GetDevTextByInde();
    }

    public void OnClickNext()
    {
        conImage++;

        // 만약 최대 이미지에 도달하면..
        if(conImage == tutoImages[conDev].sprites.Count)
        {
            // 첫 이미지 표시
            conImage = 0;
        }

        tutoImage.sprite = tutoImages[conDev].sprites[conImage];
        TutoTextChanging();
    }

    public void OnClickBefore()
    {
        conImage--;

        // 첫 이미지에서 버튼이 클릭되었다면..
        if (conImage == -1)
        {
            // 이 그룹의 마지막 이미지를 표시
            conImage = tutoImages[conDev].sprites.Count - 1;
        }

        tutoImage.sprite = tutoImages[conDev].sprites[conImage];
        TutoTextChanging();
    }


    // =========================================================== private function =================================================

    
    private void TutoTextChanging()
    {
        tutoIndex.text = string.Format("( {0} / {1} )", conImage + 1, tutoImages[conDev].sprites.Count);
    }

    private string GetDevTextByInde()
    {
        switch(conDev)
        {
            case 0:
                return "조 작";
            case 1:
                return "물 건";
            case 2:
                return "문 서";
            case 3:
                return "조 합";
            case 4:
                return "관 찰";
            default:
                return "NULL";
        }
    }

}
