using System.Collections.Generic;
using UnityEngine;

public class UICaching : MonoBehaviour {

    // 상단에 존재하는 모든 UI들(tag가 UI로 되어있다.)
    public List<GameObject> UI;



    public void ClearUI()
    {
        UI.Clear();
    }

    public void AddUI(GameObject ui)
    {
        UI.Add(ui);
    }

    public List<GameObject> GetUI()
    {
        return UI;
    }
	
}
