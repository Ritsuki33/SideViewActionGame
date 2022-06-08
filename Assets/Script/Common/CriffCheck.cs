using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriffCheck : MonoBehaviour
{

    [SerializeField] bool disable = false;
    RaycastHit2D hit;
    Gravity gravity;

    private void Start()
    {
        gravity = GetComponent<Gravity>();
    }
  
    public bool CheckCriff(bool leftflag)
    {
        if (disable) return false;
        Vector3 checkStartVector = this.transform.position;
        checkStartVector.x += (leftflag) ? -Mathf.Abs(this.transform.localScale.x / 2) : Mathf.Abs(this.transform.localScale.x / 2);
        Debug.DrawLine(checkStartVector, checkStartVector + Vector3.down, Color.red);
        RaycastHit2D hit = Physics2D.Linecast(checkStartVector, checkStartVector + Vector3.down, gravity.GetGroundLayer());
        return !hit;
    }

    public void SetDisable(bool disable)
    {
        this.disable = disable;
    }
   
}
