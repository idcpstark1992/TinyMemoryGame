using UnityEngine;

public class AudioItem : MonoBehaviour
{
    [SerializeField] private AudioSource ThisSource;
    private void Start()
    {
        ThisSource = GetComponent<AudioSource>();
    }
    public void ShotSounOnTape()
    {
        ThisSource.Play();
    }
}