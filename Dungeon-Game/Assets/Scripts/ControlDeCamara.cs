using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDeCamara : MonoBehaviour
{
    public Camera camera;
    public float duracionDelMovimiento = 1.0f;

    public void MoveTheCamera()
    {
        // Define la nueva posici�n de destino de la c�mara
        Vector3 nuevaPosicion = new Vector3(camera.transform.position.x, camera.transform.position.y + 1.1f, camera.transform.position.z);

        // Inicia una rutina para mover suavemente la c�mara
        StartCoroutine(MoverCamaraSuavemente(camera.transform.position, nuevaPosicion, duracionDelMovimiento));
    }

    IEnumerator MoverCamaraSuavemente(Vector3 posicionInicial, Vector3 posicionFinal, float duracion)
    {
        float tiempoPasado = 0f;

        while (tiempoPasado < duracion)
        {
            // Incrementa el tiempo transcurrido
            tiempoPasado += Time.deltaTime;

            // Interpola suavemente entre las posiciones inicial y final
            camera.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, tiempoPasado / duracion);

            // Espera hasta el pr�ximo fotograma
            yield return null;
        }

        // Aseg�rate de que la c�mara llegue exactamente a la posici�n final al final de la interpolaci�n
        camera.transform.position = posicionFinal;
    }
}

