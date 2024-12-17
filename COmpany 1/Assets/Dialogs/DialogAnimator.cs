using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimator : MonoBehaviour
{
    public Animator animator;  // ��������� ������ �� Animator
    public DialogManager dm;

    private void Awake()
    {
        // �������������� animator (���� �� �������� ����� ���������)
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ��� animator ����������, � ������ �������� ��������
        if (animator != null)
        {
            animator.SetBool("startOpen", true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // ���������, ��� animator ����������, � ������ �������� ��������
        if (animator != null)
        {
            animator.SetBool("startOpen", false);
        }

        // ��������� ������
        if (dm != null)
        {
            dm.EndDialog();
        }
    }
}
