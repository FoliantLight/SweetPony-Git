﻿using System;
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

    /// <summary>Список наборов предметов, полученных в этой записи</summary>
    public List<ItemSet> items = new List<ItemSet>();

    /// <summary>Список возможных ответов</summary>
    public List<string> answers = new List<string>();

    /// <summary>Список изменения дружелюбности для разных ответов</summary>
    public List<int> friendly = new List<int>();

    /// <summary>Диалоговая запись - вопрос и ответы из узла XML</summary>
    /// <param name="xml"></param>
    public NPCEntry(XmlNode xml)
    {
        question = xml.Attributes.GetNamedItem("question").Value;
        number = xml.Attributes.GetNamedItem("id").Value;
        animation = xml.Attributes.GetNamedItem("animation").Value;
        foreach (XmlNode ch in xml.SelectNodes("answ"))
        {
            friendly.Add(int.Parse(ch.Attributes.GetNamedItem("friendly").Value));
            answers.Add(ch.InnerText);
        }
        isFinish = (answers.Count == 0);
        items = ItemSet.parse(xml);
        quest = Quest.parse(xml);
    }   
}

