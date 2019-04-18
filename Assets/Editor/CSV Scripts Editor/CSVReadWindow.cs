using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CSVReadWindow : EditorWindow {

    // csv 파일을 받고, 그 파일의 정보를 캐싱하여 asset으로 생성합니다.
    // csv 파일은 반드시 Resource의 내부에 있어야 합니다. (모바일 구현을 위함)

    // csv 파일
    Object csvFile = null;
    // csv 파일 경로
    string csvFilePath = "";

    // 만들어질 Asset Name;
    string assetName = "";
    // 만들어질 Asset Path;
    string assetPath = "";
    bool pathToggle = true;


    [MenuItem("Window/Create Asset by CSV")]
    static void ShowWindow()
    {
        // 위의 경로에 새로 윈도우를 만든다.
        EditorWindow.GetWindow<CSVReadWindow>();

    }

    // 윈도우에 표시할 내용
    void OnGUI()
    {
        EditorGUIUtility.labelWidth = 150.0f;

        // Bold로 CSV File Reading 표시
        GUILayout.Label("CSV File Reading", EditorStyles.boldLabel);

        // 들여 쓰고..
        EditorGUI.indentLevel++;

        // csv 파일을 TextAsset Type으로 받는다.
        csvFile = EditorGUILayout.ObjectField("CSV File" ,csvFile, typeof(TextAsset), true);
        // 받은 csv 파일의 경로
        csvFilePath = AssetDatabase.GetAssetPath(csvFile as Object);
        // 경로를 표시
        EditorGUILayout.LabelField("CSV File Path");
        EditorGUI.indentLevel++;
        EditorGUIUtility.editingTextField = false;
        EditorGUILayout.TextField(csvFilePath);
        EditorGUIUtility.editingTextField = true;
        EditorGUI.indentLevel--;

        // 들여 쓰기 다시 빼고..
        EditorGUI.indentLevel--;

        // 한 줄 띄고..
        EditorGUILayout.Space();

        // Bold로 Making Asset Info 표시
        GUILayout.Label("Making Asset Info", EditorStyles.boldLabel);


        // 들여 쓰고..
        EditorGUI.indentLevel++;

        // 만들어질 asset의 이름을 받고
        assetName = EditorGUILayout.TextField("Asset Name", assetName);
        // 만들어질 asset의 Path를 받는다.
        pathToggle = EditorGUILayout.Toggle("Asset Path", pathToggle);
        if (pathToggle)
        {
            // Toggle이 눌러져 있으면(true 이면)..
            // asset의 경로 TextField에 csv 파일의 경로를 넣고 쓰기 권한을 없앤다.
            EditorGUIUtility.editingTextField = false;

            // asset의 경로 마지막에 csv 파일명을 지운다.
            assetPath = csvFilePath;
            if(assetPath.LastIndexOf('/') != -1)
                assetPath = assetPath.Substring(0 ,assetPath.LastIndexOf('/') + 1);
            // asset의 경로 마지막에 asset명을 적는다.
            assetPath += assetName + ".asset";
        }
        else
        {
            // Toggle이 안눌러져 있으면(false 이면)..
            // asset의 경로 TextField에 쓰기 권한을 부여하고, 기존의 내용을 지운다.
            EditorGUIUtility.editingTextField = true;

            assetPath = "";
        }
        EditorGUI.indentLevel++;
        assetPath = EditorGUILayout.TextField(assetPath);
        EditorGUI.indentLevel--;
        EditorGUIUtility.editingTextField = true;


        EditorGUILayout.Space();
        if(GUILayout.Button("Create Asset"))
        {
            // asset 정보를 담을 인스턴스 생성
            var instance = CreateInstance<ParsingData>();

            // csv 파일에서 정보를 추출하여 생성할 asset 정보 생성
            instance.CreateAssetInfo(csvFilePath);

            // 객체 정보를 담은 인스턴스 asset 형식으로 저장
            AssetDatabase.CreateAsset(instance, assetPath);
            AssetDatabase.Refresh();
        }

        //selected = EditorGUILayout.Popup("Label", selected, options);
        //if(GUI.Button(new Rect(0, 0, 100, 50), "Button"))
    }
}
