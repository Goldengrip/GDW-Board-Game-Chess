using UnityEngine.Audio;
using UnityEngine;

public class AudiManager : MonoBehaviour
{
    public static AudiManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
