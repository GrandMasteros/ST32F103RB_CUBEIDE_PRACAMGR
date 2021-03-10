/*
 * Created by SharpDevelop.
 * User: Wiktor Widzisz
 * Date: 2019-03-14
 * Time: 09:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.Xml;

namespace UX_mPMA_ParamGen
{
	/// <summary>
	/// Description of PatternGenerator.
	/// </summary>
	public partial class PatternGenerator : UserControl
	{
		SerialPort serialPort;
		Logger logger;
		XmlDocument xmldocGen;
		XmlDocument xmldocLoad;
		const int RGBmax = 64;
		const int RGB10Value = 10;
		const int Bytes2ValueMax = 100;
		const int Bytes2Value10 = 10;
		const int Bytes4Value10 = 10;
		const int Bytes4Value100 = 100;
		const int Bytes4Value1000 = 1000;
		const int Bytes4Value10000 = 10000;
		
		public PatternGenerator()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			ButtonGenHead.Enabled = false;	
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public void SetLogReference(Logger arg)
		{
			logger = arg;
		}
		public void SetSerialPortReference(SerialPort arg)
		{
			serialPort = arg;
		}
		
		public string convert1BYTESvalue(string stringValue)
		{
			try
			{	
			int value =  Int32.Parse(stringValue);
			if((value >= 0) && (value < Bytes2Value10))
			{
				return (value.ToString());
			}
			logger.Log("RGB value is not correct!");
			return "0";
			}
			catch (Exception)
			{
				logger.Log("RGB value is not correct!");
				return "0";
        	}
		}
		
		public string convertRGBvalue(string stringValue)
		{
			try
			{	
			int value =  Int32.Parse(stringValue);
			if( (RGB10Value <= value) && (value < RGBmax))
			{
				return value.ToString();
			}
			else if((value >= 0) && (value < RGB10Value))
			{
				return ("0" + value.ToString());
			}
			logger.Log("RGB value is not correct!");
			return "00";
			}
			catch (Exception)
			{
				logger.Log("RGB value is not correct!");
				return "00";
        	}
		}
		public string convert2BYTESvalue(string stringValue)
		{
			try
			{	
			int value =  Int32.Parse(stringValue);
			if( (Bytes2Value10 <= value) && (value < Bytes2ValueMax))
			{
				return value.ToString();
			}
			else if((value >= 0) && (value < Bytes2Value10))
			{
				return ("0" + value.ToString());
			}
			logger.Log("RGB value is not correct!");
			return "00";
			}
			catch (Exception)
			{
				logger.Log("RGB value is not correct!");
				return "00";
        	}
		}
		
		public string convert4BYTESvalue(string stringValue)
		{
			try
			{	
			int value =  Int32.Parse(stringValue);
			if( (Bytes4Value1000 <= value) && (value < Bytes4Value10000))
			{
				return value.ToString();
			}
			else if( (Bytes4Value100 <= value) && (value < Bytes4Value1000))
			{
				return ("0" + value.ToString());
			}
			else if( (Bytes4Value10 <= value) && (value < Bytes4Value100))
			{
				return ("00" + value.ToString());
			}
			else if((value >= 0) && (value < Bytes4Value10))
			{
				return ("000" + value.ToString());
			}
			logger.Log("RGB value is not correct!");
			return "0000";
			}
			catch (Exception)
			{
				logger.Log("RGB value is not correct!");
				return "0000";
        	}
		}
		
		void ButtonGenXMLClick(object sender, EventArgs e)
		{
			logger.Log("Generating XML File....");
			SaveFileDialog saveFileDialog= new SaveFileDialog();
			saveFileDialog.Filter = "XML-File | *.xml";
			xmldocGen = new XmlDocument();
			XmlNode docNode = xmldocGen.CreateXmlDeclaration("1.0", "UTF-8", null);
	        xmldocGen.AppendChild(docNode);
	
	        XmlNode paramNode = xmldocGen.CreateElement("Parameters");
	        xmldocGen.AppendChild(paramNode);
			if(checkBoxLed1.Checked == true)
			{
				generateXMLLed1(ref xmldocGen,ref paramNode);
			}
			if(checkBoxLed2.Checked == true)
			{
				generateXMLLed2(ref xmldocGen,ref paramNode);
			}
			if(checkBoxLed3.Checked == true)
			{
				generateXMLLed3(ref xmldocGen,ref paramNode);
			}
			if(checkBoxLed4.Checked == true)
			{
				generateXMLLed4(ref xmldocGen,ref paramNode);
			}
			if(checkBoxAudio.Checked == true)
			{
				generateXMLAudio(ref xmldocGen, ref paramNode);
			}
			if(checkBoxVibes.Checked == true)
			{
				generateXMLVibesMotor(ref xmldocGen, ref paramNode);
			}
			
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				xmldocGen.Save(saveFileDialog.FileName);
				logger.Log("XML File generated successfully");				
			}
			else
			{
				logger.Log("XML File generated failed");
			}
		}
		void ButtonLoadXMLClick(object sender, EventArgs e)
		{

	   		logger.Log("Loading XML File:");
	   		OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "XML-File | *.xml";
			xmldocLoad = new XmlDocument();
			
			if(openFileDialog.ShowDialog()== DialogResult.OK)
			{
			//openFileDialog.ShowDialog();
			xmldocLoad.Load(openFileDialog.FileName);	
			logger.Log("XML File opened successfully");	
			XmlTextReader reader = new XmlTextReader(openFileDialog.FileName);   
			// Skip non-significant whitespace  
			reader.WhitespaceHandling = WhitespaceHandling.Significant;
			checkBoxLed1.Checked = false;
			checkBoxLed2.Checked = false;
			checkBoxLed3.Checked = false;
			checkBoxLed4.Checked = false;
			checkBoxAudio.Checked = false;
			checkBoxVibes.Checked = false;
			  
			// Read nodes one at a time  
			while (reader.Read())  
			{  
			    // Print out info on node  
			    if(reader.Name == "LED")
			    {
				    logger.Log(reader.Name + " " + reader.GetAttribute("id") + " " +reader.GetAttribute("Red") + " " + 
				    reader.GetAttribute("Green") + " " + reader.GetAttribute("Blue") + " " + reader.GetAttribute("Time_On") + " "
				    + reader.GetAttribute("Time_Off") + " " + reader.GetAttribute("Number_of_blinks") + " " + reader.GetAttribute("Brightness")
				    + " " + reader.GetAttribute("Rising_time") + " " + reader.GetAttribute("Falling_time"));
			    	
			    	if(reader.GetAttribute("id")=="01" )
			    	{
			    		readXMLLed1(reader);
			    		checkBoxLed1.Checked = true;
			    	}
			    	
			    	if(reader.GetAttribute("id")=="02" )
			    	{
			    		readXMLLed2(reader);
			    		checkBoxLed2.Checked = true;
			    	}
			    	
			    	if(reader.GetAttribute("id")=="03" )
			    	{
			    		readXMLLed3(reader);
			    		checkBoxLed3.Checked = true;
			    	}
			    	
			    	if(reader.GetAttribute("id")=="04" )
			    	{
			    		readXMLLed4(reader);
			    		checkBoxLed4.Checked = true;
			    	}
			    }
			    else if(reader.Name == "Audio")
			    {
			    	logger.Log(reader.Name + " " + reader.GetAttribute("Test_Signal") + " " + reader.GetAttribute("Frequency") + " " + reader.GetAttribute("Volume")
					+ " " + reader.GetAttribute("Pattern"));
			    	readXMLAudio(reader);
			    	checkBoxAudio.Checked = true;
			    }
			    else if(reader.Name == "Vibes_Motor")
			    {
			    	logger.Log(reader.Name + " " + reader.GetAttribute("Velocity") + " " + reader.GetAttribute("Times_of_vibes") + " " + reader.GetAttribute("Amount_of_vibes"));
			    	readXMLVibes_Motor(reader);
			    	checkBoxVibes.Checked = true;
			    }
			}  
			}
			else
			{
				logger.Log("XML File opened fail");	
			}
		}
		
		void generateXMLLed1(ref XmlDocument xmldoc, ref XmlNode paramNode)
		{
		 	XmlNode ledNode = xmldoc.CreateElement("LED");
	        XmlAttribute ledID = xmldoc.CreateAttribute("id");
	        ledID.Value = "01";
	        ledNode.Attributes.Append(ledID);
	        paramNode.AppendChild(ledNode);
	        XmlAttribute redID = xmldoc.CreateAttribute("Red");
	        redID.Value = TextBoxR1.Text;
	        ledNode.Attributes.Append(redID);
	        paramNode.AppendChild(ledNode);

	        XmlAttribute greenID = xmldoc.CreateAttribute("Green");
	        greenID.Value = TextBoxG1.Text;
	        ledNode.Attributes.Append(greenID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute blueID = xmldoc.CreateAttribute("Blue");
	        blueID.Value = TextBoxB1.Text;
	        ledNode.Attributes.Append(blueID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute timeOnID = xmldoc.CreateAttribute("Time_On");
	        timeOnID.Value = TextBoxTimeOn1.Text;
	        ledNode.Attributes.Append(timeOnID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute timeOffID = xmldoc.CreateAttribute("Time_Off");
	        timeOffID.Value = TextBoxTimeOff1.Text;
	        ledNode.Attributes.Append(timeOffID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute numberOffBlinksID = xmldoc.CreateAttribute("Number_of_blinks");
	        numberOffBlinksID.Value = TextBoxNumberOfBlinks1.Text;
	        ledNode.Attributes.Append(numberOffBlinksID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute brightnessID = xmldoc.CreateAttribute("Brightness");
	        brightnessID.Value = TextBoxBrightness1.Text;
	        ledNode.Attributes.Append(brightnessID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute risingTimeID = xmldoc.CreateAttribute("Rising_time");
	        risingTimeID.Value = TextBoxRisingTime1.Text;
	        ledNode.Attributes.Append(risingTimeID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute fallingTimeID = xmldoc.CreateAttribute("Falling_time");
	        fallingTimeID.Value = TextBoxFallingTime1.Text;
	        ledNode.Attributes.Append(fallingTimeID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute OffsetID = xmldoc.CreateAttribute("Offset");
	        OffsetID.Value = TextBoxLed1Offset.Text;
	        ledNode.Attributes.Append(OffsetID);
	        paramNode.AppendChild(ledNode);
		}
		
		void generateXMLLed2(ref XmlDocument xmldoc, ref XmlNode paramNode)
		{
		 	XmlNode ledNode = xmldoc.CreateElement("LED");
	        XmlAttribute ledID = xmldoc.CreateAttribute("id");
	        ledID.Value = "02";
	        ledNode.Attributes.Append(ledID);
	        paramNode.AppendChild(ledNode);
	        XmlAttribute redID = xmldoc.CreateAttribute("Red");
	        redID.Value = TextBoxR2.Text;
	        ledNode.Attributes.Append(redID);
	        paramNode.AppendChild(ledNode);

	        XmlAttribute greenID = xmldoc.CreateAttribute("Green");
	        greenID.Value = TextBoxG2.Text;
	        ledNode.Attributes.Append(greenID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute blueID = xmldoc.CreateAttribute("Blue");
	        blueID.Value = TextBoxB2.Text;
	        ledNode.Attributes.Append(blueID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute timeOnID = xmldoc.CreateAttribute("Time_On");
	        timeOnID.Value = TextBoxTimeOn2.Text;
	        ledNode.Attributes.Append(timeOnID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute timeOffID = xmldoc.CreateAttribute("Time_Off");
	        timeOffID.Value = TextBoxTimeOff2.Text;
	        ledNode.Attributes.Append(timeOffID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute numberOffBlinksID = xmldoc.CreateAttribute("Number_of_blinks");
	        numberOffBlinksID.Value = TextBoxNumberOfBlinks2.Text;
	        ledNode.Attributes.Append(numberOffBlinksID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute brightnessID = xmldoc.CreateAttribute("Brightness");
	        brightnessID.Value = TextBoxBrightness2.Text;
	        ledNode.Attributes.Append(brightnessID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute risingTimeID = xmldoc.CreateAttribute("Rising_time");
	        risingTimeID.Value = TextBoxRisingTime2.Text;
	        ledNode.Attributes.Append(risingTimeID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute fallingTimeID = xmldoc.CreateAttribute("Falling_time");
	        fallingTimeID.Value = TextBoxFallingTime2.Text;
	        ledNode.Attributes.Append(fallingTimeID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute OffsetID = xmldoc.CreateAttribute("Offset");
	        OffsetID.Value = TextBoxLed2Offset.Text;
	        ledNode.Attributes.Append(OffsetID);
	        paramNode.AppendChild(ledNode);
		}
		
		void generateXMLLed3(ref XmlDocument xmldoc, ref XmlNode paramNode)
		{
		 	XmlNode ledNode = xmldoc.CreateElement("LED");
	        XmlAttribute ledID = xmldoc.CreateAttribute("id");
	        ledID.Value = "03";
	        ledNode.Attributes.Append(ledID);
	        paramNode.AppendChild(ledNode);
	        XmlAttribute redID = xmldoc.CreateAttribute("Red");
	        redID.Value = TextBoxR3.Text;
	        ledNode.Attributes.Append(redID);
	        paramNode.AppendChild(ledNode);

	        XmlAttribute greenID = xmldoc.CreateAttribute("Green");
	        greenID.Value = TextBoxG3.Text;
	        ledNode.Attributes.Append(greenID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute blueID = xmldoc.CreateAttribute("Blue");
	        blueID.Value = TextBoxB3.Text;
	        ledNode.Attributes.Append(blueID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute timeOnID = xmldoc.CreateAttribute("Time_On");
	        timeOnID.Value = TextBoxTimeOn3.Text;
	        ledNode.Attributes.Append(timeOnID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute timeOffID = xmldoc.CreateAttribute("Time_Off");
	        timeOffID.Value = TextBoxTimeOff3.Text;
	        ledNode.Attributes.Append(timeOffID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute numberOffBlinksID = xmldoc.CreateAttribute("Number_of_blinks");
	        numberOffBlinksID.Value = TextBoxNumberOfBlinks3.Text;
	        ledNode.Attributes.Append(numberOffBlinksID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute brightnessID = xmldoc.CreateAttribute("Brightness");
	        brightnessID.Value = TextBoxBrightness3.Text;
	        ledNode.Attributes.Append(brightnessID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute risingTimeID = xmldoc.CreateAttribute("Rising_time");
	        risingTimeID.Value = TextBoxRisingTime3.Text;
	        ledNode.Attributes.Append(risingTimeID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute fallingTimeID = xmldoc.CreateAttribute("Falling_time");
	        fallingTimeID.Value = TextBoxFallingTime3.Text;
	        ledNode.Attributes.Append(fallingTimeID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute OffsetID = xmldoc.CreateAttribute("Offset");
	        OffsetID.Value = TextBoxLed3Offset.Text;
	        ledNode.Attributes.Append(OffsetID);
	        paramNode.AppendChild(ledNode);
		}
				
		void generateXMLLed4(ref XmlDocument xmldoc, ref XmlNode paramNode)
		{
		 	XmlNode ledNode = xmldoc.CreateElement("LED");
	        XmlAttribute ledID = xmldoc.CreateAttribute("id");
	        ledID.Value = "04";
	        ledNode.Attributes.Append(ledID);
	        paramNode.AppendChild(ledNode);
	        XmlAttribute redID = xmldoc.CreateAttribute("Red");
	        redID.Value = TextBoxR4.Text;
	        ledNode.Attributes.Append(redID);
	        paramNode.AppendChild(ledNode);

	        XmlAttribute greenID = xmldoc.CreateAttribute("Green");
	        greenID.Value = TextBoxG4.Text;
	        ledNode.Attributes.Append(greenID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute blueID = xmldoc.CreateAttribute("Blue");
	        blueID.Value = TextBoxB4.Text;
	        ledNode.Attributes.Append(blueID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute timeOnID = xmldoc.CreateAttribute("Time_On");
	        timeOnID.Value = TextBoxTimeOn4.Text;
	        ledNode.Attributes.Append(timeOnID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute timeOffID = xmldoc.CreateAttribute("Time_Off");
	        timeOffID.Value = TextBoxTimeOff4.Text;
	        ledNode.Attributes.Append(timeOffID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute numberOffBlinksID = xmldoc.CreateAttribute("Number_of_blinks");
	        numberOffBlinksID.Value = TextBoxNumberOfBlinks4.Text;
	        ledNode.Attributes.Append(numberOffBlinksID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute brightnessID = xmldoc.CreateAttribute("Brightness");
	        brightnessID.Value = TextBoxBrightness4.Text;
	        ledNode.Attributes.Append(brightnessID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute risingTimeID = xmldoc.CreateAttribute("Rising_time");
	        risingTimeID.Value = TextBoxRisingTime4.Text;
	        ledNode.Attributes.Append(risingTimeID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute fallingTimeID = xmldoc.CreateAttribute("Falling_time");
	        fallingTimeID.Value = TextBoxFallingTime4.Text;
	        ledNode.Attributes.Append(fallingTimeID);
	        paramNode.AppendChild(ledNode);
	        
	        XmlAttribute OffsetID = xmldoc.CreateAttribute("Offset");
	        OffsetID.Value = TextBoxLed4Offset.Text;
	        ledNode.Attributes.Append(OffsetID);
	        paramNode.AppendChild(ledNode);
		}
		
		void generateXMLAudio(ref XmlDocument xmldoc, ref XmlNode paramNode)
		{
		 	XmlNode audioNode = xmldoc.CreateElement("Audio");
	        
	        XmlAttribute frequencyID = xmldoc.CreateAttribute("Frequency");
	        frequencyID.Value = TextBoxFrequency.Text;
	        audioNode.Attributes.Append(frequencyID);
	        paramNode.AppendChild(audioNode);

	        XmlAttribute volumeID = xmldoc.CreateAttribute("Volume");
	        volumeID.Value = TextBoxVolume.Text;
	        audioNode.Attributes.Append(volumeID);
	        paramNode.AppendChild(audioNode);
	        
	        XmlAttribute patternID = xmldoc.CreateAttribute("Pattern");
	        patternID.Value = ComboBoxPattern.Text;
	        audioNode.Attributes.Append(patternID);
	        paramNode.AppendChild(audioNode);
	        
	        XmlAttribute OffsetID = xmldoc.CreateAttribute("Offset");
	        OffsetID.Value = TextBoxAudioOffset.Text;
	        audioNode.Attributes.Append(OffsetID);
	        paramNode.AppendChild(audioNode);
		}
		
		void generateXMLVibesMotor(ref XmlDocument xmldoc, ref XmlNode paramNode)
		{
		 	XmlNode vibesMotorNode = xmldoc.CreateElement("Vibes_Motor");
	        XmlAttribute velocityID = xmldoc.CreateAttribute("Velocity");
	        velocityID.Value = TextBoxVelocity.Text;
	        vibesMotorNode.Attributes.Append(velocityID);
	        paramNode.AppendChild(vibesMotorNode);
	        
	        XmlAttribute timesBreak = xmldoc.CreateAttribute("Break");
	        timesBreak.Value = TextBoxBreakTime.Text;
	        vibesMotorNode.Attributes.Append(timesBreak);
	        paramNode.AppendChild(vibesMotorNode);

	        XmlAttribute amountOfSequences = xmldoc.CreateAttribute("Amount_of_Sequences");
	        amountOfSequences.Value = TextBoxAmountOfSequence.Text;
	        vibesMotorNode.Attributes.Append(amountOfSequences);
	        paramNode.AppendChild(vibesMotorNode);
	        
	        XmlAttribute OffsetID = xmldoc.CreateAttribute("Offset");
	        OffsetID.Value = TextBoxVibraOffset.Text;
	        vibesMotorNode.Attributes.Append(OffsetID);
	        paramNode.AppendChild(vibesMotorNode);
	        
	        XmlAttribute TimeOnVibesID = xmldoc.CreateAttribute("Time_On_Vibes");
	        TimeOnVibesID.Value = TextBoxTimeOnVibes.Text;
	        vibesMotorNode.Attributes.Append(TimeOnVibesID);
	        paramNode.AppendChild(vibesMotorNode);
	        
	        XmlAttribute TimeOffVibesID = xmldoc.CreateAttribute("Time_Off_Vibes");
	        TimeOffVibesID.Value = TextBoxTimeOffVibes.Text;
	        vibesMotorNode.Attributes.Append(TimeOffVibesID);
	        paramNode.AppendChild(vibesMotorNode);     
	  
	        XmlAttribute AmountOfVibesID = xmldoc.CreateAttribute("Amount_Of_Vibes");
	        AmountOfVibesID.Value = TextBoxAmountOfVibes.Text;
	        vibesMotorNode.Attributes.Append(AmountOfVibesID);
	        paramNode.AppendChild(vibesMotorNode);  
		}
		void readXMLLed1(XmlTextReader reader)
		{
			TextBoxR1.Text = reader.GetAttribute("Red");
			TextBoxG1.Text = reader.GetAttribute("Green");
			TextBoxB1.Text = reader.GetAttribute("Blue");
			TextBoxTimeOn1.Text = reader.GetAttribute("Time_On");
			TextBoxTimeOff1.Text = reader.GetAttribute("Time_Off");
			TextBoxNumberOfBlinks1.Text = reader.GetAttribute("Number_of_blinks");
			TextBoxBrightness1.Text = reader.GetAttribute("Brightness");
			TextBoxRisingTime1.Text = reader.GetAttribute("Rising_time");
			TextBoxFallingTime1.Text = reader.GetAttribute("Falling_time");
			TextBoxLed1Offset.Text = reader.GetAttribute("Offset");
		}
		
		void readXMLLed2(XmlTextReader reader)
		{
			TextBoxR2.Text = reader.GetAttribute("Red");
			TextBoxG2.Text = reader.GetAttribute("Green");
			TextBoxB2.Text = reader.GetAttribute("Blue");
			TextBoxTimeOn2.Text = reader.GetAttribute("Time_On");
			TextBoxTimeOff2.Text = reader.GetAttribute("Time_Off");
			TextBoxNumberOfBlinks2.Text = reader.GetAttribute("Number_of_blinks");
			TextBoxBrightness2.Text = reader.GetAttribute("Brightness");
			TextBoxRisingTime2.Text = reader.GetAttribute("Rising_time");
			TextBoxFallingTime2.Text = reader.GetAttribute("Falling_time");
			TextBoxLed2Offset.Text = reader.GetAttribute("Offset");
		}
		
		void readXMLLed3(XmlTextReader reader)
		{
			TextBoxR3.Text = reader.GetAttribute("Red");
			TextBoxG3.Text = reader.GetAttribute("Green");
			TextBoxB3.Text = reader.GetAttribute("Blue");
			TextBoxTimeOn3.Text = reader.GetAttribute("Time_On");
			TextBoxTimeOff3.Text = reader.GetAttribute("Time_Off");
			TextBoxNumberOfBlinks3.Text = reader.GetAttribute("Number_of_blinks");
			TextBoxBrightness3.Text = reader.GetAttribute("Brightness");
			TextBoxRisingTime3.Text = reader.GetAttribute("Rising_time");
			TextBoxFallingTime3.Text = reader.GetAttribute("Falling_time");
			TextBoxLed3Offset.Text = reader.GetAttribute("Offset");
		}
		
		void readXMLLed4(XmlTextReader reader)
		{
			TextBoxR4.Text = reader.GetAttribute("Red");
			TextBoxG4.Text = reader.GetAttribute("Green");
			TextBoxB4.Text = reader.GetAttribute("Blue");
			TextBoxTimeOn4.Text = reader.GetAttribute("Time_On");
			TextBoxTimeOff4.Text = reader.GetAttribute("Time_Off");
			TextBoxNumberOfBlinks4.Text = reader.GetAttribute("Number_of_blinks");
			TextBoxBrightness4.Text = reader.GetAttribute("Brightness");
			TextBoxRisingTime4.Text = reader.GetAttribute("Rising_time");
			TextBoxFallingTime4.Text = reader.GetAttribute("Falling_time");
			TextBoxLed4Offset.Text = reader.GetAttribute("Offset");
		}
		
		void readXMLAudio(XmlTextReader reader)
		{
			TextBoxFrequency.Text = reader.GetAttribute("Frequency");
			TextBoxVolume.Text = reader.GetAttribute("Volume");
			ComboBoxPattern.Text = reader.GetAttribute("Pattern");
			TextBoxAudioOffset.Text = reader.GetAttribute("Offset");
		}
		
		void readXMLVibes_Motor(XmlTextReader reader)
		{
			TextBoxVelocity.Text = reader.GetAttribute("Velocity");
			TextBoxBreakTime.Text = reader.GetAttribute("Break");
			TextBoxAmountOfSequence.Text = reader.GetAttribute("Amount_of_Sequences");
			TextBoxVibraOffset.Text = reader.GetAttribute("Offset");
			TextBoxTimeOnVibes.Text = reader.GetAttribute("Time_On_Vibes");
			TextBoxTimeOffVibes.Text = reader.GetAttribute("Time_Off_Vibes");
			TextBoxAmountOfVibes.Text = reader.GetAttribute("Amount_Of_Vibes");
		}
		
		void StartButtonClick(object sender, EventArgs e)
		{
			try
			{
			serialPort.Write(StartButton.Text + '\r');
			logger.Log(StartButton.Text );
			}
			catch(Exception)
			{
				logger.Log("[ComPort] COM port is closed, refresh and reopen!");
			}
		}
		void StopButtonClick(object sender, EventArgs e)
		{
			try
			{
			serialPort.Write(StopButton.Text + '\r');
			logger.Log(StopButton.Text);
			}
			catch(Exception)
			{
				logger.Log("[ComPort] COM port is closed, refresh and reopen!");
			}
		}
		void SendButtonAllClick(object sender, EventArgs e)
		{
			if(checkBoxLed1.Checked == true)
			{
				try
				{
				serialPort.Write(toolStripLabel1.Text+ convertRGBvalue(TextBoxR1.Text) + convertRGBvalue(TextBoxG1.Text)  + convertRGBvalue(TextBoxB1.Text) +
				                 convert4BYTESvalue(TextBoxTimeOn1.Text) + convert4BYTESvalue(TextBoxTimeOff1.Text) + convert2BYTESvalue(TextBoxNumberOfBlinks1.Text) +
				                 convert2BYTESvalue(TextBoxBrightness1.Text) + convert1BYTESvalue(TextBoxRisingTime1.Text) + convert1BYTESvalue(TextBoxFallingTime1.Text) + 
				                 convert4BYTESvalue(TextBoxLed1Offset.Text) +'\r');
				logger.Log(toolStripLabel1.Text+ convertRGBvalue(TextBoxR1.Text) + convertRGBvalue(TextBoxG1.Text)  + convertRGBvalue(TextBoxB1.Text) +
				           convert4BYTESvalue(TextBoxTimeOn1.Text) + convert4BYTESvalue(TextBoxTimeOff1.Text) + convert2BYTESvalue(TextBoxNumberOfBlinks1.Text) +
				           convert2BYTESvalue(TextBoxBrightness1.Text) + convert1BYTESvalue(TextBoxRisingTime1.Text) + convert1BYTESvalue(TextBoxFallingTime1.Text) +
				           convert4BYTESvalue(TextBoxLed1Offset.Text));
				}
				catch(Exception)
				{
					logger.Log("[ComPort] COM port is closed, refresh and reopen!");
				}
			}
			if(checkBoxLed2.Checked == true)
			{
				try
				{
				serialPort.Write(toolStripLabel2.Text+ convertRGBvalue(TextBoxR2.Text) + convertRGBvalue(TextBoxG2.Text)  + convertRGBvalue(TextBoxB2.Text) +
				                 convert4BYTESvalue(TextBoxTimeOn2.Text) + convert4BYTESvalue(TextBoxTimeOff2.Text) + convert2BYTESvalue(TextBoxNumberOfBlinks2.Text) +
				                 convert2BYTESvalue(TextBoxBrightness2.Text) + convert1BYTESvalue(TextBoxRisingTime2.Text) + convert1BYTESvalue(TextBoxFallingTime2.Text) + 
				                 convert4BYTESvalue(TextBoxLed2Offset.Text) + '\r');
				logger.Log(toolStripLabel2.Text+ convertRGBvalue(TextBoxR2.Text) + convertRGBvalue(TextBoxG2.Text)  + convertRGBvalue(TextBoxB2.Text)  +
				           convert4BYTESvalue(TextBoxTimeOn2.Text) + convert4BYTESvalue(TextBoxTimeOff2.Text) + convert2BYTESvalue(TextBoxNumberOfBlinks2.Text) +
				           convert2BYTESvalue(TextBoxBrightness2.Text) + convert1BYTESvalue(TextBoxRisingTime2.Text) + convert1BYTESvalue(TextBoxFallingTime2.Text) +
				           convert4BYTESvalue(TextBoxLed2Offset.Text));
				}
				catch(Exception)
				{
					logger.Log("[ComPort] COM port is closed, refresh and reopen!");
				}
			}
			if(checkBoxLed3.Checked == true)
			{
				try
				{
				serialPort.Write(toolStripLabel3.Text+ convertRGBvalue(TextBoxR3.Text) + convertRGBvalue(TextBoxG3.Text)  + convertRGBvalue(TextBoxB3.Text) +
				                 convert4BYTESvalue(TextBoxTimeOn3.Text) + convert4BYTESvalue(TextBoxTimeOff3.Text) + convert2BYTESvalue(TextBoxNumberOfBlinks3.Text) +
				                 convert2BYTESvalue(TextBoxBrightness3.Text) + convert1BYTESvalue(TextBoxRisingTime3.Text) + convert1BYTESvalue(TextBoxFallingTime3.Text) + 
				                 convert4BYTESvalue(TextBoxLed3Offset.Text) + '\r');
				logger.Log(toolStripLabel3.Text+ convertRGBvalue(TextBoxR3.Text) + convertRGBvalue(TextBoxG3.Text)  + convertRGBvalue(TextBoxB3.Text) +
				           convert4BYTESvalue(TextBoxTimeOn3.Text) + convert4BYTESvalue(TextBoxTimeOff3.Text) + convert2BYTESvalue(TextBoxNumberOfBlinks3.Text) +
				           convert2BYTESvalue(TextBoxBrightness3.Text) + convert1BYTESvalue(TextBoxRisingTime3.Text) + convert1BYTESvalue(TextBoxFallingTime3.Text) +
				           convert4BYTESvalue(TextBoxLed3Offset.Text));
				}
					catch(Exception)
				{
					logger.Log("[ComPort] COM port is closed, refresh and reopen!");
				}
			}
			if(checkBoxLed4.Checked == true)
			{
				try
				{
				serialPort.Write(toolStripLabel4.Text+ convertRGBvalue(TextBoxR4.Text) + convertRGBvalue(TextBoxG4.Text)  + convertRGBvalue(TextBoxB4.Text) +
				                 convert4BYTESvalue(TextBoxTimeOn4.Text) + convert4BYTESvalue(TextBoxTimeOff4.Text) + convert2BYTESvalue(TextBoxNumberOfBlinks4.Text) +
				                 convert2BYTESvalue(TextBoxBrightness4.Text) + convert1BYTESvalue(TextBoxRisingTime4.Text) + convert1BYTESvalue(TextBoxFallingTime4.Text) + 
				                 convert4BYTESvalue(TextBoxLed4Offset.Text) + '\r');
				logger.Log(toolStripLabel4.Text+ convertRGBvalue(TextBoxR4.Text) + convertRGBvalue(TextBoxG4.Text)  + convertRGBvalue(TextBoxB4.Text) +
				           convert4BYTESvalue(TextBoxTimeOn4.Text) + convert4BYTESvalue(TextBoxTimeOff4.Text) + convert2BYTESvalue(TextBoxNumberOfBlinks4.Text) +
				           convert2BYTESvalue(TextBoxBrightness4.Text) + convert1BYTESvalue(TextBoxRisingTime4.Text) + convert1BYTESvalue(TextBoxFallingTime4.Text) +
				           convert4BYTESvalue(TextBoxLed4Offset.Text));
				}
				catch(Exception)
				{
					logger.Log("[ComPort] COM port is closed, refresh and reopen!");
				}
			}
			if(checkBoxVibes.Checked == true)
			{
				try
				{
				serialPort.Write("VIB"+convert4BYTESvalue(TextBoxVelocity.Text) + convert4BYTESvalue(TextBoxBreakTime.Text) +
					                   convert2BYTESvalue(TextBoxAmountOfSequence.Text) + convert4BYTESvalue(TextBoxVibraOffset.Text) + 
					                   convert4BYTESvalue(TextBoxTimeOnVibes.Text) + convert4BYTESvalue(TextBoxTimeOffVibes.Text) + convert4BYTESvalue(TextBoxAmountOfVibes.Text) +'\r');
				logger.Log("VIB"+convert4BYTESvalue(TextBoxVelocity.Text) + convert4BYTESvalue(TextBoxBreakTime.Text) + 
					             convert2BYTESvalue(TextBoxAmountOfSequence.Text) + convert4BYTESvalue(TextBoxVibraOffset.Text) +
					             convert4BYTESvalue(TextBoxTimeOnVibes.Text) + convert4BYTESvalue(TextBoxTimeOffVibes.Text) + convert4BYTESvalue(TextBoxAmountOfVibes.Text));
				}
				catch(Exception)
				{
					logger.Log("[ComPort] COM port is closed, refresh and reopen!");
				}
			}
			if(checkBoxAudio.Checked == true)
			{
				string sound_option = "00";
				if(ComboBoxPattern.Text == "SoftBell_Notification7_3razy")
				{
					sound_option = "01";
				}
				else if(ComboBoxPattern.Text == "Its_On")
				{
					sound_option = "02";
				}
				else if(ComboBoxPattern.Text == "SoftBell_Error4")
				{
					sound_option = "03";
				}
				else if(ComboBoxPattern.Text == "SoftBell_Done8")
				{
					sound_option = "04";
				}
				else if(ComboBoxPattern.Text == "SoftBell_Done10_2razy")
				{
					sound_option = "05";
				}
				else if(ComboBoxPattern.Text == "SoftBell_Note2")
				{
					sound_option = "06";
				}
				else if(ComboBoxPattern.Text == "SoftBell_Note3")
				{
					sound_option = "07";
				}
				else if(ComboBoxPattern.Text == "SoftBell_Notification15")
				{
					sound_option = "08";
				}
				else if(ComboBoxPattern.Text == "SoftBell_Error6")
				{
					sound_option = "09";
				}
				else if(ComboBoxPattern.Text == "SoftBell_Error2")
				{
					sound_option = "10";
				}
				else if(ComboBoxPattern.Text == "sin_500Hz")
				{
					sound_option = "11";
				}
				else if(ComboBoxPattern.Text == "sin_1000Hz")
				{
					sound_option = "12";
				}
				try
				{
				serialPort.Write("AUDIO" +  sound_option + convert2BYTESvalue(TextBoxVolume.Text) +  convert4BYTESvalue(TextBoxFrequency.Text) + 
					                 convert4BYTESvalue(TextBoxAudioOffset.Text) +'\r');
				logger.Log("AUDIO"+ sound_option + convert2BYTESvalue(TextBoxVolume.Text) +  convert4BYTESvalue(TextBoxFrequency.Text) +
					          convert4BYTESvalue(TextBoxAudioOffset.Text));
				}
				catch(Exception)
				{
					logger.Log("[ComPort] COM port is closed, refresh and reopen!");
				}
			}
		}
		void TextBoxR1Click(object sender, EventArgs e)
		{
	
		}
		void Label8Click(object sender, EventArgs e)
		{
	
		}
		void Label11Click(object sender, EventArgs e)
		{
	
		}
		void Label14Click(object sender, EventArgs e)
		{
	
		}
		void GroupBoxVibesMotorEnter(object sender, EventArgs e)
		{
	
		}

	}
}
