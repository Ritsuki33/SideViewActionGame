using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ベジュ曲線を使った曲線の移動（改良点は多いか）
/// A→B→Cとベジュ曲線で移動
/// S(t)=t*A+(1-t)*B
/// E(t)=t*B+(1-t)*C
/// P(t)=t*S(t)+(1-t)*E(t)
/// </summary>
public class CurveMotion : MonoBehaviour
{
    [SerializeField, Tooltip("通過点")] GameObject[] movePosition;
    [SerializeField, Tooltip("速さ")] float speed;
    Rigidbody2D rb;

    // 中心点ポジション
    int centerpositionNum;

    // 次の通過点ポジション
    int nextMovePositionNum;

    // 始点・終点
    Vector2 startPoint;
    Vector2 endPoint;

    float length1;
    float length2;

    float ratio;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        if (movePosition != null && movePosition.Length >= 3)
        {
            this.rb.position = movePosition[0].transform.position;
            centerpositionNum = 1;
            nextMovePositionNum = 2;

            startPoint = this.movePosition[nextMovePositionNum - 2].transform.position;
            endPoint = this.movePosition[nextMovePositionNum - 1].transform.position;

            length1 = Vector2.Distance(this.movePosition[nextMovePositionNum - 2].transform.position, this.movePosition[nextMovePositionNum - 1].transform.position);
            length2 = Vector2.Distance(this.movePosition[nextMovePositionNum - 1].transform.position, this.movePosition[nextMovePositionNum].transform.position);
        }
        ratio = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movePosition == null) return;

        if (Vector2.Distance(this.rb.position, this.movePosition[nextMovePositionNum].transform.position) < 0.1f)
        {
            if (nextMovePositionNum + 2 <= movePosition.Length - 1)
            {
                centerpositionNum += 2;
                nextMovePositionNum += +2;

                startPoint = this.movePosition[nextMovePositionNum - 2].transform.position;
                endPoint = this.movePosition[nextMovePositionNum - 1].transform.position;

                length1 = Vector2.Distance(this.movePosition[nextMovePositionNum - 2].transform.position, this.movePosition[nextMovePositionNum - 1].transform.position);
                length2 = Vector2.Distance(this.movePosition[nextMovePositionNum - 1].transform.position, this.movePosition[nextMovePositionNum].transform.position);

                ratio = 0;
            }
        }

        startPoint = Vector2.MoveTowards(startPoint, this.movePosition[centerpositionNum].transform.position, length1 * speed * Time.deltaTime);
        endPoint = Vector2.MoveTowards(endPoint, this.movePosition[nextMovePositionNum].transform.position, length2 * speed * Time.deltaTime);
        ratio += speed * Time.deltaTime;
        Vector2 toVector = Vector2.Lerp(startPoint, endPoint, ratio);
        this.rb.MovePosition(toVector);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(startPoint, new Vector3(1, 1, 0));
        Gizmos.DrawCube(endPoint, new Vector3(1, 1, 0));
    }

}
