using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Savable
// public abstract class Savable : MonoBehaviour
{
    
    public abstract void Save();
    public abstract void Load();
}