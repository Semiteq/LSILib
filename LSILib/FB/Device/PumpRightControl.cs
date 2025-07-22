// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Device.PumpRightControl
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
  [Guid("5AAEBFBE-9140-4902-9C1A-36E3A327C7FC")]
  [DisplayName("Насос вправо")]
  public class PumpRightControl : PumpBaseControl
  {
    private IContainer components;

    public PumpRightControl() => this.InitializeComponent();

    public override Image getPic(ControlBase.v_state s)
    {
      switch (s)
      {
        case ControlBase.v_state.v_closed:
          return (Image) Resources.pump_right_stopped;
        case ControlBase.v_state.v_openned:
          return (Image) Resources.pump_right_started;
        case ControlBase.v_state.v_openning:
        case ControlBase.v_state.v_closing:
        case ControlBase.v_state.v_midle:
          return (Image) Resources.pump_right_starting;
        default:
          return (Image) Resources.pump_right_unreable;
      }
    }

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
  }
}
