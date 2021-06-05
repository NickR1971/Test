using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraController : MonoBehaviour
{
    private Transform m_targetView;
    private bool isFirstView;
    [SerializeField]
    private GameObject obj1;
    [SerializeField]
    private GameObject obj2;
    private CMove m_move;
    private CMove m_look;
    private Vector3 cameraPos;
    private const int maxDistance = 15;
    private const int minDistance = 5;

    ///////////////////
    // Start
    void Start()
    {
        m_move = new CMove();
        m_look = new CMove();
        m_move.SetActionSpeed(1.5f);
        isFirstView = true;
        m_targetView = obj1.transform;
        cameraPos = new Vector3(0, 2, -10);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Vector3 previousPosition = m_targetView.position;
            isFirstView = !isFirstView;
            if (isFirstView) m_targetView = obj1.transform;
            else m_targetView = obj2.transform;
            m_look.SetPositions(previousPosition, m_targetView.position);
            m_look.StartAction();
        }
    }

    private void ControlDistance()
    {
        float d = Vector3.Distance(transform.position, m_targetView.position);
        if (d > maxDistance || d < minDistance)
        {
            m_move.SetPositions(transform.position, m_targetView.position + cameraPos);
            m_move.StartAction();
        }
    }

    ////////////////////
    // LateUpdate
    private void LateUpdate()
    {
        if (m_move.IsActive())
        {
            m_move.UpdatePosition();
            transform.position = m_move.GetCurrentPosition();
        }
        else ControlDistance();

        if(m_look.IsActive())
        {
            m_look.CorrectTargetPosition(m_targetView.position);
            m_look.UpdatePosition();
            transform.LookAt(m_look.GetCurrentPosition());
        }
        else transform.LookAt(m_targetView.position);
    }
}
