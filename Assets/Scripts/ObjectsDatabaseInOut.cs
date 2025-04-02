using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDatabaseInOut : ScriptableObject
{
    public List<ObjectsDataInOut> objectsData;
}

[Serializable]
public class ObjectsDataInOut
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    public int Xpos { get; private set; }
    [field: SerializeField]
    public int Xneg { get; private set; }
    [field: SerializeField]
    public int Ypos { get; private set; }
    [field: SerializeField]
    public int Yneg { get; private set; }
    [field: SerializeField]
    public int Zpos { get; private set; }
    [field: SerializeField]
    public int Zneg { get; private set; }
}