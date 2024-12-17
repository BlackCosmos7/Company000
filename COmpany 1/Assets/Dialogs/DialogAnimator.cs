using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimator : MonoBehaviour
{
    public Animator animator;  // ƒобавл€ем ссылку на Animator
    public DialogManager dm;

    private void Awake()
    {
        // »нициализируем animator (если не присвоен через инспектор)
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // ѕровер€ем, что animator существует, и мен€ем параметр анимации
        if (animator != null)
        {
            animator.SetBool("startOpen", true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // ѕровер€ем, что animator существует, и мен€ем параметр анимации
        if (animator != null)
        {
            animator.SetBool("startOpen", false);
        }

        // «авершаем диалог
        if (dm != null)
        {
            dm.EndDialog();
        }
    }
}
