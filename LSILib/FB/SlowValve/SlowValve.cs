// Decompiled with JetBrains decompiler
// Type: LSILib.FB.SlowValve.SlowValve
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
namespace LSILib.FB.SlowValve
{
  [FBOptions(FBOptions.UseScanByTime | FBOptions.EnableChangeConfigInRT)]
  [ComVisible(true)]
  [Guid("8C11BC70-CFAF-4156-84C3-C5E87CBF85D1")]
  [CatID("58e065b5-10c2-4046-a8f4-f3eaaa3a4c2b")]
  [VisualControls(typeof (ValveUp), new Type[] {typeof (ValveRight), typeof (ValveDown), typeof (ValveLeft)})]
  [DisplayName("LSI_SlowValve")]
  [Serializable]
  public class SlowValve : VisualFBBase
  {
    private const int ID_StateDWordPin = 1;
    private const int ID_CammandDWordPout = 2;
    private const int ID_DebugPout = 3;
    private const int ID_StateUnreal = 101;
    private const int ID_StateOpenedFull = 102;
    private const int ID_StateOpenedSlow = 103;
    private const int ID_StateClosed = 104;
    private const int ID_StateOpeningStarting = 105;
    private const int ID_StateClosingStoping = 106;
    private const int ID_StateAlarm = 107;
    private const int ID_StateNewAlarm = 108;
    private const int ID_StateModeManual = 109;
    private const int ID_StateCmdOpenFull = 110;
    private const int ID_StateCmdOpenSlow = 111;
    private const int ID_StateEnaOpenFull = 112;
    private const int ID_StateEnaOpenSlow = 113;
    private const int ID_StateEnaClose = 114;
    private const int ID_StateEnaSwitch = 115;
    private const int ID_StateESOpened = 116;
    private const int ID_StateESClosed = 117;
    private const int ID_StateBlockOpen = 118;
    private const int ID_StateBlockClose = 119;
    private const int ID_StateTZOpen = 120;
    private const int ID_StateTZClose = 121;
    private const int ID_StateAlarmES = 122;
    private const int ID_StateAlarmNotOpen = 123;
    private const int ID_StateAlarmNotClose = 124;
    private const int ID_StateAlarmUnreal = 125;
    private const int ID_StateAlarmExternal = 126;
    private const int ID_StateSafetyOK = 127;
    private const int ID_StatePowerOk = 128;
    public const int ID_CmdOpenFull = 201;
    public const int ID_CmdOpenSlow = 202;
    public const int ID_CmdClose = 203;
    public const int ID_CmdChange = 204;
    public const int ID_CmdAck = 205;
    public const int ID_CmdManualOn = 206;
    public const int ID_CmdAutoOn = 207;
    public const int ID_HMIStatusDWord = 300;
    public const int ID_HMOCmdOpenFull = 310;
    public const int ID_HMOCmdOpenSlow = 311;
    public const int ID_HMOCmdClose = 312;
    public const int ID_HMOCmdChange = 313;
    public const int ID_HMOCmdAck = 314;
    public const int ID_HMOCmdManualOn = 315;
    public const int ID_HMOCmdAutoOn = 316;
    private long iIntenalCmdImpTime_ms = 1500;
    private bool bInternalUseManualIcon;
    private LSILib.FB.SlowValve.SlowValve.ClAct bInternalLeftClickAction = LSILib.FB.SlowValve.SlowValve.ClAct.open_window;
    private LSILib.FB.SlowValve.SlowValve.ClAct bInternalRightClickAction;
    private bool bInternalUseManualAutoButtons = true;
    private bool bInternalUseAckButton = true;
    private bool bInternalUseHMICmd = true;
    private bool bInternalUseOpenFullButton = true;
    private bool bInternalUseOpenSlowButton = true;
    private bool bInternalUseCloseButton = true;
    private bool bInternalUseSwitchButton = true;
    [NonSerialized]
    private long _iCmdOpenFull_ms;
    [NonSerialized]
    private long _iCmdOpenSlow_ms;
    [NonSerialized]
    private long _iCmdClose_ms;
    [NonSerialized]
    private long _iCmdChange_ms;
    [NonSerialized]
    private long _iCmdAck_ms;
    [NonSerialized]
    private long _iCmdManualOn_ms;
    [NonSerialized]
    private long _iCmdAutoOn_ms;
    [NonSerialized]
    private long _iLastTime_ms;

    [DisplayName(" 1. Длительность коммандного импульса (с)")]
    [Description("Длительность времени, в течении которого выдаётся команда для передачи в контроллер")]
    public double flCmdImpTime
    {
      get => (double) this.iIntenalCmdImpTime_ms / 1000.0;
      set
      {
        if (value <= 0.0)
          return;
        this.iIntenalCmdImpTime_ms = (long) (value * 1000.0);
      }
    }

