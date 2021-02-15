using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float m_amplitude;
                     private float m_verticalAnchor;
                     private float m_currentElevation;
                     private float m_currentFrequency;
    [SerializeField] private float m_frequency;
                     private float m_currentAngle;
    [SerializeField] private float m_angleSpeed;
    private Vector3 location;
    void Awake() {

        location = this.gameObject.transform.position;
        m_verticalAnchor = location.y;
        m_currentElevation = 0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAngles();
        Oscillate();
        Turn();
    }
    void UpdateAngles() {
        m_currentFrequency += m_frequency; //* Time.deltaTime;
        //m_currentAngle += m_angleSpeed * Time.deltaTime;
    }
    void Oscillate() {
        m_currentElevation = m_verticalAnchor + m_amplitude * Mathf.Sin(m_currentFrequency);
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, m_currentElevation, this.gameObject.transform.position.z);
    }
    void Turn() {
        this.gameObject.transform.Rotate(Vector3.up, m_angleSpeed*Time.deltaTime, Space.Self);
    }
}
