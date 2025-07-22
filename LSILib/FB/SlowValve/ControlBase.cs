// Decompiled with JetBrains decompiler
// Type: LSILib.FB.SlowValve.ControlBase
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

using FB.VisualFB;
using InSAT.Library.Gui;
using InSAT.Library.Interop.Win32;
using InSAT.OPC;
using LSILib.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace LSILib.FB.SlowValve
{
  public class ControlBase : VisualControlBase
  {
    [NonSerialized]
    private int iOldState = -1;
    [NonSerialized]
    private ControlWindow _form;
    [NonSerialized]
    private int frame_width;
    [NonSerialized]
    private bool opening;
    [NonSerialized]
    private bool closing;
    [NonSerialized]
    private OpcQuality old_q;
    private title_type _tt;
    private IContainer components;
    private PictureBox pictureBox1;

    [DisplayName("Надпись контрола")]
    public title_type tt
    {
      get => this._tt;
      set => this._tt = value;
    }

    public ControlBase()
      : base(true)
    {
      this.InitializeComponent();
      this.VisibleChanged += new EventHandler(this.Control_VisibleChanged);
      this._form = (ControlWindow) null;
      this.frame_width = 5;
      this.iOldState = -1;
      this.opening = false;
      this.closing = false;
      this.pictureBox1.Image = this.getPic(ControlBase.v_state.v_openned_full);
    }

    public virtual Image getPic(ControlBase.v_state s) => (Image) Resources.valve2_top_unreable;

    public Image getPicForForm(ControlBase.v_state s)
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

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.FBConnector.DesignMode)
        return;
      int num = this.FBConnector.GetPinValue<int>(300);
      OpcQuality pinQuality = this.FBConnector.GetPinQuality(300);
      if (this.iOldState != num || pinQuality != this.old_q)
      {
        this.iOldState = num;
        this.old_q = pinQuality;
        this.opening = false;
        this.closing = false;
        if (pinQuality != OpcQuality.Ok)
          num = 0;
        if ((num & 8) != 0)
          this.pictureBox1.Image = this.getPic(ControlBase.v_state.v_openned_full);
        else if ((num & 16) != 0)
          this.pictureBox1.Image = this.getPic(ControlBase.v_state.v_opened_slow);
        else if ((num & 32) != 0)
          this.pictureBox1.Image = this.getPic(ControlBase.v_state.v_closed);
        else if ((num & 64) != 0)
          this.opening = true;
        else if ((num & 128) != 0)
          this.closing = true;
        else
          this.pictureBox1.Image = this.getPic(ControlBase.v_state.v_wtf);
      }
      if (this.IsLightBlink)
      {
        if ((num & 2) != 0 || (num & 4) != 0)
          FrameDrawer.Draw(e.Graphics, e.ClipRectangle, Color.Red, this.frame_width);
        if (this.opening)
          this.pictureBox1.Image = this.getPic(ControlBase.v_state.v_openning);
        if (!this.closing)
          return;
        this.pictureBox1.Image = this.getPic(ControlBase.v_state.v_closing);
      }
      else
      {
        if ((num & 2) != 0 && (num & 4) == 0)
          FrameDrawer.Draw(e.Graphics, e.ClipRectangle, Color.Red, this.frame_width);
        if (this.opening)
          this.pictureBox1.Image = this.getPic(ControlBase.v_state.v_closed);
        if (!this.closing)
          return;
        this.pictureBox1.Image = this.getPic(ControlBase.v_state.v_openned_full);
      }
    }

    private void pic_resize()
    {
      if (this.Width < 20)
        this.Width = 20;
      if (this.Height < 20)
        this.Height = 20;
      int num1 = (int) ((double) this.Height * 0.01);
      int num2 = num1 < 3 ? 3 : num1;
      int num3 = (int) ((double) this.Width * 0.01);
      int num4 = num3 < 3 ? 3 : num3;
      int num5 = num2 < num4 ? num2 : num4;
      this.pictureBox1.Top = num5;
      this.pictureBox1.Height = this.Height - 2 * num5;
      this.pictureBox1.Left = num5;
      this.pictureBox1.Width = this.Width - 2 * num5;
      int num6 = num5 / 2;
      this.frame_width = num6 < 2 ? 2 : num6;
    }

    private void size_ch(object sender, EventArgs e) => this.pic_resize();

    private void Control_Load(object sender, EventArgs e)
    {
      this.BackColor = Color.Empty;
      this.pic_resize();
    }

    protected override void ToDesign()
    {
      this.CloseForm();
      base.ToDesign();
    }

    private void Control_VisibleChanged(object sender, EventArgs e)
    {
      if (this.Visible)
        return;
      this.CloseForm();
    }

    private void CloseForm()
    {
      if (this._form == null)
        return;
      this._form.Close();
    }

    private void form_FormClosed(object sender, FormClosedEventArgs e)
    {
      ((Form) sender).FormClosed -= new FormClosedEventHandler(this.form_FormClosed);
      this._form = (ControlWindow) null;
    }

    private void ControlClick(object sender, EventArgs e)
    {
      if (this.FBConnector.DesignMode)
        return;
      MouseEventArgs mouseEventArgs = (MouseEventArgs) e;
      if (this._form == null)
      {
        ControlWindow controlWindow = new ControlWindow(this, new Point(Cursor.Position.X, Cursor.Position.Y));
        controlWindow.FormClosed += new FormClosedEventHandler(this.form_FormClosed);
        controlWindow.Show((IWin32Window) Win32Window.FromInt32(User32.GetParent(this.Handle)));
        this._form = controlWindow;
      }
      else
        this.CloseForm();
      int button = (int) mouseEventArgs.Button;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pictureBox1 = new PictureBox();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.pictureBox1.Image = (Image) Resources.valve2_top_oppened_full;
      this.pictureBox1.Location = new Point(4, 4);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(142, 142);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Click += new EventHandler(this.ControlClick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.pictureBox1);
      this.Name = nameof (ControlBase);
      this.Load += new EventHandler(this.Control_Load);
      this.VisibleChanged += new EventHandler(this.Control_VisibleChanged);
      this.Click += new EventHandler(this.ControlClick);
      this.SizeChanged += new EventHandler(this.size_ch);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
    }

    public enum v_state
    {
      v_closed,
      v_openned_full,
      v_opened_slow,
      v_openning,
      v_closing,
      v_wtf,
    }
  }
}