    [DisplayName(" 2. Отображать ручной режим в контролле")]
    [Description("Если выбрана эта опция, режим работы блока (ручной или автоматический) будет отображаться в види иконки 'ручка' на отображении элемента")]
    public bool bUseManualIcon
    {
      get => this.bInternalUseManualIcon;
      set => this.bInternalUseManualIcon = value;
    }

    [DisplayName(" 3. Действие при клике на контрол левой кнопкой")]
    [Description("Опция определяет, что будет происходить при клике левой кнопкой на контрол: ничго, открть окно управления, сменить команду, дать команду старт/открыть, дать команду стоп / закрть. Настройку можно переопределить на мнемосхеме")]
    public LSILib.FB.SlowValve.SlowValve.ClAct bILeftClickAction
    {
      get => this.bInternalLeftClickAction;
      set => this.bInternalLeftClickAction = value;
    }

    [Description("Опция определяет, что будет происходить при клике правой кнопкой на контрол: ничго, открть окно управления, сменить команду, дать команду старт/открыть, дать команду стоп / закрть. Настройку можно переопределить на мнемосхеме")]
    [DisplayName(" 4. Действие при клике на контрол правой кнопкой")]
    public LSILib.FB.SlowValve.SlowValve.ClAct bRightClickAction
    {
      get => this.bInternalRightClickAction;
      set => this.bInternalRightClickAction = value;
    }

    [Description("Определяет будут ли показываться кнопки перекллючения режима (ручной автомат) в окне управления. Настройку можно переопределить на мнемосхеме")]
    [DisplayName(" 5. Использовать в окне управления кнопки переключения режима управления (ручной / автомат)")]
    public bool bIUseManualAutoButtons
    {
      get => this.bInternalUseManualAutoButtons;
      set => this.bInternalUseManualAutoButtons = value;
    }

    [DisplayName(" 6. Использовать в окне управления кнопку сброса аварий")]
    [Description("Определяет будет ли показываться кнопка сброса аварий в окне управления. Настройку можно переопределить на мнемосхеме")]
    public bool bUseAckButton
    {
      get => this.bInternalUseAckButton;
      set => this.bInternalUseAckButton = value;
    }

    [Description("Определяет можно ли передать в контроллер коменды Открыть Быстро / Медленно, Закрыть, Сменить. На мнемосхеме переопредить нельзя")]
    [DisplayName(" 7. Разрешить передачу команд управления из контролла и окна управления")]
    public bool bUseHMICmd
    {
      get => this.bInternalUseHMICmd;
      set
      {
        this.bInternalUseHMICmd = value;
        if (this.bInternalUseHMICmd)
          return;
        this.bInternalUseOpenFullButton = false;
        this.bInternalUseOpenSlowButton = false;
        this.bInternalUseCloseButton = false;
        this.bInternalUseSwitchButton = false;
      }
    }

    [Description("Определяет будет ли показываться кнопка Открыть Быстро в окне управления. Если запрещены команды управления, то кнопка не показывается. Настройку можно переопределить на мнемосхеме")]
    [DisplayName(" 8. Использовать в окне оправления кнопку Открыть Быстро")]
    public bool bIUseOpenFullButton
    {
      get => this.bInternalUseOpenFullButton;
      set => this.bInternalUseOpenFullButton = this.bInternalUseHMICmd && value;
    }

    [DisplayName(" 9. Использовать в окне оправления кнопку Открыть Медлено")]
    [Description("Определяет будет ли показываться кнопка Открыть Медлено в окне управления. Если запрещены команды управления, то кнопка не показывается. Настройку можно переопределить на мнемосхеме")]
    public bool bIUseOpenSlowButton
    {
      get => this.bInternalUseOpenSlowButton;
      set => this.bInternalUseOpenSlowButton = this.bInternalUseHMICmd && value;
    }

    [Description("Определяет будет ли показываться кнопка Закрыть в окне управления. Если запрещены команды управления, то кнопка не показывается. Настройку можно переопределить на мнемосхеме")]
    [DisplayName(" 10. Использовать в окне оправления кнопку Закрыть")]
    public bool bUseCloseStopButton
    {
      get => this.bInternalUseCloseButton;
      set => this.bInternalUseCloseButton = this.bInternalUseHMICmd && value;
    }

    [Description("Определяет будет ли показываться кнопка Переключить в окне управления. Если запрещены команды управления, то кнопка не показывается. Настройку можно переопределить на мнемосхеме")]
    [DisplayName("11. Использовать в окне оправления кнопку Переключить")]
    public bool bUseSwitchButton
    {
      get => this.bInternalUseSwitchButton;
      set => this.bInternalUseSwitchButton = this.bInternalUseHMICmd && value;
    }

