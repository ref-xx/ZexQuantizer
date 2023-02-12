using System.Windows.Forms;
namespace ZeXQuantizer
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
            this.button1 = new System.Windows.Forms.Button();
            this.picSource = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.chkNoBright = new System.Windows.Forms.CheckBox();
            this.picDest = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.chkInvalids = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button10 = new System.Windows.Forms.Button();
            this.rdFree = new System.Windows.Forms.RadioButton();
            this.rdProcClash = new System.Windows.Forms.RadioButton();
            this.rdTimex = new System.Windows.Forms.RadioButton();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.button11 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.txtBright = new System.Windows.Forms.TextBox();
            this.txtNormal = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkValidCell = new System.Windows.Forms.CheckBox();
            this.btnCellDone = new System.Windows.Forms.Button();
            this.btnRevertCell = new System.Windows.Forms.Button();
            this.picCell = new System.Windows.Forms.PictureBox();
            this.button13 = new System.Windows.Forms.Button();
            this.btnSavePNG = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.picOriginal = new System.Windows.Forms.PictureBox();
            this.chkQuantizeInvalids = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDest)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCell)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load Image Above";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // picSource
            // 
            this.picSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSource.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picSource.Location = new System.Drawing.Point(290, 10);
            this.picSource.Name = "picSource";
            this.picSource.Size = new System.Drawing.Size(275, 222);
            this.picSource.TabIndex = 1;
            this.picSource.TabStop = false;
            this.picSource.Click += new System.EventHandler(this.picSource_Click);
            this.picSource.Paint += new System.Windows.Forms.PaintEventHandler(this.picSource_Paint);
            this.picSource.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picSource_MouseClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 152);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 22);
            this.button2.TabIndex = 2;
            this.button2.Text = "2.ZxQuantize";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(10, 10);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(275, 20);
            this.txtFilename.TabIndex = 3;
            this.txtFilename.Text = "guybrush.png";
            // 
            // chkNoBright
            // 
            this.chkNoBright.AutoSize = true;
            this.chkNoBright.Location = new System.Drawing.Point(7, 87);
            this.chkNoBright.Name = "chkNoBright";
            this.chkNoBright.Size = new System.Drawing.Size(96, 17);
            this.chkNoBright.TabIndex = 5;
            this.chkNoBright.Text = "Remove Bright";
            this.chkNoBright.UseVisualStyleBackColor = true;
            // 
            // picDest
            // 
            this.picDest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDest.Location = new System.Drawing.Point(569, 10);
            this.picDest.Name = "picDest";
            this.picDest.Size = new System.Drawing.Size(256, 192);
            this.picDest.TabIndex = 7;
            this.picDest.TabStop = false;
            this.picDest.Move += new System.EventHandler(this.picDest_Move);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(49, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(43, 20);
            this.button3.TabIndex = 8;
            this.button3.Text = "2X";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(98, 42);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(39, 20);
            this.button4.TabIndex = 9;
            this.button4.Text = "3X";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // chkGrid
            // 
            this.chkGrid.AutoSize = true;
            this.chkGrid.Checked = true;
            this.chkGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGrid.Location = new System.Drawing.Point(5, 20);
            this.chkGrid.Name = "chkGrid";
            this.chkGrid.Size = new System.Drawing.Size(45, 17);
            this.chkGrid.TabIndex = 10;
            this.chkGrid.Text = "Grid";
            this.chkGrid.UseVisualStyleBackColor = true;
            this.chkGrid.CheckedChanged += new System.EventHandler(this.chkGrid_CheckedChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 47);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(113, 22);
            this.button5.TabIndex = 11;
            this.button5.Text = "Load with Requester";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(71, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Load an image";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(5, 42);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(38, 20);
            this.button6.TabIndex = 13;
            this.button6.Text = "1X";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // chkInvalids
            // 
            this.chkInvalids.AutoSize = true;
            this.chkInvalids.Enabled = false;
            this.chkInvalids.Location = new System.Drawing.Point(75, 19);
            this.chkInvalids.Name = "chkInvalids";
            this.chkInvalids.Size = new System.Drawing.Size(62, 17);
            this.chkInvalids.TabIndex = 14;
            this.chkInvalids.Text = "Invalids";
            this.chkInvalids.UseVisualStyleBackColor = true;
            this.chkInvalids.CheckedChanged += new System.EventHandler(this.chkInvalids_CheckedChanged);
            this.chkInvalids.EnabledChanged += new System.EventHandler(this.chkInvalids_EnabledChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkQuantizeInvalids);
            this.groupBox1.Controls.Add(this.button10);
            this.groupBox1.Controls.Add(this.rdFree);
            this.groupBox1.Controls.Add(this.rdProcClash);
            this.groupBox1.Controls.Add(this.rdTimex);
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.chkNoBright);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(10, 221);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(123, 183);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "2. Quantize Options";
            // 
            // button10
            // 
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(48, 38);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(70, 20);
            this.button10.TabIndex = 21;
            this.button10.Text = "Switch 8x8";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // rdFree
            // 
            this.rdFree.AutoSize = true;
            this.rdFree.Checked = true;
            this.rdFree.Location = new System.Drawing.Point(7, 63);
            this.rdFree.Name = "rdFree";
            this.rdFree.Size = new System.Drawing.Size(108, 17);
            this.rdFree.TabIndex = 20;
            this.rdFree.TabStop = true;
            this.rdFree.Text = "1x1 Pre-formatted";
            this.rdFree.UseVisualStyleBackColor = true;
            this.rdFree.CheckedChanged += new System.EventHandler(this.rdFree_CheckedChanged);
            // 
            // rdProcClash
            // 
            this.rdProcClash.AutoSize = true;
            this.rdProcClash.Location = new System.Drawing.Point(7, 40);
            this.rdProcClash.Name = "rdProcClash";
            this.rdProcClash.Size = new System.Drawing.Size(42, 17);
            this.rdProcClash.TabIndex = 19;
            this.rdProcClash.Text = "8x8";
            this.rdProcClash.UseVisualStyleBackColor = true;
            this.rdProcClash.CheckedChanged += new System.EventHandler(this.rdProcClash_CheckedChanged);
            // 
            // rdTimex
            // 
            this.rdTimex.AutoSize = true;
            this.rdTimex.Location = new System.Drawing.Point(7, 18);
            this.rdTimex.Name = "rdTimex";
            this.rdTimex.Size = new System.Drawing.Size(42, 17);
            this.rdTimex.TabIndex = 18;
            this.rdTimex.Text = "8x1";
            this.rdTimex.UseVisualStyleBackColor = true;
            this.rdTimex.CheckedChanged += new System.EventHandler(this.rdTimex_CheckedChanged);
            // 
            // button9
            // 
            this.button9.Enabled = false;
            this.button9.Location = new System.Drawing.Point(48, 16);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(70, 20);
            this.button9.TabIndex = 17;
            this.button9.Text = "Switch 8x1";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Enabled = false;
            this.button8.Location = new System.Drawing.Point(5, 19);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(113, 22);
            this.button8.TabIndex = 7;
            this.button8.Text = "Save As Scr...";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.txtScale);
            this.groupBox2.Controls.Add(this.chkGrid);
            this.groupBox2.Controls.Add(this.chkInvalids);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Location = new System.Drawing.Point(140, 308);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(146, 96);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "View Options";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(49, 67);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(88, 20);
            this.button7.TabIndex = 16;
            this.button7.Text = "X";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // txtScale
            // 
            this.txtScale.Location = new System.Drawing.Point(5, 68);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(38, 20);
            this.txtScale.TabIndex = 15;
            this.txtScale.Text = "5";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(7, 75);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(112, 22);
            this.button11.TabIndex = 17;
            this.button11.Text = "Crop to 256x192";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtBright);
            this.groupBox3.Controls.Add(this.button14);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.button12);
            this.groupBox3.Controls.Add(this.txtNormal);
            this.groupBox3.Controls.Add(this.button11);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Location = new System.Drawing.Point(10, 36);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(123, 179);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "1. Input";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Bri";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Non";
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(77, 147);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(42, 20);
            this.button12.TabIndex = 20;
            this.button12.Text = "Set Values";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // txtBright
            // 
            this.txtBright.Location = new System.Drawing.Point(92, 121);
            this.txtBright.Name = "txtBright";
            this.txtBright.Size = new System.Drawing.Size(27, 20);
            this.txtBright.TabIndex = 19;
            this.txtBright.Text = "251";
            // 
            // txtNormal
            // 
            this.txtNormal.Location = new System.Drawing.Point(36, 121);
            this.txtNormal.Name = "txtNormal";
            this.txtNormal.Size = new System.Drawing.Size(27, 20);
            this.txtNormal.TabIndex = 18;
            this.txtNormal.Text = "200";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSavePNG);
            this.groupBox4.Controls.Add(this.button8);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(11, 410);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(275, 50);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "3. Output";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button15);
            this.groupBox5.Controls.Add(this.button13);
            this.groupBox5.Controls.Add(this.chkValidCell);
            this.groupBox5.Controls.Add(this.btnCellDone);
            this.groupBox5.Controls.Add(this.btnRevertCell);
            this.groupBox5.Controls.Add(this.picCell);
            this.groupBox5.Location = new System.Drawing.Point(139, 39);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(147, 263);
            this.groupBox5.TabIndex = 20;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "3. Retouch";
            // 
            // chkValidCell
            // 
            this.chkValidCell.AutoSize = true;
            this.chkValidCell.Enabled = false;
            this.chkValidCell.Location = new System.Drawing.Point(96, 0);
            this.chkValidCell.Name = "chkValidCell";
            this.chkValidCell.Size = new System.Drawing.Size(40, 17);
            this.chkValidCell.TabIndex = 16;
            this.chkValidCell.Text = "Ok";
            this.chkValidCell.UseVisualStyleBackColor = true;
            // 
            // btnCellDone
            // 
            this.btnCellDone.Location = new System.Drawing.Point(79, 233);
            this.btnCellDone.Name = "btnCellDone";
            this.btnCellDone.Size = new System.Drawing.Size(62, 22);
            this.btnCellDone.TabIndex = 15;
            this.btnCellDone.Text = "Push";
            this.btnCellDone.UseVisualStyleBackColor = true;
            this.btnCellDone.Click += new System.EventHandler(this.btnCellDone_Click);
            // 
            // btnRevertCell
            // 
            this.btnRevertCell.Location = new System.Drawing.Point(9, 207);
            this.btnRevertCell.Name = "btnRevertCell";
            this.btnRevertCell.Size = new System.Drawing.Size(67, 22);
            this.btnRevertCell.TabIndex = 14;
            this.btnRevertCell.Text = "Revert";
            this.btnRevertCell.UseVisualStyleBackColor = true;
            this.btnRevertCell.Click += new System.EventHandler(this.btnRevertCell_Click);
            // 
            // picCell
            // 
            this.picCell.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCell.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picCell.Location = new System.Drawing.Point(9, 21);
            this.picCell.Name = "picCell";
            this.picCell.Size = new System.Drawing.Size(128, 180);
            this.picCell.TabIndex = 2;
            this.picCell.TabStop = false;
            this.picCell.Click += new System.EventHandler(this.picCell_Click);
            this.picCell.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picCell_MouseClick);
            this.picCell.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCell_MouseDown);
            this.picCell.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCell_MouseMove);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(9, 233);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(67, 22);
            this.button13.TabIndex = 17;
            this.button13.Text = "Get First";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // btnSavePNG
            // 
            this.btnSavePNG.Location = new System.Drawing.Point(152, 19);
            this.btnSavePNG.Name = "btnSavePNG";
            this.btnSavePNG.Size = new System.Drawing.Size(113, 22);
            this.btnSavePNG.TabIndex = 8;
            this.btnSavePNG.Text = "Save As PNG....";
            this.btnSavePNG.UseVisualStyleBackColor = true;
            this.btnSavePNG.Click += new System.EventHandler(this.btnSavePNG_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(4, 147);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(67, 20);
            this.button14.TabIndex = 23;
            this.button14.Text = "Default";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(79, 206);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(62, 22);
            this.button15.TabIndex = 18;
            this.button15.Text = "Flip Bri";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // picOriginal
            // 
            this.picOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOriginal.Location = new System.Drawing.Point(569, 208);
            this.picOriginal.Name = "picOriginal";
            this.picOriginal.Size = new System.Drawing.Size(256, 192);
            this.picOriginal.TabIndex = 21;
            this.picOriginal.TabStop = false;
            // 
            // chkQuantizeInvalids
            // 
            this.chkQuantizeInvalids.AutoSize = true;
            this.chkQuantizeInvalids.Enabled = false;
            this.chkQuantizeInvalids.Location = new System.Drawing.Point(7, 129);
            this.chkQuantizeInvalids.Name = "chkQuantizeInvalids";
            this.chkQuantizeInvalids.Size = new System.Drawing.Size(86, 17);
            this.chkQuantizeInvalids.TabIndex = 22;
            this.chkQuantizeInvalids.Text = "Invalids Only";
            this.chkQuantizeInvalids.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 468);
            this.Controls.Add(this.picOriginal);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.picDest);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.picSource);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(869, 505);
            this.Name = "Form1";
            this.Text = "ZeXQuantizer v1 12.02.23";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDest)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCell)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private PictureBox picSource;
        private Button button2;
        private TextBox txtFilename;
        private CheckBox chkNoBright;
        private PictureBox picDest;
        private Button button3;
        private Button button4;
        private CheckBox chkGrid;
        private Button button5;
        private Label label1;
        private Button button6;
        private CheckBox chkInvalids;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button button7;
        private TextBox txtScale;
        private Button button8;
        private Button button9;
        private RadioButton rdFree;
        private RadioButton rdProcClash;
        private RadioButton rdTimex;
        private Button button10;
        private Button button11;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private PictureBox picCell;
        private Button btnCellDone;
        private Button btnRevertCell;
        private CheckBox chkValidCell;
        private Label label3;
        private Label label2;
        private Button button12;
        private TextBox txtBright;
        private TextBox txtNormal;
        private Button button13;
        private Button btnSavePNG;
        private Button button14;
        private Button button15;
        private PictureBox picOriginal;
        private CheckBox chkQuantizeInvalids;
    }
}

