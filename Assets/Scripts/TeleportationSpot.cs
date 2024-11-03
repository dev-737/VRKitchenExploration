using UnityEngine;

public class TeleportationSpot : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Renderer m_Renderer;

    void Start()
    {        
        target = transform;

        if (m_Renderer == null) m_Renderer = GetComponent<Renderer>();
        m_Renderer.enabled = false;
    }

    public void OnGazeEnter()
    {
        m_Renderer.enabled = true;
    }

    public void OnGazeExit()
    {
        m_Renderer.enabled = false;
    }
}
