using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] private SO_InputManager m_Input = null;
    [SerializeField] private Launch m_Lauch = null;
    [SerializeField] private float m_Speed;
    [SerializeField] private Transform m_Parent = null;
    private GameObject m_Freezbe = null;

    private float m_SmoothTime = 0.05f;
    private float m_CurrentVelo;
    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement(m_Input.MoveVector);
        Throw(m_Input.ThrowB);
        //if (Keyboard.current.spaceKey.wasPressedThisFrame)
        //{
        //    CreateTargetLauch();
        //}
        if (m_Input.MoveVector.sqrMagnitude == 0)
            return;
    }

    void PlayerMovement(Vector3 p_Direction)
    {
        if(p_Direction != Vector3.zero)
        {
            transform.position += new Vector3(p_Direction.x, 0, p_Direction.y) * m_Speed * Time.deltaTime;

            float l_TargetAngle = Mathf.Atan2(p_Direction.x, p_Direction.y) * Mathf.Rad2Deg;
            float l_Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, l_TargetAngle, ref m_CurrentVelo, m_SmoothTime);
            transform.rotation = Quaternion.Euler(0f, l_Angle, 0f);
            m_Animator.SetBool("Move", true);
        }
        else
        {
            m_Animator.SetBool("Move", false);
        }
    }

    void CreateTargetLauch()
    {
        Transform l_Target = new GameObject("Target").transform;
        l_Target.position = transform.position + transform.forward * 15;
        m_Lauch.Target = l_Target;
        //m_Lauch.DrawTrajectory();
        Debug.Log(l_Target.position);
    }

    private void Throw(bool p_Trow)
    {
        if(p_Trow)
        {
            m_Animator.SetTrigger("Throw");
            m_Input.ThrowB = false;
        }
    }

    public void ThrowFreezbe()
    {
        if(m_Freezbe != null)
        {
            
            m_Freezbe.transform.parent = null;
            m_Freezbe = null;
        }
    }

    public Transform ParentHand
    {
        get { return m_Parent; }
    }

    public GameObject FreezbeClass
    {
        set { m_Freezbe = value; }
    }
}
