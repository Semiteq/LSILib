// Decompiled with JetBrains decompiler
// Type: LSILib.FB.SlowValve.ValveRight
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

using LSILib.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace LSILib.FB.SlowValve
{
  [DisplayName("Вправо")]
  [Guid("EA4AF99D-8C16-4d66-A8A0-551CD45E9686")]
  [ComVisible(true)]
  public class ValveRight : ControlBase
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

    public ValveRight() => this.InitializeComponent();

    public override Image getPic(ControlBase.v_state s)
    {
      switch (s)
      {
        case ControlBase.v_state.v_closed:
          return (Image) Resources.valve2_right_closed;
        case ControlBase.v_state.v_openned_full:
          return (Image) Resources.valve2_right_oppened_full;
        case ControlBase.v_state.v_opened_slow:
          return (Image) Resources.valve2_right_closed_full;
        case ControlBase.v_state.v_openning:
        case ControlBase.v_state.v_closing:
          return (Image) Resources.valve2_right_changing_full;
        default:
          return (Image) Resources.valve2_right_unreablel;
      }
    }
  }
}
