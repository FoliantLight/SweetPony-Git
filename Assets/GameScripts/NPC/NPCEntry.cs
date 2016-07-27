using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

/// <summary>Диалоговая запись. Вопрос NPC и варианты ответа MainPerson.</summary>
public class NPCEntry
{ 
    /// <summary>Вопрос NPC</summary>
    public string question;

    /// <summary>Номер вопроса: номер предыдущего + . + номер ответа с 1</summary>
    public string number;

    /// <summary>Название анимации, которую воспроизводит НИП ожидая ответа игрока</summary>
    public string animation;

    /// <summary>Завершается ли диалог этой записью</summary>
    public bool isFinish = false;

    /// <summary>Список квестов, полученных в этой записи</summary>
    public List<Quest> quest = new List<Quest>();

    /// <summary>Список полученных наборов предметов</summary>
    public List<ItemSet> drag = new List<ItemSet>();

    /// <summary>Список потерянных наборов предметов</summary>
    public List<ItemSet> drop = new List<ItemSet>();

    /// <summary>Список возможных ответов</summary>
    public List<string> answers = new List<string>();

    /// <summary>Список изменения дружелюбности для предыдущих ответов</summary>
    public List<int> currFriendly = new List<int>();
    public List<int> prevFriendly;


    /// <summary>Имя, которым себя назвал НИП. Если пустое - значит еще не назвал</summary>
    public string name = "";

    /// <summary>Диалоговая запись - вопрос и ответы из узла XML</summary>
    /// <param name="xml"></param>
    public NPCEntry(XmlNode xml)
    {
        question = xml.Attributes.GetNamedItem("question").Value;
        if (xml.Name != "default")
            number = xml.Attributes.GetNamedItem("id").Value;
        animation = xml.Attributes.GetNamedItem("animation").Value;
        foreach (XmlNode ch in xml.SelectNodes("answ"))
        {
            currFriendly.Add(int.Parse(ch.Attributes.GetNamedItem("friendly").Value));
            answers.Add(ch.InnerText);
        }
        isFinish = (answers.Count == 0);

        drag = ItemSet.parse(xml.SelectSingleNode("drag"));
        drag = ItemSet.parse(xml.SelectSingleNode("drop"));

        if (xml.SelectSingleNode("name") != null)
            name = xml.SelectSingleNode("name").InnerText;

        quest = Quest.parse(xml);
    }   
}

