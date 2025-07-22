// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Table.Table
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
namespace LSILib.FB.Table
{
  [CatID("58e065b5-10c2-4046-a8f4-f3eaaa3a4c2b")]
  [Guid("7C5F89E3-0465-47c7-B9F6-16BC87C33DD7")]
  [FBOptions(FBOptions.UseScanByTime | FBOptions.EnableChangeConfigInRT)]
  [VisualControls(typeof (TableControl))]
  [DisplayName("LSI_Table")]
  [ComVisible(true)]
  [Serializable]
  public class Table : VisualFBBase
  {
    private const int ID_ActualLine = 1;
    private const int ID_EnaLoad = 2;
    public const int ID_HMI_CommProtocol = 1001;
    public const int ID_HMI_AddrArea = 1002;
    public const int ID_HMI_FloatBaseAddr = 1003;
    public const int ID_HMI_FloatAreaSize = 1004;
    public const int ID_HMI_IntBaseAddr = 1005;
    public const int ID_HMI_IntAreaSize = 1006;
    public const int ID_HMI_BoolBaseAddr = 1007;
    public const int ID_HMI_BoolAreaSize = 1008;
    public const int ID_HMI_ControlBaseAddr = 1009;
    public const int ID_HMI_IP1 = 1010;
    public const int ID_HMI_IP2 = 1011;
    public const int ID_HMI_IP3 = 1012;
    public const int ID_HMI_IP4 = 1013;
    public const int ID_HMI_Port = 1014;
    public const int ID_HMI_Timeout = 1015;
    public const int ID_HMI_ActualLine = 1016;
    public const int ID_HMI_Status = 1017;
    private LSILib.FB.Table.Table.ControllerProtokol _enumProtocol;
    private LSILib.FB.Table.Table.SLMP_area _enumSLMP_area = LSILib.FB.Table.Table.SLMP_area.R;
    private uint _uFloatBaseAddr;
    private uint _uFloatAreaSize = 100;
    private uint _uIntBaseAddr = 100;
    private uint _uIntAreaSize = 100;
    private uint _uBoolBaseAddr = 200;
    private uint _uBoolAreaSize = 100;
    private uint _uControlBaseAddr = 200;
    private uint _conntrollerIP1 = 192;
    private uint _conntrollerIP2 = 168;
    private uint _conntrollerIP3;
    private uint _conntrollerIP4 = 1;
    private uint _conntrollerTCPPort = 502;
    private uint _timeout = 1000;

    [DisplayName(" 1. Протокол обмена передачи данных в контроллер")]
    [Description("Определяет по какому протоколу передаются данные в контроллер")]
    public LSILib.FB.Table.Table.ControllerProtokol enumProtocol
    {
      get => this._enumProtocol;
      set => this._enumProtocol = value;
    }

    [DisplayName(" 2. Пространство хранения данных при использовании SLMP")]
    [Description("Определяет в какой области (D или R) помещаются данные таблицы")]
    public LSILib.FB.Table.Table.SLMP_area enumSLMP_area
    {
      get => this._enumSLMP_area;
      set => this._enumSLMP_area = value;
    }

    [Description("Определяет начальный адрес, куда помещаются данные типа 'вещественный'")]
    [DisplayName(" 3.  Базовый адрес хранения данных типа Real (Float)")]
    public uint uFloatBaseAddr
    {
      get => this._uFloatBaseAddr;
      set => this._uFloatBaseAddr = value;
    }

    [Description("Определяет размер области для данных типа 'вещественный'. в 16-тибитных словах (2 слова на переменную). Если Используется например область с адресами 100..199, то это 100 слов или 50 переменных типа float. Укажите в это параметре 100.")]
    [DisplayName(" 4.  Размер области хранения данных типа Real (Float)")]
    public uint uFloatAreaSize
    {
      get => this._uFloatAreaSize;
      set => this._uFloatAreaSize = value;
    }

    [Description("Определяет начальный адрес, куда помещаются данные типа 'целый 16 бит'")]
    [DisplayName(" 5.  Базовый адрес хранения данных типа Int")]
    public uint uIntBaseAddr
    {
      get => this._uIntBaseAddr;
      set => this._uIntBaseAddr = value;
    }

    [DisplayName(" 6.  Размер области хранения данных типа Int")]
    [Description("Определяет размер области для данных типа 'целый 16 бит'")]
    public uint uIntAreaSize
    {
      get => this._uIntAreaSize;
      set => this._uIntAreaSize = value;
    }

    [DisplayName(" 7.  Базовый адрес хранения данных типа Boolean")]
    [Description("Определяет начальный адрес, куда помещаются данные типа 'логический'. Упаковываются в 16ти битные слова.")]
    public uint uBoolBaseAddr
    {
      get => this._uBoolBaseAddr;
      set => this._uBoolBaseAddr = value;
    }

