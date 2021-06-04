using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamerController : MonoBehaviour
{
    private Transform m_transform;
    private Transform m_targetView;
    private bool isFirstView;
    [SerializeField]
    private GameObject obj1;
    [SerializeField]
    private GameObject obj2;
    private CMove move;

    ///////////////////
    // Start
    void Start()
    {
        m_transform = GetComponent<Transform>();
        move = new CMove();
        isFirstView = true;
        m_targetView = obj1.GetComponent<Transform>();
    }

    ////////////////////
    // LateUpdate
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isFirstView = !isFirstView;
            if(isFirstView) m_targetView = obj1.GetComponent<Transform>();
            else m_targetView = obj2.GetComponent<Transform>();
        }
        m_transform.LookAt(m_targetView.position);
    }
}
