// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Device.ValveSlideValveBaseControl
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace LSILib.FB.Device
{
  public class ValveSlideValveBaseControl : ControlBase
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

    public ValveSlideValveBaseControl() => this.InitializeComponent();

    public override string getOpenStartText() => "Открыть";

    public override string getCloseStopText() => "Закрыть";
  }
}
