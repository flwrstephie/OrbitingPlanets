using UnityEngine;

public class NeptuneMoonOrbit : MonoBehaviour
{
    public Transform Neptune;
    public float MoonMass = 0.8f; // Adjusted moon mass for Neptune's moons
    public float InitialOrbitSpeed = 6f; // Faster orbiting due to Neptune's stronger gravity
    public float StabilizingSpeed = 0.1f;

    private Vector3 moonVelocity;
    private PlanetBehavior neptuneScript;

    private void Start()
    {
        if (Neptune != null)
        {
            neptuneScript = Neptune.GetComponent<PlanetBehavior>();
            Vector3 directionToNeptune = (Neptune.position - transform.position).normalized;
            moonVelocity = Vector3.Cross(directionToNeptune, Vector3.up) * InitialOrbitSpeed;
        }
    }

    private void Update()
    {
        if (neptuneScript == null) return;

        Vector3 directionToNeptune = Neptune.position - transform.position;
        float distance = directionToNeptune.magnitude;

        float forceMagnitude = neptuneScript.CalculateGravitationalForce(MoonMass, distance);
        Vector3 gravitationalForce = directionToNeptune.normalized * forceMagnitude;

        moonVelocity += gravitationalForce * Time.deltaTime;

        Vector3 desiredVelocity = Vector3.Cross(directionToNeptune.normalized, Vector3.up) * moonVelocity.magnitude;
        moonVelocity = Vector3.Lerp(moonVelocity, desiredVelocity, Time.deltaTime * StabilizingSpeed);

        moonVelocity.y = 0;
        transform.position += moonVelocity * Time.deltaTime;

        Debug.DrawLine(transform.position, Neptune.position, Color.green);
        Debug.DrawRay(transform.position, moonVelocity, Color.red);
    }
}
