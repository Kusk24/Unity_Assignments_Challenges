using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed = 0.25f;
    public GameObject dogPrefab;

    public float horizontalInput;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        }

        if (transform.position.x > 27)
        {
            transform.position = new Vector3(27, transform.position.y, transform.position.z);
        } else if (transform.position.x < -42) {
            transform.position = new Vector3(-42, transform.position.y, transform.position.z);
        }

        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.back * horizontalInput * speed);
    }
}
