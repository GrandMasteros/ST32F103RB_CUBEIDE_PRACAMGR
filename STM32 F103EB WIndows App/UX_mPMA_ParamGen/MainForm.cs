/*
 * Created by SharpDevelop.
 * User: Wiktor Widzisz
 * Date: 2019-03-01
 * Time: 12:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace UX_mPMA_ParamGen
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		SerialPort serialPort = new SerialPort();
		//MainForm mainForm =  new MainForm();
		DataGrid dataGrid = new DataGrid();
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//		
			//
			comPort.SetLogReference(logger);
			comPort.SetSerialPortReference(serialPort);
			//comPort.SetMainFormReference(mainForm);
//			logger.Log("lll");
			//logger1.
		}
			
		
		
		void LeftButtonClick(object sender, EventArgs e)
		{
			serialPort.Write("left");
		}
		
		void RightButtonClick(object sender, EventArgs e)
		{
			serialPort.Write("righ");
		}
		
		void MeasButtonClick(object sender, EventArgs e)
		{
			serialPort.Write("meas");
		}
		
		void TestButtonClick(object sender, EventArgs e)
		{
		     Stream myStream ;    
		     SaveFileDialog saveFileDialog1 = new SaveFileDialog();
		 
		     saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"  ;
		     saveFileDialog1.FilterIndex = 2 ;
		     saveFileDialog1.RestoreDirectory = true ;
		 
		     if(saveFileDialog1.ShowDialog() == DialogResult.OK)
		     {
		         if((myStream = saveFileDialog1.OpenFile()) != null)
		         {
		             // Code to write the stream goes here.
		             for (int i = 0; i < 180; i++)
		             {
		             	string string_data;
					 	serialPort.Write("righ");
					 	System.Threading.Thread.Sleep(1000);
					 	serialPort.Write("meas");
					 	System.Threading.Thread.Sleep(1000);
		             	string_data = serialPort.ReadExisting();
		             	logger.Log(string_data);
		             	string_data = string_data + '\n';
		             	byte[] bytes = Encoding.ASCII.GetBytes(string_data); 
					 	myStream.Write(bytes, 0, bytes.Length);
		             }
		             myStream.Close();
		         }
		     }
		}
	}
}
