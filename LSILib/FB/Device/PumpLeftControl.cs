// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Device.PumpLeftControl
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
  [Guid("6F6FFA02-3CB9-4c83-8E37-C9B2642D5A83")]
  [DisplayName("Насос влево")]
  [ComVisible(true)]
  public class PumpLeftControl : PumpBaseControl
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

    public PumpLeftControl() => this.InitializeComponent();

    public override Image getPic(ControlBase.v_state s)
    {
      switch (s)
      {
        case ControlBase.v_state.v_closed:
          return (Image) Resources.pump_left_stopped;
        case ControlBase.v_state.v_openned:
          return (Image) Resources.pump_left_started;
        case ControlBase.v_state.v_openning:
        case ControlBase.v_state.v_closing:
        case ControlBase.v_state.v_midle:
          return (Image) Resources.pump_left_starting;
        default:
          return (Image) Resources.pump_left_unreable;
      }
    }
  }
}
