using UnityEngine;

public class StaticInfoForSound {

    // 현재 배경 음악을 출력하는 오디오 Source
    public static AudioSource con_BGM_Audio;
    // 현재 배경 음악의 크기(메인 씬에서 조정한 값을 stage에서 사용할 수 있어야 하므로 static)
    public static float BGMSound = 0.5f;

    // 효과음의 크기(메인 씬에서 조정한 값을 stage에서 사용할 수 있어야 하므로 static)
    public static float EffectSound = 0.5f;

    // 딱히 넣을곳이 없으므로 여기다가 쳐넣어야겠다..
    // 현재 진행되는 slot 정보
    public static int playingSlotIndex = 0;
    
}
