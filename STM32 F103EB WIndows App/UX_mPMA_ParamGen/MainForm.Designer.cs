/*
 * Created by SharpDevelop.
 * User: Wiktor Widzisz
 * Date: 2019-03-01
 * Time: 12:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace UX_mPMA_ParamGen
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		public System.ComponentModel.IContainer components = null;
		public UX_mPMA_ParamGen.ComPort comPort;

		
		/// <summary>
		/// Disposes resources used by the form.
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
			this.comPort = new UX_mPMA_ParamGen.ComPort();
			this.LeftButton = new System.Windows.Forms.Button();
			this.RightButton = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.logger = new UX_mPMA_ParamGen.Logger();
			this.MeasButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comPort
			// 
			this.comPort.Location = new System.Drawing.Point(37, 444);
			this.comPort.Name = "comPort";
			this.comPort.Size = new System.Drawing.Size(864, 36);
			this.comPort.TabIndex = 0;
			// 
			// LeftButton
			// 
			this.LeftButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.LeftButton.Location = new System.Drawing.Point(12, 12);
			this.LeftButton.Name = "LeftButton";
			this.LeftButton.Size = new System.Drawing.Size(158, 65);
			this.LeftButton.TabIndex = 2;
			this.LeftButton.Text = "Left";
			this.LeftButton.UseVisualStyleBackColor = true;
			this.LeftButton.Click += new System.EventHandler(this.LeftButtonClick);
			// 
			// RightButton
			// 
			this.RightButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.RightButton.Location = new System.Drawing.Point(176, 12);
			this.RightButton.Name = "RightButton";
			this.RightButton.Size = new System.Drawing.Size(157, 65);
			this.RightButton.TabIndex = 3;
			this.RightButton.Text = "Right";
			this.RightButton.UseVisualStyleBackColor = true;
			this.RightButton.Click += new System.EventHandler(this.RightButtonClick);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.button4.Location = new System.Drawing.Point(677, 12);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(143, 65);
			this.button4.TabIndex = 5;
			this.button4.Text = "Save";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.button5.Location = new System.Drawing.Point(512, 12);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(143, 65);
			this.button5.TabIndex = 6;
			this.button5.Text = "Stop";
			this.button5.UseVisualStyleBackColor = true;
			// 
			// logger
			// 
			this.logger.Location = new System.Drawing.Point(26, 157);
			this.logger.Name = "logger";
			this.logger.Size = new System.Drawing.Size(864, 229);
			this.logger.TabIndex = 0;
			// 
			// MeasButton
			// 
			this.MeasButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.MeasButton.Location = new System.Drawing.Point(339, 12);
			this.MeasButton.Name = "MeasButton";
			this.MeasButton.Size = new System.Drawing.Size(157, 65);
			this.MeasButton.TabIndex = 7;
			this.MeasButton.Text = "Meas";
			this.MeasButton.UseVisualStyleBackColor = true;
			this.MeasButton.Click += new System.EventHandler(this.MeasButtonClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(930, 507);
			this.Controls.Add(this.MeasButton);
			this.Controls.Add(this.logger);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.RightButton);
			this.Controls.Add(this.LeftButton);
			this.Controls.Add(this.comPort);
			this.Name = "MainForm";
			this.Text = "UX_mPMA_ParamGen";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button MeasButton;
		private UX_mPMA_ParamGen.Logger logger;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button RightButton;
		private System.Windows.Forms.Button LeftButton;
	}
}
