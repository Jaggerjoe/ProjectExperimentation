using UnityEngine;
using UnityEngine.InputSystem;

public class Launch : MonoBehaviour
{
    [SerializeField] private Rigidbody Freezbe;
    [SerializeField] private Transform Target;
    [SerializeField] private float m_Gravity = -18;
    [SerializeField] private float m_H = 15;

    private Vector3 m_PreviousVelocity;
    private Vector3 m_Velocity;
    private Vector3 m_PrePos;
    // Start is called before the first frame update
    void Start()
    {
        Freezbe.useGravity = false;
    }

    private void Update()
    {
        Vector3 l_NewPos = transform.position;
        m_Velocity = (Freezbe.position - m_PreviousVelocity) / Time.deltaTime;
        m_PreviousVelocity = Freezbe.position;
        m_PrePos = l_NewPos;
        DrawTrajectory();
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Lanch();
        }
    }

    void Lanch()
    {
        Physics.gravity = Vector3.forward * m_Gravity;
        Freezbe.useGravity = true;
        Freezbe.velocity = CalculateLaunchVelocity().InitialVelocity;
    }

    LaunchData CalculateLaunchVelocity()
    {
        float l_DispalecementZ = Target.position.z - Freezbe.position.z;
        Vector3 l_DispalementXY = new Vector3(Target.position.x - Freezbe.position.x, Target.position.y - Freezbe.position.y,0);
        float l_Time = (Mathf.Sqrt(-2 * m_H / m_Gravity) + Mathf.Sqrt(2 * (l_DispalecementZ - m_H) / m_Gravity));
        Vector3 l_VelocityZ = Vector3.forward * Mathf.Sqrt(-2 * m_Gravity * m_H);
        Vector3 l_VelocityXY = l_DispalementXY / l_Time;

        return new LaunchData( l_VelocityXY + l_VelocityZ * -Mathf.Sign(m_Gravity), l_Time);
    }

    void DrawTrajectory()
    {
        LaunchData l_Launch = CalculateLaunchVelocity();
        Vector3 l_PreviousDrawPoint = Freezbe.position;

        int l_Resolution = 30;
        for (int i = 1; i <= l_Resolution; i++)
        {
            float l_SimulateTime = i / (float)l_Resolution * l_Launch.TimeToTarget;
            Vector3 l_Displacement = l_Launch.InitialVelocity * l_SimulateTime + Vector3.forward * m_Gravity * l_SimulateTime * l_SimulateTime / 2f;
            Vector3 l_DrawPoint = Freezbe.position + l_Displacement;
            Debug.DrawLine(l_PreviousDrawPoint, l_DrawPoint, Color.red);
            l_PreviousDrawPoint = l_DrawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 InitialVelocity;
        public readonly float TimeToTarget;

        public LaunchData(Vector3 p_InitialVel, float p_TimeToTarget)
        {
            this.InitialVelocity = p_InitialVel;
            this.TimeToTarget = p_TimeToTarget;
        }
    }

    private void OnDrawGizmos()
    {
        LaunchData l_Launch = CalculateLaunchVelocity();
        Vector3 l_PreviousDrawPoint = Freezbe.position;

        int l_Resolution = 50;
        for (int i = 1; i <= l_Resolution; i++)
        {
            float l_SimulateTime = i / (float)l_Resolution * l_Launch.TimeToTarget;
            Vector3 l_Displacement = l_Launch.InitialVelocity * l_SimulateTime + Vector3.forward * m_Gravity * l_SimulateTime * l_SimulateTime / 2f;
            Vector3 l_DrawPoint = Freezbe.position + l_Displacement;
            Gizmos.DrawWireSphere(l_DrawPoint, .5f);
            l_PreviousDrawPoint = l_DrawPoint;
        }
    }
}
