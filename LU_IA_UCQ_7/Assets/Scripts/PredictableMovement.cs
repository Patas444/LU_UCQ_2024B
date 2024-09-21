using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictableMovement : SimpleMovement
{

    [SerializeField] private GameObject[] PatrolPoints;

    private int CurrentPatrolPoint = 0;

    [SerializeField] private float PatrolPointToleranceRadius;

    // Start is called before the first frame update
    new void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Tenemos que definir un �rea de aceptaci�n/tolerancia de "ya llegu�, �cu�l sigue?" 
        // Queremos checar si la distancia entre el punto de patrullaje actual y nuestro agente es menor o igual que 
        // un radio de tolerancia.
        if (Utilities.Utility.IsInsideRadius(PatrolPoints[CurrentPatrolPoint].transform.position, transform.position,
                PatrolPointToleranceRadius))
        {
            // Si estamos dentro, entonces ya llegamos y ya nos podemos ir hacia el siguiente punto de patrullaje.
            CurrentPatrolPoint++;
            CurrentPatrolPoint %= PatrolPoints.Length;

            // 0 % 4 = 0
            // 1 % 4 = 1
            // 2 % 4 = 2
            // 3 % 4 = 3
            // 4 % 4 = 0
            // 5 % 4 = 1

            // La otra forma ser�a usando un if
            // Si nuestro contador "CurrentPatrolPoints" es mayor o igual que el n�mero de elementos en el arreglo "PatrolPoints"
            // if (CurrentPatrolPoint >= PatrolPoints.Length)
            // {
            //     // Entonces lo reseteamos a 0.
            //     CurrentPatrolPoint = 0;
            // }
        }

        // Hacemos que nuestro agente haga Seek a los puntos de patrullaje
        // Ser� al punto de patrullaje al cual estemos yendo actualmente.
        Vector3 PosToTarget = PuntaMenosCola(PatrolPoints[CurrentPatrolPoint].transform.position, transform.position); // SEEK

        Velocity += PosToTarget.normalized * MaxAcceleration * Time.deltaTime;

        // Queremos que lo m�s r�pido que pueda ir sea a MaxSpeed unidades por segundo. Sin importar qu� tan grande sea la
        // flecha de PosToTarget.
        // Como la magnitud y la direcci�n de un vector se pueden separar, �nicamente necesitamos limitar la magnitud para
        // que no sobrepase el valor de MaxSpeed.
        Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);

        transform.position += Velocity * Time.deltaTime;

    }
}
