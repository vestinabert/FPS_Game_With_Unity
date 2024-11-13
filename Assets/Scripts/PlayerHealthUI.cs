using TMPro; 
using UnityEngine; 
using UnityEngine.UI; 

// Klasė, kuri valdo žaidėjo sveikatos atvaizdavimą ekrane
public class PlayerHealthUI : MonoBehaviour 
{ 
    // Vaizdo komponentas, rodantis sveikatos juostos užpildymą
    [SerializeField] private Image healthFill; 
    // Teksto laukas, rodantis dabartinę sveikatą skaitine verte
    [SerializeField] private TMP_Text healthText; 

    // Metodas, kuris atnaujina sveikatos juostos ir teksto reikšmes
    public void UpdateHealth(float currentHealth, float maxHealth) 
    { 
        // Nustatome sveikatos juostos užpildymo lygį pagal sveikatos santykį
        healthFill.fillAmount = currentHealth / maxHealth; 
        // Nustatome teksto lauką rodant dabartinę sveikatą kaip skaitinę reikšmę
        healthText.text = currentHealth.ToString(); 
    } 
}  
