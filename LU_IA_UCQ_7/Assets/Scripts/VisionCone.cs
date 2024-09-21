using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public float viewRadius = 5f; // Radio del cono de visión
    [Range(0, 360)]
    public float viewAngle = 90f; // Ángulo del cono de visión
    public LayerMask targetMask;  // Máscara de los objetos que queremos detectar (GameObjects)
    public LayerMask obstacleMask; // Máscara de obstáculos (paredes, etc.)

    public Transform target; // El GameObject que queremos detectar

    private void Update()
    {
        DetectTargetsInView();
    }

    void DetectTargetsInView()
    {
        // Verificamos si el target está dentro del radio de visión
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, target.position) < viewRadius)
        {
            // Verificamos si el target está dentro del ángulo de visión
            float angleBetweenAgentAndTarget = Vector3.Angle(transform.forward, directionToTarget);
            if (angleBetweenAgentAndTarget < viewAngle / 2)
            {
                // Verificamos si no hay un obstáculo entre el agente y el target
                if (!Physics.Raycast(transform.position, directionToTarget, Vector3.Distance(transform.position, target.position), obstacleMask))
                {
                    Debug.Log("Target Detectado: " + target.name);
                }
            }
        }
    }

    // Método auxiliar para visualizar el cono de visión en la escena de Unity
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2);

        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
    }

    // Método auxiliar para calcular la dirección desde un ángulo
    private Vector3 DirFromAngle(float angleInDegrees)
    {
        angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
