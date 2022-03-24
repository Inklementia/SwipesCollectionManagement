
namespace SwipesCollectionManagement.UI
{
    partial class SwipesCollectionManagementForm
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
            this.tbSwipesCollectionManagement = new System.Windows.Forms.TabControl();
            this.tbTerminals = new System.Windows.Forms.TabPage();
            this.dgvTerminals = new System.Windows.Forms.DataGridView();
            this.Ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbSwipes = new System.Windows.Forms.TabPage();
            this.btnStart = new System.Windows.Forms.Button();
            this.prgbProcess = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tbSwipesCollectionManagement.SuspendLayout();
            this.tbTerminals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTerminals)).BeginInit();
            this.SuspendLayout();
            // 
            // tbSwipesCollectionManagement
            // 
            this.tbSwipesCollectionManagement.Controls.Add(this.tbTerminals);
            this.tbSwipesCollectionManagement.Controls.Add(this.tbSwipes);
            this.tbSwipesCollectionManagement.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbSwipesCollectionManagement.Location = new System.Drawing.Point(0, 0);
            this.tbSwipesCollectionManagement.Name = "tbSwipesCollectionManagement";
            this.tbSwipesCollectionManagement.SelectedIndex = 0;
            this.tbSwipesCollectionManagement.Size = new System.Drawing.Size(800, 394);
            this.tbSwipesCollectionManagement.TabIndex = 1;
            // 
            // tbTerminals
            // 
            this.tbTerminals.Controls.Add(this.dgvTerminals);
            this.tbTerminals.Location = new System.Drawing.Point(4, 22);
            this.tbTerminals.Name = "tbTerminals";
            this.tbTerminals.Padding = new System.Windows.Forms.Padding(3);
            this.tbTerminals.Size = new System.Drawing.Size(792, 368);
            this.tbTerminals.TabIndex = 0;
            this.tbTerminals.Text = "Terminals";
            this.tbTerminals.UseVisualStyleBackColor = true;
            // 
            // dgvTerminals
            // 
            this.dgvTerminals.AllowUserToAddRows = false;
            this.dgvTerminals.AllowUserToDeleteRows = false;
            this.dgvTerminals.AllowUserToResizeColumns = false;
            this.dgvTerminals.AllowUserToResizeRows = false;
            this.dgvTerminals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTerminals.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvTerminals.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvTerminals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTerminals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ip,
            this.Status});
            this.dgvTerminals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTerminals.Location = new System.Drawing.Point(3, 3);
            this.dgvTerminals.Name = "dgvTerminals";
            this.dgvTerminals.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvTerminals.ShowCellErrors = false;
            this.dgvTerminals.ShowCellToolTips = false;
            this.dgvTerminals.ShowEditingIcon = false;
            this.dgvTerminals.ShowRowErrors = false;
            this.dgvTerminals.Size = new System.Drawing.Size(786, 362);
            this.dgvTerminals.StandardTab = true;
            this.dgvTerminals.TabIndex = 0;
            // 
            // Ip
            // 
            this.Ip.HeaderText = "IP";
            this.Ip.Name = "Ip";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // tbSwipes
            // 
            this.tbSwipes.Location = new System.Drawing.Point(4, 22);
            this.tbSwipes.Name = "tbSwipes";
            this.tbSwipes.Padding = new System.Windows.Forms.Padding(3);
            this.tbSwipes.Size = new System.Drawing.Size(792, 368);
            this.tbSwipes.TabIndex = 1;
            this.tbSwipes.Text = "Swipes";
            this.tbSwipes.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStart.Location = new System.Drawing.Point(528, 400);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(260, 38);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start Process";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // prgbProcess
            // 
            this.prgbProcess.Location = new System.Drawing.Point(12, 415);
            this.prgbProcess.Name = "prgbProcess";
            this.prgbProcess.Size = new System.Drawing.Size(500, 23);
            this.prgbProcess.TabIndex = 3;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProgress.Location = new System.Drawing.Point(12, 397);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(31, 15);
            this.lblProgress.TabIndex = 4;
            this.lblProgress.Text = "0 %";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // SwipesCollectionManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.prgbProcess);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tbSwipesCollectionManagement);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SwipesCollectionManagementForm";
            this.Text = "Swipes Collection Management";
            this.tbSwipesCollectionManagement.ResumeLayout(false);
            this.tbTerminals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTerminals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbSwipesCollectionManagement;
        private System.Windows.Forms.TabPage tbTerminals;
        private System.Windows.Forms.DataGridView dgvTerminals;
        private System.Windows.Forms.TabPage tbSwipes;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ProgressBar prgbProcess;
        private System.Windows.Forms.Label lblProgress;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}

