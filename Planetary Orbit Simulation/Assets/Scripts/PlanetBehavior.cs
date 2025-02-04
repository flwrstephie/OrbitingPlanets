using UnityEngine;

public abstract class PlanetBehavior : MonoBehaviour
{
    public float Mass;
    public float GravitationalConstant = 0.1f;

    public virtual float CalculateGravitationalForce(float moonMass, float distance)
    {
        distance = Mathf.Clamp(distance, 2f, 50f);
        return GravitationalConstant * (moonMass * Mass) / (distance * distance);
    }
}
