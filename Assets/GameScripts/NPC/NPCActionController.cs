using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPCActionController : ActionItem {

    Transform canvas;
    Text question;
    Text nameText;
    List<Transform> buttons = new List<Transform>();

    NPC npc = null;
    /// <summary>Получение ссылок на объекты диалогового окна внутри объекта НИПа</summary>
    public void Start()
    {
        canvas = transform.FindChild("Canvas");
        question = canvas.FindChild("Question").FindChild("Text").GetComponent<Text>();
        #region Всем созданным кнопкам добавляется обработчик нажатия
        int i = 0;
        var a = canvas.FindChild("NPCAnswer");
        while (a != null)
        {
            int buttonNumber = i;
            a.GetComponent<Button>().onClick.AddListener(() => checkAnswer(buttonNumber));
            Debug.Log("Добавлен обработчик для кнопки " + buttonNumber);
            buttons.Add(a);
            i++;
            a = canvas.FindChild("NPCAnswer (" + i + ")");
        }
        #endregion
        nameText = canvas.FindChild("Name").FindChild("Text").GetComponent<Text>();
        canvas.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>Пояление диалогового окна</summary>
    /// <param name="name">Имя НИПа</param>
    /// <param name="xmlHasChanged">Если xml изменялся вне игры, его надо сначала зашифровать, чтобы класс NPC мог его правильно считать</param>
    public override void triggerAction() {
        name = this.gameObject.name;
        bool xmlHasChanged = true;

        if (xmlHasChanged) {
            NPC.encode(name);
        }
            
        npc = new NPC(name);

        // TODO: canvas.FindChild("Circle").GetComponent<SpriteRender>().sprite = name + "_face.png";
        nameText.text = "";
        canvas.GetComponent<Canvas>().enabled = true;
        showEntry(npc.getEntry());
    }

    public override void exitAction() {
        canvas.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>Прячет кнопку с ответом</summary>
    /// <param name="buttonObject">Объект кнопки с ответом</param>
    void hideButton(Transform buttonObject)
    {
        buttonObject.GetComponent<Image>().enabled = false;
        buttonObject.FindChild("Text").GetComponent<Text>().text = "";
    }

    /// <summary>Показывает кнопку с ответом</summary>
    /// <param name="buttonObject">Объект кнопки с ответом</param>
    /// <param name="text">Текст на кнопке (ответ)</param>
    void showButton(Transform buttonObject, string text = "")
    {
        buttonObject.GetComponent<Image>().enabled = true;
        buttonObject.FindChild("Text").GetComponent<Text>().text = text;
    }

    /// <summary>Вывод диалоговой записи в интерфейс</summary>
    /// <param name="entry">Диалоговая запись</param>
    void showEntry(NPCEntry entry)
    {
        Debug.Log("Номер записи " + entry.number);
        question.text = entry.question;
        #region Появляются/скрываются лишние кнопки
        int answersCount;
        if (buttons.Count < entry.answers.Count)
        {
            answersCount = buttons.Count;
            Debug.Log("Слишком много вариантов ответов " + entry.answers.Count);
        }
        else
        {
            answersCount = entry.answers.Count;
            for (int i = entry.answers.Count; i < buttons.Count; hideButton(buttons[i++])) ;
        }
        #endregion

        for (int i = 0; i < answersCount; i++)
        {
            showButton(buttons[i], entry.answers[i]);
            #region TODO: разные кнопки для ответов в 1 и 2 строки
            // рассчитать длину строки entry.answers[i] в пикселях
            // установить, одну или две строки займет ответ
            // buttons[i].GetComponent<Image>().sprite = one_answer или two_answer
            // buttons[i].GetComponent<Button>().spriteState.pressedSprite = one_answer_pressed или two_answer_pressed
            #endregion
        }
        if (entry.name != "")
            nameText.text = entry.name;
    }

    /// <summary>Обработка события нажатия на кнопку ответа</summary>
    public void checkAnswer(int answerNumber)
    {
        if (npc == null) return;
        Debug.Log("Функция checkAnswer получила номер кнопки " + answerNumber);
        showEntry(npc.getEntry(answerNumber + 1));
    }
}
