using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "SO_Input", menuName = "Game/SO_Input")]
public class SO_InputManager : ScriptableObject
{
    
   [SerializeField] InputActionAsset m_PlayerActionAsset = null;
    Vector2 m_Movement;
    bool m_Reister = false;
    bool m_Rewind = false;
    bool m_Throw = false;
    bool m_DoubleJumping = false;
    bool m_Graping = false;
    int m_NumberJump;


    private void OnEnable()
    {
        BindInput(true);
    }

    private void OnDisable()
    {
       BindInput(false);
    }

    public void BindInput(bool p_IsEnable)
    {
        if(m_PlayerActionAsset == null)
        {
            return;
        }

        if(p_IsEnable)
        {
            m_PlayerActionAsset.FindAction("Player/Movement").performed += Move;
            m_PlayerActionAsset.FindAction("Player/Movement").canceled += Move;

            m_PlayerActionAsset.FindAction("Player/Register").started += Register;
            m_PlayerActionAsset.FindAction("Player/Rewind").started += StopRegister;

            m_PlayerActionAsset.FindAction("Player/Throw").started += Throw;
            //m_PlayerActionAsset.FindAction("Player/Throw").canceled += StopThrow;


            //m_PlayerActionAsset.FindAction("player/Grab").started += StartGrabing;
            //m_PlayerActionAsset.FindAction("player/Grab").canceled += StopGrabing;

            m_PlayerActionAsset.Enable();
        }
        else
        {
            m_PlayerActionAsset.FindAction("Player/Movement").performed -= Move;
            m_PlayerActionAsset.FindAction("Player/Movement").canceled -= Move;

            m_PlayerActionAsset.FindAction("Player/Register").started -= Register;
            m_PlayerActionAsset.FindAction("Player/Rewind").started -= StopRegister;

            m_PlayerActionAsset.FindAction("Player/Throw").started -= Throw;
            m_PlayerActionAsset.FindAction("Player/Throw").canceled -= StopThrow;

            //m_PlayerActionAsset.FindAction("player/Grab").started -= StartGrabing;
            //m_PlayerActionAsset.FindAction("player/Grab").canceled -= StopGrabing;

            m_PlayerActionAsset.Disable();
        }
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        m_Movement = ctx.ReadValue<Vector2>();
        m_Movement = Vector2.ClampMagnitude(m_Movement, 1f);
    }

    private void Register(InputAction.CallbackContext ctx)
    {
        m_Reister = true;
        m_NumberJump += 1;
    }

    private void StopRegister(InputAction.CallbackContext ctx)
    {
        m_Rewind = true;
    }
    private void Throw(InputAction.CallbackContext ctx)
    {
        m_Throw = true;
    }
    private void StopThrow(InputAction.CallbackContext ctx)
    {
        m_Throw = false;
    }
    public Vector2 MoveVector
    {
        get { return m_Movement; } 
        set { m_Movement = value; }
    }

    public bool RegisterBool
    {
        get { return m_Reister; }
        set { m_Reister = value; }
    }

    public bool RewindBool
    {
        get { return m_Rewind; }
        set { m_Rewind = value; }
    }

    public bool ThrowB
    {
        get { return m_Throw; }
        set { m_Throw = value; }
    }
}
