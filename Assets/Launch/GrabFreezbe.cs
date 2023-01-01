using UnityEngine;

public class GrabFreezbe : MonoBehaviour
{
    [SerializeField] private Transform m_Hand = null;
    private Controller m_Controller = null;
    private Launch m_Launch = null;

    // Start is called before the first frame update
    void Start()
    {
        m_Controller = GetComponent<Controller>();  
        m_Launch = GetComponent<Launch>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Freezbe"))
        {
            other.transform.position = m_Controller.ParentHand.transform.position;
            other.transform.parent = m_Controller.ParentHand;
            m_Controller.FreezbeClass = other.gameObject;
            m_Launch.Object = other.transform;
            other.GetComponent<Collider>().enabled = false;
        }
    }
}
