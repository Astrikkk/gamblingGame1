using UnityEngine;

public class PressButtonAnimation : MonoBehaviour
{
    public Animator animator; // Посилання на компонент Animator
    public string triggerName = "ButtonPress"; // Назва трігера у компоненті Animator

    public void PlayAnimation()
    {
        // Перевіряємо, чи компонент Animator та назва трігера встановлені
        if (animator != null && !string.IsNullOrEmpty(triggerName))
        {
            // Запускаємо трігер в компоненті Animator
            animator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogError("Animator component or trigger name is not assigned!");
        }
    }
}
