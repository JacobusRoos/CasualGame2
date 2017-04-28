using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SaveData
{
    public DateTime CreationTimestamp { get; set; }

    public float Ectoplasm { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }

    public Dictionary<int, SerializablePlot> Plots { get; set; }
}

[System.Serializable]
public struct SerializableVector3
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public static implicit operator Vector3(SerializableVector3 v)
    {
        return new Vector3(v.X, v.Y, v.Z);
    }

    public static implicit operator SerializableVector3(Vector3 v)
    {
        return new SerializableVector3() { X = v.x, Y = v.y, Z = v.z };
    }
}

[System.Serializable]
public struct SerializablePlot
{
    public SerializableSoul[] Souls { get; set; }
}

[System.Serializable]
public struct SerializableSoul
{
    public float EctoPerSecond { get; set; }
    public float EctoPerHarvest { get; set; }
    public float Lifespan { get; set; }
    public float TimeToRipe { get; set; }
}