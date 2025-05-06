//using Unity.VisualScripting;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] int sizeX = 9;
    [SerializeField] int sizeY = 9;
    [SerializeField] int sizeZ = 9;

    public void setX(int value)
    {
        sizeX = value;
    }

    public void setY(int value)
    {
        sizeY = value;
    }

    public void setZ(int value)
    {
        sizeZ = value;
    }

    public int getX()
    {
        return sizeX;
    }

    public int getY()
    {
        return sizeY;
    }

    public int getZ()
    {
        return sizeZ;
    }
}
