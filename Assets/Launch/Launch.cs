using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Launch : MonoBehaviour
{
    [SerializeField] private Rigidbody Freezbe;
    [SerializeField] private Transform Target;
    [SerializeField] private float m_Gravity = -18;
    [SerializeField] private float m_H = 15;
    [SerializeField] private float m_Speed = 15;

    [SerializeField] private List<Vector3> m_ListPoints = new List<Vector3>();
    private int m_WayPointIndex = 0;
    private bool m_Launch = false;
    private int m_Resolution = 20;

    // Start is called before the first frame update
    void Start()
    {
        Freezbe.useGravity = false;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !m_Launch)
        {
            DrawTrajectory();
            Lanch();
        }

    }

    private void FixedUpdate()
    {
    
    }
    void Lanch()
    {
        Physics.gravity = Vector3.forward * m_Gravity;
        Freezbe.useGravity = true;
        Freezbe.velocity = CalculateLaunchVelocity().InitialVelocity;
        //Freezbe.position = Vector3.MoveTowards(Freezbe.transform.position, m_ListPoints[m_WayPointIndex], m_Speed * Time.fixedDeltaTime);
        //if(Freezbe.position == m_ListPoints[m_WayPointIndex])
        //{
        //    m_WayPointIndex += 1;
        //}
        //if(m_WayPointIndex == m_ListPoints.Count)
        //{
        //    m_Launch = false;
        //    m_WayPointIndex = 0;
        //}
    }

    LaunchData CalculateLaunchVelocity()
    {
        float l_DispalecementZ = Target.position.z - Freezbe.position.z;
        Vector3 l_DispalementXY = new Vector3(Target.position.x - Freezbe.position.x, Target.position.y - Freezbe.position.y,0);
        float l_Time = (Mathf.Sqrt(-2 * m_H / m_Gravity) + Mathf.Sqrt(2 * (l_DispalecementZ - m_H) / m_Gravity));
        Vector3 l_VelocityZ = Vector3.forward * Mathf.Sqrt(-2 * m_Gravity * m_H);
        Vector3 l_VelocityXY = l_DispalementXY / l_Time;

        return new LaunchData(l_VelocityXY + l_VelocityZ * -Mathf.Sign(m_Gravity), l_Time);
    }

    void DrawTrajectory()
    {
        LaunchData l_Launch = CalculateLaunchVelocity();
        Vector3 l_PreviousDrawPoint = Freezbe.position;

        m_ListPoints.Clear();
        for (int i = 1; i <= m_Resolution; i++)
        {
            float l_SimulateTime = i / (float)m_Resolution * l_Launch.TimeToTarget;
            Vector3 l_Displacement = l_Launch.InitialVelocity * l_SimulateTime + Vector3.forward * m_Gravity * l_SimulateTime * l_SimulateTime / 2f;
            Vector3 l_DrawPoint = Freezbe.position + l_Displacement;
            m_ListPoints.Add(l_DrawPoint);
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

        for (int i = 1; i <= m_Resolution; i++)
        {
            float l_SimulateTime = i / (float)m_Resolution * l_Launch.TimeToTarget;
            Vector3 l_Displacement = l_Launch.InitialVelocity * l_SimulateTime + Vector3.forward * m_Gravity * l_SimulateTime * l_SimulateTime / 2f;
            Vector3 l_DrawPoint = Freezbe.position + l_Displacement;
            Gizmos.DrawWireSphere(l_DrawPoint, .5f);
            l_PreviousDrawPoint = l_DrawPoint;
        }
    }
}
