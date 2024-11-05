using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StoneMovement))]
public class Stone : Destructible
{
    public enum Size
    {
        Small,
        Normal,
        Big,
        Huge
    }

    [SerializeField] private Size size;
    [SerializeField] private float spawnUpForce;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinDropChance;

    private StoneMovement movement;
    private SpriteRenderer spriteRenderer;
    //private LevelManager levelManager;
    //private ProgressPanel progressPanel;

    private void Awake()
    {
        movement = GetComponent<StoneMovement>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //levelManager = FindObjectOfType<LevelManager>();
        //progressPanel = FindObjectOfType<ProgressPanel>();

        Die.AddListener(OnStoneDestroyed);

        SetSize(size);
    }

    private void OnDestroy()
    {
        Die.RemoveListener(OnStoneDestroyed);
    }

    private void OnStoneDestroyed()
    {
        if (Random.value < coinDropChance)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }

        if (size != Size.Small)
        {
            SpawnStones();
        }

        //levelManager.StoneDestroyed(); 
        Destroy(gameObject);
        //levelManager.CheckForRemainingStones(); 
    }

    private void SpawnStones()
    {
        for (int i = 0; i < 2; i++)
        {
            Stone stone = Instantiate(this, transform.position, Quaternion.identity);
            stone.SetSize(size - 1);
            stone.maxHitPoints = Mathf.Clamp(maxHitPoints / 2, 1, maxHitPoints);
            stone.movement.AddVerticalVelocity(spawnUpForce);
            stone.movement.SetHorizontalDirection((i % 2 * 2) - 1);
            stone.SetColor(spriteRenderer.color);
            //levelManager.RegisterNewStone(); 
        }
    }

    public void SetSize(Size size)
    {
        if (size < 0)
        {
            return;
        }

        transform.localScale = GetVectorFromSize(size);
        this.size = size;
    }

    private Vector3 GetVectorFromSize(Size size)
    {
        switch (size)
        {
            case Size.Huge:
                return new Vector3(1, 1, 1);
            case Size.Big:
                return new Vector3(0.75f, 0.75f, 0.75f);
            case Size.Normal:
                return new Vector3(0.6f, 0.6f, 0.6f);
            case Size.Small:
                return new Vector3(0.4f, 0.4f, 0.4f);
            default:
                return Vector3.one;
        }
    }

    public void SetColor(Color color)
    {
        if (spriteRenderer != null)
        {
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }
}
