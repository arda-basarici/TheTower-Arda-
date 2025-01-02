using UnityEngine;

public class Identifier : MonoBehaviour
{
    [SerializeField] private string id;
    public string ID => id;

    protected void Awake()
    {
        if (string.IsNullOrEmpty(id))
        {
            id = System.Guid.NewGuid().ToString();
        }
    }
}
