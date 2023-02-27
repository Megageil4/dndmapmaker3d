using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Cube : ScriptableObject
{
    public int PosX;
    public int PosY;
    public int PosZ;
    public int SizeX;
    public int SizeY;
    public int SizeZ;
    public string Color;

    public Cube(int posX, int posY, int posZ, int sizeX, int sizeY, int sizeZ, string color)
    {
        PosX = posX;
        PosY = posY;
        PosZ = posZ;
        SizeX = sizeX;
        SizeY = sizeY;
        SizeZ = sizeZ;
        Color = color;
    }
}
