namespace VideoLOB
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            videoSeed = new RichTextBox();
            GenerateVideoButton = new Button();
            GenerateAudioButton = new Button();
            audioSeed = new RichTextBox();
            durationText = new TextBox();
            frequencyText = new TextBox();
            sampleRateText = new TextBox();
            GenerateImageButton = new Button();
            GenerateModelButton = new Button();
            channelsBox = new ComboBox();
            depthBox = new ComboBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            audioParametersText = new TextBox();
            textBox4 = new TextBox();
            verticesNumeric = new NumericUpDown();
            facesNumeric = new NumericUpDown();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            textBox8 = new TextBox();
            widthNumeric = new NumericUpDown();
            heightNumeric = new NumericUpDown();
            textBox9 = new TextBox();
            textBox10 = new TextBox();
            textBox11 = new TextBox();
            durationNumeric = new NumericUpDown();
            sampleNumeric = new NumericUpDown();
            frequencyNumeric = new NumericUpDown();
            scaleNumeric = new NumericUpDown();
            textBox12 = new TextBox();
            GenerateBookButton = new Button();
            titleNumeric = new NumericUpDown();
            contentNumeric = new NumericUpDown();
            textBox13 = new TextBox();
            textBox14 = new TextBox();
            textBox15 = new TextBox();
            textBox16 = new TextBox();
            textBox17 = new TextBox();
            textBox18 = new TextBox();
            lineNumeric = new NumericUpDown();
            textBox19 = new TextBox();
            textBox20 = new TextBox();
            paragraphNumeric = new NumericUpDown();
            textBox21 = new TextBox();
            trackBarB = new TrackBar();
            trackBarA = new TrackBar();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            checkBox1 = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            button10 = new Button();
            button11 = new Button();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)verticesNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)facesNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)widthNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heightNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)durationNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sampleNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)frequencyNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)scaleNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)titleNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)contentNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lineNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)paragraphNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarA).BeginInit();
            SuspendLayout();
            // 
            // videoSeed
            // 
            videoSeed.Location = new Point(667, 324);
            videoSeed.Name = "videoSeed";
            videoSeed.Size = new Size(121, 114);
            videoSeed.TabIndex = 0;
            videoSeed.Text = "";
            videoSeed.Visible = false;
            // 
            // GenerateVideoButton
            // 
            GenerateVideoButton.Location = new Point(667, 295);
            GenerateVideoButton.Name = "GenerateVideoButton";
            GenerateVideoButton.Size = new Size(121, 23);
            GenerateVideoButton.TabIndex = 1;
            GenerateVideoButton.Text = "Generate Video";
            GenerateVideoButton.UseVisualStyleBackColor = true;
            GenerateVideoButton.Click += GenerateVideoButton_Click;
            // 
            // GenerateAudioButton
            // 
            GenerateAudioButton.Location = new Point(91, 187);
            GenerateAudioButton.Name = "GenerateAudioButton";
            GenerateAudioButton.Size = new Size(121, 23);
            GenerateAudioButton.TabIndex = 3;
            GenerateAudioButton.Text = "Generate Audio";
            GenerateAudioButton.UseVisualStyleBackColor = true;
            GenerateAudioButton.Click += GenerateAudioButton_Click;
            // 
            // audioSeed
            // 
            audioSeed.Location = new Point(91, 216);
            audioSeed.Name = "audioSeed";
            audioSeed.Size = new Size(121, 71);
            audioSeed.TabIndex = 4;
            audioSeed.Text = "";
            audioSeed.Visible = false;
            // 
            // durationText
            // 
            durationText.Enabled = false;
            durationText.Location = new Point(218, 97);
            durationText.Name = "durationText";
            durationText.Size = new Size(149, 23);
            durationText.TabIndex = 7;
            durationText.Text = "1 - 3600 Seconds ( 1 Hour )";
            // 
            // frequencyText
            // 
            frequencyText.Enabled = false;
            frequencyText.Location = new Point(218, 69);
            frequencyText.Name = "frequencyText";
            frequencyText.Size = new Size(149, 23);
            frequencyText.TabIndex = 8;
            frequencyText.Text = "1 - 440 Hz";
            // 
            // sampleRateText
            // 
            sampleRateText.Enabled = false;
            sampleRateText.Location = new Point(218, 41);
            sampleRateText.Name = "sampleRateText";
            sampleRateText.Size = new Size(149, 23);
            sampleRateText.TabIndex = 9;
            sampleRateText.Text = "1 - 44100 Hz";
            // 
            // GenerateImageButton
            // 
            GenerateImageButton.Location = new Point(399, 244);
            GenerateImageButton.Name = "GenerateImageButton";
            GenerateImageButton.Size = new Size(121, 23);
            GenerateImageButton.TabIndex = 11;
            GenerateImageButton.Text = "Generate Image";
            GenerateImageButton.UseVisualStyleBackColor = true;
            GenerateImageButton.Click += GenerateImageButton_Click;
            // 
            // GenerateModelButton
            // 
            GenerateModelButton.Location = new Point(613, 158);
            GenerateModelButton.Name = "GenerateModelButton";
            GenerateModelButton.Size = new Size(121, 23);
            GenerateModelButton.TabIndex = 13;
            GenerateModelButton.Text = "Generate 3D Model";
            GenerateModelButton.UseVisualStyleBackColor = true;
            GenerateModelButton.Click += GenerateModelButton_Click;
            // 
            // channelsBox
            // 
            channelsBox.FormattingEnabled = true;
            channelsBox.Items.AddRange(new object[] { "1", "2" });
            channelsBox.Location = new Point(91, 128);
            channelsBox.Name = "channelsBox";
            channelsBox.Size = new Size(121, 23);
            channelsBox.TabIndex = 14;
            channelsBox.Text = "Channels";
            // 
            // depthBox
            // 
            depthBox.FormattingEnabled = true;
            depthBox.Items.AddRange(new object[] { "8", "16", "24", "32" });
            depthBox.Location = new Point(91, 158);
            depthBox.Name = "depthBox";
            depthBox.Size = new Size(121, 23);
            depthBox.TabIndex = 15;
            depthBox.Text = "Bit Depth";
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new Point(12, 41);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(73, 23);
            textBox1.TabIndex = 16;
            textBox1.Text = "Sample Rate";
            textBox1.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Location = new Point(12, 69);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(73, 23);
            textBox2.TabIndex = 17;
            textBox2.Text = "Frequency";
            textBox2.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox3
            // 
            textBox3.Enabled = false;
            textBox3.Location = new Point(12, 98);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(73, 23);
            textBox3.TabIndex = 18;
            textBox3.Text = "Duration";
            textBox3.TextAlign = HorizontalAlignment.Right;
            // 
            // audioParametersText
            // 
            audioParametersText.Enabled = false;
            audioParametersText.Location = new Point(91, 12);
            audioParametersText.Name = "audioParametersText";
            audioParametersText.Size = new Size(121, 23);
            audioParametersText.TabIndex = 22;
            audioParametersText.Text = "Audio Parameters";
            audioParametersText.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            textBox4.Enabled = false;
            textBox4.Location = new Point(613, 12);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(120, 23);
            textBox4.TabIndex = 23;
            textBox4.Text = "3D Model Parameters";
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // verticesNumeric
            // 
            verticesNumeric.Location = new Point(613, 42);
            verticesNumeric.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            verticesNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            verticesNumeric.Name = "verticesNumeric";
            verticesNumeric.Size = new Size(120, 23);
            verticesNumeric.TabIndex = 24;
            verticesNumeric.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // facesNumeric
            // 
            facesNumeric.Location = new Point(613, 71);
            facesNumeric.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            facesNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            facesNumeric.Name = "facesNumeric";
            facesNumeric.Size = new Size(120, 23);
            facesNumeric.TabIndex = 25;
            facesNumeric.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // textBox5
            // 
            textBox5.Enabled = false;
            textBox5.Location = new Point(557, 42);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(50, 23);
            textBox5.TabIndex = 26;
            textBox5.Text = "Vertices";
            textBox5.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox6
            // 
            textBox6.Enabled = false;
            textBox6.Location = new Point(557, 71);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(50, 23);
            textBox6.TabIndex = 27;
            textBox6.Text = "Faces";
            textBox6.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox7
            // 
            textBox7.Enabled = false;
            textBox7.Location = new Point(613, 100);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(120, 23);
            textBox7.TabIndex = 28;
            textBox7.Text = "1 - 1000";
            textBox7.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox8
            // 
            textBox8.Enabled = false;
            textBox8.Location = new Point(401, 129);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(120, 23);
            textBox8.TabIndex = 29;
            textBox8.Text = "Image Parameters";
            textBox8.TextAlign = HorizontalAlignment.Center;
            // 
            // widthNumeric
            // 
            widthNumeric.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            widthNumeric.Location = new Point(400, 158);
            widthNumeric.Maximum = new decimal(new int[] { 1920, 0, 0, 0 });
            widthNumeric.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            widthNumeric.Name = "widthNumeric";
            widthNumeric.Size = new Size(120, 23);
            widthNumeric.TabIndex = 30;
            widthNumeric.Value = new decimal(new int[] { 640, 0, 0, 0 });
            // 
            // heightNumeric
            // 
            heightNumeric.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            heightNumeric.Location = new Point(400, 187);
            heightNumeric.Maximum = new decimal(new int[] { 1080, 0, 0, 0 });
            heightNumeric.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            heightNumeric.Name = "heightNumeric";
            heightNumeric.Size = new Size(120, 23);
            heightNumeric.TabIndex = 31;
            heightNumeric.Value = new decimal(new int[] { 480, 0, 0, 0 });
            // 
            // textBox9
            // 
            textBox9.Enabled = false;
            textBox9.Location = new Point(345, 158);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(49, 23);
            textBox9.TabIndex = 32;
            textBox9.Text = "Width";
            textBox9.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox10
            // 
            textBox10.Enabled = false;
            textBox10.Location = new Point(345, 186);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(50, 23);
            textBox10.TabIndex = 33;
            textBox10.Text = "Height";
            textBox10.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox11
            // 
            textBox11.Enabled = false;
            textBox11.Location = new Point(401, 216);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(119, 23);
            textBox11.TabIndex = 34;
            textBox11.Text = "2 - 1920 x 2 - 1080";
            // 
            // durationNumeric
            // 
            durationNumeric.Location = new Point(92, 98);
            durationNumeric.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            durationNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            durationNumeric.Name = "durationNumeric";
            durationNumeric.Size = new Size(120, 23);
            durationNumeric.TabIndex = 35;
            durationNumeric.Value = new decimal(new int[] { 3600, 0, 0, 0 });
            // 
            // sampleNumeric
            // 
            sampleNumeric.Location = new Point(92, 41);
            sampleNumeric.Maximum = new decimal(new int[] { 44100, 0, 0, 0 });
            sampleNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            sampleNumeric.Name = "sampleNumeric";
            sampleNumeric.Size = new Size(120, 23);
            sampleNumeric.TabIndex = 36;
            sampleNumeric.Value = new decimal(new int[] { 44100, 0, 0, 0 });
            // 
            // frequencyNumeric
            // 
            frequencyNumeric.Location = new Point(92, 69);
            frequencyNumeric.Maximum = new decimal(new int[] { 440, 0, 0, 0 });
            frequencyNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            frequencyNumeric.Name = "frequencyNumeric";
            frequencyNumeric.Size = new Size(120, 23);
            frequencyNumeric.TabIndex = 37;
            frequencyNumeric.Value = new decimal(new int[] { 440, 0, 0, 0 });
            // 
            // scaleNumeric
            // 
            scaleNumeric.Location = new Point(613, 129);
            scaleNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            scaleNumeric.Name = "scaleNumeric";
            scaleNumeric.Size = new Size(120, 23);
            scaleNumeric.TabIndex = 38;
            scaleNumeric.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // textBox12
            // 
            textBox12.Enabled = false;
            textBox12.Location = new Point(557, 129);
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(50, 23);
            textBox12.TabIndex = 39;
            textBox12.Text = "Scale";
            textBox12.TextAlign = HorizontalAlignment.Right;
            // 
            // GenerateBookButton
            // 
            GenerateBookButton.Location = new Point(91, 300);
            GenerateBookButton.Name = "GenerateBookButton";
            GenerateBookButton.Size = new Size(121, 23);
            GenerateBookButton.TabIndex = 40;
            GenerateBookButton.Text = "Generate Book";
            GenerateBookButton.UseVisualStyleBackColor = true;
            GenerateBookButton.Click += GenerateBookButton_Click;
            // 
            // titleNumeric
            // 
            titleNumeric.Location = new Point(118, 329);
            titleNumeric.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            titleNumeric.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            titleNumeric.Name = "titleNumeric";
            titleNumeric.Size = new Size(120, 23);
            titleNumeric.TabIndex = 41;
            titleNumeric.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // contentNumeric
            // 
            contentNumeric.Increment = new decimal(new int[] { 80, 0, 0, 0 });
            contentNumeric.Location = new Point(118, 358);
            contentNumeric.Maximum = new decimal(new int[] { 80000000, 0, 0, 0 });
            contentNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            contentNumeric.Name = "contentNumeric";
            contentNumeric.Size = new Size(120, 23);
            contentNumeric.TabIndex = 42;
            contentNumeric.Value = new decimal(new int[] { 800, 0, 0, 0 });
            // 
            // textBox13
            // 
            textBox13.Enabled = false;
            textBox13.Location = new Point(12, 357);
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(100, 23);
            textBox13.TabIndex = 43;
            textBox13.Text = "Content Length";
            textBox13.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox14
            // 
            textBox14.Enabled = false;
            textBox14.Location = new Point(12, 328);
            textBox14.Name = "textBox14";
            textBox14.Size = new Size(100, 23);
            textBox14.TabIndex = 44;
            textBox14.Text = "Title Length";
            textBox14.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox15
            // 
            textBox15.Enabled = false;
            textBox15.Location = new Point(244, 357);
            textBox15.Name = "textBox15";
            textBox15.Size = new Size(77, 23);
            textBox15.TabIndex = 45;
            textBox15.Text = "1 - 80000000";
            // 
            // textBox16
            // 
            textBox16.Enabled = false;
            textBox16.Location = new Point(244, 328);
            textBox16.Name = "textBox16";
            textBox16.Size = new Size(77, 23);
            textBox16.TabIndex = 46;
            textBox16.Text = "8 - 128";
            // 
            // textBox17
            // 
            textBox17.Enabled = false;
            textBox17.Location = new Point(739, 128);
            textBox17.Name = "textBox17";
            textBox17.Size = new Size(49, 23);
            textBox17.TabIndex = 47;
            textBox17.Text = "1 - 100";
            // 
            // textBox18
            // 
            textBox18.Enabled = false;
            textBox18.Location = new Point(12, 386);
            textBox18.Name = "textBox18";
            textBox18.Size = new Size(100, 23);
            textBox18.TabIndex = 48;
            textBox18.Text = "Line Length";
            textBox18.TextAlign = HorizontalAlignment.Right;
            // 
            // lineNumeric
            // 
            lineNumeric.Location = new Point(118, 387);
            lineNumeric.Maximum = new decimal(new int[] { 80, 0, 0, 0 });
            lineNumeric.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            lineNumeric.Name = "lineNumeric";
            lineNumeric.Size = new Size(120, 23);
            lineNumeric.TabIndex = 49;
            lineNumeric.Value = new decimal(new int[] { 80, 0, 0, 0 });
            // 
            // textBox19
            // 
            textBox19.Enabled = false;
            textBox19.Location = new Point(244, 386);
            textBox19.Name = "textBox19";
            textBox19.Size = new Size(77, 23);
            textBox19.TabIndex = 50;
            textBox19.Text = "10 - 80";
            // 
            // textBox20
            // 
            textBox20.Enabled = false;
            textBox20.Location = new Point(244, 415);
            textBox20.Name = "textBox20";
            textBox20.Size = new Size(77, 23);
            textBox20.TabIndex = 53;
            textBox20.Text = "10 - 80";
            // 
            // paragraphNumeric
            // 
            paragraphNumeric.Location = new Point(118, 416);
            paragraphNumeric.Maximum = new decimal(new int[] { 80, 0, 0, 0 });
            paragraphNumeric.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            paragraphNumeric.Name = "paragraphNumeric";
            paragraphNumeric.Size = new Size(120, 23);
            paragraphNumeric.TabIndex = 52;
            paragraphNumeric.Value = new decimal(new int[] { 40, 0, 0, 0 });
            // 
            // textBox21
            // 
            textBox21.Enabled = false;
            textBox21.Location = new Point(12, 415);
            textBox21.Name = "textBox21";
            textBox21.Size = new Size(100, 23);
            textBox21.TabIndex = 51;
            textBox21.Text = "Paragraph Length";
            textBox21.TextAlign = HorizontalAlignment.Right;
            // 
            // trackBarB
            // 
            trackBarB.Location = new Point(327, 394);
            trackBarB.Maximum = 1114111;
            trackBarB.Minimum = 32;
            trackBarB.Name = "trackBarB";
            trackBarB.Size = new Size(104, 45);
            trackBarB.TabIndex = 54;
            trackBarB.Value = 1114111;
            trackBarB.Scroll += trackBar2_Scroll;
            // 
            // trackBarA
            // 
            trackBarA.Location = new Point(327, 328);
            trackBarA.Maximum = 1114112;
            trackBarA.Minimum = 32;
            trackBarA.Name = "trackBarA";
            trackBarA.Size = new Size(104, 45);
            trackBarA.TabIndex = 55;
            trackBarA.Value = 32;
            trackBarA.Scroll += trackBar1_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(437, 331);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 56;
            label1.Text = "32";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(437, 395);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 57;
            label2.Text = "1114111";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(327, 308);
            label3.Name = "label3";
            label3.Size = new Size(151, 15);
            label3.TabIndex = 58;
            label3.Text = "Character Range (UniCode)";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Enabled = false;
            checkBox1.Location = new Point(613, 186);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(90, 19);
            checkBox1.TabIndex = 59;
            checkBox1.Text = "Solid Object";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(484, 308);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 60;
            button1.Text = "ASCII";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(484, 337);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 61;
            button2.Text = "Arabic";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(484, 366);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 62;
            button3.Text = "Chinese";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(484, 395);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 63;
            button4.Text = "Cyrillic";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(484, 424);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 64;
            button5.Text = "Devanagari";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(565, 279);
            button6.Name = "button6";
            button6.Size = new Size(75, 23);
            button6.TabIndex = 69;
            button6.Text = "Greek";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(565, 308);
            button7.Name = "button7";
            button7.Size = new Size(75, 23);
            button7.TabIndex = 68;
            button7.Text = "Hebrew";
            button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Location = new Point(565, 337);
            button8.Name = "button8";
            button8.Size = new Size(75, 23);
            button8.TabIndex = 67;
            button8.Text = "Japanese";
            button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new Point(565, 366);
            button9.Name = "button9";
            button9.Size = new Size(75, 23);
            button9.TabIndex = 66;
            button9.Text = "Korean";
            button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            button10.Location = new Point(565, 395);
            button10.Name = "button10";
            button10.Size = new Size(75, 23);
            button10.TabIndex = 65;
            button10.Text = "Thai";
            button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            button11.Location = new Point(565, 424);
            button11.Name = "button11";
            button11.Size = new Size(75, 23);
            button11.TabIndex = 70;
            button11.Text = "Turkish";
            button11.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(466, 283);
            label4.Name = "label4";
            label4.Size = new Size(93, 15);
            label4.TabIndex = 71;
            label4.Text = "UniCode Presets";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(button11);
            Controls.Add(button6);
            Controls.Add(button7);
            Controls.Add(button8);
            Controls.Add(button9);
            Controls.Add(button10);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(checkBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(trackBarA);
            Controls.Add(trackBarB);
            Controls.Add(textBox20);
            Controls.Add(paragraphNumeric);
            Controls.Add(textBox21);
            Controls.Add(textBox19);
            Controls.Add(lineNumeric);
            Controls.Add(textBox18);
            Controls.Add(textBox17);
            Controls.Add(textBox16);
            Controls.Add(textBox15);
            Controls.Add(textBox14);
            Controls.Add(textBox13);
            Controls.Add(contentNumeric);
            Controls.Add(titleNumeric);
            Controls.Add(GenerateBookButton);
            Controls.Add(textBox12);
            Controls.Add(scaleNumeric);
            Controls.Add(frequencyNumeric);
            Controls.Add(sampleNumeric);
            Controls.Add(durationNumeric);
            Controls.Add(textBox11);
            Controls.Add(textBox10);
            Controls.Add(textBox9);
            Controls.Add(heightNumeric);
            Controls.Add(widthNumeric);
            Controls.Add(textBox8);
            Controls.Add(textBox7);
            Controls.Add(textBox6);
            Controls.Add(textBox5);
            Controls.Add(facesNumeric);
            Controls.Add(verticesNumeric);
            Controls.Add(textBox4);
            Controls.Add(audioParametersText);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(depthBox);
            Controls.Add(channelsBox);
            Controls.Add(GenerateModelButton);
            Controls.Add(GenerateImageButton);
            Controls.Add(sampleRateText);
            Controls.Add(frequencyText);
            Controls.Add(durationText);
            Controls.Add(audioSeed);
            Controls.Add(GenerateAudioButton);
            Controls.Add(GenerateVideoButton);
            Controls.Add(videoSeed);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Gallery of Babel";
            ((System.ComponentModel.ISupportInitialize)verticesNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)facesNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)widthNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)heightNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)durationNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)sampleNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)frequencyNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)scaleNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)titleNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)contentNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)lineNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)paragraphNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarB).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarA).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        //Buttons
        private Button GenerateVideoButton;
        private Button GenerateAudioButton;
        private Button GenerateImageButton;
        private Button GenerateModelButton;
        //Seed Boxes
        private RichTextBox videoSeed;// Video Seed
        private RichTextBox audioSeed;// Audio Seed
        //Audio Parameters
        private TextBox durationText;
        private TextBox frequencyText;
        private TextBox sampleRateText;
        private ComboBox channelsBox;
        private ComboBox depthBox;
        //Labels
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox audioParametersText;
        private TextBox textBox4;
        private NumericUpDown verticesNumeric;
        private NumericUpDown facesNumeric;
        private TextBox textBox5;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;
        private NumericUpDown widthNumeric;
        private NumericUpDown heightNumeric;
        private TextBox textBox9;
        private TextBox textBox10;
        private TextBox textBox11;
        private NumericUpDown durationNumeric;
        private NumericUpDown sampleNumeric;
        private NumericUpDown frequencyNumeric;
        private NumericUpDown scaleNumeric;
        private TextBox textBox12;
        private Button GenerateBookButton;
        private NumericUpDown titleNumeric;
        private NumericUpDown contentNumeric;
        private TextBox textBox13;
        private TextBox textBox14;
        private TextBox textBox15;
        private TextBox textBox16;
        private TextBox textBox17;
        private TextBox textBox18;
        private NumericUpDown lineNumeric;
        private TextBox textBox19;
        private TextBox textBox20;
        private NumericUpDown paragraphNumeric;
        private TextBox textBox21;
        private TrackBar trackBarB;
        private TrackBar trackBarA;
        private Label label1;
        private Label label2;
        private Label label3;
        private CheckBox checkBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button10;
        private Button button11;
        private Label label4;
    }
}