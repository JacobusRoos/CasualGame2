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
    public float ScytheRank { get; set; }

    public Dictionary<int, SerializablePlot> Plots { get; set; }
}

[Serializable]
public struct SerializableColor
{
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; }

    public static implicit operator Color(SerializableColor c)
    {
        return new Color(c.R, c.G, c.B, c.A);
    }

    public static implicit operator SerializableColor(Color c)
    {
        return new SerializableColor() { R = c.r, G = c.g, B = c.b, A = c.a };
    }
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

    public SerializableColor BaseColor { get; set; }
    public SerializableColor MatureColor { get; set; }
}