// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Device.Motor
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

using LSILib.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace LSILib.FB.Device
{
  [ComVisible(true)]
  [Guid("03BE948C-7B32-4dce-9643-4CE8488B431B")]
  [DisplayName("Мотор")]
  public class Motor : ControlBase
  {
    private IContainer components;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.Font;
    }

    public Motor() => this.InitializeComponent();

    public override Image getPicForForm(ControlBase.v_state s) => this.getPic(s);

    public override string getOpenStartText() => "Пуск";

    public override string getCloseStopText() => "Стоп";

    public override Image getPic(ControlBase.v_state s)
    {
      switch (s)
      {
        case ControlBase.v_state.v_closed:
          return (Image) Resources.motor_stoped;
        case ControlBase.v_state.v_openned:
          return (Image) Resources.motor_started;
        case ControlBase.v_state.v_openning:
        case ControlBase.v_state.v_closing:
        case ControlBase.v_state.v_midle:
          return (Image) Resources.motor_starting;
        default:
          return (Image) Resources.motor_unreable;
      }
    }
  }
}
