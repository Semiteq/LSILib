// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Device.SlideValveDown
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
  [DisplayName("Шибер вниз")]
  [Guid("5C6CFFC3-65A4-4ac8-84F7-981D36B396A4")]
  public class SlideValveDown : SlideValveBaseControl
  {
    private IContainer components;

    public SlideValveDown() => this.InitializeComponent();

    public override Image getPic(ControlBase.v_state s)
    {
      switch (s)
      {
        case ControlBase.v_state.v_closed:
          return (Image) Resources.slide_valve_closed_bottom;
        case ControlBase.v_state.v_openned:
          return (Image) Resources.slide_valve_openned_bottom;
        case ControlBase.v_state.v_openning:
        case ControlBase.v_state.v_closing:
        case ControlBase.v_state.v_midle:
          return (Image) Resources.slide_valve_changing_bottom;
        default:
          return (Image) Resources.slide_valve_unreable_bottom;
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
