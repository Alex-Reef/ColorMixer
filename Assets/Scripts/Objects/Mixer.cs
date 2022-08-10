using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class Mixer : MonoBehaviour
{
    private List<GameObject> Fruits;
    public GameObject Lid, JuiceCylinder;
    private bool MixerOpened;

    public static Mixer Instance;

    private void Awake()
    {
        Instance = this;
        Fruits = new List<GameObject>();
        ClearMixer();
    }

    public void AddToMixer(GameObject fruit)
    {
        StopCoroutine("CloseMixerTimer");
        OpenMixer(true);
        FruitAnimation(fruit);
        MixerAnimation();
        Fruits.Add(fruit);
        StartCoroutine("CloseMixerTimer", 3.5f);
    }

    private void MixerAnimation()
    {
        Transform childTransform = transform.GetChild(0);
        Vector3 StartTransform = new Vector3(childTransform.rotation.x, childTransform.rotation.y, childTransform.rotation.z); ;
        Vector3 newTransform = new Vector3(childTransform.rotation.x + 0.3f, childTransform.rotation.y, childTransform.rotation.z);
        newTransform.z = 0.1f;

        childTransform.DORotate(newTransform, 0.3f).OnComplete(
            () => childTransform.DORotate(StartTransform, 0.3f)
        );
    }

    private void FruitAnimation(GameObject fruit)
    {
        var TargetPosition = transform.position;
        TargetPosition.y = 1.5f;

        fruit.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.5f); // Scale fruit;

        fruit.GetComponent<SphereCollider>().enabled = true;
        fruit.GetComponent<Rigidbody>().isKinematic = false; // enable collision detected

        // Before move to blender, but at 1.5f heighing, after downing, at 0.35f hight of bottom
        fruit.transform.DOMove(TargetPosition, 1)
            .OnComplete(() =>
                fruit.transform.DOMove(
                    new Vector3(transform.position.x, transform.position.y + 0.35f, transform.position.z), 1)
            );
    }

    IEnumerator CloseMixerTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        OpenMixer(false);
    }

    public void ClearMixer()
    {
        JuiceCylinder.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
        Fruits.Clear();
    }

    public int Mix()
    {
        if (Fruits.Count == 0)
            return 0;

        StopCoroutine("CloseMixerTimer");
        OpenMixer(false);
        JuiceAnimation();

        var color = ColorMixer.ColorMix(JuiceCylinder.GetComponent<Renderer>().material.color, Fruits.ToArray());
        color.a = 0.8f;
        JuiceCylinder.GetComponent<Renderer>().material.color = color;

        return ColorMixer.ColorEquals(color, LevelsManager.Instance.levelsSettings.Levels[LevelsManager.Instance.CurrentLevel].ResultColor);
    }

    private void JuiceAnimation()
    {
        JuiceCylinder.transform.DOMoveY(0.5f, 0.5f).OnComplete(
            () => JuiceCylinder.transform.DOMoveY(1.1f, 1.5f));
        JuiceCylinder.transform.DOScale(new Vector3(0.35f, 0.45f, 0.35f), 0.5f).OnComplete(
            () => JuiceCylinder.transform.DOScale(new Vector3(0.75f, 0.45f, 0.75f), 1.5f));
    }

    public void HideCylinder(){
        JuiceCylinder.transform.DOMoveY(0.5f, 0.1f);
    }

    public void OpenMixer(bool open)
    {
        if (open)
            Lid.transform.DOMoveY(2f, 0.5f);
        else
            Lid.transform.DOMoveY(1.45f, 0.5f);
    }
}