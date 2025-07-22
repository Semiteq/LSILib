// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Device.Device
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
namespace LSILib.FB.Device
{
  [ComVisible(true)]
  [CatID("58e065b5-10c2-4046-a8f4-f3eaaa3a4c2b")]
  [VisualControls(typeof (PumpRightControl), new Type[] {typeof (PumpDownControl), typeof (PumpLeftControl), typeof (PumpUpControl), typeof (ValveHor), typeof (ValveVer), typeof (SlideValveUp), typeof (SlideValveRight), typeof (SlideValveDown), typeof (SlideValveLeft), typeof (SlideValveSimpleUp), typeof (SlideValveSimpleRight), typeof (SlideValveSimpleDown), typeof (SlideValveSimpleLeft), typeof (Motor)})]
  [Guid("97665C73-E215-49a9-8289-63B6571F22AB")]
  [DisplayName("LSI_Device")]
  [FBOptions(FBOptions.UseScanByTime | FBOptions.EnableChangeConfigInRT)]
  [Serializable]
  public class Device : VisualFBBase
  {
    private const int ID_StateDWordPin = 1;
    private const int ID_CammandDWordPout = 2;
    private const int ID_DebugPout = 3;
    private const int ID_StateUnreal = 101;
    private const int ID_StateOpenedStarted = 102;
    private const int ID_StateClosedStoped = 103;
    private const int ID_StateOpeningStarting = 104;
    private const int ID_StateClosingStoping = 105;
    private const int ID_StateAlarm = 106;
    private const int ID_StateNewAlarm = 107;
    private const int ID_StateModeManual = 108;
    private const int ID_StateCmdOpenStart = 109;
    private const int ID_StateCmdCloseStop = 110;
    private const int ID_StateEnaOpenStart = 111;
    private const int ID_StateEnaCloseStop = 112;
    private const int ID_StateEnaSwitch = 113;
    private const int ID_StateESOpenedStarted = 114;
    private const int ID_StateESClosedStoped = 115;
    private const int ID_StateBlockOpenStart = 116;
    private const int ID_StateBlockCloseStop = 117;
    private const int ID_StateTZOpenStart = 118;
    private const int ID_StateTZCloseStop = 119;
    private const int ID_StateAlarmES = 120;
    private const int ID_StateAlarmNotOpenStart = 121;
    private const int ID_StateAlarmNotCloseStop = 122;
    private const int ID_StateAlarmUnreal = 123;
    private const int ID_StateAlarmExternal = 124;
    private const int ID_StateSafetyOK = 125;
    private const int ID_StatePowerOk = 126;
    private const int ID_StateBonusFlag1 = 127;
    private const int ID_StateBonusFlag2 = 128;
    public const int ID_CmdOpenStart = 201;
    public const int ID_CmdCloseStop = 202;
    public const int ID_CmdChange = 203;
    public const int ID_CmdAck = 204;
    public const int ID_CmdManualOn = 205;
    public const int ID_CmdAutoOn = 206;
    public const int ID_HMIStatusDWord = 300;
    public const int ID_HMOCmdOpenStart = 310;
    public const int ID_HMOCmdCloseStop = 311;
    public const int ID_HMOCmdChange = 312;
    public const int ID_HMOCmdAck = 313;
    public const int ID_HMOCmdManualOn = 314;
    public const int ID_HMOCmdAutoOn = 315;
    private long iIntenalCmdImpTime_ms = 1500;
    private bool bInternalUseManualIcon;
    private LSILib.FB.Device.Device.ClAct bInternalLeftClickAction = LSILib.FB.Device.Device.ClAct.open_window;
    private LSILib.FB.Device.Device.ClAct bInternalRightClickAction;
    private bool bInternalUseManualAutoButtons = true;
    private bool bInternalUseAckButton = true;
    private bool bInternalUseHMICmd = true;
    private bool bInternalUseOpenStartButton = true;
    private bool bInternalUseCloseStopButton = true;
    private bool bInternalUseSwitchButton = true;
    [NonSerialized]
    private long _iCmdOpenStart_ms;
    [NonSerialized]
    private long _iCmdCloseStop_ms;
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

    [Description("Длительность времени, в течении которого выдаётся команда для передачи в контроллер")]
    [DisplayName(" 1. Длительность коммандного импульса (с)")]
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

    [Description("Опция определяет, что будет происходить при клике левой кнопкой на контрол: ничго, открть окно управления, сменить команду, дать команду старт/открыть, дать команду стоп / закрть. Настройку можно переопределить на мнемосхеме")]
    [DisplayName(" 3. Действие при клике на контрол левой кнопкой")]
    public LSILib.FB.Device.Device.ClAct bILeftClickAction
    {
      get => this.bInternalLeftClickAction;
      set => this.bInternalLeftClickAction = value;
    }

