# Simulador de Conservação Patrimonial
**UNDB · Tecnologias Emergentes**  
Dupla 1 — Core Gameplay & Sistema Lógico

---

## Scripts implementados

### `ProblemData.cs`
ScriptableObject que representa um problema do jogo.

| Campo | Tipo | Descrição |
|---|---|---|
| `problemName` | string | Nome do problema (ex: "Umidade") |
| `correctToolName` | string | Nome exato da ferramenta correta |
| `problemSprite` | Sprite | Imagem exibida na parede |
| `hint` | string | Texto de dica do Diário de Campo |

---

### `Tool.cs`
Componente colocado em cada ferramenta da bancada.

| Campo | Descrição |
|---|---|
| `toolName` | Nome único da ferramenta — deve bater exatamente com `correctToolName` do ProblemData |

**Métodos:**
- `Use()` — envia a tentativa ao GameManager. Conectar ao evento VR (Activated / SelectEntered)
- `OnMouseDown()` — atalho para teste no Editor sem VR

---

### `GameManager.cs`
Singleton. Controla todo o loop do jogo.

| Campo Inspector | Descrição |
|---|---|
| `problems[]` | Array com os ProblemData na ordem de aparição |
| `problemImage` | Image da parede que exibe o sprite |
| `problemLabel` | TMP com o nome do problema |
| `feedbackCorrect` | GameObject do feedback verde (✓) |
| `feedbackWrong` | GameObject do feedback vermelho (✗) |
| `resultPanel` | Painel final com resultado |
| `resultText` | TMP com texto do Chef |
| `feedbackDuration` | Tempo em segundos que o feedback fica visível (padrão: 1.2s) |

**Método público:**
- `TryAnswer(string toolName)` — chamado por `Tool.Use()`
- `GetCurrentHint()` — retorna a dica do problema atual (para o Diário de Campo)

---

## Problemas cadastrados

| Asset | `problemName` | `correctToolName` | Hint |
|---|---|---|---|
| Umidade | Umidade | `Higrometro` | Use o higrômetro para medir a umidade da parede |
| Mofo | Mofo | `Lupa` | Use a lupa para identificar colônias de mofo |
| Cupim | Cupim | `CameraDeInspecao` | Use a câmera de inspeção para detectar cupins |
| Rachadura | Rachadura | `Lanterna` | Use a lanterna para examinar a profundidade da rachadura |

> ⚠️ O campo `correctToolName` e o `toolName` das ferramentas devem ser idênticos — sem acento, sem espaço.

---

## Ferramentas mapeadas

| Objeto na cena | `toolName` |
|---|---|
| Higrômetro | `Higrometro` |
| Lupa | `Lupa` |
| Câmera de inspeção | `CameraDeInspecao` |
| Lanterna | `Lanterna` |

---

## Loop do jogo

```
START
  ↓
LoadProblem(index)
  ↓
Aguarda TryAnswer()
  ↓
Correto → hits++ → feedback verde → NextProblem()
Errado  → attempts++ → feedback vermelho → mesma rodada
  ↓
index >= problems.Length
  ↓
ShowResult() — exibe acertos, tentativas e eficiência
```

---

## O que falta / próximos passos

- [ ] Conectar objetos de UI no Inspector do GameManager
- [ ] Adicionar componente `Tool.cs` em cada ferramenta do Workbench
- [ ] Adicionar sprites dos problemas nos ProblemData
- [ ] Conectar `Tool.Use()` aos eventos VR (XR Interactable → Activated)
- [ ] Integrar `GetCurrentHint()` com o Diário de Campo (outra dupla)
- [ ] Integrar tela de resultado com o sistema do Chef (outra dupla)