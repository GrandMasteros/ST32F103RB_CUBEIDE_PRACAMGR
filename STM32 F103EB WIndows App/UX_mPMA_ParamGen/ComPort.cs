/*
 * Created by SharpDevelop.
 * User: Wiktor Widzisz
 * Date: 2019-03-04
 * Time: 11:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;

namespace UX_mPMA_ParamGen
{
	/// <summary>
	/// Description of ComPort.
	/// </summary>
	public partial class ComPort : UserControl
	{
		SerialPort serialPort;
		Logger logger;
		int timeout;
		
		public ComPort()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
      		refresh();
      		if (ComListCombo.Items.Count > 0)
      		{
      			ComListCombo.SelectedIndex = ComListCombo.Items.Count - 1;
      		}
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			BaudListCombo.SelectedIndex = 4;
			ParityCombo.SelectedIndex = 2;
      		StopBitsCombo.SelectedIndex = 0;
      		timeoutSet(1000);
		}
			
    void RefreshButtonClick(object sender, EventArgs e)
    {
      refresh();
    }

    void refresh()
    {
      ComListCombo.Items.Clear();
      ComListCombo.Items.AddRange(SerialPort.GetPortNames());
      if (ComListCombo.Items.Count > 0)
      {
        ComListCombo.SelectedIndex = 0;
      }
      if (ComListCombo.Items.Count > 0)
      {
      	ComListCombo.SelectedIndex = ComListCombo.Items.Count - 1;
      }
    }
    
     void OpenCloseButtonClick(object sender, EventArgs e)
    {
      if (OpenCloseButton.Text == "Open")
      {
      	
        if (setParameters())
        {
          try
          {
            serialPort.Open();
            logger.Log("[ComPort] Serial port opened succesfully (" + serialPort.PortName + " " + serialPort.BaudRate
            + " " + serialPort.Parity + " " + serialPort.StopBits + ")");
            ComListCombo.Enabled = false;
            BaudListCombo.Enabled = false;
            RefreshButton.Enabled = false;
            ParityCombo.Enabled = false;
            StopBitsCombo.Enabled = false;
            //raiseEvent(ComPortOpened);
            OpenCloseButton.Text = "Close";
          }
          catch (UnauthorizedAccessException)
          {
            logger.Log("[ComPort] Failed to open serial port");
          }
          catch (Exception)
          {
          	logger.Log("[ComPort] COM port does not exist");
          }
          
        }
        else
        {
          logger.Log("[ComPort] COM port does not exist");
        }
      }
      else
      {
      	try
        {
	        logger.Log("[ComPort]" + serialPort.PortName + " closed");
	        serialPort.Close();
	        ComListCombo.Enabled = true;
	        BaudListCombo.Enabled = true;
	        RefreshButton.Enabled = true;
	        ParityCombo.Enabled = true;
	        StopBitsCombo.Enabled = true;
	        //raiseEvent(ComPortClosed);
	        OpenCloseButton.Text = "Open";
      	}
      	catch (Exception)
        {
          	logger.Log("COM port is closed, refresh and reopen!");
          	ComListCombo.Enabled = true;
	        BaudListCombo.Enabled = true;
	        RefreshButton.Enabled = true;
	        ParityCombo.Enabled = true;
	        StopBitsCombo.Enabled = true;
	        
	        //raiseEvent(ComPortClosed);
	        OpenCloseButton.Text = "Open";
        }
      }
    }
     
    bool setParameters()
    {
      bool result = true;

      if (ComListCombo.Text.Length > 0)
      {
        serialPort.PortName = ComListCombo.Text;
      }
      else
      {
        return false;
      }

      serialPort.BaudRate = int.Parse(BaudListCombo.Text);

      if (ParityCombo.Text.Equals("Even"))
      {
        serialPort.Parity = Parity.Even;
      }
      else if (ParityCombo.Text.Equals("Mark"))
      {
        serialPort.Parity = Parity.Mark;
      }
      else if (ParityCombo.Text.Equals("None"))
      {
        serialPort.Parity = Parity.None;
      }
      else if (ParityCombo.Text.Equals("Odd"))
      {
        serialPort.Parity = Parity.Odd;
      }
      else if (ParityCombo.Text.Equals("Space"))
      {
        serialPort.Parity = Parity.Space;
      }
      else
      {
        result = false;
      }

      if (StopBitsCombo.Text.Equals("One"))
      {
        serialPort.StopBits = StopBits.One;
      }
      else if (StopBitsCombo.Text.Equals("Two"))
      {
        serialPort.StopBits = StopBits.Two;
      }
      else
      {
        result = false;
      }

      return result;
    }
    
    void raiseEvent(EventHandler handler)
    {
      if (null != handler)
      {
        handler(this, EventArgs.Empty);
      }
    }

    public void SetLogReference(Logger arg)
    {
      logger = arg;
    }
    
    void timeoutSet(int val)
    {
      timeout = val;
      TimeoutTextBox.Text = timeout + "";
    }
    
    public SerialPort getSerialPort()
    {
    	return serialPort;
    }
    public void SetSerialPortReference(SerialPort arg)
	{
		serialPort = arg;
	}
    
	}
}
