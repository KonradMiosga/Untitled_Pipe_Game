using UnityEngine;

public class Node
{
    public Vector3Int cords;
    public bool isFree;
    public Connections connections;

    public GameObject gameObject;

    public Node(Vector3Int cords, bool isFree, Connections connections, GameObject gameObject = null)
    {
        this.cords = cords;
        this.isFree = isFree;
        this.connections = connections;
        this.gameObject = gameObject;
    }

        public override string ToString()
    {
        return $"[X+: {connections.Xpos}, X-: {connections.Xneg}, Y+: {connections.Ypos}, Y-: {connections.Yneg}, Z+: {connections.Zpos}, Z-: {connections.Zneg}]";
    }
}
