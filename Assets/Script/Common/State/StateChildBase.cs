using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateChildBase<T>
{
    protected StateControllerBase<T> controller;

    protected int stateType{ set; get; }

    public virtual void Initialize(int stateType){
        this.stateType = stateType;
        //controller = new StateControllerBase<T>();
    }

    // 入場処理
    public abstract void OnEnter(T component);

    // 退場処理
    public abstract void OnExit(T component);

    // 更新処理
    public virtual int StateFixedUpdate(T component) { return this.stateType; }
    public virtual int StateUpdate(T component) { return this.stateType; }
   
}
