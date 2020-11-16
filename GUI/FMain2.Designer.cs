
namespace GUI
{
    partial class FMain2
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
            this.tpTourGroup = new System.Windows.Forms.TabPage();
            this.tpLocation = new System.Windows.Forms.TabPage();
            this.tpCustomer = new System.Windows.Forms.TabPage();
            this.tpStaff = new System.Windows.Forms.TabPage();
            this.tpThongKe = new System.Windows.Forms.TabPage();
            this.ucTour1 = new GUI.Tour.UcTour();
            this.tcMain.SuspendLayout();
            this.tpTour.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpTour);
            this.tcMain.Controls.Add(this.tpTourGroup);
            this.tcMain.Controls.Add(this.tpLocation);
            this.tcMain.Controls.Add(this.tpCustomer);
            this.tcMain.Controls.Add(this.tpStaff);
            this.tcMain.Controls.Add(this.tpThongKe);
            this.tcMain.Location = new System.Drawing.Point(13, 13);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1141, 622);
            this.tcMain.TabIndex = 0;
            // 
            // tpTour
            // 
            this.tpTour.Controls.Add(this.ucTour1);
            this.tpTour.Location = new System.Drawing.Point(4, 25);
            this.tpTour.Name = "tpTour";
            this.tpTour.Padding = new System.Windows.Forms.Padding(3);
            this.tpTour.Size = new System.Drawing.Size(1133, 593);
            this.tpTour.TabIndex = 0;
            this.tpTour.Text = "Quản lý Tour";
            this.tpTour.UseVisualStyleBackColor = true;
            // 
            // tpTourGroup
            // 
            this.tpTourGroup.Location = new System.Drawing.Point(4, 25);
            this.tpTourGroup.Name = "tpTourGroup";
            this.tpTourGroup.Padding = new System.Windows.Forms.Padding(3);
            this.tpTourGroup.Size = new System.Drawing.Size(1133, 593);
            this.tpTourGroup.TabIndex = 1;
            this.tpTourGroup.Text = "Quản lý Đoàn";
            this.tpTourGroup.UseVisualStyleBackColor = true;
            // 
            // tpLocation
            // 
            this.tpLocation.Location = new System.Drawing.Point(4, 25);
            this.tpLocation.Name = "tpLocation";
            this.tpLocation.Size = new System.Drawing.Size(1133, 593);
            this.tpLocation.TabIndex = 2;
            this.tpLocation.Text = "Quản lý Địa điểm";
            this.tpLocation.UseVisualStyleBackColor = true;
            // 
            // tpCustomer
            // 
            this.tpCustomer.Location = new System.Drawing.Point(4, 25);
            this.tpCustomer.Name = "tpCustomer";
            this.tpCustomer.Size = new System.Drawing.Size(1133, 593);
            this.tpCustomer.TabIndex = 3;
            this.tpCustomer.Text = "Quản lý Khách hàng";
            this.tpCustomer.UseVisualStyleBackColor = true;
            // 
            // tpStaff
            // 
            this.tpStaff.Location = new System.Drawing.Point(4, 25);
            this.tpStaff.Name = "tpStaff";
            this.tpStaff.Size = new System.Drawing.Size(1133, 593);
            this.tpStaff.TabIndex = 4;
            this.tpStaff.Text = "Quản lý Nhân viên";
            this.tpStaff.UseVisualStyleBackColor = true;
            // 
            // tpThongKe
            // 
            this.tpThongKe.Location = new System.Drawing.Point(4, 25);
            this.tpThongKe.Name = "tpThongKe";
            this.tpThongKe.Size = new System.Drawing.Size(1133, 593);
            this.tpThongKe.TabIndex = 5;
            this.tpThongKe.Text = "Thống kê";
            this.tpThongKe.UseVisualStyleBackColor = true;
            // 
            // ucTour1
            // 
            this.ucTour1.AutoSize = true;
            this.ucTour1.Location = new System.Drawing.Point(7, 7);
            this.ucTour1.Name = "ucTour1";
            this.ucTour1.Size = new System.Drawing.Size(1126, 586);
            this.ucTour1.TabIndex = 0;
            // 
            // FMain2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 647);
            this.Controls.Add(this.tcMain);
            this.Name = "FMain2";
            this.Text = "FMain2";
            this.tcMain.ResumeLayout(false);
            this.tpTour.ResumeLayout(false);
            this.tpTour.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpTour;
        private System.Windows.Forms.TabPage tpTourGroup;
        private System.Windows.Forms.TabPage tpLocation;
        private System.Windows.Forms.TabPage tpCustomer;
        private System.Windows.Forms.TabPage tpStaff;
        private System.Windows.Forms.TabPage tpThongKe;
        private Tour.UcTour ucTour1;
    }
}