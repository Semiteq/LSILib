// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Table.TableEnumType
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

using System.Collections.Generic;

#nullable disable
namespace LSILib.FB.Table
{
  internal class TableEnumType
  {
    private Dictionary<string, int> pairs;
    private string _Name;

    public TableEnumType(string Name)
    {
      this._Name = Name;
      this.pairs = new Dictionary<string, int>();
    }

    public string Name => this._Name;

    public void add_enum(string str, int val)
    {
      if (string.IsNullOrEmpty(str))
        return;
      if (this.pairs.ContainsKey(str))
        this.pairs[str] = val;
      else
        this.pairs.Add(str, val);
    }

    public int enum_counts => this.pairs.Count;

    public string get_name_by_ittr_num(int ittr_num)
    {
      int num = 0;
      foreach (KeyValuePair<string, int> pair in this.pairs)
      {
        if (num == ittr_num)
          return pair.Key;
        ++num;
      }
      return "";
    }

    public int get_value_by_ittr_num(int ittr_num)
    {
      int num = 0;
      foreach (KeyValuePair<string, int> pair in this.pairs)
      {
        if (num == ittr_num)
          return pair.Value;
        ++num;
      }
      return 0;
    }

    public string get_by_num(int num)
    {
      foreach (KeyValuePair<string, int> pair in this.pairs)
      {
        if (pair.Value == num)
          return pair.Key;
      }
      return "";
    }

    public int get_by_str(string str) => this.pairs.ContainsKey(str) ? this.pairs[str] : 0;

    public bool TryParse(string str, out int val)
    {
      if (this.pairs.ContainsKey(str))
      {
        val = this.pairs[str];
        return true;
      }
      val = 0;
      return false;
    }
  }
}
