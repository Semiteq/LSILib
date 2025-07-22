// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Table.TableColum
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

#nullable disable
namespace LSILib.FB.Table
{
  internal class TableColum
  {
    private string _Name;
    private cell_types _type;
    private TableEnumType _EnumType;
    private int _grid_index;

    public TableColum(string Name, cell_types type)
    {
      this._Name = Name;
      this._type = type;
    }

    public TableColum(string Name, TableEnumType enum_type)
    {
      this._Name = Name;
      this._type = cell_types.rt_enum;
      this._EnumType = enum_type;
    }

    public string Name => this._Name;

    public cell_types type => this._type;

    public TableEnumType EnumType => this._EnumType;

    public int grid_index
    {
      get => this._grid_index;
      set => this._grid_index = value;
    }
  }
}
