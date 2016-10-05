using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

/// <summary>Диалоговая запись. Вопрос NPC и варианты ответа MainPerson.</summary>
public class NPCEntry
{ 
    /// <summary>Вопрос NPC</summary>
    public string question = "";

    /// <summary>Номер вопроса: номер предыдущего + . + номер ответа с 1</summary>
    public string number;

    /// <summary>Название анимации, которую воспроизводит НИП ожидая ответа игрока</summary>
    public string animation = "";

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

	/// <summary>Если происходит покупка игроком чего нибудь</summary>
	public int money = 0;

    /// <summary>Имя, которым себя назвал НИП. Если пустое - значит еще не назвал</summary>
    public string name = "";

	/// <summary>Если игрок говорит сам с собой</summary>
	public bool isMonolog = false;

    /// <summary>Парсинг узла диалоговой записи - вопрос и ответы из узла XML</summary>
    /// <param name="xml"></param>
    public NPCEntry(XmlNode xml)
    {
		try{
        	question = xml.Attributes.GetNamedItem("question").Value;
		} catch (Exception e) {}

        if (xml.Name != "default")
            number = xml.Attributes.GetNamedItem("id").Value;
		
		try{
        	animation = xml.Attributes.GetNamedItem("animation").Value;
		} catch (Exception e) {}


		try{
			money = int.Parse(xml.Attributes.GetNamedItem("money").Value);
		} catch (Exception e) {}

		try{
			isMonolog = xml.Attributes.GetNamedItem("monolog").Value == "true";
		} catch (Exception e) {}
				

        foreach (XmlNode ch in xml.SelectNodes("answ"))
            answers.Add(ch.InnerText);
		
        isFinish = (answers.Count == 0);

        drag = ItemSet.parse(xml.SelectSingleNode("drag"));
        drop = ItemSet.parse(xml.SelectSingleNode("drop"));

        if (xml.SelectSingleNode("name") != null)
            name = xml.SelectSingleNode("name").InnerText;

        quest = Quest.parse(xml);
    }   
}

