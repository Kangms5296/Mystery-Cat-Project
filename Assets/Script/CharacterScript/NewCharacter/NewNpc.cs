using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNpc : NewCharacter {


    // =========================================== 공개 메서드 ============================================

    // Use this for initialization
    // Start() 함수에 작성할 내용을 이 함수에 작성할 것!!
    // Start() 함수 작성 금지!!
    protected override void TemplateStart()
    {

    }
    

    // =========================================== 비공개 메서드 ============================================


    // 이전에 작용되고 있던 애니메이션을 표시 - 같은 애니메이션이 계속 호출 되는것을 방지, 4방향의 Idle 중 어느 방향을 호출할 지 선택
    private BEHAVIOR_MODE beforeDir = BEHAVIOR_MODE.IDLE;

    protected override void ChangeAnimation(Vector2 moveVector)
    {
        // 이동 애니메이션
        if (isMoving)
        {
            // 오른쪽
            if (moveVector.x > 0)
            {
                // 위
                if (moveVector.y > 0)
                {
                    if (beforeDir != BEHAVIOR_MODE.MOVE_TOPRIGHT)
                    {
                        // 이전에 발동된 Trigger을 해제
                        anim.ResetTrigger("TopLeft");
                        anim.ResetTrigger("BotLeft");
                        anim.ResetTrigger("BotRight");

                        // 새로운 이동 Trigger 지정
                        anim.SetTrigger("TopRight");

                        beforeDir = BEHAVIOR_MODE.MOVE_TOPRIGHT;
                    }
                }
                // 아래
                else
                {
                    if (beforeDir != BEHAVIOR_MODE.MOVE_BOTRIGHT)
                    {
                        // 이전에 발동된 Trigger을 해제
                        anim.ResetTrigger("TopLeft");
                        anim.ResetTrigger("BotLeft");
                        anim.ResetTrigger("TopRight");

                        // 새로운 이동 Trigger 지정
                        anim.SetTrigger("BotRight");

                        beforeDir = BEHAVIOR_MODE.MOVE_BOTRIGHT;
                    }
                }
            }
            // 왼쪽
            else
            {
                // 위
                if (moveVector.y > 0)
                {
                    if (beforeDir != BEHAVIOR_MODE.MOVE_TOPLEFT)
                    {
                        // 이전에 발동된 Trigger을 해제
                        anim.ResetTrigger("TopRight");
                        anim.ResetTrigger("BotLeft");
                        anim.ResetTrigger("BotRight");

                        // 새로운 이동 Trigger 지정
                        anim.SetTrigger("TopLeft");

                        beforeDir = BEHAVIOR_MODE.MOVE_TOPLEFT;
                    }
                }
                // 아래
                else
                {
                    if (beforeDir != BEHAVIOR_MODE.MOVE_BOTLEFT)
                    {
                        // 이전에 발동된 Trigger을 해제
                        anim.ResetTrigger("TopLeft");
                        anim.ResetTrigger("BotRight");
                        anim.ResetTrigger("BotRight");

                        // 새로운 이동 Trigger 지정
                        anim.SetTrigger("BotLeft");

                        beforeDir = BEHAVIOR_MODE.MOVE_BOTLEFT;
                    }
                }
            }
        }
        // 정지 애니메이션
        else
        {
            anim.SetTrigger("Idle");
            beforeDir = BEHAVIOR_MODE.IDLE;
        }
    }
}
