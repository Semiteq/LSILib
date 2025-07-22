// Decompiled with JetBrains decompiler
// Type: LSILib.FB.ValueInt.ValueInt
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
namespace LSILib.FB.ValueInt
{
  [Guid("457F57E4-C42C-4e7c-94B5-C39A3A8C7CC3")]
  [ComVisible(true)]
  [DisplayName("LSI_ValueInt")]
  [CatID("58e065b5-10c2-4046-a8f4-f3eaaa3a4c2b")]
  [FBOptions(FBOptions.UseScanByTime | FBOptions.EnableChangeConfigInRT)]
  [VisualControls(typeof (ValueIntView))]
  [Serializable]
  public class ValueInt : VisualFBBase
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
    private int _iActualValue;
    [NonSerialized]
    private int _iLastCmd;
    [NonSerialized]
    private bool _bCmdWas;
    [NonSerialized]
    private bool _bDoOffline;

    protected override void ToRuntime()
    {
      this._iActualValue = 0;
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
        this._iActualValue = this.GetPinInt(1);
        this.VisualPins.SetPinValue(300, (object) this._iActualValue);
        this.SetPinValue(5, (object) this._iActualValue);
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
        this.SetPinValue(3, (object) this._iLastCmd);
      }
      if (this.VisualPins.GetPinQuality(311) != OpcQuality.Ok || this.VisualPins.GetPinQuality(310) != OpcQuality.Ok)
        return;
      if (this.VisualPins.GetPinBool(311) && flag2)
      {
        int pinInt = this.VisualPins.GetPinInt(310);
        if (!this._bCmdWas || this._bCmdWas && pinInt != this._iLastCmd)
        {
          this.SetPinValue(3, (object) pinInt);
        }
        else
        {
          this._bDoOffline = true;
          this.SetPinValue(3, (object) pinInt, OpcQuality.CommFailure);
        }
        this._iLastCmd = pinInt;
        this._bCmdWas = true;
      }
      this.VisualPins.SetPinValue(311, (object) false);
    }
  }
}
