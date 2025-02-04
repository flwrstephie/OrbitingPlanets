using UnityEngine;

public class MarsPlanet : PlanetBehavior
{
    private void Awake()
    {
        Mass = 500f; // Mars's mass (smaller than Earth)
    }
}
