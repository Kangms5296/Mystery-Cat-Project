using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class InformationData : MonoBehaviour {

    // 현재 플레이어가 가지고 있는 Document_Information 목록을 app 종료 후에도 유지하도록 함.
    // XML 파일은 User 쪽에서 접근이 가능하므로 이에 대한 보안 조치가 필요.


    // Resource 파일에 Information xml Data가 있는지 확인
    public bool IsXmlExist()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("IngameData/Document_Information/Data");
        if (textAsset == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Resources 파일에 Information xml Data를 생성
    public void CreateXmlData()
    {
        XmlDocument xmlDoc = new XmlDocument();

        // xml 버전, encoding, standalone
        XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);

        // 해당 정보를 xml에 추가
        xmlDoc.AppendChild(xmlDeclaration);

        // root node 생성
        XmlNode inventory = xmlDoc.CreateNode(XmlNodeType.Element, "Information", string.Empty);
        // root 추가
        xmlDoc.AppendChild(inventory);

        xmlDoc.Save("./Assets/Resources/IngameData/Document_Information/Data.xml");
    }

    // Resources 파일로 현재 인벤토리 Data들을 저장
    public void SaveXmlData()
    {
        // XML 파일 존재
        if (IsXmlExist())
        {
            // No work
        }
        // XML 파일 존재x
        else
        {
            // 우선 XML 파일을 만든다.
            CreateXmlData();
        }
        
        // xml 파일에 접근
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("./Assets/Resources/IngameData/Document_Information/Data.xml");

        // 기존의 문서 정보를 삭제
        xmlDoc.SelectSingleNode("Information").RemoveAll();

        DocumentInformationManager temp = GetComponent<DocumentInformationManager>();
        int slotCount = temp.slots.Length;

        // 모든 slot에 대해..
        for (int i = 0; i < slotCount; i++)
        {
            // 자식 노드 생성
            XmlNode clue = xmlDoc.CreateNode(XmlNodeType.Element, "clue", string.Empty);
            xmlDoc.SelectSingleNode("Information").AppendChild(clue);

            // 자식 노드에 들어갈 속성 ID 생성
            XmlElement Name = xmlDoc.CreateElement("Name");
            Name.InnerText = temp.slots[i].informationName;
            clue.AppendChild(Name);
        }

        // 변경사항 저장
        xmlDoc.Save("./Assets/Resources/IngameData/Document_Information/Data.xml");
    }

    // Resources 파일에서 현재 Document_Info UI로 Data들을 불러오기
    public void LoadXmlData()
    {
        // XML 파일 존재
        if (IsXmlExist())
        {
            DocumentInformationManager temp = GetComponent<DocumentInformationManager>();

            // xml 파일에 접근
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("./Assets/Resources/IngameData/Document_Information/Data.xml");

            // 현재 xml에 저장되어있는 아이템 정보들을 가져와서..
            XmlNodeList clues = xmlDoc.SelectNodes("Information/clue");

            // 각 slot에 Load 한다.
            foreach (XmlNode clue in clues)
            {
                temp.KnowNewInfo(clue.SelectSingleNode("Name").InnerText);
            }

        }
    }
}
