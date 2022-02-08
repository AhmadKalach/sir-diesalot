using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public Transform currentRespawnPoint;
    public GameObject playerPrefab;
    public TextMeshProUGUI checkpointText;
    public float checkPointAnimationTime;
    public UnityEvent GameStartEvent;
    public UnityEvent playerDeadEvent;
    public UnityEvent playerWinEvent;
    public float panelStartY;

    [Header("Respawn")]
    public float waitBeforeRespawn;
    public float respawnCameraMoveTime;

    [Header("Win")]
    public TextMeshProUGUI TimeText;
    public RectTransform winPanel;

    Vector2 checkpointTextInitialPos;
    public float startTime;

    // Start is called before the first frame update
    void Start()
    {
        checkpointTextInitialPos = checkpointText.rectTransform.anchoredPosition;
        startTime = Time.time;
    }

    public void ReloadSceneAfterWait(float waitTime)
    {
        StartCoroutine(WaitThenReload(waitTime));
    }

    public void StartGame()
    {
        startTime = Time.time;
        GameStartEvent.Invoke();
        Transform toFollow = mainCamera.Follow;
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(waitBeforeRespawn);
        sequence.Append(toFollow.transform.DOLocalMove((Vector2)currentRespawnPoint.position, respawnCameraMoveTime));
        sequence.AppendCallback(() =>
        {
            GameObject newPlayer = Instantiate(playerPrefab, currentRespawnPoint.position, Quaternion.identity);
            toFollow.transform.parent = newPlayer.transform;
            newPlayer.GetComponent<PlayerState>().cameraFollowPoint = toFollow.gameObject;
        });
    }

    public void Win()
    {
        playerWinEvent.Invoke();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().win = true;
        TimeText.text = "Time: " + (Time.time - startTime).ToString("F1") + " Seconds";
        winPanel.anchoredPosition = new Vector2(0, panelStartY);
        winPanel.DOAnchorPosY(0, checkPointAnimationTime / 4);
    }

    public void NewCheckpoint()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(checkpointText.rectTransform.DOAnchorPosY(-checkpointTextInitialPos.y, checkPointAnimationTime / 4));
        sequence.AppendInterval(checkPointAnimationTime);
        sequence.Append(checkpointText.rectTransform.DOAnchorPosY(checkpointTextInitialPos.y, checkPointAnimationTime / 4));
    }

    IEnumerator WaitThenReload(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WaitThenMoveToCameraToSpawnAndRespawn()
    {
        playerDeadEvent.Invoke();
        Transform toFollow = mainCamera.Follow;
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(waitBeforeRespawn);
        sequence.Append(toFollow.transform.DOLocalMove((Vector2)currentRespawnPoint.position, respawnCameraMoveTime));
        sequence.AppendCallback(() =>
        {
            GameObject newPlayer = Instantiate(playerPrefab, currentRespawnPoint.position, Quaternion.identity);
            toFollow.transform.parent = newPlayer.transform;
            newPlayer.GetComponent<PlayerState>().cameraFollowPoint = toFollow.gameObject;
        });
    }
}
