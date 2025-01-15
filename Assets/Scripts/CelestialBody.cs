using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CelestialBody : GravityObject
{
    public float radius;
    public float mass;
    public Vector3 initialVelocity;
    private Vector3 currentVelocity;

    public string bodyName = "Unnamed";
    public Vector3 velocity { get; private set; }
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentVelocity = initialVelocity;

    }

    public void UpdateVelocity(CelestialBody[] allBodies, float timeStep)
    {
        foreach (CelestialBody otherBody in allBodies)
        {
            if (otherBody != this)
            {
                float sqrDistance = (otherBody.rb.position - rb.position).sqrMagnitude;
                Vector3 forceDirection = (otherBody.rb.position - rb.position).normalized;
                Vector3 force = forceDirection * Universe.gravitationalConstant * mass * otherBody.mass / sqrDistance;
                Vector3 acceleration = force / mass;
                currentVelocity += acceleration * timeStep;
            }
        }
    }

    public void UpdatePosition(float timeStep)
    {
        rb.position += currentVelocity * timeStep;
    }

    public Rigidbody Rigidbody
    {
        get
        {
            return rb;
        }
    }

    public Vector3 Position
    {
        get
        {
            return rb.position;
        }
    }
}
