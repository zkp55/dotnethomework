using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CayleyTree {
  public partial class FormMain : Form {

    private Graphics graphics;
    public int Degree1 { get; set; } = 30;//分支角度
    public int Degree2 { get; set; } = 20;//分支角度
    public double Per1 { get; set; } = 0.6;//分支长度比1
    public double Per2 { get; set; } = 0.7;//分支长度比2
    public double Length { get; set; } = 100; //主干高度
    public int N { get; set; } = 10;//迭代次数
    public Pen ThePen { get; set; } = Pens.Red;//画笔

    public FormMain() {
      InitializeComponent();
    }

    private void FormMain_Load(object sender, EventArgs e) {
      txtDegree1.DataBindings.Add("Text", this, "Degree1");
      txtDegree2.DataBindings.Add("Text", this, "Degree2");
      txtPer1.DataBindings.Add("Text", this, "Per1");
      txtPer2.DataBindings.Add("Text", this, "Per2");
      txtLength.DataBindings.Add("Text", this, "Length");
      txtN.DataBindings.Add("Text", this, "N");

      cbxColor.DataSource = new Pen[] { Pens.Red, Pens.Green, Pens.Yellow };
      cbxColor.DisplayMember = "Color";
      cbxColor.DataBindings.Add("SelectedItem", this, "ThePen");
    }

    private void btnDraw_Click(object sender, EventArgs e) {
      graphics = this.panel2.CreateGraphics();
      graphics.Clear(panel2.BackColor);
      Task.Run(()=> DrawCayleyTree(this.N, panel2.Width / 2, 
                 panel2.Height - 20, this.Length, -Math.PI / 2));
    }

    void DrawCayleyTree(int n, double x0, double y0, double len, double th) {
      if (n == 0) return;
      double x1 = x0 + len * Math.Cos(th);
      double y1 = y0 + len * Math.Sin(th);
      graphics.DrawLine(ThePen,
         (int)x0, (int)y0, (int)x1, (int)y1);
      DrawCayleyTree(n - 1, x1, y1, this.Per1 * len, th + this.Degree1 * Math.PI / 180);
      DrawCayleyTree(n - 1, x1, y1, this.Per2 * len, th - this.Degree2 * Math.PI / 180);
    }
  }
}