    [Description("Опция определяет, что будет происходить при клике правой кнопкой на контрол: ничго, открть окно управления, сменить команду, дать команду старт/открыть, дать команду стоп / закрть. Настройку можно переопределить на мнемосхеме")]
    [DisplayName(" 4. Действие при клике на контрол правой кнопкой")]
    public LSILib.FB.Device.Device.ClAct bRightClickAction
    {
      get => this.bInternalRightClickAction;
      set => this.bInternalRightClickAction = value;
    }

    [DisplayName(" 5. Использовать в окне управления кнопки переключения режима управления (ручной / автомат)")]
    [Description("Определяет будут ли показываться кнопки перекллючения режима (ручной автомат) в окне управления. Настройку можно переопределить на мнемосхеме")]
    public bool bIUseManualAutoButtons
    {
      get => this.bInternalUseManualAutoButtons;
      set => this.bInternalUseManualAutoButtons = value;
    }

    [Description("Определяет будет ли показываться кнопка сброса аварий в окне управления. Настройку можно переопределить на мнемосхеме")]
    [DisplayName(" 6. Использовать в окне управления кнопку сброса аварий")]
    public bool bUseAckButton
    {
      get => this.bInternalUseAckButton;
      set => this.bInternalUseAckButton = value;
    }

    [Description("Определяет можно ли передать в контроллер коменды Открыть / Пуск, Закрыть / Стоп, Сменить. На мнемосхеме переопредить нельзя")]
    [DisplayName(" 7. Разрешить передачу команд управления из контролла и окна управления")]
    public bool bUseHMICmd
    {
      get => this.bInternalUseHMICmd;
      set
      {
        this.bInternalUseHMICmd = value;
        if (this.bInternalUseHMICmd)
          return;
        this.bInternalUseOpenStartButton = false;
        this.bInternalUseCloseStopButton = false;
        this.bInternalUseSwitchButton = false;
      }
    }

    [DisplayName(" 8. Использовать в окне оправления кнопку Открыть / Пуск")]
    [Description("Определяет будет ли показываться кнопка Открыть / Пуск в окне управления. Если запрещены команды управления, то кнопка не показывается. Настройку можно переопределить на мнемосхеме")]
    public bool bIUseOpenStartButton
    {
      get => this.bInternalUseOpenStartButton;
      set => this.bInternalUseOpenStartButton = this.bInternalUseHMICmd && value;
    }

    [DisplayName(" 9. Использовать в окне оправления кнопку Закрыть / Стоп")]
    [Description("Определяет будет ли показываться кнопка Закрыть / Стоп в окне управления. Если запрещены команды управления, то кнопка не показывается. Настройку можно переопределить на мнемосхеме")]
    public bool bUseCloseStopButton
    {
      get => this.bInternalUseCloseStopButton;
      set => this.bInternalUseCloseStopButton = this.bInternalUseHMICmd && value;
    }

    [DisplayName("10. Использовать в окне оправления кнопку Переключить")]
    [Description("Определяет будет ли показываться кнопка Переключить в окне управления. Если запрещены команды управления, то кнопка не показывается. Настройку можно переопределить на мнемосхеме")]
    public bool bUseSwitchButton
    {
      get => this.bInternalUseSwitchButton;
      set => this.bInternalUseSwitchButton = this.bInternalUseHMICmd && value;
    }