    [Description("Определяет размер области для данных типа 'логический'. Определяется в 16-ти битных словах")]
    [DisplayName(" 8.  Размер области хранения данных типа Boolean")]
    public uint uBoolAreaSize
    {
      get => this._uBoolAreaSize;
      set => this._uBoolAreaSize = value;
    }

    [DisplayName(" 9.  Базовый адрес контрольной области")]
    [Description("Определяет начальный адрес, где располагается зона контрольных данных (3 слова)")]
    public uint uControlBaseAddr
    {
      get => this._uControlBaseAddr;
      set => this._uControlBaseAddr = value;
    }

    [Description("IP адрес контроллера байт 1")]
    [DisplayName("10.  IP адрес контроллера байт 1")]
    public uint conntrollerIP1
    {
      get => this._conntrollerIP1;
      set => this._conntrollerIP1 = value;
    }

    [DisplayName("11.  IP адрес контроллера байт 2")]
    [Description("IP адрес контроллера байт 2")]
    public uint conntrollerIP2
    {
      get => this._conntrollerIP2;
      set => this._conntrollerIP2 = value;
    }

    [Description("IP адрес контроллера байт 3")]
    [DisplayName("12.  IP адрес контроллера байт 3")]
    public uint conntrollerIP3
    {
      get => this._conntrollerIP3;
      set => this._conntrollerIP3 = value;
    }

    [Description("IP адрес контроллера байт 4")]
    [DisplayName("13.  IP адрес контроллера байт 4")]
    public uint conntrollerIP4
    {
      get => this._conntrollerIP4;
      set => this._conntrollerIP4 = value;
    }

    [DisplayName("14.  TCP порт")]
    [Description("TCP порт")]
    public uint conntrollerTCPPort
    {
      get => this._conntrollerTCPPort;
      set => this._conntrollerTCPPort = value;
    }

    [DisplayName("15.  Timeout")]
    [Description("Timeout")]
    public uint timeout
    {
      get => this._timeout;
      set => this._timeout = value;
    }

    protected override void ToRuntime()
    {
    }

    protected override void ToDesign()
    {
    }

    protected override void UpdateData()
    {
      this.VisualPins.SetPinValue(1001, (object) (this._enumProtocol == LSILib.FB.Table.Table.ControllerProtokol.Modbus ? 1 : (this._enumProtocol == LSILib.FB.Table.Table.ControllerProtokol.SLMP_not_implimated ? 2 : 0)));
      this.VisualPins.SetPinValue(1002, (object) (this._enumSLMP_area == LSILib.FB.Table.Table.SLMP_area.D ? 1 : (this._enumSLMP_area == LSILib.FB.Table.Table.SLMP_area.R ? 2 : 0)));
      this.VisualPins.SetPinValue(1003, (object) this._uFloatBaseAddr);
      this.VisualPins.SetPinValue(1004, (object) this._uFloatAreaSize);
      this.VisualPins.SetPinValue(1005, (object) this._uIntBaseAddr);
      this.VisualPins.SetPinValue(1006, (object) this._uIntAreaSize);
      this.VisualPins.SetPinValue(1007, (object) this._uBoolBaseAddr);
      this.VisualPins.SetPinValue(1008, (object) this._uBoolAreaSize);
      this.VisualPins.SetPinValue(1009, (object) this._uControlBaseAddr);
      this.VisualPins.SetPinValue(1010, (object) this._conntrollerIP1);
      this.VisualPins.SetPinValue(1011, (object) this._conntrollerIP2);
      this.VisualPins.SetPinValue(1012, (object) this._conntrollerIP3);
      this.VisualPins.SetPinValue(1013, (object) this._conntrollerIP4);
      this.VisualPins.SetPinValue(1014, (object) this._conntrollerTCPPort);
      this.VisualPins.SetPinValue(1015, (object) this._timeout);
      int num1 = this.GetPinInt(1);
      bool flag1 = this.GetPinQuality(1) == OpcQuality.Ok;
      bool pinBool = this.GetPinBool(2);
      bool flag2 = this.GetPinQuality(2) == OpcQuality.Ok;
      if (!flag1)
        num1 = -1;
      uint num2 = (uint) (0 + (pinBool ? 1 : 0) + (flag2 ? 2 : 0) + (flag1 ? 4 : 0));
      this.VisualPins.SetPinValue(1016, (object) num1);
      this.VisualPins.SetPinValue(1017, (object) num2);
    }

    public enum ControllerProtokol
    {
      Modbus,
      SLMP_not_implimated,
    }

    public enum SLMP_area
    {
      D,
      R,
    }
  }
}
