using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class RewindTime : MonoBehaviour
{
    [SerializeField] private SO_InputManager m_Input = null;

    [SerializeField]
    List<Vector3> m_Position = new List<Vector3> ();

    [SerializeField] float m_NextRegister = 1.25f;
    [SerializeField] float m_CurrentSpeedTimer;
    [SerializeField] float m_CurrentTime = 0;
    [SerializeField] float m_MaxPositionRegister = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_Input.RewindBool = false;
        m_Input.RegisterBool= false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RegiterPosition();
        Rewind();
        Debug.Log(m_Input.RegisterBool);
        Debug.Log(m_Input.RewindBool);
    }

    void RegiterPosition()
    {
        if( m_Position.Count > Mathf.Round(5f/Time.fixedDeltaTime))
        {
            m_Position.RemoveAt(m_Position.Count - 1);
        }
        else if(m_Input.RegisterBool)
        {
            m_Position.Insert(0,transform.position);
        }
    }

    void Rewind()
    {
        if(m_Position.Count != 0 && m_Input.RewindBool)
        {
            Debug.Log("reset0");
            m_Input.RegisterBool = false;
            m_Input.BindInput(false);
            transform.position = m_Position[0];
            m_Position.RemoveAt(0);
        }
        else if (m_Position.Count == 0)
        {
            m_Input.RewindBool = false;
            m_Input.BindInput(true);
        }
    }
}
