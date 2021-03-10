/*
 * Created by SharpDevelop.
 * User: Wiktor Widzisz
 * Date: 2019-03-05
 * Time: 08:44
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
	/// Description of Logger.
	/// </summary>
	public partial class Logger : UserControl
	{
		
	public enum Dir
    {
      NONE,
      OUT,
      IN,
      TAB,
      ERR
    }
		public Logger()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		    delegate void addCallback(string str, Dir dir);

    void add(string str, Dir dir)
    {
      if (LogTextBox.InvokeRequired)
      {
        addCallback d = new addCallback(add);
        this.Invoke(d, new object[] { str, dir });
      }
      else
      {
        timestamp(dir);
        LogTextBox.AppendText(str);
        LogTextBox.AppendText(Environment.NewLine);
      }
    }

    public void Log(string str)
    {
      add(str, Dir.NONE);
    }
    
    void timestamp(Dir dir)
    {
      string stamp = "";
      stamp += DateTime.Now.ToString("T") + " ";
      if (dir.Equals(Dir.OUT))
      {
        stamp += "-> ";
      }
      else if (dir.Equals(Dir.IN))
      {
        stamp += "<- ";
      }
      else if (dir.Equals(Dir.ERR))
      {
        stamp += "ER ";
      }
      else if (dir.Equals(Dir.TAB))
      {
        stamp = spaces(stamp.Length + 3);
      }
      LogTextBox.AppendText(stamp);
    }
       
    string spaces(int count)
    {
      string ret = "";
      for (int i = 0; i < count; i++)
    {
        ret += " ";
    }
      return ret;
    }
	}
}
