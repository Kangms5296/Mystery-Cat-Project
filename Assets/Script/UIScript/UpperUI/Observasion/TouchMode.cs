public interface TouchMode {

    // 현재 클릭한 ObservationWay에 대한, NPC 버튼다운 패턴 정의
    void OnButtonDownNPC(TouchPointScript touchPoint);

    // 현재 클릭한 ObservationWay에 대한, NPC 드래그 패턴 정의
    void OnDragNPC();

    // 현재 클릭한 ObservationWay에 대한, NPC 버튼업 패턴 정의
    void OnButtonUpNPC();

    // 현재 모드에서 특정 Touch Area 접근
    void InTouchArea(TouchPointScript conTouch);

    // 현재 모드에서 특정 Touch Area 탈출
    void OutTouchArea();
}
