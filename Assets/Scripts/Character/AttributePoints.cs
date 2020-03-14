using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttributePoints
{
    public int Fire;

    public int Water;

    public int Air;

    public int Earth;

    public int Light;

    public int Health;

    public void Initialize(int fire, int water, int air, int earth, int light, int health)
    {
        Fire = fire;
        Water = water;
        Air = air;
        Earth = earth;
        Light = light;
        Health = health;
    }
}

