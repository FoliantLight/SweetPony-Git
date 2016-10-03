using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.IO;


public class NPC
{
    /// <summary>Путь для папки с xml.</summary>
    static public string Path = "Assets/NPCs/";

    /// <summary>Имя нипа. Определяет xml с диалогом и другие файлы для нипа, если понадобится</summary>
    public string name;
    /// <summary>Активные - дают квесты и учавствуют в диалогах. Пассивные - не дают заданий, не общаются.</summary>
    bool isActive = true;
    /// <summary>Начальная позиция нипа. Его дом</summary>
    public Vector3 startPosition;
    /// <summary>Начальная позиция нипа случайна.Для нипов-путешественников, например.</summary>
    bool isRandomStartPosition = false;
    /// <summary>Ночные нипы не возвращаются домой для сна</summary>
    bool isNight = true;
    /// <summary>Диалог с НИПом</summary>
    NPCDialog dialog;
    /// <summary>Номера квестов, которые НИП может принять</summary>
    public List<int> recived_quest = new List<int>();
    /// <summary>Уровень дружелюбности</summary>
    public int friendly = 0;
    /// <summary>Имя, которым НИП представился игроку</summary>
    public string shownName = "";
        


    /// <summary></summary>
    /// <param name="name">Имя НИПа и xml файла с его параметрами</param>
    public NPC(string name)
    {
        this.name = name;

        StreamReader r = new StreamReader(Path + name + ".exml");
        var doc = new XmlDocument();
        doc.LoadXml(Utils.AES_decrypt(r.ReadToEnd()));
        r.Close();

        var root = doc.DocumentElement;

        startPosition = new Vector3(
            float.Parse(root.GetAttribute("x")),
            float.Parse(root.GetAttribute("y")),
            float.Parse(root.GetAttribute("z")));

        recived_quest = Quest.parseIndexRecieved(
            root.GetElementsByTagName("recieved_quests").Item(0));

        dialog = new NPCDialog(
            root.GetElementsByTagName("dialog").Item(0));
    }

    /// <summary>Получение следующего вопроса</summary>
    /// <param name="answerIndex">Номер ответа на предыдущий вопрос. Счет с 1. Получение первого вопроса - без параметра </param>
    /// <returns>Диалоговая запись - вопрос, ответы, выдаваемые квесты и инвентарь</returns>
    public NPCEntry getEntry(int answerIndex = 0)
    {
        var entry = dialog.getEntry(answerIndex);
        if (answerIndex != 0)
            friendly += entry.prevFriendly[answerIndex - 1];
        shownName = entry.name;
        return entry;
    }

    /// <summary>Сохранение класса в файле XML</summary>
    public void save()
    {
        // TODO: создание XML

        // Utils.AES_encrypt(xml)
    }


    /// <summary>Шифровка XML файла с нипом</summary>
    /// <param name="name">имя нипа</param>
    public static void encode(string name)
    {
        var r = new StreamReader(NPC.Path + name + ".xml");
        var w = new StreamWriter(NPC.Path + name + ".exml");
        w.Write(Utils.AES_encrypt(r.ReadToEnd()));
        r.Close();
        w.Close();
    }




}

