using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "SO_Input", menuName = "Game/SO_Input")]
public class SO_InputManager : ScriptableObject
{
    
   [SerializeField] InputActionAsset m_PlayerActionAsset = null;
    Vector2 m_Movement;
    bool m_Throw = false;

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

            m_PlayerActionAsset.FindAction("Player/Throw").started += Throw;

            m_PlayerActionAsset.Enable();
        }
        else
        {
            m_PlayerActionAsset.FindAction("Player/Movement").performed -= Move;
            m_PlayerActionAsset.FindAction("Player/Movement").canceled -= Move;

            m_PlayerActionAsset.FindAction("Player/Throw").started -= Throw;

            m_PlayerActionAsset.Disable();
        }
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        m_Movement = ctx.ReadValue<Vector2>();
        m_Movement = Vector2.ClampMagnitude(m_Movement, 1f);
    }

    private void Throw(InputAction.CallbackContext ctx)
    {
        m_Throw = true;
    }

    public Vector2 MoveVector
    {
        get { return m_Movement; } 
        set { m_Movement = value; }
    }

    public bool ThrowB
    {
        get { return m_Throw; }
        set { m_Throw = value; }
    }
}
