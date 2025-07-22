// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Device.SlideValveUp
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
  [Guid("55653B45-2AB8-4295-8614-BD0C9C1C4AC6")]
  [DisplayName("Шибер вверх")]
  public class SlideValveUp : SlideValveBaseControl
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

    public SlideValveUp() => this.InitializeComponent();

    public override Image getPic(ControlBase.v_state s)
    {
      switch (s)
      {
        case ControlBase.v_state.v_closed:
          return (Image) Resources.slide_valve_closed;
        case ControlBase.v_state.v_openned:
          return (Image) Resources.slide_valve_openned;
        case ControlBase.v_state.v_openning:
        case ControlBase.v_state.v_closing:
        case ControlBase.v_state.v_midle:
          return (Image) Resources.slide_valve_changing;
        default:
          return (Image) Resources.slide_valve_unreable;
      }
    }
  }
}
