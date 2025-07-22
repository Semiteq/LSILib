// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Value.Value
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

using FB;
using FB.VisualFB;
using InSAT.Library.Interop;
using InSAT.OPC;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

#nullable disable
namespace LSILib.FB.Value
{
  [CatID("58e065b5-10c2-4046-a8f4-f3eaaa3a4c2b")]
  [FBOptions(FBOptions.UseScanByTime | FBOptions.EnableChangeConfigInRT)]
  [DisplayName("LSI_Value")]
  [ComVisible(true)]
  [Guid("BE9BFE8E-EAA8-45ac-BE16-11799B09EBB3")]
  [VisualControls(typeof (ValueView))]
  [Serializable]
  public class Value : VisualFBBase
  {
    private const int ID_ValueActualPin = 1;
    private const int ID_ChangeEnaPin = 2;
    private const int ID_ValueCmdPout = 3;
    private const int ID_DebugPout = 4;
    private const int ID_LastOKValuePout = 5;
    public const int ID_HMIValue = 300;
    public const int ID_HMIValueStatus = 301;
    public const int ID_HMOCmdNewValue = 310;
    public const int ID_HMOCmdDoIt = 311;
    [NonSerialized]
    private double _fActualValue;
    [NonSerialized]
    private double _fLastCmd;
    [NonSerialized]
    private bool _bCmdWas;
    [NonSerialized]
    private bool _bDoOffline;

    protected override void ToRuntime()
    {
      this._fActualValue = 0.0;
      this._bCmdWas = false;
      this._bDoOffline = false;
    }

    protected override void ToDesign()
    {
    }

    protected override void UpdateData()
    {
      bool flag1 = this.GetPinQuality(1) == OpcQuality.Ok;
      uint num = 0;
      bool flag2 = false;
      if (flag1)
      {
        this._fActualValue = this.GetPinDouble(1);
        this.VisualPins.SetPinValue(300, (object) this._fActualValue);
        this.SetPinValue(5, (object) this._fActualValue);
        num = 1U;
      }
      if (this.GetPinQuality(2) == OpcQuality.Ok && this.GetPinBool(2))
      {
        num += 2U;
        flag2 = true;
      }
      this.VisualPins.SetPinValue(301, (object) num);
      if (this._bDoOffline)
      {
        this._bDoOffline = false;
        this.SetPinValue(3, (object) this._fLastCmd);
      }
      if (this.VisualPins.GetPinQuality(311) != OpcQuality.Ok || this.VisualPins.GetPinQuality(310) != OpcQuality.Ok)
        return;
      if (this.VisualPins.GetPinBool(311) && flag2)
      {
        double pinDouble = this.VisualPins.GetPinDouble(310);
        if (!this._bCmdWas || this._bCmdWas && pinDouble != this._fLastCmd)
        {
          this.SetPinValue(3, (object) pinDouble);
        }
        else
        {
          this._bDoOffline = true;
          this.SetPinValue(3, (object) pinDouble, OpcQuality.CommFailure);
        }
        this._fLastCmd = pinDouble;
        this._bCmdWas = true;
      }
      this.VisualPins.SetPinValue(311, (object) false);
    }
  }
}
