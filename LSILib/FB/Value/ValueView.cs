// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Value.ValueView
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

using FB.VisualFB;
using InSAT.OPC;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace LSILib.FB.Value
{
  [ComVisible(true)]
  [DisplayName("Значение")]
  [Guid("A79D662B-8437-4918-B5B8-A352FA49F1F4")]
  public class ValueView : VisualControlBase
  {
    private Color _bg_color_ch_ena = Color.White;
    private Color _bg_color_ch_dis = Color.FromArgb(224, 224, 224);
    private Color _text_color_value_ok = Color.Black;
    private Color _text_color_value_fail = Color.LightGray;
    private int _num_after_point = 1;
    private string _string_befor = "";
    private string _string_after = "";
    private string _format = "F1";
    private ValueView.NumView _num_view;
    [NonSerialized]
    private bool _bChangeEna;
    [NonSerialized]
    private bool _bSensOk;
    [NonSerialized]
    private double _fActuaValue;
    [NonSerialized]
    private bool _bToEdit;
    [NonSerialized]
    private bool _bCancel;
    [NonSerialized]
    private bool _bEnter;
    [NonSerialized]
    private string _sNewValue = "";
    private IContainer components;
    private Label label_value;
    private TextBox textBox_value;

    public ValueView()
      : base(true)
    {
      this.InitializeComponent();
      this.VisibleChanged += new EventHandler(this.ValueView_VisibleChanged);
    }

    [DisplayName("Цвет фона (редакт. разр.)")]
    public Color bg_color_ch_ena
    {
      get => this._bg_color_ch_ena;
      set
      {
        if (value != Color.Transparent)
          this._bg_color_ch_ena = value;
        this.label_value.BackColor = this._bg_color_ch_ena;
      }
    }

    [DisplayName("Цвет фона (редакт. запр.)")]
    public Color bg_color_ch_dis
    {
      get => this._bg_color_ch_dis;
      set
      {
        if (!(value != Color.Transparent))
          return;
        this._bg_color_ch_dis = value;
      }
    }

    [DisplayName("Шрифт")]
    public Font text_font
    {
      get => this.label_value.Font;
      set
      {
        this.label_value.Font = value;
        this.textBox_value.Font = value;
      }
    }

    [DisplayName("Цвет текста (online)")]
    public Color text_color_ok
    {
      get => this._text_color_value_ok;
      set
      {
        if (value != Color.Transparent)
          this._text_color_value_ok = value;
        this.label_value.ForeColor = this._text_color_value_ok;
      }
    }

    [DisplayName("Цвет текста (offine)")]
    public Color text_color_fail
    {
      get => this._text_color_value_fail;
      set
      {
        if (!(value != Color.Transparent))
          return;
        this._text_color_value_fail = value;
      }
    }

    [DisplayName("Выравнивание")]
    public ContentAlignment ali
    {
      get => this.label_value.TextAlign;
      set
      {
        this.label_value.TextAlign = value;
        switch (value)
        {
          case ContentAlignment.TopLeft:
          case ContentAlignment.MiddleLeft:
          case ContentAlignment.BottomLeft:
            this.textBox_value.TextAlign = HorizontalAlignment.Left;
            break;
          case ContentAlignment.TopRight:
          case ContentAlignment.MiddleRight:
          case ContentAlignment.BottomRight:
            this.textBox_value.TextAlign = HorizontalAlignment.Right;
            break;
          default:
            this.textBox_value.TextAlign = HorizontalAlignment.Center;
            break;
        }
      }
    }

    [DisplayName("Число точек после запятой")]
    public int num_after_point
    {
      get => this._num_after_point;
      set
      {
        this._num_after_point = value < 0 ? 0 : (value > 5 ? 5 : value);
        this._format = this.make_fomat_string();
        this.label_value.Text = this.make_text_value();
      }
    }

    [DisplayName("Формат отображения")]
    public ValueView.NumView num_view
    {
      get => this._num_view;
      set
      {
        this._num_view = value;
        this._format = this.make_fomat_string();
        this.label_value.Text = this.make_text_value();
      }
    }

    private string make_fomat_string()
    {
      string str = "F";
      if (this._num_view == ValueView.NumView.exponential_view)
        str = "E";
      return str + this._num_after_point.ToString();
    }

    [DisplayName("текст до")]
    public string text_befor
    {
      get => this._string_befor;
      set
      {
        this._string_befor = value;
        this.label_value.Text = this.make_text_value();
      }
    }

    [DisplayName("текст после")]
    public string text_after
    {
      get => this._string_after;
      set
      {
        this._string_after = value;
        this.label_value.Text = this.make_text_value();
      }
    }

    private string make_text_value()
    {
      return this._string_befor + this._fActuaValue.ToString(this._format) + this._string_after;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.FBConnector.DesignMode)
        return;
      int pinValue = this.FBConnector.GetPinValue<int>(301);
      OpcQuality pinQuality = this.FBConnector.GetPinQuality(301);
      this._fActuaValue = this.FBConnector.GetPinValue<double>(300);
      if (this.FBConnector.GetPinQuality(300) == OpcQuality.Ok)
        this.label_value.Text = this.make_text_value();
      bool flag1 = false;
      bool flag2 = false;
      if (pinQuality == OpcQuality.Ok)
      {
        flag1 = (pinValue & 2) != 0;
        flag2 = (pinValue & 1) != 0;
      }
      if (flag1 != this._bChangeEna)
      {
        this._bChangeEna = flag1;
        this.label_value.BackColor = this._bChangeEna ? this._bg_color_ch_ena : this._bg_color_ch_dis;
      }
      if (flag2 != this._bSensOk)
      {
        this._bSensOk = flag2;
        this.label_value.ForeColor = this._bSensOk ? this._text_color_value_ok : this._text_color_value_fail;
      }
      if (this._bToEdit)
      {
        this._bToEdit = false;
        this.textBox_value.Text = this._fActuaValue.ToString(this._format);
        this.label_value.Visible = false;
        this.textBox_value.Visible = true;
        this.textBox_value.Focus();
      }
      if (!this.textBox_value.Focused)
        this._bCancel = true;
      if (this._bEnter)
      {
        this._bEnter = false;
        double result = 0.0;
        if (double.TryParse(this._sNewValue, out result))
        {
          this.FBConnector.SetPinValue(310, (object) result);
          this.FBConnector.SetPinValue(311, (object) true);
        }
        else
        {
          int num = (int) MessageBox.Show("Не верный формат");
        }
        this._bCancel = true;
      }
      if (!this._bCancel)
        return;
      this._bCancel = false;
      this.label_value.Visible = true;
      this.textBox_value.Visible = false;
    }

    private void pic_resize()
    {
      if (this.Width < 20)
        this.Width = 20;
      if (this.Height < 20)
        this.Height = 20;
      this.label_value.Top = 1;
      this.label_value.Left = 1;
      this.label_value.Height = this.Height - 2;
      this.label_value.Width = this.Width - 2;
      this.textBox_value.Top = (this.Height - this.textBox_value.Height) / 2;
      this.textBox_value.Left = 1;
      this.textBox_value.Width = this.Width - 2;
    }

    private void ValueView_Load(object sender, EventArgs e)
    {
      this.pic_resize();
      this._bChangeEna = false;
      this.label_value.BackColor = this._bg_color_ch_dis;
      this.label_value.ForeColor = this._text_color_value_fail;
    }

    private void ValueView_SizeChanged(object sender, EventArgs e) => this.pic_resize();

    private void ValueView_VisibleChanged(object sender, EventArgs e)
    {
      this.label_value.Visible = true;
      this.textBox_value.Visible = false;
      this._bCancel = true;
    }

    private void label_value_Click(object sender, EventArgs e)
    {
      if (!this._bChangeEna)
        return;
      this._bToEdit = true;
    }

    private void textBox_value_Leave(object sender, EventArgs e) => this._bCancel = true;

    private void textBox_value_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Escape)
        this._bCancel = true;
      if (e.KeyData != Keys.Return)
        return;
      this._sNewValue = this.textBox_value.Text;
      this._bEnter = true;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label_value = new Label();
      this.textBox_value = new TextBox();
      this.SuspendLayout();
      this.label_value.BackColor = Color.DimGray;
      this.label_value.BorderStyle = BorderStyle.FixedSingle;
      this.label_value.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label_value.ImageAlign = ContentAlignment.MiddleRight;
      this.label_value.Location = new Point(3, 16);
      this.label_value.Name = "label_value";
      this.label_value.Size = new Size(87, 77);
      this.label_value.TabIndex = 1;
      this.label_value.Text = "0.0";
      this.label_value.TextAlign = ContentAlignment.MiddleCenter;
      this.label_value.Click += new EventHandler(this.label_value_Click);
      this.textBox_value.Location = new Point(35, 96);
      this.textBox_value.Name = "textBox_value";
      this.textBox_value.Size = new Size(94, 20);
      this.textBox_value.TabIndex = 2;
      this.textBox_value.TextAlign = HorizontalAlignment.Center;
      this.textBox_value.Visible = false;
      this.textBox_value.KeyDown += new KeyEventHandler(this.textBox_value_KeyDown);
      this.textBox_value.Leave += new EventHandler(this.textBox_value_Leave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.textBox_value);
      this.Controls.Add((Control) this.label_value);
      this.Name = nameof (ValueView);
      this.Load += new EventHandler(this.ValueView_Load);
      this.VisibleChanged += new EventHandler(this.ValueView_VisibleChanged);
      this.SizeChanged += new EventHandler(this.ValueView_SizeChanged);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public enum NumView
    {
      float_view,
      exponential_view,
    }
  }
}
