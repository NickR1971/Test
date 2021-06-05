using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraController : MonoBehaviour
{
    private Transform targetView;
    private bool isFirstView;
    [SerializeField]
    private GameObject obj1;
    [SerializeField]
    private GameObject obj2;
    private CMove move;
    private CMove look;
    private Vector3 cameraOffset;
    private const int maxDistance = 15;
    private const int minDistance = 5;

    ///////////////////
    // Start
    void Start()
    {
        move = new CMove();
        look = new CMove();
        move.SetActionSpeed(1.5f);
        isFirstView = true;
        targetView = obj1.transform;
        cameraOffset = new Vector3(0, 2, -10);
    }

    public void ChangeTagetView(Transform _target)
    {
        Vector3 previousPosition = targetView.position;
        targetView = _target;
        look.SetPositions(previousPosition, targetView.position);
        look.StartAction();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isFirstView = !isFirstView;
            //if (isFirstView) targetView = obj1.transform;
            //else targetView = obj2.transform;
            ChangeTagetView((isFirstView) ? obj1.transform : obj2.transform);
        }
    }

    private void ControlDistance()
    {
        float d = Vector3.Distance(transform.position, targetView.position);
        if (d > maxDistance || d < minDistance)
        {
            move.SetPositions(transform.position, targetView.position + cameraOffset);
            move.StartAction();
        }
    }

    ////////////////////
    // LateUpdate
    private void LateUpdate()
    {
        if (move.IsActive())
        {
            move.UpdatePosition();
            transform.position = move.GetCurrentPosition();
        }
        else ControlDistance();

       // transform.position = Vector3.Lerp(transform.position, targetView.position + cameraOffset, Time.deltaTime * 5f);

        if (look.IsActive())
        {
            look.CorrectTargetPosition(targetView.position);
            look.UpdatePosition();
            transform.LookAt(look.GetCurrentPosition());
        }
        else transform.LookAt(targetView.position);
    }
}
