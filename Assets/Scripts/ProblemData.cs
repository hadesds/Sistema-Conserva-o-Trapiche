using UnityEngine;

// Clique com botão direito na pasta Assets → Create → Game → ProblemData
[CreateAssetMenu(fileName = "NewProblem", menuName = "Game/ProblemData")]
public class ProblemData : ScriptableObject
{
    [Header("Identificação")]
    public string problemName;         // ex: "Umidade", "Mofo", "Cupim"
    public string correctToolName;     // nome exato da ferramenta correta
    public Sprite problemSprite;       // imagem do problema na parede

    [Header("Feedback")]
    [TextArea] public string hint;     // texto do Diário de Campo
}