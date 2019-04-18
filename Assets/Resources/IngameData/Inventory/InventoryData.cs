using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class InventoryData : MonoBehaviour
{
    // 현재 플레이어가 가지고 있는 Inventory 아이템 목록을 app 종료 후에도 유지하도록 함.
    // XML 파일은 User 쪽에서 접근이 가능하므로 이에 대한 보안 조치가 필요.



    // Resource 파일에 Inventory xml Data가 있는지 확인
    public bool IsXmlExist()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("IngameData/Inventory/Data");
        if (textAsset == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Resources 파일에 Inventory xml Data를 생성
    public void CreateXmlData()
    {
        XmlDocument xmlDoc = new XmlDocument();

        // xml 버전, encoding, standalone
        XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);

        // 해당 정보를 xml에 추가
        xmlDoc.AppendChild(xmlDeclaration);

        // root node 생성
        XmlNode inventory = xmlDoc.CreateNode(XmlNodeType.Element, "Inventory", string.Empty);
        // root 추가
        xmlDoc.AppendChild(inventory);

        xmlDoc.Save("./Assets/Resources/IngameData/Inventory/Data.xml");
    }

    // Resources 파일로 현재 인벤토리 Data들을 저장
    public void SaveXmlData()
    {
        ContentScript temp = GetComponent<ContentScript>();
        int maxSlot = temp.GetItemSlotsCount();

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
        xmlDoc.Load("./Assets/Resources/IngameData/Inventory/Data.xml");

        // 기존의 아이템 정보를 삭제
        xmlDoc.SelectSingleNode("Inventory").RemoveAll();

        // 모든 slot에 대해..
        for (int i = 0; i < maxSlot; i++)
        {
            // 자식 노드 생성
            XmlNode item = xmlDoc.CreateNode(XmlNodeType.Element, "item", string.Empty);
            xmlDoc.SelectSingleNode("Inventory").AppendChild(item);

            // 자식 노드에 들어갈 속성 ID 생성
            XmlElement ID = xmlDoc.CreateElement("ID");
            ID.InnerText = temp.GetIDByIndexInItemSlot(i);
            item.AppendChild(ID);

            // 자식 노드에 들어갈 속성 ID 생성
            XmlElement count = xmlDoc.CreateElement("Count");
            count.InnerText = temp.GetCountByIndexInItemSlot(i).ToString();
            item.AppendChild(count);
        }

        // 변경사항 저장
        xmlDoc.Save("./Assets/Resources/IngameData/Inventory/Data.xml");
    }

    // Resources 파일에서 현재 인벤토리로 Data들을 불러오기
    public void LoadXmlData()
    {
        ContentScript temp = GetComponent<ContentScript>();

        // XML 파일 존재
        if (IsXmlExist())
        {
            // xml 파일에 접근
            //TextAsset textAsset = Resources.Load<TextAsset>("IngameData/Inventory/Data");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("./Assets/Resources/IngameData/Inventory/Data.xml");

            // 현재 xml에 저장되어있는 아이템 정보들을 가져와서..
            XmlNodeList items = xmlDoc.SelectNodes("Inventory/item");

            foreach(XmlNode item in items)
            {
                // 아이템 갯수만큼..
                int count = int.Parse(item.SelectSingleNode("Count").InnerText);
                for(int i = 0; i < count; i++)
                {
                    // 아이템 획득
                    temp.GetItem(item.SelectSingleNode("ID").InnerText);
                }
            }

        }
    }
}
