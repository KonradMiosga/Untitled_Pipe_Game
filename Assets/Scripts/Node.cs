using UnityEngine;

public class Node
{
    public Vector3Int cords;
    public bool isFree;
    public Connections connections;

    public Node(Vector3Int cords, bool isFree, Connections connections)
    {
        this.cords = cords;
        this.isFree = isFree;
        this.connections = connections;
    }
}
