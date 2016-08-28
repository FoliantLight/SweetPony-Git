using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

/// <summary> Набор нескольких предметов одного типа </summary>
[System.Serializable]
public class ItemSet {
    /// <summary>Количство предметов данного типа</summary>
    [SerializeField]
    private int m_count;
    /// <summary>Тип предмета</summary>
    [SerializeField]
    private string m_name;

    public int count {
        get { return m_count; }
        set { m_count = value; }
    }

    public string name {
        get { return m_name; }
        set { m_name = value; }
    }

    /// <summary>Создание набора предметов одного типа</summary>
    /// <param name="name">Тип предмета</param>
    /// <param name="count">Количество экземпляров этого предмета</param>
    public ItemSet(string name, int count) {
        m_name = name;
        m_count = count;
    }

    /// <summary>Парсер списка объектов ItemSet из узла Xml</summary>
    /// <param name="root">узел, содержащий список предметов</param>
    /// <returns>Список предметов</returns>
    public static List<ItemSet> parse(XmlNode root) {
        var res = new List<ItemSet>();
        if (root == null) return res;
        foreach (XmlNode c in root.SelectNodes("item"))
            res.Add(new ItemSet(
                c.InnerText,
                int.Parse(c.Attributes.GetNamedItem("count").Value)
                ));
        return res;
    }

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType())
            return false;
        ItemSet p = (ItemSet)obj;
        return (m_name.Equals(p.m_name)) && (m_count == p.m_count);
    }

    public override int GetHashCode() {
        return m_name.GetHashCode() * m_count;
    }
}

