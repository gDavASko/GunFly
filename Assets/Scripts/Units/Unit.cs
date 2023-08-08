using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    public virtual void Init(Vector3 pos, Quaternion rotation, Transform parent)
    {
        transform.position = pos;
        transform.rotation = rotation;
        transform.SetParent(parent);
    }

    public virtual void Dispose()
    {
        Destroy(this.gameObject);
    }
}