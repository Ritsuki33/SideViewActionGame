using UnityEngine;

public class CollideManager 
{

    // 踏んでいるかどうか
    public static RaycastHit2D StepOn(Vector3 position,CircleCollider2D circleCollider,float landJudgeDistance,LayerMask groundLayer)
    {

        Vector3 circlePoint = position + (Vector3)circleCollider.offset;
        float radius = circleCollider.radius;

        RaycastHit2D hit = Physics2D.CircleCast(circlePoint, radius, Vector2.down, landJudgeDistance, groundLayer);

        return hit;
    }

    public static RaycastHit2D HitHead(Transform transform, CapsuleCollider2D capseleCollider, float landJudgeDistance, LayerMask layerMask)
    {

        Vector2 point = (Vector2)transform.position + capseleCollider.offset;
        Vector2 headDirection = Quaternion.Euler(0, 0, transform.rotation.z) * Vector2.up;
        
        RaycastHit2D hit = Physics2D.CapsuleCast(point, capseleCollider.size, capseleCollider.direction, 0, headDirection, landJudgeDistance, layerMask);

        return hit;
    }

}