    protected override void ToRuntime()
    {
      this._iCmdOpenFull_ms = 0L;
      this._iCmdOpenSlow_ms = 0L;
      this._iCmdClose_ms = 0L;
      this._iCmdChange_ms = 0L;
      this._iCmdAck_ms = 0L;
      this._iCmdManualOn_ms = 0L;
      this._iCmdAutoOn_ms = 0L;
      this._iLastTime_ms = DateTime.Now.ToLocalTime().Ticks / 10000L;
    }

    protected override void ToDesign()
    {
    }

    protected override void UpdateData()
    {
      uint num1 = this.GetPinUint(1);
      if (this.GetPinQuality(1) != OpcQuality.Ok)
        num1 = 0U;
      this.VisualPins.SetPinValue(300, (object) num1);
      this.SetPinValue(101, (object) (((int) num1 & 1) == 0));
      this.SetPinValue(102, (object) (((int) num1 & 8) != 0));
      this.SetPinValue(103, (object) (((int) num1 & 16) != 0));
      this.SetPinValue(104, (object) (((int) num1 & 32) != 0));
      this.SetPinValue(105, (object) (((int) num1 & 64) != 0));
      this.SetPinValue(106, (object) (((int) num1 & 128) != 0));
      this.SetPinValue(107, (object) (((int) num1 & 2) != 0));
      this.SetPinValue(108, (object) (((int) num1 & 4) != 0));
      this.SetPinValue(109, (object) (((int) num1 & 256) != 0));
      this.SetPinValue(110, (object) (((int) num1 & 512) != 0));
      this.SetPinValue(111, (object) (((int) num1 & 1024) != 0));
      this.SetPinValue(112, (object) (((int) num1 & 2048) != 0));
      this.SetPinValue(113, (object) (((int) num1 & 4096) != 0));
      this.SetPinValue(114, (object) (((int) num1 & 8192) != 0));
      this.SetPinValue(115, (object) (bool) (((int) num1 & 3072) == 3072 || ((int) num1 & 5632) == 4096 ? 1 : (((int) num1 & 8704) == 8704 ? 1 : 0)));
      this.SetPinValue(116, (object) (((int) num1 & 16384) != 0));
      this.SetPinValue(117, (object) (((int) num1 & 32768) != 0));
      this.SetPinValue(118, (object) (((int) num1 & 65536) != 0));
      this.SetPinValue(119, (object) (((int) num1 & 131072) != 0));
      this.SetPinValue(120, (object) (((int) num1 & 262144) != 0));
      this.SetPinValue(121, (object) (((int) num1 & 524288) != 0));
      this.SetPinValue(122, (object) (((int) num1 & 1048576) != 0));
      this.SetPinValue(123, (object) (((int) num1 & 2097152) != 0));
      this.SetPinValue(124, (object) (((int) num1 & 4194304) != 0));
      this.SetPinValue(125, (object) (((int) num1 & 8388608) != 0));
      this.SetPinValue(126, (object) (((int) num1 & 16777216) != 0));
      this.SetPinValue((int) sbyte.MaxValue, (object) (((int) num1 & 268435456) != 0));
      this.SetPinValue(128, (object) (((int) num1 & 536870912) != 0));
      long num2 = DateTime.Now.ToLocalTime().Ticks / 10000L;
      long num3 = num2 - this._iLastTime_ms;
      this._iLastTime_ms = num2;
      if (num3 > 0L)
      {
        this._iCmdOpenFull_ms -= num3;
        if (this._iCmdOpenFull_ms < 0L)
          this._iCmdOpenFull_ms = 0L;
        this._iCmdOpenSlow_ms -= num3;
        if (this._iCmdOpenSlow_ms < 0L)
          this._iCmdOpenSlow_ms = 0L;
        this._iCmdClose_ms -= num3;
        if (this._iCmdClose_ms < 0L)
          this._iCmdClose_ms = 0L;
        this._iCmdChange_ms -= num3;
        if (this._iCmdChange_ms < 0L)
          this._iCmdChange_ms = 0L;
        this._iCmdAck_ms -= num3;
        if (this._iCmdAck_ms < 0L)
          this._iCmdAck_ms = 0L;
        this._iCmdManualOn_ms -= num3;
        if (this._iCmdManualOn_ms < 0L)
          this._iCmdManualOn_ms = 0L;
        this._iCmdAutoOn_ms -= num3;
        if (this._iCmdAutoOn_ms < 0L)
          this._iCmdAutoOn_ms = 0L;
      }
      if (this.VisualPins.GetPinQuality(310) == OpcQuality.Ok && this.VisualPins.GetPinBool(310))
      {
        this._iCmdOpenFull_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdOpenSlow_ms = 0L;
        this._iCmdClose_ms = 0L;
        this._iCmdChange_ms = 0L;
        this.VisualPins.SetPinValue(310, (object) false);
      }
      if (this.VisualPins.GetPinQuality(311) == OpcQuality.Ok && this.VisualPins.GetPinBool(311))
      {
        this._iCmdOpenFull_ms = 0L;
        this._iCmdOpenSlow_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdClose_ms = 0L;
        this._iCmdChange_ms = 0L;
        this.VisualPins.SetPinValue(311, (object) false);
      }
      if (this.VisualPins.GetPinQuality(312) == OpcQuality.Ok && this.VisualPins.GetPinBool(312))
      {
        this._iCmdOpenFull_ms = 0L;
        this._iCmdOpenSlow_ms = 0L;
        this._iCmdClose_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdChange_ms = 0L;
        this.VisualPins.SetPinValue(312, (object) false);
      }
      if (this.VisualPins.GetPinQuality(313) == OpcQuality.Ok && this.VisualPins.GetPinBool(313))
      {
        this._iCmdOpenFull_ms = 0L;
        this._iCmdOpenSlow_ms = 0L;
        this._iCmdClose_ms = 0L;
        this._iCmdChange_ms = this.iIntenalCmdImpTime_ms;
        this.VisualPins.SetPinValue(313, (object) false);
      }
      if (this.VisualPins.GetPinQuality(314) == OpcQuality.Ok && this.VisualPins.GetPinBool(314))
      {
        this._iCmdAck_ms = this.iIntenalCmdImpTime_ms;
        this.VisualPins.SetPinValue(314, (object) false);
      }
      if (this.VisualPins.GetPinQuality(315) == OpcQuality.Ok && this.VisualPins.GetPinBool(315))
      {
        this._iCmdManualOn_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdAutoOn_ms = 0L;
        this.VisualPins.SetPinValue(315, (object) false);
      }
      if (this.VisualPins.GetPinQuality(316) == OpcQuality.Ok && this.VisualPins.GetPinBool(316))
      {
        this._iCmdManualOn_ms = 0L;
        this._iCmdAutoOn_ms = this.iIntenalCmdImpTime_ms;
        this.VisualPins.SetPinValue(316, (object) false);
      }
      if (this.GetPinQuality(201) == OpcQuality.Ok && this.GetPinBool(201))
      {
        this._iCmdOpenFull_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdOpenSlow_ms = 0L;
        this._iCmdClose_ms = 0L;
        this._iCmdChange_ms = 0L;
      }
      if (this.GetPinQuality(202) == OpcQuality.Ok && this.GetPinBool(202))
      {
        this._iCmdOpenFull_ms = 0L;
        this._iCmdOpenSlow_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdClose_ms = 0L;
        this._iCmdChange_ms = 0L;
      }
      if (this.GetPinQuality(203) == OpcQuality.Ok && this.GetPinBool(203))
      {
        this._iCmdOpenFull_ms = 0L;
        this._iCmdOpenSlow_ms = 0L;
        this._iCmdClose_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdChange_ms = 0L;
      }
      if (this.GetPinQuality(204) == OpcQuality.Ok && this.GetPinBool(204))
      {
        this._iCmdOpenFull_ms = 0L;
        this._iCmdOpenSlow_ms = 0L;
        this._iCmdClose_ms = 0L;
        this._iCmdChange_ms = this.iIntenalCmdImpTime_ms;
      }
      if (this.GetPinQuality(205) == OpcQuality.Ok && this.GetPinBool(205))
        this._iCmdAck_ms = this.iIntenalCmdImpTime_ms;
      if (this.GetPinQuality(206) == OpcQuality.Ok && this.GetPinBool(206))
      {
        this._iCmdManualOn_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdAutoOn_ms = 0L;
      }
      if (this.GetPinQuality(207) == OpcQuality.Ok && this.GetPinBool(207))
      {
        this._iCmdManualOn_ms = 0L;
        this._iCmdAutoOn_ms = this.iIntenalCmdImpTime_ms;
      }
      int num4 = 0;
      if (this._iCmdOpenFull_ms > 0L)
        ++num4;
      if (this._iCmdOpenSlow_ms > 0L)
        num4 += 2;
      if (this._iCmdClose_ms > 0L)
        num4 += 4;
      if (this._iCmdChange_ms > 0L)
        num4 += 8;
      if (this._iCmdAck_ms > 0L)
        num4 += 16;
      if (this._iCmdManualOn_ms > 0L)
        num4 += 32;
      if (this._iCmdAutoOn_ms > 0L)
        num4 += 64;
      this.SetPinValue(2, (object) num4);
    }

    public enum ClAct
    {
      nothing = 0,
      open_window = 1,
      change_cmd = 2,
      open_full = 3,
      open_slow = 3,
      close = 5,
    }
  }
}
