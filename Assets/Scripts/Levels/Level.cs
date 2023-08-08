using System.Runtime.CompilerServices;
using UnityEngine;

public class Level : MonoBehaviour, ILevel
{
    [SerializeField] private string _id = "level";
    public string Id => _id;

    public void Dispose()
    {
        Destroy(this.gameObject);
    }
}