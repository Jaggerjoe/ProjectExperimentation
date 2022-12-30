using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] private Controller m_Controller = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThrowFreezbe()
    {
        m_Controller.ThrowFreezbe();
    }
}
