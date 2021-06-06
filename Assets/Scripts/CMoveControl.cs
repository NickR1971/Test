using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMoveControl : CTransformController
{
    protected override void DoAction()
    {
        transform.position = action.GetCurrentPosition();
    }
}
