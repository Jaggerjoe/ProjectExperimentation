using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Launch : MonoBehaviour
{
    [SerializeField] private Transform Freezbe;
    [SerializeField] private Transform m_Target;
    [SerializeField] private Transform m_TargetPrefab;

    [SerializeField] private float m_Gravity = -18;
    [SerializeField] private float m_H = 15;
    [SerializeField] private float m_Speed = 15;

    [SerializeField] private List<Vector3> m_ListPoints = new List<Vector3>();
    [SerializeField] private Vector3[] m_Position;

    private int m_WayPointIndex = 0;
    private int m_Resolution = 50;

    private bool m_Launch = false;

    // Start is called before the first frame update
    void Start()
    {
        //Freezbe.useGravity = false;
        m_Position = new Vector3[m_Resolution];
        QuadraticBeizerCurve();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !m_Launch)
        {
            //    DrawTrajectory();
            m_Launch = true;
        }
    }

    private void FixedUpdate()
    {
        if (m_Launch)
            Lanch();
    }
    public void Lanch()
    {
        //Physics.gravity = Vector3.forward * m_Gravity;
        //Freezbe.useGravity = true;
        //Freezbe.velocity = CalculateLaunchVelocity().InitialVelocity;
        if(m_Launch)
        {
            Freezbe.position = Vector3.MoveTowards(Freezbe.transform.position, m_Position[m_WayPointIndex], m_Speed * Time.fixedDeltaTime);
            if (Freezbe.position == m_Position[m_WayPointIndex])
            {
                m_WayPointIndex += 1;
            }
            if (m_WayPointIndex == m_Position.Length)
            {
                m_Launch = false;
                m_WayPointIndex = 0;
            }
        }
    }

    public void QuadraticBeizerCurve()
    {
        for (int i = 1; i < m_Resolution + 1; i++)
        {
            float t = i / (float)m_Resolution;
            m_Position[i - 1] = CalculateQuadratiqueBezierCurve(t, Freezbe.localPosition, PositionHeightCuvre(Freezbe.localPosition, m_Target.position), m_Target.localPosition);
        }
    }

    private Vector3 PositionHeightCuvre(Vector3 p0,Vector3 p1)
    {
        float x = (p1.x + p0.x)/2;
        float y = (p1.y + p0.y)/2;
        float z = (p1.z + p0.z)/2;
        m_TargetPrefab.position = new Vector3(p0.x, y, p1.z);

        
       return m_TargetPrefab.position;
    }

    private Vector3 CalculateQuadratiqueBezierCurve(float t , Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
    #region Kinematique Equation
    //LaunchData CalculateLaunchVelocity()
    //{
    //    float l_DispalecementZ = m_Target.position.z - Freezbe.position.z;
    //    Vector3 l_DispalementXY = new Vector3(m_Target.position.x - Freezbe.position.x, m_Target.position.y - Freezbe.position.y,0);
    //    float l_Time = (Mathf.Sqrt(-2 * m_H / m_Gravity) + Mathf.Sqrt(2 * (l_DispalecementZ - m_H) / m_Gravity));
    //    Vector3 l_VelocityZ = Vector3.forward * Mathf.Sqrt(-2 * m_Gravity * m_H);
    //    Vector3 l_VelocityXY = l_DispalementXY / l_Time;

    //    return new LaunchData(l_VelocityXY + l_VelocityZ, l_Time);
    //}

    //public void DrawTrajectory()
    //{
    //    LaunchData l_Launch = CalculateLaunchVelocity();
    //    Vector3 l_PreviousDrawPoint = Freezbe.position;

    //    m_ListPoints.Clear();
    //    for (int i = 1; i <= m_Resolution; i++)
    //    {
    //        float l_SimulateTime = i / (float)m_Resolution * l_Launch.TimeToTarget;
    //        Vector3 l_Displacement = l_Launch.InitialVelocity * l_SimulateTime + Vector3.forward * m_Gravity * l_SimulateTime * l_SimulateTime / 2f;
    //        Vector3 l_DrawPoint = Freezbe.position + l_Displacement;
    //        m_ListPoints.Add(l_DrawPoint);
    //        Debug.DrawLine(l_PreviousDrawPoint, l_DrawPoint, Color.red);
    //        l_PreviousDrawPoint = l_DrawPoint;
    //    }
    //    //m_Launch = true;
    //    //Lanch();
    //}

    //struct LaunchData
    //{
    //    public readonly Vector3 InitialVelocity;
    //    public readonly float TimeToTarget;

    //    public LaunchData(Vector3 p_InitialVel, float p_TimeToTarget)
    //    {
    //        this.InitialVelocity = p_InitialVel;
    //        this.TimeToTarget = p_TimeToTarget;
    //    }
    //}
    #endregion
    private void OnDrawGizmos()
    {
        ////With kinematique Equation
        //if(Freezbe != null && m_Target != null)
        //{
        //    LaunchData l_Launch = CalculateLaunchVelocity();
        //    Vector3 l_PreviousDrawPoint = Freezbe.position;

        //    for (int i = 1; i <= m_Resolution; i++)
        //    {
        //        float l_SimulateTime = i / (float)m_Resolution * l_Launch.TimeToTarget;
        //        Vector3 l_Displacement = l_Launch.InitialVelocity * l_SimulateTime + Vector3.forward * m_Gravity * l_SimulateTime * l_SimulateTime / 2f;
        //        Vector3 l_DrawPoint = Freezbe.position + l_Displacement;
        //        Gizmos.DrawWireSphere(l_DrawPoint, .5f);
        //        l_PreviousDrawPoint = l_DrawPoint;
        //    }
        //}

        if (!m_Launch)
        {
            for (int i = 1; i < m_Resolution + 1; i++)
            {
                float t = i / (float)m_Resolution;
                m_Position[i - 1] = CalculateQuadratiqueBezierCurve(t, Freezbe.position, PositionHeightCuvre(Freezbe.localPosition, m_Target.position), m_Target.localPosition);
                Gizmos.DrawWireSphere(m_Position[i - 1], .5f);
            }
        }
    }

    public Transform Object
    {
        set { Freezbe = value; }
    }

    public Transform Target
    {
        set {  m_Target = value; }
    }

}
