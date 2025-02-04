using UnityEngine;

public class EarthMoonOrbit : MonoBehaviour
{
    public Transform Earth;
    public float MoonMass = 1f;
    public float InitialOrbitSpeed = 5f;
    public float StabilizingSpeed = 0.1f;

    private Vector3 moonVelocity;
    private PlanetBehavior earthScript;

    private void Start()
    {
        if (Earth != null)
        {
            earthScript = Earth.GetComponent<PlanetBehavior>();
            Vector3 directionToEarth = (Earth.position - transform.position).normalized;
            moonVelocity = Vector3.Cross(directionToEarth, Vector3.up) * InitialOrbitSpeed;
        }
    }

    private void Update()
    {
        if (earthScript == null) return;

        Vector3 directionToEarth = Earth.position - transform.position;
        float distance = directionToEarth.magnitude;

        float forceMagnitude = earthScript.CalculateGravitationalForce(MoonMass, distance);
        Vector3 gravitationalForce = directionToEarth.normalized * forceMagnitude;

        moonVelocity += gravitationalForce * Time.deltaTime;

        Vector3 desiredVelocity = Vector3.Cross(directionToEarth.normalized, Vector3.up) * moonVelocity.magnitude;
        moonVelocity = Vector3.Lerp(moonVelocity, desiredVelocity, Time.deltaTime * StabilizingSpeed);

        moonVelocity.y = 0;
        transform.position += moonVelocity * Time.deltaTime;

        Debug.DrawLine(transform.position, Earth.position, Color.green);
        Debug.DrawRay(transform.position, moonVelocity, Color.red);
    }
}
