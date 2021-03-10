/*
 * Created by SharpDevelop.
 * User: Wiktor
 * Date: 30.05.2019
 * Time: 20:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UX_mPMA_ParamGen
{
	/// <summary>
	/// Description of Hamming.
	/// </summary>
	public partial class Hamming : UserControl
	{
		Logger logger;
		public Hamming()
		{
			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Label1Click(object sender, EventArgs e)
		{
			
		}
		public void SetLogReference(Logger arg)
    {
      logger = arg;
    }
	}
}
