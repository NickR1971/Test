using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraTest : MonoBehaviour
{
    [SerializeField] private GameObject obj1;
    [SerializeField] private GameObject obj2;
    private bool isFirstView;
    private Transform targetView;
    [SerializeField] private Vector3 cameraOffset;
    private const int maxDistance = 15;
    private const int minDistance = 5;
    private CMoveControl move;
    private CLookControl look;

    //////////////
    // Start
    void Start()
    {
        move = GetComponent<CMoveControl>();
        look = GetComponent<CLookControl>();
        move.SetActionSpeed(1.5f);
        isFirstView = true;
        targetView = obj1.transform;
        cameraOffset = new Vector3(0, 2, -10);
    }

    public void ChangeTagetView(Transform _target)
    {
        Vector3 previousPosition = targetView.position;
        targetView = _target;
        look.SetActionFromTo(previousPosition, targetView.position);
        look.StartAction();
    }

    /// //////////
    // Update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isFirstView = !isFirstView;
            ChangeTagetView((isFirstView) ? obj1.transform : obj2.transform);
        }
    }

    private void ControlDistance()
    {
        float d = Vector3.Distance(transform.position, targetView.position);
        if (d > maxDistance || d < minDistance)
        {
            move.SetActionFromTo(transform.position, targetView.position + cameraOffset);
            move.StartAction();
        }
    }
    ////////////////////
    // LateUpdate
    private void LateUpdate()
    {
        if(!move.OnUpdateAction())
            ControlDistance();

        look.CorrectTargetPosition(targetView.position);
        if ( !look.OnUpdateAction())
            transform.LookAt(targetView.position);
    }
}
