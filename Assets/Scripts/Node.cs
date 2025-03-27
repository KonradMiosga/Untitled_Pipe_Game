using UnityEngine;

public class Node
{
    public Vector3Int cords;
    public bool isPipe;
    public Connections connections;

    public Node(Vector3Int cords, bool isPipe, Connections connections)
    {
        this.cords = cords;
        this.isPipe = isPipe;
        this.connections = connections;
    }
}
