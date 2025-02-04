using UnityEngine;

public class MarsMoonOrbit : MonoBehaviour
{
    public Transform Mars;
    public float MoonMass = 0.5f;
    public float InitialOrbitSpeed = 4f;
    public float StabilizingSpeed = 0.1f;

    private Vector3 moonVelocity;
    private PlanetBehavior marsScript;

    private void Start()
    {
        if (Mars != null)
        {
            marsScript = Mars.GetComponent<PlanetBehavior>();
            Vector3 directionToMars = (Mars.position - transform.position).normalized;
            moonVelocity = Vector3.Cross(directionToMars, Vector3.up) * InitialOrbitSpeed;
        }
    }

    private void Update()
    {
        if (marsScript == null) return;

        Vector3 directionToMars = Mars.position - transform.position;
        float distance = directionToMars.magnitude;

        float forceMagnitude = marsScript.CalculateGravitationalForce(MoonMass, distance);
        Vector3 gravitationalForce = directionToMars.normalized * forceMagnitude;

        moonVelocity += gravitationalForce * Time.deltaTime;

        Vector3 desiredVelocity = Vector3.Cross(directionToMars.normalized, Vector3.up) * moonVelocity.magnitude;
        moonVelocity = Vector3.Lerp(moonVelocity, desiredVelocity, Time.deltaTime * StabilizingSpeed);

        moonVelocity.y = 0;
        transform.position += moonVelocity * Time.deltaTime;

        Debug.DrawLine(transform.position, Mars.position, Color.green);
        Debug.DrawRay(transform.position, moonVelocity, Color.red);
    }
}
