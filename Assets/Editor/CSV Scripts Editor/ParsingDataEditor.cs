using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(ParsingData))]
public class ParsingDataEditor : Editor
{
    // 원본 클래스에 정의되어 있는, 모든 아이템의 정보를 담고 있는 딕셔너리에 접근하기 위한 스크립트
    ParsingData parsingData;
    
    //private List<Dictionary<string, object>> itemlist = new List<Dictionary<string, object>>();

    void OnEnable()
    {
        // 원본 클래스에 정의되어 있는, 모든 아이템의 정보를 담고 있는 딕셔너리에 접근하기 위한 스크립트 캐싱.
        parsingData = (ParsingData)target;
    }



    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        // Bold로 CSV File Info 표시
        GUILayout.Label("CSV File Info", EditorStyles.boldLabel);
   
        // 들여 쓰고..
        EditorGUI.indentLevel++;

        EditorGUILayout.TextField("CSV FIle Path   : ", parsingData.csvAllPath);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Contents Count : ", parsingData.list.Count.ToString());
        parsingData.type = (ParsingData.CSV_TYPE)EditorGUILayout.EnumPopup("CSV File Type   : ", parsingData.type);
        EditorGUILayout.Space();

        // 들여 쓴거 뺴고..
        EditorGUI.indentLevel--;
        

        if (EditorGUI.EndChangeCheck())
        {
            //변경전에 Undo 에 등록
            Undo.RecordObject(parsingData, "Change CSV");

        }
    }
}
