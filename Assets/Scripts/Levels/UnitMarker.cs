using UnityEngine;

public class UnitMarker : MonoBehaviour
{
    [SerializeField] private string _id = "enemy";
    public string Id => _id;
}