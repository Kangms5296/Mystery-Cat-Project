using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportReaction : DelayedReaction
{
    public List<TeleportInfo> infos;

    protected override void ImmediateReaction()
    {
        // Inspector 창에서 전달받은 모든 객체에 대해..
        foreach (TeleportInfo info in infos)
        {
            // 각 캐릭터를 지정된 위치로 이동
            info.character.MoveToPositionImmediately(info.position);
        }
    }
}

// 캐릭터 이동에 대한 정보의 구조
[System.Serializable]
public class TeleportInfo
{
    // 이 캐릭터가..
    public NewCharacter character;

    // 이 위치로 이동
    public Vector3 position;
}
