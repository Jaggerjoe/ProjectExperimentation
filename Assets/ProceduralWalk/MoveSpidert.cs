using DitzelGames.FastIK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveSpidert : MonoBehaviour
{
    [SerializeField] private float m_Speed = .5f;
    [SerializeField] private Transform m_TargetPoint;
    [SerializeField] private Transform m_IKTarget = null;
    [SerializeField] private AnimationCurve m_Curve = null;

    private float m_Lerp = 0;
    Vector3 m_OldPosition = Vector3.zero;
    float elapsedTime = 0;
    float m_DesireTime = 500;
    bool m_CanMove = false;

    // Start is called before the first frame update
    void Start()
    {
        //IkComp = GetComponentInChildren<FastIKFabric>();
        m_OldPosition=m_IKTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move(Time.deltaTime);
        MoveLegsSpider(Time.deltaTime);
    }

    private void Move(float p_DeltaTime)
    {
        transform.position += Vector3.right * p_DeltaTime * m_Speed;
    }
    
    void MoveLegsSpider(float p_DeltaTime)
    {
        float l_Dist = Vector3.Distance(m_TargetPoint.position, m_IKTarget.position);
        if(l_Dist > 2f) 
        {
           m_CanMove = true;
        }
        if(l_Dist < .3f)
        {
            m_CanMove = false;
            elapsedTime = 0;
        }
        if(m_CanMove)
        {
            elapsedTime += p_DeltaTime * 20;
            float l_Percenatge = elapsedTime / m_DesireTime;
            m_IKTarget.position = Vector3.Lerp(m_IKTarget.position, m_TargetPoint.position, l_Percenatge);
            //m_CanMove = false;
        }
    }
    IEnumerator MoveLeg()
    {
        while (Vector3.Distance(m_TargetPoint.position, m_IKTarget.position) >= .1f)
        {
           
            Debug.Log(Vector3.Distance(m_TargetPoint.position, m_IKTarget.position));
        }
            yield return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(m_TargetPoint.position, .2f);
    }
}
