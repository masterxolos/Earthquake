using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour
{
    private bool isDragging = false;
    private GridManager gridManager;
    private Collider2D itemCollider;
    public GameObject firstLocation;
    public TextMeshProUGUI feedbackText;
    public SpriteRenderer feedbackTextBackground;

    public Vector3 initialSize;
    public Vector3 endSize;

    [SerializeField]private string currentItemName; 
    
    void Start()
    {
        initialSize = gameObject.transform.localScale;
        gridManager = GameObject.FindObjectOfType<GridManager>();
        itemCollider = GetComponent<Collider2D>();
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Check for collisions with other draggable items
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, itemCollider.bounds.size, 0f);
        bool hasCollision = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                hasCollision = true;
                break;
            }
        }

        // Snap the item to the nearest grid cell if it's inside the grid and has no collision
        if (gridManager != null && gridManager.IsInsideGrid(transform.position) && !hasCollision)
        {
            // Change the parent to the grid
            transform.parent = gridManager.transform;

            // Snap to the grid
            transform.position = gridManager.GetNearestGridPosition(transform.position);

            switch (currentItemName)
            {
                case "water":
                    feedbackText.text = "Doğru Seçim! Depremden sonra 72 saat yetecek kadar su almalısın.";
                    break;

                case "clothes":
                    feedbackText.text = "Harika! Hava koşullarına uygun giysiler seçtin. Sıcaklık değişimlerine karşı hazırlıklı olmalısın.";
                    break;

                case "whistle":
                    feedbackText.text = "Çok Akıllıca! İnsanların dikkatini çekebilmek için bir düdük bulundurmalısın.";
                    break;

                case "pen":
                    feedbackText.text = "Güzel Seçim! Not almak veya iletişim kurmak için bir kalem her zaman işe yarar.";
                    break;

                case "shelf":
                    feedbackText.text = "Sanırım bir hata oldu. Raf ne yazık ki taşınabilir değil. Lütfen başka bir şey seç.";
                    break;

                case "calculator":
                    feedbackText.text = "İyi Düşündün! Hesaplamalar yapmak veya sayıları kontrol etmek için bir hesap makinesi önemli olabilir.";
                    break;

                case "keyboard":
                    feedbackText.text = "Tuşları neden taşıyorsun ki? Sanırım bu öğe bir hata. Lütfen başka bir şey seç.";
                    break;

                case "scissors":
                    feedbackText.text = "Keskin bir şey bulundurmak her zaman işe yarar. Tebrikler!";
                    break;

                case "radio":
                    feedbackText.text = "Mükemmel Seçim! Haberleri takip etmek ve iletişim kurmak için bir radyo çok önemli.";
                    break;

                default:
                    feedbackText.text = "Bu öğe deprem çantasına uygun değil gibi görünüyor. Lütfen başka bir şey seç.";
                    break;
            }
            gameObject.transform.localScale = endSize;
            feedbackText.gameObject.SetActive(true);
            feedbackTextBackground.gameObject.SetActive(true);
            StartCoroutine(FadeOutText());
        }
        else
        {
            // Change the parent back to the initial parent (e.g., firstLocation)
            transform.parent = firstLocation.transform.parent.transform;

            // Move to the initial position
            transform.position = firstLocation.transform.position;
            transform.localScale = initialSize;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
        }
    }
    
    
    IEnumerator FadeOutText()
    {
        feedbackText.color = new Color(feedbackText.color.r, feedbackText.color.g, feedbackText.color.b, 1);
        feedbackTextBackground.color = new Color(feedbackTextBackground.color.r, feedbackTextBackground.color.g, feedbackTextBackground.color.b, 1f);
        // Wait for 1 second
        yield return new WaitForSeconds(1.5f);

        // Use a loop to gradually decrease the text's alpha (transparency)
        float elapsedTime = 0f;
        float fadeDuration = 1f; // You can adjust the duration as needed

        Color originalColor = feedbackText.color;
        Color originalBackgroundColor = feedbackTextBackground.color;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;  

            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            feedbackText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            feedbackTextBackground.color = new Color(originalBackgroundColor.r, originalBackgroundColor.g, originalBackgroundColor.b, alpha);

            yield return null;
        }

        // Ensure the text is completely invisible
        feedbackText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        feedbackTextBackground.color = new Color(originalBackgroundColor.r, originalBackgroundColor.g, originalBackgroundColor.b, 0f);

        feedbackText.gameObject.SetActive(false);
        feedbackTextBackground.gameObject.SetActive(false);
    }
    
}