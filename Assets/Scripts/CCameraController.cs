using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraController : MonoBehaviour
{
    private Transform m_transform;
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
        m_transform = GetComponent<Transform>();
        m_move = new CMove();
        m_look = new CMove();
        m_move.SetActionSpeed(1.5f);
        isFirstView = true;
        m_targetView = obj1.GetComponent<Transform>();
        cameraPos = new Vector3(0, 2, -10);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Vector3 v;
            v = m_targetView.position;
            isFirstView = !isFirstView;
            if (isFirstView) m_targetView = obj1.GetComponent<Transform>();
            else m_targetView = obj2.GetComponent<Transform>();
            m_look.SetPositions(v, m_targetView.position);
            m_look.StartAction();
        }
    }

    private void CheckDistance()
    {
     float d;

        d = Vector3.Distance(m_transform.position, m_targetView.position);
        if (d > maxDistance || d < minDistance)
        {
            m_move.SetPositions(m_transform.position, m_targetView.position + cameraPos);
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
            //testVec = m_move.GetCurrentPosition();
            m_transform.position = m_move.GetCurrentPosition();
        }
        else CheckDistance();

        if(m_look.IsActive())
        {
            m_look.UpdatePosition();
            m_transform.LookAt(m_look.GetCurrentPosition());
        }
        else m_transform.LookAt(m_targetView.position);
    }
}
