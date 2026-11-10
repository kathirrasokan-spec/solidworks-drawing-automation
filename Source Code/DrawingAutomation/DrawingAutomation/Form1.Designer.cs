
namespace DrawingAutomation
{
    partial class SHEETSELECTION
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TemplateSelection = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BasicDimension = new System.Windows.Forms.CheckBox();
            this.Hole = new System.Windows.Forms.CheckBox();
            this.Pattern = new System.Windows.Forms.CheckBox();
            this.Chamfer = new System.Windows.Forms.CheckBox();
            this.Fillet = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TemplateSelection
            // 
            this.TemplateSelection.FormattingEnabled = true;
            this.TemplateSelection.Items.AddRange(new object[] {
            "SIZE A0",
            "SIZE A1",
            "SIZE A2",
            "SIZE A3",
            "SIZE A4L",
            "SIZE A4P"});
            this.TemplateSelection.Location = new System.Drawing.Point(149, 39);
            this.TemplateSelection.Name = "TemplateSelection";
            this.TemplateSelection.Size = new System.Drawing.Size(87, 24);
            this.TemplateSelection.TabIndex = 0;
            this.TemplateSelection.Text = "Select";
            //this.TemplateSelection.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(98, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "CREATE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "TEMPLATE";
            // 
            // BasicDimension
            // 
            this.BasicDimension.AutoSize = true;
            this.BasicDimension.Location = new System.Drawing.Point(84, 88);
            this.BasicDimension.Name = "BasicDimension";
            this.BasicDimension.Size = new System.Drawing.Size(158, 21);
            this.BasicDimension.TabIndex = 3;
            this.BasicDimension.Text = "BASIC DIMENSIONS";
            this.BasicDimension.UseVisualStyleBackColor = true;
            this.BasicDimension.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Hole
            // 
            this.Hole.AutoSize = true;
            this.Hole.Location = new System.Drawing.Point(84, 126);
            this.Hole.Name = "Hole";
            this.Hole.Size = new System.Drawing.Size(68, 21);
            this.Hole.TabIndex = 4;
            this.Hole.Text = "HOLE";
            this.Hole.UseVisualStyleBackColor = true;
            this.Hole.CheckedChanged += new System.EventHandler(this.Hole_CheckedChanged);
            // 
            // Pattern
            // 
            this.Pattern.AutoSize = true;
            this.Pattern.Location = new System.Drawing.Point(84, 169);
            this.Pattern.Name = "Pattern";
            this.Pattern.Size = new System.Drawing.Size(104, 21);
            this.Pattern.TabIndex = 5;
            this.Pattern.Text = "PATTERNS";
            this.Pattern.UseVisualStyleBackColor = true;
            this.Pattern.CheckedChanged += new System.EventHandler(this.Pattern_CheckedChanged);
            // 
            // Chamfer
            // 
            this.Chamfer.AutoSize = true;
            this.Chamfer.Location = new System.Drawing.Point(84, 208);
            this.Chamfer.Name = "Chamfer";
            this.Chamfer.Size = new System.Drawing.Size(96, 21);
            this.Chamfer.TabIndex = 6;
            this.Chamfer.Text = "CHAMFER";
            this.Chamfer.UseVisualStyleBackColor = true;
            this.Chamfer.CheckedChanged += new System.EventHandler(this.Chamfer_CheckedChanged);
            // 
            // Fillet
            // 
            this.Fillet.AutoSize = true;
            this.Fillet.Location = new System.Drawing.Point(84, 249);
            this.Fillet.Name = "Fillet";
            this.Fillet.Size = new System.Drawing.Size(75, 21);
            this.Fillet.TabIndex = 7;
            this.Fillet.Text = "FILLET";
            this.Fillet.UseVisualStyleBackColor = true;
            this.Fillet.CheckedChanged += new System.EventHandler(this.Fillet_CheckedChanged);
            // 
            // SHEETSELECTION
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 364);
            this.Controls.Add(this.Fillet);
            this.Controls.Add(this.Chamfer);
            this.Controls.Add(this.Pattern);
            this.Controls.Add(this.Hole);
            this.Controls.Add(this.BasicDimension);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TemplateSelection);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SHEETSELECTION";
            this.Text = "SHEET SIZE SELECTION";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox TemplateSelection;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox BasicDimension;
        private System.Windows.Forms.CheckBox Hole;
        private System.Windows.Forms.CheckBox Pattern;
        private System.Windows.Forms.CheckBox Chamfer;
        private System.Windows.Forms.CheckBox Fillet;
    }
}