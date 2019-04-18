using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPointScript : MonoBehaviour {

    public enum Way { PULL, LOOK};
    public Way way;

    // Touch Event ID
    public string ID;

    private ObservationManager eventManager;


    [HideInInspector]
    public int touchCount;

    public enum NPCSpecialWork { ELSE, MILKBOY };
    public NPCSpecialWork nPCSpecialWork;

    private void Start()
    {
        eventManager = GameObject.Find("ObservationDisplayer").GetComponent<ObservationManager>();
        EventTrigger.Entry entry1, entry2, entry3, entry4, entry5;




        // Event Trigger 컴포넌트 추가
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        // Event Trigger 이벤트 추가
        switch (way)
        {
            case Way.PULL:
                entry1 = new EventTrigger.Entry();
                entry1.eventID = EventTriggerType.PointerDown;
                entry1.callback.AddListener((eventData) => { eventManager.OnButtonDownNPC(this); });
                eventTrigger.triggers.Add(entry1);

                entry2 = new EventTrigger.Entry();
                entry2.eventID = EventTriggerType.PointerUp;
                entry2.callback.AddListener((eventData) => { eventManager.OnButtonUpNPC(); });
                eventTrigger.triggers.Add(entry2);
                break;
            case Way.LOOK:
                entry1 = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerDown
                };
                entry1.callback.AddListener((eventData) => { eventManager.OnButtonDownNPC(this); });
                eventTrigger.triggers.Add(entry1);

                entry2 = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerUp
                };
                entry2.callback.AddListener((eventData) => { eventManager.OnButtonUpNPC(); });
                eventTrigger.triggers.Add(entry2);

                entry3 = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerEnter
                };
                entry3.callback.AddListener((eventData) => { eventManager.InTouchArea(this); });
                eventTrigger.triggers.Add(entry3);

                entry4 = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.Drag
                };
                entry4.callback.AddListener((eventData) => { eventManager.OnDragNPC(); });
                eventTrigger.triggers.Add(entry4);

                entry5 = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerExit
                };
                entry5.callback.AddListener((eventData) => { eventManager.OutTouchArea(); });
                eventTrigger.triggers.Add(entry5);

                break;
        }

    }
}
