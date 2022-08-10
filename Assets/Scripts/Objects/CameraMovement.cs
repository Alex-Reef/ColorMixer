using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    private Vector3 StartPosition;
    void Start()
    {
        StartPosition = Camera.main.transform.position;
    }

    public void MoveToBlender()
    {
        transform.DOMove(new Vector3(-1.4f, 1.5f, 4.9f), 1);
        transform.DORotate(new Vector3(34, 0, 0), 1, RotateMode.Fast).OnComplete(() => Mixer.Instance.OpenMixer(true));
    }

    public void MoveAway()
    {
        transform.DOMove(new Vector3(StartPosition.x, StartPosition.y, StartPosition.z), 1);
        transform.DORotate(new Vector3(40, -75, 0), 1, RotateMode.Fast).OnComplete(() => Mixer.Instance.OpenMixer(false));
    }
}
