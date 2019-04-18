using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixConditionConverter : MonoBehaviour {

    public static string Converter(string mixResultName)
    {
        string tempConditionName = "x";

        switch(mixResultName)
        {
            case "보안관 신분증(위조)":
                tempConditionName = "02_Clue_MakeIdCard";
                break;
        }
        return tempConditionName;
    }
}
