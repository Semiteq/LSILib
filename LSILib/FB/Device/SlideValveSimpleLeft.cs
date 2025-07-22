// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Device.SlideValveSimpleLeft
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
  [DisplayName("Шибер простой влево")]
  [ComVisible(true)]
  [Guid("EDCB819E-6D4A-4ce0-ACFF-697B6B09CACC")]
  public class SlideValveSimpleLeft : SlideValveBaseControl
  {
    private IContainer components;

    public SlideValveSimpleLeft() => this.InitializeComponent();

    public override Image getPic(ControlBase.v_state s)
    {
      switch (s)
      {
        case ControlBase.v_state.v_closed:
          return (Image) Resources.slider_right_gor;
        case ControlBase.v_state.v_openned:
          return (Image) Resources.slider_left_gor;
        case ControlBase.v_state.v_openning:
        case ControlBase.v_state.v_closing:
        case ControlBase.v_state.v_midle:
          return (Image) Resources.slider_mid_gor;
        default:
          return (Image) Resources.slider_unreable_gor;
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
