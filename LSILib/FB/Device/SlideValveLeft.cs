// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Device.SlideValveLeft
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
  [DisplayName("Шибер влево")]
  [Guid("78631242-105B-4fa4-9748-AE98553092A8")]
  [ComVisible(true)]
  public class SlideValveLeft : SlideValveBaseControl
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

    public SlideValveLeft() => this.InitializeComponent();

    public override Image getPic(ControlBase.v_state s)
    {
      switch (s)
      {
        case ControlBase.v_state.v_closed:
          return (Image) Resources.slide_valve_closed_left;
        case ControlBase.v_state.v_openned:
          return (Image) Resources.slide_valve_openned_left;
        case ControlBase.v_state.v_openning:
        case ControlBase.v_state.v_closing:
        case ControlBase.v_state.v_midle:
          return (Image) Resources.slide_valve_changing_left;
        default:
          return (Image) Resources.slide_valve_unreable_left;
      }
    }
  }
}
