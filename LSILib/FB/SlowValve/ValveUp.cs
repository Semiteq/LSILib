// Decompiled with JetBrains decompiler
// Type: LSILib.FB.SlowValve.ValveUp
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
  [ComVisible(true)]
  [Guid("1D1B6CCF-15CB-4da2-8E34-53E9BD6E9895")]
  [DisplayName("Вверх")]
  public class ValveUp : ControlBase
  {
    private IContainer components;

    public ValveUp() => this.InitializeComponent();

    public override Image getPic(ControlBase.v_state s)
    {
      switch (s)
      {
        case ControlBase.v_state.v_closed:
          return (Image) Resources.valve2_top_closed;
        case ControlBase.v_state.v_openned_full:
          return (Image) Resources.valve2_top_oppened_full;
        case ControlBase.v_state.v_opened_slow:
          return (Image) Resources.valve2_top_closed_full;
        case ControlBase.v_state.v_openning:
        case ControlBase.v_state.v_closing:
          return (Image) Resources.valve2_top_changing_full;
        default:
          return (Image) Resources.valve2_top_unreable;
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
