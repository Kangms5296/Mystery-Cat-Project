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

    // 현재 슬롯에 보여지는 Text UI
    private Text innerText;

    // slot 관리 manager
    private DocumentInformationManager documentInformationManager;





    // ============================================================== Public Function =============================================================

    public void Init(DocumentInformationManager parent)
    {
        documentInformationManager = parent;

        innerText = transform.Find("Text").GetComponent<Text>();
    }

    public void OnClickSlot()
    {
        documentInformationManager.ClickNewSlot(gameObject);
    }
    
    public void SetNewSlot(string tempName, string tempExp)
    {
        informationName = tempName;
        informationExp = tempExp;

        innerText.text = informationName;
    }


    // ============================================================= Get Set Function =============================================================
}
