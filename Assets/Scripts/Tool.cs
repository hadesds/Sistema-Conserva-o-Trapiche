using UnityEngine;

// Coloque esse script em cada objeto de ferramenta na bancada
public class Tool : MonoBehaviour
{
    [Tooltip("Nome único da ferramenta. Deve bater com o 'correctToolName' do ProblemData.")]
    public string toolName;

    // Chamado quando o jogador clica/usa a ferramenta
    // Para VR: conecte ao evento de XR Interactable (SelectEntered ou Activated)
    // Para teste no Editor: basta chamar Use() por um botão de UI ou OnMouseDown
    public void Use()
    {
        GameManager.Instance.TryAnswer(toolName);
    }

    // Atalho para teste rápido sem VR — clique com o mouse no editor
    private void OnMouseDown()
    {
        Use();
    }
}