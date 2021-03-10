/*
 * Created by SharpDevelop.
 * User: Wiktor Widzisz
 * Date: 2019-03-04
 * Time: 11:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace UX_mPMA_ParamGen
{
	partial class ComPort
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripComboBox ComListCombo;
		private System.Windows.Forms.ToolStripComboBox BaudListCombo;
		private System.Windows.Forms.ToolStripComboBox ParityCombo;
		private System.Windows.Forms.ToolStripComboBox StopBitsCombo;
		private System.Windows.Forms.ToolStripButton OpenCloseButton;
		private System.Windows.Forms.ToolStripButton RefreshButton;
		private System.Windows.Forms.ToolStripTextBox TimeoutTextBox;
		private System.Windows.Forms.ToolStripLabel TimeoutLabel;
		private System.Windows.Forms.ToolStripDropDownButton CSDropDown;
		private System.Windows.Forms.ToolStripMenuItem BT1CS;
		private System.Windows.Forms.ToolStripMenuItem BT2CS;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComPort));
			this.ComListCombo = new System.Windows.Forms.ToolStripComboBox();
			this.BaudListCombo = new System.Windows.Forms.ToolStripComboBox();
			this.ParityCombo = new System.Windows.Forms.ToolStripComboBox();
			this.StopBitsCombo = new System.Windows.Forms.ToolStripComboBox();
			this.OpenCloseButton = new System.Windows.Forms.ToolStripButton();
			this.RefreshButton = new System.Windows.Forms.ToolStripButton();
			this.TimeoutTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.TimeoutLabel = new System.Windows.Forms.ToolStripLabel();
			this.CSDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.BT1CS = new System.Windows.Forms.ToolStripMenuItem();
			this.BT2CS = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ComListCombo
			// 
			this.ComListCombo.Name = "ComListCombo";
			this.ComListCombo.Size = new System.Drawing.Size(75, 25);
			// 
			// BaudListCombo
			// 
			this.BaudListCombo.Items.AddRange(new object[] {
			"9600",
			"19200",
			"38400",
			"57600",
			"115200",
			"230400",
			"460800",
			"921600"});
			this.BaudListCombo.Name = "BaudListCombo";
			this.BaudListCombo.Size = new System.Drawing.Size(75, 25);
			this.BaudListCombo.Tag = "115200";
			// 
			// ParityCombo
			// 
			this.ParityCombo.Items.AddRange(new object[] {
			"Even",
			"Mark",
			"None",
			"Odd",
			"Space"});
			this.ParityCombo.Name = "ParityCombo";
			this.ParityCombo.Size = new System.Drawing.Size(75, 25);
			// 
			// StopBitsCombo
			// 
			this.StopBitsCombo.Items.AddRange(new object[] {
			"One",
			"Two"});
			this.StopBitsCombo.Name = "StopBitsCombo";
			this.StopBitsCombo.Size = new System.Drawing.Size(75, 25);
			// 
			// OpenCloseButton
			// 
			this.OpenCloseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.OpenCloseButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenCloseButton.Image")));
			this.OpenCloseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.OpenCloseButton.Name = "OpenCloseButton";
			this.OpenCloseButton.Size = new System.Drawing.Size(40, 22);
			this.OpenCloseButton.Text = "Open";
			this.OpenCloseButton.Click += new System.EventHandler(this.OpenCloseButtonClick);
			// 
			// RefreshButton
			// 
			this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.RefreshButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshButton.Image")));
			this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.Size = new System.Drawing.Size(50, 22);
			this.RefreshButton.Text = "Refresh";
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButtonClick);
			// 
			// TimeoutTextBox
			// 
			this.TimeoutTextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.TimeoutTextBox.Name = "TimeoutTextBox";
			this.TimeoutTextBox.Size = new System.Drawing.Size(50, 25);
			// 
			// TimeoutLabel
			// 
			this.TimeoutLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.TimeoutLabel.Name = "TimeoutLabel";
			this.TimeoutLabel.Size = new System.Drawing.Size(82, 22);
			this.TimeoutLabel.Text = "Timeout (ms):";
			// 
			// CSDropDown
			// 
			this.CSDropDown.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.CSDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.CSDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.BT1CS,
			this.BT2CS});
			this.CSDropDown.Image = ((System.Drawing.Image)(resources.GetObject("CSDropDown.Image")));
			this.CSDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CSDropDown.Name = "CSDropDown";
			this.CSDropDown.Size = new System.Drawing.Size(34, 22);
			this.CSDropDown.Text = "CS";
			// 
			// BT1CS
			// 
			this.BT1CS.CheckOnClick = true;
			this.BT1CS.Name = "BT1CS";
			this.BT1CS.Size = new System.Drawing.Size(116, 22);
			this.BT1CS.Text = "BT_1 CS";
			// 
			// BT2CS
			// 
			this.BT2CS.CheckOnClick = true;
			this.BT2CS.Name = "BT2CS";
			this.BT2CS.Size = new System.Drawing.Size(116, 22);
			this.BT2CS.Text = "BT_2 CS";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.ComListCombo,
			this.BaudListCombo,
			this.ParityCombo,
			this.StopBitsCombo,
			this.OpenCloseButton,
			this.RefreshButton,
			this.TimeoutTextBox,
			this.TimeoutLabel,
			this.CSDropDown});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(797, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// ComPort
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStrip1);
			this.Name = "ComPort";
			this.Size = new System.Drawing.Size(797, 28);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
