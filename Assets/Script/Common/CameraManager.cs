using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] GameObject target;
    [SerializeField] float speed = 0.5f;
    [SerializeField] bool constraintX = false;
    [SerializeField] bool constraintY = false;

    public enum Mode
    {
        None,
        Constant,   //　等速移動
        Spring      // ばね移動
    };
    
    public Mode CurrentMode{
        get;set;
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentMode=Mode.Constant;
    }

    // Update is called once per frame
    void Update()
    {
        // 差分
        Vector2 currentPos = this.transform.position;
        Vector2 targetPos = target.transform.position;
        Vector2 difference = targetPos - currentPos;
        

        switch (CurrentMode)
        {
            case Mode.None:
                break;
            case Mode.Constant:
                Vector2 mv = speed * difference.normalized;
                if (constraintX) mv.x = 0;
                if (constraintY) mv.y = 0;
                Vector2 nextPos = currentPos + mv;
                if ((currentPos.x <targetPos.x&&targetPos.x<nextPos.x)||(currentPos.x >targetPos.x&&targetPos.x>nextPos.x)){
                    mv.x = targetPos.x - currentPos.x;
                }

                if ((currentPos.y < targetPos.y && targetPos.y < nextPos.y) || (currentPos.y > targetPos.y && targetPos.y > nextPos.y))
                {
                    mv.y = targetPos.y - currentPos.y;
                }
                this.transform.Translate(mv);
                break;
            case Mode.Spring:
                mv = speed * difference;
                if (constraintX) mv.x = 0;
                if (constraintY) mv.y = 0;
                this.transform.Translate(mv);
                break;
        }
    }
    
}
