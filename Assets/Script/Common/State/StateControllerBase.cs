using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class StateControllerBase<T>
{
    protected Dictionary<int, StateChildBase<T>> stateDic = new Dictionary<int, StateChildBase<T>>();

    public int currentState{ protected set; get; }

    public abstract void Initalize(T component,int initalizeStateType);

    private void OnUpdate(Func<T, int> Update, T component)
    {
        int nextState = Update(component);

        if (currentState == nextState)
        {
            return;
        }

        stateDic[currentState].OnExit(component);

        currentState = nextState;

        stateDic[currentState].OnEnter(component);
    }

    public void FixedUpdateSequence(T component)
    {
        OnUpdate(stateDic[currentState].StateFixedUpdate, component);
        
    }

    public void UpdateSequence(T component)
    {
        OnUpdate(stateDic[currentState].StateUpdate, component);
    }

    protected void AutoStateTransitionSequence(T component,int nextState){
        if(currentState==nextState){
            return;
        }

        stateDic[currentState].OnExit(component);
        currentState = nextState;
        stateDic[currentState].OnEnter(component);
    }

    public int GetCurrentState()
    {
        return currentState;
    }
 
}


