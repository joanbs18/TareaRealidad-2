using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    public float speed = 160f; // Fuerza aplicada a la bola hacia adelante
    public float lateralSpeed = 5f; // Velocidad de movimiento lateral
    public List<GameObject> bolos; // Lista de bolos

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Busca un objeto con el nombre "Sphere" (la bola)
        GameObject balloonObject = GameObject.Find("Sphere");

        if (balloonObject != null)
        {
            Debug.Log("Balloon object found!");

            // Accede al Rigidbody para aplicar movimiento físico
            rb = balloonObject.GetComponent<Rigidbody>();

            if (rb == null)
            {
                Debug.LogError("No se encontró Rigidbody en el objeto Sphere");
            }
        }
        else
        {
            Debug.Log("Balloon object not found");
        }

        // Inicializa la lista de bolos buscando todos los objetos con el tag "Bolo"
        bolos = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bolo"));
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento lateral (izquierda y derecha) basado en input de teclas
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D o flechas izquierda/derecha
        Vector3 lateralMovement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        if (rb != null)
        {
            // Mueve la bola lateralmente (solo si no está rodando hacia adelante)
            rb.velocity = new Vector3(lateralMovement.x * lateralSpeed, rb.velocity.y, rb.velocity.z);
        }

        // Si se presiona la barra espaciadora, impulsa la bola hacia adelante en la dirección actual
        if (Input.GetKeyDown(KeyCode.Space) && rb != null)
        {
            rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

        // Chequea cuántos bolos siguen en pie
        int bolosEnPie = ContarBolosEnPie();
        Debug.Log("Bolos en pie: " + bolosEnPie);
    }

    // Método para detectar colisiones
    private void OnCollisionEnter(Collision collision)
    {
        // Si la esfera colisiona con un bolo u otro objeto
        if (collision.gameObject.tag == "Bolo") // Asegúrate de que los bolos tienen el tag "Bolo"
        {
            Debug.Log("¡Chocaste con un bolo!");
            // Los bolos caerán debido a la física de Unity si tienen Rigidbody
        }
    }

    // Método para contar los bolos que siguen en pie
    private int ContarBolosEnPie()
    {
        int count = 0;

        foreach (GameObject bolo in bolos)
        {
            if (bolo != null)
            {
                // Verifica si el bolo está inclinado (por ejemplo, más de 30 grados en el eje X o Z)
                Vector3 rotation = bolo.transform.eulerAngles;

                // Consideramos que un bolo está caído si se ha inclinado más de 30 grados en X o Z
                if (Mathf.Abs(rotation.x) < 30 && Mathf.Abs(rotation.z) < 30)
                {
                    count++; // Si está suficientemente vertical, sigue en pie
                }
            }
        }

        return count; // Retorna la cantidad de bolos en pie
    }
}