    protected override void ToRuntime()
    {
      this._iCmdOpenStart_ms = 0L;
      this._iCmdCloseStop_ms = 0L;
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
      this.SetPinValue(106, (object) (((int) num1 & 2) != 0));
      this.SetPinValue(107, (object) (((int) num1 & 4) != 0));
      this.SetPinValue(108, (object) (((int) num1 & 128) != 0));
      this.SetPinValue(109, (object) (((int) num1 & 256) != 0));
      this.SetPinValue(110, (object) (((int) num1 & 512) != 0));
      this.SetPinValue(111, (object) (((int) num1 & 1024) != 0));
      this.SetPinValue(112, (object) (((int) num1 & 2048) != 0));
      this.SetPinValue(113, (object) (bool) (((int) num1 & 1280) == 1024 ? 1 : (((int) num1 & 2560) == 2048 ? 1 : 0)));
      this.SetPinValue(114, (object) (((int) num1 & 4096) != 0));
      this.SetPinValue(115, (object) (((int) num1 & 8192) != 0));
      this.SetPinValue(116, (object) (((int) num1 & 16384) != 0));
      this.SetPinValue(117, (object) (((int) num1 & 32768) != 0));
      this.SetPinValue(118, (object) (((int) num1 & 65536) != 0));
      this.SetPinValue(119, (object) (((int) num1 & 131072) != 0));
      this.SetPinValue(120, (object) (((int) num1 & 262144) != 0));
      this.SetPinValue(121, (object) (((int) num1 & 524288) != 0));
      this.SetPinValue(122, (object) (((int) num1 & 1048576) != 0));
      this.SetPinValue(123, (object) (((int) num1 & 2097152) != 0));
      this.SetPinValue(124, (object) (((int) num1 & 4194304) != 0));
      this.SetPinValue(125, (object) (((int) num1 & 268435456) != 0));
      this.SetPinValue(126, (object) (((int) num1 & 536870912) != 0));
      this.SetPinValue((int) sbyte.MaxValue, (object) (((int) num1 & 1073741824) != 0));
      this.SetPinValue(128, (object) (((int) num1 & int.MinValue) != 0));
      long num2 = DateTime.Now.ToLocalTime().Ticks / 10000L;
      long num3 = num2 - this._iLastTime_ms;
      this._iLastTime_ms = num2;
      if (num3 > 0L)
      {
        this._iCmdOpenStart_ms -= num3;
        if (this._iCmdOpenStart_ms < 0L)
          this._iCmdOpenStart_ms = 0L;
        this._iCmdCloseStop_ms -= num3;
        if (this._iCmdCloseStop_ms < 0L)
          this._iCmdCloseStop_ms = 0L;
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
        this._iCmdOpenStart_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdCloseStop_ms = 0L;
        this._iCmdChange_ms = 0L;
        this.VisualPins.SetPinValue(310, (object) false);
      }
      if (this.VisualPins.GetPinQuality(311) == OpcQuality.Ok && this.VisualPins.GetPinBool(311))
      {
        this._iCmdOpenStart_ms = 0L;
        this._iCmdCloseStop_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdChange_ms = 0L;
        this.VisualPins.SetPinValue(311, (object) false);
      }
      if (this.VisualPins.GetPinQuality(312) == OpcQuality.Ok && this.VisualPins.GetPinBool(312))
      {
        this._iCmdOpenStart_ms = 0L;
        this._iCmdCloseStop_ms = 0L;
        this._iCmdChange_ms = this.iIntenalCmdImpTime_ms;
        this.VisualPins.SetPinValue(312, (object) false);
      }
      if (this.VisualPins.GetPinQuality(313) == OpcQuality.Ok && this.VisualPins.GetPinBool(313))
      {
        this._iCmdAck_ms = this.iIntenalCmdImpTime_ms;
        this.VisualPins.SetPinValue(313, (object) false);
      }
      if (this.VisualPins.GetPinQuality(314) == OpcQuality.Ok && this.VisualPins.GetPinBool(314))
      {
        this._iCmdManualOn_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdAutoOn_ms = 0L;
        this.VisualPins.SetPinValue(314, (object) false);
      }
      if (this.VisualPins.GetPinQuality(315) == OpcQuality.Ok && this.VisualPins.GetPinBool(315))
      {
        this._iCmdManualOn_ms = 0L;
        this._iCmdAutoOn_ms = this.iIntenalCmdImpTime_ms;
        this.VisualPins.SetPinValue(315, (object) false);
      }
      if (this.GetPinQuality(201) == OpcQuality.Ok && this.GetPinBool(201))
      {
        this._iCmdOpenStart_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdCloseStop_ms = 0L;
        this._iCmdChange_ms = 0L;
      }
      if (this.GetPinQuality(202) == OpcQuality.Ok && this.GetPinBool(202))
      {
        this._iCmdOpenStart_ms = 0L;
        this._iCmdCloseStop_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdChange_ms = 0L;
      }
      if (this.GetPinQuality(203) == OpcQuality.Ok && this.GetPinBool(203))
      {
        this._iCmdOpenStart_ms = 0L;
        this._iCmdCloseStop_ms = 0L;
        this._iCmdChange_ms = this.iIntenalCmdImpTime_ms;
      }
      if (this.GetPinQuality(204) == OpcQuality.Ok && this.GetPinBool(204))
        this._iCmdAck_ms = this.iIntenalCmdImpTime_ms;
      if (this.GetPinQuality(205) == OpcQuality.Ok && this.GetPinBool(205))
      {
        this._iCmdManualOn_ms = this.iIntenalCmdImpTime_ms;
        this._iCmdAutoOn_ms = 0L;
      }
      if (this.GetPinQuality(206) == OpcQuality.Ok && this.GetPinBool(206))
      {
        this._iCmdManualOn_ms = 0L;
        this._iCmdAutoOn_ms = this.iIntenalCmdImpTime_ms;
      }
      int num4 = 0;
      if (this._iCmdOpenStart_ms > 0L)
        ++num4;
      if (this._iCmdCloseStop_ms > 0L)
        num4 += 2;
      if (this._iCmdChange_ms > 0L)
        num4 += 4;
      if (this._iCmdAck_ms > 0L)
        num4 += 8;
      if (this._iCmdManualOn_ms > 0L)
        num4 += 16;
      if (this._iCmdAutoOn_ms > 0L)
        num4 += 32;
      this.SetPinValue(2, (object) num4);
    }

    public enum ClAct
    {
      nothing,
      open_window,
      change_cmd,
      open_start,
      close_stop,
    }
  }
}
