/// <summary>
/// This script belongs to cowsins™ as a part of the cowsins´ FPS Engine. All rights reserved. 
/// </summary>
using UnityEngine;
namespace cowsins {
public class DestroyMe : MonoBehaviour, IPooledObject
{
    public float timeToDestroy;

    public void OnObjectSpawn()
    {
        Invoke(nameof(DestroyMeObj), timeToDestroy);
    }

    // Update is called once per frame
    void DestroyMeObj()
    {
        this.gameObject.SetActive(false);
    }
}
}