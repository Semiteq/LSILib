// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Table.RecipeLine
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

using System.Collections.Generic;

#nullable disable
namespace LSILib.FB.Table
{
  internal class RecipeLine
  {
    private List<TableColum> _colums;
    private List<TCell> _cells = new List<TCell>();

    public RecipeLine(List<TableColum> colums)
    {
      this._colums = colums;
      foreach (TableColum colum in this._colums)
        this._cells.Add(new TCell(colum));
    }

    public RecipeLine(List<TableColum> colums, string fileline)
    {
      this._colums = colums;
      string str1 = fileline;
      foreach (TableColum colum in this._colums)
      {
        int length = str1.IndexOf(';');
        string str2;
        if (length <= 0)
        {
          str2 = str1;
          str1 = "";
        }
        else
        {
          str2 = str1.Substring(0, length);
          str1 = length + 1 <= str1.Length ? str1.Substring(length + 1) : "";
        }
        this._cells.Add(new TCell(colum, str2));
      }
    }

    public RecipeLine(
      List<TableColum> colums,
      ushort[] int_data,
      ref int int_index,
      ushort[] float_data,
      ref int float_index,
      ushort[] bool_data,
      ref int bool_index)
    {
      this._colums = colums;
      foreach (TableColum colum in this._colums)
        this._cells.Add(new TCell(colum));
      for (int index = 0; index < this._cells.Count; ++index)
      {
        if (this._cells[index].colum.type == cell_types.rt_int || this._cells[index].colum.type == cell_types.rt_enum)
        {
          this._cells[index].servalue = (uint) int_data[int_index];
          ++int_index;
        }
        if (this._cells[index].colum.type == cell_types.rt_float)
        {
          this._cells[index].servalue = (uint) float_data[float_index * 2] + ((uint) float_data[float_index * 2 + 1] << 16);
          ++float_index;
        }
        if (this._cells[index].colum.type == cell_types.rt_bool)
        {
          this._cells[index].servalue = (uint) ((int) bool_data[bool_index / 16] >> bool_index % 16 & 1);
          ++bool_index;
        }
      }
    }

    public List<TableColum> colums => this._colums;

    public List<TCell> cells => this._cells;
  }
}
