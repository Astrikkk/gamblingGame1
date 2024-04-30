using UnityEngine;

public class PressButtonAnimation : MonoBehaviour
{
    public Animator animator; // ��������� �� ��������� Animator
    public string triggerName = "ButtonPress"; // ����� ������ � ��������� Animator

    public void PlayAnimation()
    {
        // ����������, �� ��������� Animator �� ����� ������ ����������
        if (animator != null && !string.IsNullOrEmpty(triggerName))
        {
            // ��������� ����� � ��������� Animator
            animator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogError("Animator component or trigger name is not assigned!");
        }
    }
}
