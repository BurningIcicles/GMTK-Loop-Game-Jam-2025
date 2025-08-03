using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Start is called before the first frame update
    
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _audioSource.volume = PlayerPrefs.GetFloat("Volume");
    }
}
