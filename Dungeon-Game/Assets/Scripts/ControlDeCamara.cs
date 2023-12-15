using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDeCamara : MonoBehaviour
{
    public Camera camera;
    public float duracionDelMovimiento = 1.0f;

    public void MoveTheCamera()
    {
        // Define la nueva posición de destino de la cámara
        Vector3 nuevaPosicion = new Vector3(camera.transform.position.x, camera.transform.position.y + 1.1f, camera.transform.position.z);

        // Inicia una rutina para mover suavemente la cámara
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

            // Espera hasta el próximo fotograma
            yield return null;
        }

        // Asegúrate de que la cámara llegue exactamente a la posición final al final de la interpolación
        camera.transform.position = posicionFinal;
    }
}

