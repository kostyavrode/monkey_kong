using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraFollower : MonoBehaviour
{
    [SerializeField] private float trackingSpeed=2f;
    [SerializeField] private float offsetZ=3f;
    [SerializeField] private float offsetY = 3f;
    [SerializeField] private float offsetX = 0.66f;
    [SerializeField] private Transform target;
    [SerializeField] private Transform bananaPlace;
    [SerializeField] private Transform startPlace;
    private bool isBananaShown;
    public void StartShow()
    {
        transform.DOMove(bananaPlace.position, 2f).SetEase(Ease.Flash).OnComplete(StopShow);
        transform.DORotateQuaternion(startPlace.rotation, 1f);
    }
    public void StopShow()
    {
        StartCoroutine(ShowBananaDelay());
    }
    public void GameCam()
    {
        transform.DOMove(startPlace.position, 1f).OnComplete(StartFollow);
        
    }
    public void StartFollow()
    {
        isBananaShown = true;
    }
    private void Update()
    {
        if (isBananaShown)
        {
            Vector3 tempPosition = new Vector3(target.position.x + offsetX, target.position.y + offsetY, target.position.z - offsetZ);
            transform.position = Vector3.Lerp(transform.position, tempPosition, trackingSpeed * Time.deltaTime);
        }
    }
    private IEnumerator ShowBananaDelay()
    {
        yield return new WaitForSeconds(2f);
        GameCam();
    }
}
