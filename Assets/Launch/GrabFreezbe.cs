using UnityEngine;

public class GrabFreezbe : MonoBehaviour
{
    [SerializeField] private Transform m_Hand = null;
    private Controller m_Controller = null;
    // Start is called before the first frame update
    void Start()
    {
      m_Controller = GetComponent<Controller>();   
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
        }
    }
}
