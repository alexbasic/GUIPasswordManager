namespace Ru.Mail.AlexBasic.GUIPasswordManager.Forms
{
    partial class AddSecretForm
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
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.commentTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.isPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Location = new System.Drawing.Point(104, 19);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.PlaceholderText = "Name of the secret";
            this.nameTextBox.Size = new System.Drawing.Size(402, 30);
            this.nameTextBox.TabIndex = 0;
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AddButton.Location = new System.Drawing.Point(142, 228);
            this.AddButton.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(228, 35);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            // 
            // commentTextBox
            // 
            this.commentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commentTextBox.Location = new System.Drawing.Point(104, 185);
            this.commentTextBox.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.PlaceholderText = "Any addititional info";
            this.commentTextBox.Size = new System.Drawing.Size(402, 30);
            this.commentTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 188);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Comment";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Value";
            // 
            // valueTextBox
            // 
            this.valueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueTextBox.Location = new System.Drawing.Point(104, 62);
            this.valueTextBox.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.PlaceholderText = "Value of the secret";
            this.valueTextBox.Size = new System.Drawing.Size(402, 30);
            this.valueTextBox.TabIndex = 6;
            this.valueTextBox.UseSystemPasswordChar = true;
            // 
            // groupComboBox
            // 
            this.groupComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.Location = new System.Drawing.Point(104, 143);
            this.groupComboBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(203, 31);
            this.groupComboBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 146);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Group";
            // 
            // isPasswordCheckBox
            // 
            this.isPasswordCheckBox.AutoSize = true;
            this.isPasswordCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.isPasswordCheckBox.Checked = true;
            this.isPasswordCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isPasswordCheckBox.Location = new System.Drawing.Point(12, 104);
            this.isPasswordCheckBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.isPasswordCheckBox.Name = "isPasswordCheckBox";
            this.isPasswordCheckBox.Size = new System.Drawing.Size(120, 27);
            this.isPasswordCheckBox.TabIndex = 9;
            this.isPasswordCheckBox.Text = "Is password";
            this.isPasswordCheckBox.UseVisualStyleBackColor = true;
            this.isPasswordCheckBox.CheckedChanged += new System.EventHandler(this.isPasswordCheckBox_CheckedChanged);
            // 
            // AddSecretForm
            // 
            this.AcceptButton = this.AddButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 282);
            this.Controls.Add(this.isPasswordCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupComboBox);
            this.Controls.Add(this.valueTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.commentTextBox);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.nameTextBox);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(338, 157);
            this.Name = "AddSecretForm";
            this.Text = "Add secret";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AddButton;
        public System.Windows.Forms.TextBox nameTextBox;
        public System.Windows.Forms.TextBox commentTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox groupComboBox;
        public System.Windows.Forms.CheckBox isPasswordCheckBox;
    }
}