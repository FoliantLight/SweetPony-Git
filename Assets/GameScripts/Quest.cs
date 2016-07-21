using System.Collections.Generic;
using System.Xml;

/// <summary>Квест</summary>
public class Quest
{
    /// <summary>Идентификатор квеста</summary>
    public int id;
    /// <summary>Награда за выполнение квеста - список наборов предметов</summary>
    List<ItemSet> revard = new List<ItemSet>();
    /// <summary>Выполнен ли квест</summary>
    bool isDone = false;

    /// <summary>Создание квеста</summary>
    /// <param name="id">Номер квеста. Идентификатор</param>
    /// <param name="revard">Награда - список наборов предметов</param>
    public Quest(int id, List<ItemSet> revard)
    {
        this.id = id;
        this.revard = new List<ItemSet>(revard);
    }

    /// <summary>Выполнение квеста</summary>
    /// <returns>Награда - список наборов предметов</returns>
    public List<ItemSet> done()
    {
        isDone = true;
        return revard;
    }

    /// <summary>Парсер списка квестов из узла Xml</summary>
    /// <param name="root">узел, содержащий список квестов</param>
    /// <returns>Список квестов</returns>
    public static List<Quest> parse(XmlNode root)
    {
        var res = new List<Quest>();
        foreach (XmlNode c in root.SelectNodes("quest"))
            res.Add(new Quest(
                int.Parse(c.Attributes.GetNamedItem("id").Value),
                ItemSet.parse(c)
                ));
        return res;
    }
}


