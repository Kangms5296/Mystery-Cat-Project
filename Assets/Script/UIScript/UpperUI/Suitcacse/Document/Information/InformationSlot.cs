using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationSlot : MonoBehaviour {

    // 현재 슬롯에 보여지는 단서 이름
    public string informationName;
    // 현재 슬롯에 보여지는 단서 설명
    [TextArea]
    public string informationExp;
    // 현재 슬롯이 보여지는 단서의 이미지
    public Sprite informationImage;

    // slot 관리 manager
    private DocumentInformationManager documentInformationManager;





    // ============================================================== Public Function =============================================================

    public void Init(DocumentInformationManager parent)
    {
        documentInformationManager = parent;
    }

    public void OnClickSlot()
    {
        documentInformationManager.ClickNewSlot(gameObject);
    }

    // ============================================================= Get Set Function =============================================================
}
