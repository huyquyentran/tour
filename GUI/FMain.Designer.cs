
namespace GUI
{
    partial class FMain
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpTour = new System.Windows.Forms.TabPage();
            this.ucTour = new GUI.Tour.UcTour();
            this.tpTourGroup = new System.Windows.Forms.TabPage();
            this.ucTourGroup = new GUI.CustomUserControls.TourGroup.UcTourGroup();
            this.tpThongKe = new System.Windows.Forms.TabPage();
            this.ucStatistic = new GUI.CustomUserControls.Statistic.Statistic();
            this.tcMain.SuspendLayout();
            this.tpTour.SuspendLayout();
            this.tpTourGroup.SuspendLayout();
            this.tpThongKe.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpTour);
            this.tcMain.Controls.Add(this.tpTourGroup);
            this.tcMain.Controls.Add(this.tpThongKe);
            this.tcMain.Location = new System.Drawing.Point(3, -1);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1141, 622);
            this.tcMain.TabIndex = 0;
            // 
            // tpTour
            // 
            this.tpTour.Controls.Add(this.ucTour);
            this.tpTour.Location = new System.Drawing.Point(4, 25);
            this.tpTour.Name = "tpTour";
            this.tpTour.Padding = new System.Windows.Forms.Padding(3);
            this.tpTour.Size = new System.Drawing.Size(1133, 593);
            this.tpTour.TabIndex = 0;
            this.tpTour.Text = "Quản lý Tour";
            this.tpTour.UseVisualStyleBackColor = true;
            // 
            // ucTour
            // 
            this.ucTour.AutoSize = true;
            this.ucTour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTour.Location = new System.Drawing.Point(3, 3);
            this.ucTour.Name = "ucTour";
            this.ucTour.Size = new System.Drawing.Size(1127, 587);
            this.ucTour.TabIndex = 0;
            // 
            // tpTourGroup
            // 
            this.tpTourGroup.Controls.Add(this.ucTourGroup);
            this.tpTourGroup.Location = new System.Drawing.Point(4, 25);
            this.tpTourGroup.Name = "tpTourGroup";
            this.tpTourGroup.Padding = new System.Windows.Forms.Padding(3);
            this.tpTourGroup.Size = new System.Drawing.Size(1133, 593);
            this.tpTourGroup.TabIndex = 1;
            this.tpTourGroup.Text = "Quản lý Đoàn";
            this.tpTourGroup.UseVisualStyleBackColor = true;
            // 
            // ucTourGroup
            // 
            this.ucTourGroup.Location = new System.Drawing.Point(7, 7);
            this.ucTourGroup.Name = "ucTourGroup";
            this.ucTourGroup.Size = new System.Drawing.Size(1126, 586);
            this.ucTourGroup.TabIndex = 0;
            // 
            // tpThongKe
            // 
            this.tpThongKe.Controls.Add(this.ucStatistic);
            this.tpThongKe.Location = new System.Drawing.Point(4, 25);
            this.tpThongKe.Name = "tpThongKe";
            this.tpThongKe.Size = new System.Drawing.Size(1133, 593);
            this.tpThongKe.TabIndex = 5;
            this.tpThongKe.Text = "Thống kê";
            this.tpThongKe.UseVisualStyleBackColor = true;
            // 
            // ucStatistic
            // 
            this.ucStatistic.Location = new System.Drawing.Point(4, 4);
            this.ucStatistic.Name = "ucStatistic";
            this.ucStatistic.Size = new System.Drawing.Size(1128, 587);
            this.ucStatistic.TabIndex = 0;
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 606);
            this.Controls.Add(this.tcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FMain";
            this.Text = "Quản lý Tour Du Lịch";
            this.tcMain.ResumeLayout(false);
            this.tpTour.ResumeLayout(false);
            this.tpTour.PerformLayout();
            this.tpTourGroup.ResumeLayout(false);
            this.tpThongKe.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpTour;
        private System.Windows.Forms.TabPage tpTourGroup;
        private System.Windows.Forms.TabPage tpThongKe;
        private Tour.UcTour ucTour;
        private CustomUserControls.TourGroup.UcTourGroup ucTourGroup;
        private CustomUserControls.Statistic.Statistic ucStatistic;
    }
}