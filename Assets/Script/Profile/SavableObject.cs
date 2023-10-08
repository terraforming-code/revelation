using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SavableObject : MonoBehaviour 
// public abstract class Savable : MonoBehaviour
{
    public abstract void Save();
    public abstract void Load();
    public virtual void CreateNew() {}
}
