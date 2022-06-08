using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerController 
{
    public bool RightRun();
    public bool LeftRun();
    public bool RightDash();
    public bool LeftDash();
    public bool Jump();
}
