using System;
using System.Collections.Generic;
using System.Xml;

/// <summary>Инвентарь игрока. Предметы разного типа в разном количестве</summary>
public class Inventory
{
    List<ItemSet> pack = new List<ItemSet>();
    /// <summary>Создание инвентаря</summary>
    public Inventory() { }

    /// <summary>Загрузка предметов инвентаря из XML</summary>
    /// <param name="xml">Узел, содержащий инвентарьL</param>
    public Inventory(XmlNode xml)
    {
        // TODO: XML parser
    }

    /// <summary>Cохранение списка предметов инвентаря в XML</summary>
    /// <param name="path">Путь к файлу XML</param>
    public void saveToXML(string path)
    {
        // TODO: serializer
    }

    /// <summary> Добавление нескольких предметов одного типа</summary>
    /// <param name="set">Набор предметов одного типа</param>
    public void add(ItemSet set)
    {
        ItemSet a = null;
        a = pack.Find(x => (x.Equals(set)));
        if (a == null)
            pack.Add(set);
        else
            a.count += set.count;
    }

    /// <summary> Удаление нескольких предметов одного типа</summary>
    /// <param name="set">Набор предметов одного типа</param>
    /// <returns>Если требуется удалить предметы, которых не в инвентаре, то возвращает false и ничего не удаляет</returns>
    public bool remove(ItemSet set)
    {
        ItemSet a = null;
        a = pack.Find(x => (x.Equals(set)));
        if (a == null)
            return false;
        if (a.count < set.count)
            return false;
        a.count -= set.count;
        return true;
    }


}
