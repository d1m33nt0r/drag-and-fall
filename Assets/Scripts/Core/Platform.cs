using UnityEngine;

namespace Core
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private int m_segmentsCount;
        [SerializeField] private PlatformSegment m_segment;
        
        private PlatformSegment[] m_segments;
        private float m_angle;
        
        
        private void Start()
        {
            m_angle = 360 / m_segmentsCount;
            Construct();
        }

        private void Construct()
        {
            m_segments = new PlatformSegment[m_segmentsCount];
            var position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

            for (int i = 1; i <= m_segmentsCount; i++)
                m_segments[i - 1] = Instantiate(m_segment, position, Quaternion.AngleAxis(m_angle * i, Vector3.up), transform);
        }
    }
}