using UnityEngine;
using System.Collections;
using DG.Tweening;
public class Ped : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void GoToTarget(Vector3 pos, Vector3 rotation)
    {
        EnableAnimation(1);
        transform.DORotate(rotation, 1.5f).OnComplete(
            () => transform.DORotate(new Vector3(0, 0, 0), 0.5f));
        transform.DOMove(pos, 2f).OnComplete(
            () => ShowOrder());
    }

    public void ShowOrder()
    {
        EnableAnimation(0);
        UIManager.Instance.SetColor(
            "Order",
            "Liquid",
            LevelsManager.Instance.levelsSettings.Levels[LevelsManager.Instance.CurrentLevel].ResultColor
        );
        UIManager.Instance.Show("Order");

        StartCoroutine("ShowOrderTimeOut", true);
    }

    IEnumerator ShowOrderTimeOut(bool enable)
    {
        yield return new WaitForSeconds(2f);
        LevelsManager.Instance.EnableControll(enable);
    }

    public void EnableAnimation(int AnimIndex)
    {
        animator.runtimeAnimatorController = PedManager.Instance.Animators[AnimIndex];
    }
}