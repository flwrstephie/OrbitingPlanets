using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Planet : MonoBehaviour
{
    public float Mass;
    public float GravitationalConstant = 0.1f;

    public virtual float CalculateGravitationalForce(float moonMass, float distance)
    {
        distance = Mathf.Clamp(distance, 2f, 50f);
        return GravitationalConstant * (moonMass * Mass) / (distance * distance);
    }
}

public class Earth : Planet
{
    private void Awake()
    {
        Mass = 1000f;
    }
}

public class MoonOrbit : MonoBehaviour
{
    public Transform Planet;
    public float MoonMass = 1f;
    public float InitialOrbitSpeed = 5f;
    public float StabilizingSpeed = 0.1f;

    private Vector3 moonVelocity;
    private Planet planetScript;

    private void Start()
    {
        if (Planet != null)
        {
            planetScript = Planet.GetComponent<Planet>();
            Vector3 directionToPlanet = (Planet.position - transform.position).normalized;
            moonVelocity = Vector3.Cross(directionToPlanet, Vector3.up) * InitialOrbitSpeed;
        }
    }

    private void Update()
    {
        if (planetScript == null) return;

        Vector3 directionToPlanet = Planet.position - transform.position;
        float distance = directionToPlanet.magnitude;

        float forceMagnitude = planetScript.CalculateGravitationalForce(MoonMass, distance);
        Vector3 gravitationalForce = directionToPlanet.normalized * forceMagnitude;

        moonVelocity += gravitationalForce * Time.deltaTime;

        Vector3 desiredVelocity = Vector3.Cross(directionToPlanet.normalized, Vector3.up) * moonVelocity.magnitude;
        moonVelocity = Vector3.Lerp(moonVelocity, desiredVelocity, Time.deltaTime * StabilizingSpeed);

        moonVelocity.y = 0;
        transform.position += moonVelocity * Time.deltaTime;

        Debug.DrawLine(transform.position, Planet.position, Color.green);
        Debug.DrawRay(transform.position, moonVelocity, Color.red);
    }
}
