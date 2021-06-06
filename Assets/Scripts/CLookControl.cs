using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLookControl : CTransformController
{
    protected override void DoAction()
    {
        transform.LookAt(action.GetCurrentPosition());
    }
}
