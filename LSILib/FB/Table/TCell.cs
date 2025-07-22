// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Table.TCell
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

using System;

#nullable disable
namespace LSILib.FB.Table
{
  internal class TCell
  {
    private TableColum _colum;
    private bool bval;
    private double fval;
    private int ival;

    public TCell(TableColum colum, string value)
    {
      this._colum = colum;
      this.SetNewValue(value);
    }

    public TCell(TableColum colum, byte[] buf, ref int offset)
    {
      this._colum = colum;
      if (this._colum.type == cell_types.rt_bool)
        this.bval = BitConverter.ToUInt32(buf, offset) != 0U;
      if (this._colum.type == cell_types.rt_float)
        this.fval = (double) BitConverter.ToSingle(buf, offset);
      if (this._colum.type == cell_types.rt_int || this._colum.type == cell_types.rt_enum)
        this.ival = BitConverter.ToInt32(buf, offset);
      offset += 4;
    }

    public TCell(TableColum colum)
    {
      this._colum = colum;
      if (this._colum.type != cell_types.rt_enum)
        return;
      this.ival = this._colum.EnumType.get_value_by_ittr_num(0);
    }

    public uint servalue
    {
      get
      {
        return this._colum.type == cell_types.rt_bool ? (!this.bval ? 0U : 1U) : (this._colum.type == cell_types.rt_int || this._colum.type == cell_types.rt_enum ? (uint) this.ival : BitConverter.ToUInt32(BitConverter.GetBytes((float) this.fval), 0));
      }
      set
      {
        if (this._colum.type == cell_types.rt_bool)
          this.bval = value != 0U;
        else if (this._colum.type == cell_types.rt_int || this._colum.type == cell_types.rt_enum)
          this.ival = (int) value;
        else
          this.fval = (double) BitConverter.ToSingle(BitConverter.GetBytes(value), 0);
      }
    }

    public string fieldval
    {
      get
      {
        if (this._colum.type == cell_types.rt_bool)
          return !this.bval ? "Нет" : "Да";
        if (this._colum.type == cell_types.rt_int)
          return this.ival.ToString();
        return this._colum.type == cell_types.rt_enum ? this._colum.EnumType.get_by_num(this.ival) : this.fval.ToString("G4");
      }
    }

    public TableColum colum => this._colum;

    public override string ToString() => this.fieldval;

    public int val_int
    {
      get
      {
        return this.colum.type == cell_types.rt_bool ? (this.bval ? 1 : 0) : (this.colum.type == cell_types.rt_float ? (int) this.fval : this.ival);
      }
    }

    public bool val_bool
    {
      get
      {
        if (this.colum.type == cell_types.rt_int || this.colum.type == cell_types.rt_enum)
          return this.ival != 0;
        return this.colum.type == cell_types.rt_float ? this.fval != 0.0 : this.bval;
      }
    }

    public double val_float
    {
      get
      {
        return this.colum.type == cell_types.rt_bool ? (this.bval ? 1.0 : 0.0) : (this.colum.type == cell_types.rt_int || this.colum.type == cell_types.rt_enum ? (double) this.ival : this.fval);
      }
    }

    public void SetNewValue(string value)
    {
      if (this._colum.type == cell_types.rt_bool)
      {
        if (value.ToUpper() == "TRUE" || value.ToUpper() == "ДА" || value.ToUpper() == "YES" || value.ToUpper() == "ON" || value.ToUpper() == "1")
        {
          this.bval = true;
        }
        else
        {
          if (!(value.ToUpper() == "FALSE") && !(value.ToUpper() == "НЕТ") && !(value.ToUpper() == "NO") && !(value.ToUpper() == "OFF") && !(value.ToUpper() == "0"))
            throw new Exception("wrong value(booltype): \"" + value + "\"");
          this.bval = false;
        }
      }
      else if (this._colum.type == cell_types.rt_float)
      {
        if (!double.TryParse(value, out this.fval))
          throw new Exception("wrong value(floattype): \"" + value + "\"");
      }
      else if (this._colum.type == cell_types.rt_int)
      {
        if (!int.TryParse(value, out this.ival))
          throw new Exception("wrong value(floattype): \"" + value + "\"");
      }
      else
      {
        if (this._colum.type != cell_types.rt_enum)
          throw new Exception("unknowen cell type");
        if (!this._colum.EnumType.TryParse(value, out this.ival))
          throw new Exception("wrong value(enumtype " + this._colum.EnumType.Name + "): \"" + value + "\"");
      }
    }
  }
}
