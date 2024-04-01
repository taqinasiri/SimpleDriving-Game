using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedGainPerSecond = 0.2f;
    [SerializeField] private float turnSpeed = 5f;

    private int steerValue = 0;

    private void Update()
    {
        speed += speedGainPerSecond * Time.deltaTime;
        transform.Rotate(0,steerValue * turnSpeed * Time.deltaTime,0);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Steer(int value) => steerValue = value;
}