using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private SO_InputManager m_Input = null;
    [SerializeField] private float m_Speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement(m_Input.MoveVector);
    }
    void PlayerMovement(Vector3 p_Direction)
    {
        transform.position += p_Direction * m_Speed * Time.deltaTime;
    }
}
