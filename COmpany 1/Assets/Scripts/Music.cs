using System.Collections;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip[] audioClips; // Массив для хранения звуков
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(PlaySounds());
    }

    private IEnumerator PlaySounds()
    {
        int index = 0;

        while (true)
        {
            // Проигрываем текущий звук
            audioSource.clip = audioClips[index];
            audioSource.Play();

            // Ждем, пока звук закончится
            yield return new WaitForSeconds(audioClips[index].length);

            // Ждем случайное время от 20 до 30 секунд
            float waitTime = Random.Range(15f, 20f);
            yield return new WaitForSeconds(waitTime);

            // Переходим к следующему звуку
            index = (index + 1) % audioClips.Length;
        }
    }
}

