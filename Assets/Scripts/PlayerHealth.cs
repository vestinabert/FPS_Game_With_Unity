using UnityEngine; 

// Klasė, kuri valdo žaidėjo sveikatą
public class PlayerHealth : MonoBehaviour 
{ 
    // Maksimali sveikata, kurią galima nustatyti inspektoriuje
    [SerializeField, Min(0)] private float maxHealth; 
    // Pradinė sveikata, kurią galima nustatyti inspektoriuje
    [SerializeField, Min(0)] private float initialHealth; 
    // Nuoroda į UI elementą, kuris atvaizduoja sveikatą
    [SerializeField] private PlayerHealthUI healthUI; 

    // Kintamasis, skirtas dabartinės sveikatos saugojimui
    private float currentHealth; 

    // Metodas, kuris vykdomas žaidimo pradžioje
    private void Start() 
    { 
        // Nustatome dabartinę sveikatą kaip pradinę sveikatą
        currentHealth = initialHealth; 
        // Atnaujiname sveikatos UI su dabartine ir maksimalia sveikata
        healthUI.UpdateHealth(currentHealth, maxHealth); 
    } 

    // Metodas, kuris sumažina sveikatą, kai žaidėjas gauna žalą
    public void Hit(float damage) 
    { 
        // Jei žala yra lygi arba mažesnė už nulį, nieko nedarome
        if (damage <= 0) 
        { 
            return; 
        } 

        // Sumažiname dabartinę sveikatą pagal žalos dydį
        currentHealth -= damage; 

        // Jei sveikata nukrenta iki nulio arba žemiau
        if (currentHealth <= 0) 
        { 
            // Žaidėjas miršta. Iškviečiame mirties ekraną... 
            // Bus baigta kitose pamokose
        } 

        // Atnaujiname sveikatos UI su dabartine ir maksimalia sveikata
        healthUI.UpdateHealth(currentHealth, maxHealth); 
    } 

    // Metodas, kuris prideda sveikatos žaidėjui
    public void AddHealth(float value) 
    { 
        // Jei vertė yra lygi arba mažesnė už nulį, nieko nedarome
        if (value <= 0) 
        { 
            return; 
        } 

        // Pridedame sveikatos dabartinei sveikatai
        currentHealth += value; 

        // Jei dabartinė sveikata viršija maksimalų sveikatos lygį
        if (currentHealth > maxHealth) 
        { 
            // Nustatome dabartinę sveikatą kaip maksimalią sveikatą
            currentHealth = maxHealth; 
        } 

        // Atnaujiname sveikatos UI su dabartine ir maksimalia sveikata
        healthUI.UpdateHealth(currentHealth, maxHealth); 
    } 
} 
