using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 2.0f;

    void Update()
    {
        // Déplace le personnage vers l'avant
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
