// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Device.SlideValveSimpleUp
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
  [DisplayName("Шибер простой вверх")]
  [ComVisible(true)]
  [Guid("2E408959-F8BE-4f17-9264-552FD8BE8519")]
  public class SlideValveSimpleUp : SlideValveBaseControl
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

    public SlideValveSimpleUp() => this.InitializeComponent();

    public override Image getPic(ControlBase.v_state s)
    {
      switch (s)
      {
        case ControlBase.v_state.v_closed:
          return (Image) Resources.slider_down;
        case ControlBase.v_state.v_openned:
          return (Image) Resources.slider_up;
        case ControlBase.v_state.v_openning:
        case ControlBase.v_state.v_closing:
        case ControlBase.v_state.v_midle:
          return (Image) Resources.slider_mid;
        default:
          return (Image) Resources.slider_unreable;
      }
    }
  }
}
