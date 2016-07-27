using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPCActionController : ActionItem {
    Transform canvas;
    Transform text;
    Transform dd;
    Transform nameText;

    NPC npc = null;

    /// <summary>Получение ссылок на объекты внутри объекта НИПа</summary>
    public void Start()
    {
        canvas = transform.FindChild("Canvas");
        text = canvas.transform.FindChild("Text");
        dd = canvas.transform.FindChild("Dropdown");
        nameText = canvas.transform.FindChild("NameText");

        canvas.transform.position = new Vector3(0, -100, 0);
        nameText.GetComponent<Text>().text = "";
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
        canvas.transform.position = new Vector3(0, 0, 0);
        showEntry(npc.getEntry());
    }

    /// <summary>Вывод диалоговой записи в интерфейс</summary>
    /// <param name="entry">Диалоговая запись</param>
    void showEntry(NPCEntry entry)
    {
        text.GetComponent<Text>().text = entry.question;
        var ddlist = new List<Dropdown.OptionData>();
        foreach (var s in entry.answers)
            ddlist.Add(new Dropdown.OptionData(s));
        dd.GetComponent<Dropdown>().options = ddlist;
        if (entry.name != "")
            nameText.GetComponent<Text>().text = entry.name;
    }

    
    public void checkAnswer()
    {
        if (npc == null) return;
        showEntry(npc.getEntry(dd.GetComponent<Dropdown>().value + 1));
    }
}
