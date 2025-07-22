// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Device.ValveBaseControl
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

using LSILib.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace LSILib.FB.Device
{
  public class ValveBaseControl : ValveSlideValveBaseControl
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

    public ValveBaseControl() => this.InitializeComponent();

    public override Image getPicForForm(ControlBase.v_state s)
    {
      switch (s)
      {
        case ControlBase.v_state.v_closed:
          return (Image) Resources.valve_closed;
        case ControlBase.v_state.v_openned:
          return (Image) Resources.valve_openned;
        case ControlBase.v_state.v_openning:
        case ControlBase.v_state.v_closing:
        case ControlBase.v_state.v_midle:
          return (Image) Resources.valve_changing;
        default:
          return (Image) Resources.valve_unreable;
      }
    }
  }
}
