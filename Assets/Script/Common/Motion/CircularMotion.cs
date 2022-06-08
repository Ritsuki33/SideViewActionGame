using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 円移動
public class CircularMotion : MonoBehaviour
{
     
    [SerializeField] GameObject gameObject;

    [SerializeField,Tooltip("移動速度")] float speed;
    [SerializeField,Tooltip("半径")] float radius;
    [SerializeField,Tooltip("追尾速度")] float followRate;

    [SerializeField] private float moveAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate(){
        if(gameObject){
            // ターゲットに追随
            this.transform.position = Vector3.Lerp(
                this.transform.position, 
                gameObject.transform.position + (this.transform.position - gameObject.transform.position).normalized * radius,
                followRate);

            // ターゲットを円運動
            // radiun to angle
            float angularVelocity = -speed / (this.transform.position - gameObject.transform.position).magnitude;
            float angle = (angularVelocity / Mathf.Deg2Rad) * Time.deltaTime;
            moveAngle += angle;

            // 360度を超えたらリセット
            moveAngle %= 360;
            
            
            this.transform.RotateAround(gameObject.transform.position, Vector3.forward, angle);

            this.transform.rotation =Quaternion.Euler(0, 0, 0);
        }
    }
}
