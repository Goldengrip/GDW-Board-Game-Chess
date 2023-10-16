using UnityEngine.Audio;
using UnityEngine;

public class AudiManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
