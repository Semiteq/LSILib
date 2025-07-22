// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Device.SlideValveSimpleDown
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
  [Guid("B5EE21A7-B3DE-49a6-AB67-A29B80AD0022")]
  [DisplayName("Шибер простой вниз")]
  public class SlideValveSimpleDown : SlideValveBaseControl
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

    public SlideValveSimpleDown() => this.InitializeComponent();

    public override Image getPic(ControlBase.v_state s)
    {
      switch (s)
      {
        case ControlBase.v_state.v_closed:
          return (Image) Resources.slider_up;
        case ControlBase.v_state.v_openned:
          return (Image) Resources.slider_down;
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
