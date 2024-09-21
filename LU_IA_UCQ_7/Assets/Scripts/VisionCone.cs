using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public float viewRadius = 5f; // Radio del cono de visi�n
    [Range(0, 360)]
    public float viewAngle = 90f; // �ngulo del cono de visi�n
    public LayerMask targetMask;  // M�scara de los objetos que queremos detectar (GameObjects)
    public LayerMask obstacleMask; // M�scara de obst�culos (paredes, etc.)

    public Transform target; // El GameObject que queremos detectar

    private void Update()
    {
        DetectTargetsInView();
    }

    void DetectTargetsInView()
    {
        // Verificamos si el target est� dentro del radio de visi�n
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, target.position) < viewRadius)
        {
            // Verificamos si el target est� dentro del �ngulo de visi�n
            float angleBetweenAgentAndTarget = Vector3.Angle(transform.forward, directionToTarget);
            if (angleBetweenAgentAndTarget < viewAngle / 2)
            {
                // Verificamos si no hay un obst�culo entre el agente y el target
                if (!Physics.Raycast(transform.position, directionToTarget, Vector3.Distance(transform.position, target.position), obstacleMask))
                {
                    Debug.Log("Target Detectado: " + target.name);
                }
            }
        }
    }

    // M�todo auxiliar para visualizar el cono de visi�n en la escena de Unity
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2);

        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
    }

    // M�todo auxiliar para calcular la direcci�n desde un �ngulo
    private Vector3 DirFromAngle(float angleInDegrees)
    {
        angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
