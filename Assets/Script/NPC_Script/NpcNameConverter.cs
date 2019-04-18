using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcNameConverter {

	public static string Converter(string name)
    {
        switch(name)
        {
            case "NPC1_Police":
                return "경찰";
            case "NPC2_???":
                return "범인";
            case "NPC3_Detective":
                return "형사";
            case "NPC4_Father":
                return "꼬맹이 아빠";
            case "NPC5_Homeless":
                return "노숙묘";
            case "NPC6_bartender":
                return "바텐더";
            case "NPC7_Milkboy":
                return "우유배달소년";
            case "NPC8_Customer":
                return "바 손님";
            case "NPC9_Library":
                return "사서 동료";
            case "경찰":
                return "NPC1_Police";
            case "범인":
                return "NPC2_???";
            case "형사":
                return "NPC3_Detective";
            case "꼬맹이 아빠":
                return "NPC4_Father";
            case "노숙묘":
                return "NPC5_Homeless";
            case "바텐더":
                return "NPC6_bartender";
            case "우유배달소년":
                return "NPC7_Milkboy";
            case "바 손님":
                return "NPC8_Customer";
            case "사서 동료":
                return "NPC9_Library";
        }
        return "";
    }
}
