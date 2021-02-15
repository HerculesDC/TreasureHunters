using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expendable_Movement : MonoBehaviour
{
    //TODO: REDO!
    [SerializeField] private float m_forwardSpeed;
    [SerializeField] private float m_turnSpeed;
    [SerializeField] private float m_jumpForce;
    [SerializeField] private float m_groundDistance;

    private Transform m_hullTransform;
    private Collider m_collider;
    private Rigidbody m_rb;

    private bool m_bGrounded;
    public bool Grounded { get { return m_bGrounded; } }

    void Awake() {

        m_bGrounded = false;
        m_rb = this.gameObject.GetComponentInChildren<Rigidbody>();
        m_collider = this.gameObject.GetComponentInChildren<Collider>();
        m_hullTransform = m_collider.gameObject.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
        Jump();
        Equate();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray();
        ray.origin = m_hullTransform.position;//WILL HAVE TO CHECK WHAT TO DO LATER
        ray.direction = Vector3.down;
        m_bGrounded = Physics.Raycast(ray, out hit, m_groundDistance);
        Debug.DrawRay(ray.origin, ray.direction * m_groundDistance, Color.red);
    }

    void Move() {
        if (m_bGrounded) {
            m_hullTransform.forward += m_hullTransform.forward * m_forwardSpeed*Input.GetAxis("Vertical")*Time.deltaTime;
        }
    }

    void Turn() {
        float temp = m_bGrounded ? 1 : 0.5f;
        m_rb.AddTorque(Vector3.up * m_turnSpeed*Input.GetAxis("Horizontal")*Time.deltaTime*temp);
    }

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (m_bGrounded) {
                m_rb.AddForce(Vector3.up * m_jumpForce);
            }
        }
    }

    void Equate() {
        this.gameObject.transform.position = m_hullTransform.position;
        this.gameObject.transform.rotation = m_hullTransform.rotation;
    }
}
