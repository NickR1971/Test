using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CTransformController : MonoBehaviour
{
    protected Vector3 target;
    protected CMove action;

    protected abstract void DoAction();

    // Start
    void Awake()
    {
        target = transform.position;
        action = new CMove();
    }
    
    public bool OnUpdateAction()
    {
        if(action.IsActive())
        {
            action.UpdatePosition();
            DoAction();
        }

        return action.IsActive();
    }
    
    public void SetActionFromTo(Vector3 _start, Vector3 _target)
    {
        action.SetPositions(_start, target =  _target);
    }

    public void StartAction()
    {
        action.StartAction();
    }

    public void SetActionTime(float _t)
    {
        action.SetActionTime(_t);
    }

    public void SetActionSpeed(float _speed)
    {
        action.SetActionSpeed(_speed);
    }

    public void CorrectTargetPosition(Vector3 _target)
    {
        action.CorrectTargetPosition(target = _target);
    }
}
