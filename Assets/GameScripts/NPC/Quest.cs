using System.Collections.Generic;
using System.Xml;

/// <summary>Квест</summary>
public class Quest
{
    /// <summary>Идентификатор квеста</summary>
    public int id;
    /// <summary>Награда за выполнение квеста - список наборов предметов</summary>
    public List<ItemSet> drag = new List<ItemSet>();
    /// <summary>Список наборов предметов, которые НИП заберет</summary>
    public List<ItemSet> drop = new List<ItemSet>();
    /// <summary>Выполнен ли квест</summary>
    public bool isDone = false;
    /// <summary>Сколько баллов престижа можно получить за выполнение этого квеста</summary>
    public int prestige = 0;
    /// <summary>Минимальное количество баллов престижа, которое может быть у игорока для получения этого квеста</summary>
    public int minPrestige = 0;
    /// <summary>Максимальное количество баллов престижа, которое может быть у игорока для получения этого квеста</summary>
    public int maxPrestige = 0;


    /// <summary>Создание квеста</summary>
    /// <param name="prestige">Начисление/списание (в зависимости от знака) престижа у игорка за выполнение этого квеста</param>
    /// <param name="minPrestige">Минимальное количество престижа у игрока, при котором он может взять этот квест</param>
    /// <param name="maxPrestige">Максимальное количество престижа у игрока, при котором он может взять этот квест</param>
    /// <param name="id">Номер квеста. Идентификатор</param>
    /// <param name="revard">Награда - список наборов предметов</param>
    public Quest(int id, int prestige, int minPrestige, int maxPrestige, List<ItemSet> drag, List<ItemSet> drop)
    {
        this.id = id;
        this.prestige = prestige;
        this.minPrestige = minPrestige;
        this.maxPrestige = maxPrestige;
        this.drag = new List<ItemSet>(drag);
        this.drop = new List<ItemSet>(drop);
    }

    public void done()
    {
        isDone = true;
    }

    /// <summary>Парсер списка квестов из узла Xml</summary>
    /// <param name="root">узел, содержащий список квестов</param>
    /// <returns>Список квестов</returns>
    public static List<Quest> parse(XmlNode root)
    {
        var res = new List<Quest>();
        if (root == null) return res;
        foreach (XmlNode c in root.SelectNodes("quest"))
            res.Add(new Quest(
                int.Parse(c.Attributes.GetNamedItem("id").Value),
                int.Parse(c.Attributes.GetNamedItem("prestige").Value),
                int.Parse(c.Attributes.GetNamedItem("minPrestige").Value),
                int.Parse(c.Attributes.GetNamedItem("maxPrestige").Value),
                ItemSet.parse(c.SelectSingleNode("drag")),
                ItemSet.parse(c.SelectSingleNode("drop"))
                ));
        return res;
    }

    /// <summary>Парсер списка номеров квестов из узла Xml для принимаемых квестов</summary>
    /// <param name="root">узел, содержащий список номеров квестов</param>
    /// <returns>Список номеров квестов</returns>
    public static List<int> parseIndexRecieved(XmlNode root)
    {
        var res = new List<int>();
        foreach (XmlNode c in root.SelectNodes("quest"))
            res.Add(int.Parse(c.InnerText));
        return res;
    }
}


