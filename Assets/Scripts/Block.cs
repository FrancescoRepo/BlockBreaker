using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    /*
     * Config Params
     */
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private int maxHits;
    [SerializeField] private Sprite[] hitSprites;

    /*
     * Cached Reference
     */
    private Level _level;
    private GameSession _gameSession;
    private int _timesHit = 0;


    // Start is called before the first frame update
    void Start()
    {
        _level = FindObjectOfType<Level>();

        if (tag.Equals("Breakable"))
            _level.CountBreakableBlocks();

        _gameSession = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag.Equals("Breakable"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        _timesHit++;
        if (_timesHit == maxHits)
        {
            DestroyBlock();
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[_timesHit - 1];
        }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);

        Destroy(gameObject);

        _level.BlockDestroyed();

        TriggerSparklesVFX();

        _gameSession.AddToScore();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);

    }
}
