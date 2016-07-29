using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPCActionController : ActionItem {
    Transform canvas;
    Transform question;
    Transform nameText;
    List<Transform> answs = new List<Transform>();

    public static int MaxAnswCount = 4;

    NPC npc = null;

    /// <summary>Получение ссылок на объекты внутри объекта НИПа</summary>
    public void Start()
    {
        canvas = transform.FindChild("Canvas");
        question = canvas.transform.FindChild("Question");
        answs.Add(canvas.transform.FindChild("Button"));
        for (int i = 1; i < MaxAnswCount; i++)
            answs.Add(canvas.transform.FindChild("Button ("+i+")"));

        nameText = canvas.transform.FindChild("Name");

        canvas.transform.position = new Vector3(0, -100, 0);
    }

    /// <summary>Игрок подходит к НИПу</summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        // получение ссылки на контроллер игорока
        var mpac = other.gameObject.GetComponent<MainPersonActionController>();
        // буква Е над игороком
        mpac.isUseAroundEnter(transform.gameObject);
    }

    /// <summary>Игрок отходит от НИПа - буква Е убирается</summary>
    void OnTriggerExit2D(Collider2D other)
    {
        // получение ссылки на контроллер игрока
        var mpac = other.gameObject.GetComponent<MainPersonActionController>();
        // буква Е над игроком исчезает
        mpac.isUseAroundExit();
        // диалоговая форма исчезает
        canvas.transform.position = new Vector3(0, -100, 0);
    }


    /// <summary>Начало диалога с встреченным НИПом</summary>
    /// <param name="name">Имя нипа - из свойства встреченного объекта</param>
    public void startDialog(string name)
    {
        npc = new NPC(name);
        canvas.transform.position = new Vector3(0, 1.35F, 0);
        showEntry(npc.getEntry());
    }

    /// <summary>Вывод диалоговой записи в интерфейс</summary>
    /// <param name="entry">Диалоговая запись</param>
    void showEntry(NPCEntry entry)
    {
        question.GetComponent<Text>().text = entry.question;

        for (int i = 0; i < (entry.answers.Count < MaxAnswCount ? entry.answers.Count : MaxAnswCount); i++)
            answs[i].transform.FindChild("Text").GetComponent<Text>().text = entry.answers[i];
        if (entry.name != "")
            nameText.GetComponent<Text>().text = entry.name;
    }

    
    public void checkAnswer()
    {
        if (npc == null) return;
      //  showEntry(npc.getEntry(number));
    }
}
