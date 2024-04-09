//using Telerik.WinControls.UI;

using System.Windows.Forms;
namespace NumerousResultEntry
{
    partial class NumerousResultEntry
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerFocus = new System.Windows.Forms.Timer(this.components);
         //   this.panel1 = new System.Windows.Forms.Panel();
            this.listViewEntities = new System.Windows.Forms.ListView();
            this.txtEnterdEntity = new System.Windows.Forms.TextBox();
            this.close_button = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtEditEntity = new System.Windows.Forms.TextBox();
            this.Ok_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
         //   this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerFocus
            // 
            this.timerFocus.Interval = 1000;
            this.timerFocus.Tick += new System.EventHandler(this.timerFocus_Tick);
            // 
            // panel1
            // 
            this.panel2.Controls.Add(this.listViewEntities);
            this.panel2.Controls.Add(this.txtEnterdEntity);
            this.panel2.Controls.Add(this.close_button);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.txtEditEntity);
            this.panel2.Controls.Add(this.Ok_button);
       //     this.panel1.Location = new System.Drawing.Point(0,0);
         //   this.panel1.Name = "panel1";
           // this.panel1.Size = new System.Drawing.Size(0, 0);
          //  this.panel1.TabIndex = 7;


            this.panel2.Location = new System.Drawing.Point(38, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(895, 481);
            this.panel2.TabIndex = 8;


            // 
            // listViewEntities
            // 
            this.listViewEntities.FullRowSelect = true;
            this.listViewEntities.Location = new System.Drawing.Point(97, 94);
            this.listViewEntities.Name = "listViewEntities";
            this.listViewEntities.Size = new System.Drawing.Size(731, 304);
            this.listViewEntities.TabIndex = 12;
            this.listViewEntities.UseCompatibleStateImageBehavior = false;
            this.listViewEntities.View = System.Windows.Forms.View.Details;
            this.listViewEntities.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewEntities_KeyDown);
            // 
            // txtEnterdEntity
            // 
            this.txtEnterdEntity.Enabled = false;
            this.txtEnterdEntity.Location = new System.Drawing.Point(383, 42);
            this.txtEnterdEntity.Name = "txtEnterdEntity";
            this.txtEnterdEntity.Size = new System.Drawing.Size(251, 20);
            this.txtEnterdEntity.TabIndex = 11;
            this.txtEnterdEntity.Visible = false;
            // 
            // close_button
            // 
            this.close_button.Location = new System.Drawing.Point(383, 423);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(75, 25);
            this.close_button.TabIndex = 10;
            this.close_button.Text = "Close";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.Close_button_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTitle.Location = new System.Drawing.Point(406, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(70, 26);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "label1";
            // 
            // txtEditEntity
            // 
            this.txtEditEntity.Location = new System.Drawing.Point(97, 42);
            this.txtEditEntity.Name = "txtEditEntity";
            this.txtEditEntity.Size = new System.Drawing.Size(167, 20);
            this.txtEditEntity.TabIndex = 8;
            this.txtEditEntity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEditEntity_KeyPress);
            // 
            // Ok_button
            // 
            this.Ok_button.Location = new System.Drawing.Point(97, 423);
            this.Ok_button.Name = "Ok_button";
            this.Ok_button.Size = new System.Drawing.Size(75, 25);
            this.Ok_button.TabIndex = 7;
            this.Ok_button.Text = "OK";
            this.Ok_button.UseVisualStyleBackColor = true;
            this.Ok_button.Click += new System.EventHandler(this.Ok_button_Click);
            // 
            // panel2
            // 
     
            // 
            // NumerousResultEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
          //  this.Controls.Add(this.panel1);
            this.Name = "NumerousResultEntry";
            this.Size = new System.Drawing.Size(997, 552);
            this.Resize += new System.EventHandler(this.NumerousResultEntry_Resize);
    //        this.panel1.ResumeLayout(false);
      //      this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerFocus;
  //      private Panel panel1
        private System.Windows.Forms.ListView listViewEntities;
        private System.Windows.Forms.TextBox txtEnterdEntity;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtEditEntity;
        private System.Windows.Forms.Button Ok_button;
        private Panel panel2;

    }
}
