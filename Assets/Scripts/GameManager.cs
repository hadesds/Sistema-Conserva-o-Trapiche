using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Coloque esse script em um GameObject vazio chamado "GameManager" na cena
public class GameManager : MonoBehaviour
{
    // ── Singleton ──────────────────────────────────────────────────────────
    public static GameManager Instance { get; private set; }

    // ── Inspector ──────────────────────────────────────────────────────────
    [Header("Problemas — arraste na ordem que quiser")]
    public ProblemData[] problems;

    [Header("UI — parede")]
    public Image problemImage;          // Image da parede que exibe o sprite
    public TextMeshProUGUI problemLabel; // (opcional) nome do problema

    [Header("UI — feedback")]
    public GameObject feedbackCorrect;  // painel/ícone verde ✓
    public GameObject feedbackWrong;    // painel/ícone vermelho ✗

    [Header("UI — resultado final")]
    public GameObject resultPanel;      // painel que aparece no fim
    public TextMeshProUGUI resultText;  // texto do Chef

    [Header("Configuração")]
    public float feedbackDuration = 1.2f; // segundos que o feedback fica visível

    // ── Estado interno ──────────────────────────────────────────────────────
    private int currentIndex = 0;
    private int hits = 0;
    private int attempts = 0;
    private bool waitingForAnswer = true;

    // ───────────────────────────────────────────────────────────────────────
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        resultPanel.SetActive(false);
        feedbackCorrect.SetActive(false);
        feedbackWrong.SetActive(false);
        LoadProblem(0);
    }

    // ── Pública — chamada por Tool.Use() ───────────────────────────────────
    public void TryAnswer(string toolName)
    {
        if (!waitingForAnswer) return;   // ignora cliques durante feedback
        if (currentIndex >= problems.Length) return;

        attempts++;
        waitingForAnswer = false;

        bool correct = (toolName == problems[currentIndex].correctToolName);

        if (correct)
        {
            hits++;
            ShowFeedback(true);
            Invoke(nameof(NextProblem), feedbackDuration);
        }
        else
        {
            ShowFeedback(false);
            Invoke(nameof(ResetFeedback), feedbackDuration);
        }
    }

    // ── Privadas ────────────────────────────────────────────────────────────
    void LoadProblem(int index)
    {
        if (index >= problems.Length)
        {
            ShowResult();
            return;
        }

        ProblemData p = problems[index];

        if (problemImage != null && p.problemSprite != null)
            problemImage.sprite = p.problemSprite;

        if (problemLabel != null)
            problemLabel.text = p.problemName;

        waitingForAnswer = true;
    }

    void NextProblem()
    {
        feedbackCorrect.SetActive(false);
        feedbackWrong.SetActive(false);
        currentIndex++;
        LoadProblem(currentIndex);
    }

    void ResetFeedback()
    {
        feedbackWrong.SetActive(false);
        waitingForAnswer = true; // jogador pode tentar de novo
    }

    void ShowFeedback(bool correct)
    {
        feedbackCorrect.SetActive(correct);
        feedbackWrong.SetActive(!correct);
    }

    void ShowResult()
    {
        resultPanel.SetActive(true);

        float efficiency = attempts > 0 ? (float)hits / attempts * 100f : 0f;

        resultText.text =
            $"Você acertou {hits} de {problems.Length} problemas.\n" +
            $"Total de tentativas: {attempts}\n" +
            $"Eficiência: {efficiency:F0}%";
    }

    // ── Utilitário público (para o Diário de Campo) ─────────────────────────
    public string GetCurrentHint()
    {
        if (currentIndex < problems.Length)
            return problems[currentIndex].hint;
        return "";
    }
}