using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

/// <summary> Набор нескольких предметов одного типа </summary>
public class ItemSet
{
    /// <summary>Количство предметов данного типа</summary>
    public int count;
    /// <summary>Тип предмета</summary>
    public string name;
    /// <summary>Создание набора предметов одного типа</summary>
    /// <param name="name">Тип предмета</param>
    /// <param name="count">Количество экземпляров этого предмета</param>
    public ItemSet(string name, int count)
    {
        this.name = name;
        this.count = count;
    }

    /// <summary>Парсер списка объектов ItemSet из узла Xml</summary>
    /// <param name="root">узел, содержащий список предметов</param>
    /// <returns>Список предметов</returns>
    public static List<ItemSet> parse(XmlNode root)
    {
        var res = new List<ItemSet>();
        if (root == null) return res;
        foreach (XmlNode c in root.SelectNodes("item"))
            res.Add(new ItemSet(
                c.InnerText,
                int.Parse(c.Attributes.GetNamedItem("count").Value)
                ));
        return res;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        ItemSet p = (ItemSet)obj;
        return (name.Equals(p.name)) && (count == p.count);
    }
    public override int GetHashCode() { return name.GetHashCode() * count; }
}

