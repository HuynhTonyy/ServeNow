using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private CustomerManager Instance;
    private void Awake() {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
