namespace BusTicketSystem.GUI
{
    partial class NewBookingForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblRoute = new System.Windows.Forms.Label();
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.lblBus = new System.Windows.Forms.Label();
            this.cmbBus = new System.Windows.Forms.ComboBox();
            this.lblTravelDate = new System.Windows.Forms.Label();
            this.dtpTravelDate = new System.Windows.Forms.DateTimePicker();
            this.lblSeatSelection = new System.Windows.Forms.Label();
            this.pnlSeats = new System.Windows.Forms.Panel();
            this.lblSelectedSeat = new System.Windows.Forms.Label();
            this.txtSelectedSeat = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.btnBook = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(109, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(169, 29);
            this.lblTitle.TabIndex = 30;
            this.lblTitle.Text = "New Booking";
            // 
            // lblRoute
            // 
            this.lblRoute.AutoSize = true;
            this.lblRoute.Location = new System.Drawing.Point(109, 79);
            this.lblRoute.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRoute.Name = "lblRoute";
            this.lblRoute.Size = new System.Drawing.Size(46, 16);
            this.lblRoute.TabIndex = 31;
            this.lblRoute.Text = "Route:";
            // 
            // cmbRoute
            // 
            this.cmbRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Location = new System.Drawing.Point(242, 75);
            this.cmbRoute.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(332, 24);
            this.cmbRoute.TabIndex = 32;
            // 
            // lblBus
            // 
            this.lblBus.AutoSize = true;
            this.lblBus.Location = new System.Drawing.Point(109, 116);
            this.lblBus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBus.Name = "lblBus";
            this.lblBus.Size = new System.Drawing.Size(33, 16);
            this.lblBus.TabIndex = 33;
            this.lblBus.Text = "Bus:";
            // 
            // cmbBus
            // 
            this.cmbBus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBus.FormattingEnabled = true;
            this.cmbBus.Location = new System.Drawing.Point(242, 112);
            this.cmbBus.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBus.Name = "cmbBus";
            this.cmbBus.Size = new System.Drawing.Size(332, 24);
            this.cmbBus.TabIndex = 34;
            // 
            // lblTravelDate
            // 
            this.lblTravelDate.AutoSize = true;
            this.lblTravelDate.Location = new System.Drawing.Point(109, 153);
            this.lblTravelDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTravelDate.Name = "lblTravelDate";
            this.lblTravelDate.Size = new System.Drawing.Size(81, 16);
            this.lblTravelDate.TabIndex = 35;
            this.lblTravelDate.Text = "Travel Date:";
            // 
            // dtpTravelDate
            // 
            this.dtpTravelDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTravelDate.Location = new System.Drawing.Point(242, 149);
            this.dtpTravelDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpTravelDate.MinDate = new System.DateTime(2025, 5, 15, 21, 1, 12, 525);
            this.dtpTravelDate.Name = "dtpTravelDate";
            this.dtpTravelDate.Size = new System.Drawing.Size(159, 22);
            this.dtpTravelDate.TabIndex = 36;
            // 
            // lblSeatSelection
            // 
            this.lblSeatSelection.AutoSize = true;
            this.lblSeatSelection.Location = new System.Drawing.Point(109, 202);
            this.lblSeatSelection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSeatSelection.Name = "lblSeatSelection";
            this.lblSeatSelection.Size = new System.Drawing.Size(97, 16);
            this.lblSeatSelection.TabIndex = 37;
            this.lblSeatSelection.Text = "Seat Selection:";
            // 
            // pnlSeats
            // 
            this.pnlSeats.AutoSize = true;
            this.pnlSeats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSeats.Location = new System.Drawing.Point(242, 202);
            this.pnlSeats.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSeats.Name = "pnlSeats";
            this.pnlSeats.Size = new System.Drawing.Size(752, 332);
            this.pnlSeats.TabIndex = 38;
            // 
            // lblSelectedSeat
            // 
            this.lblSelectedSeat.AutoSize = true;
            this.lblSelectedSeat.Location = new System.Drawing.Point(109, 605);
            this.lblSelectedSeat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedSeat.Name = "lblSelectedSeat";
            this.lblSelectedSeat.Size = new System.Drawing.Size(95, 16);
            this.lblSelectedSeat.TabIndex = 39;
            this.lblSelectedSeat.Text = "Selected Seat:";
            // 
            // txtSelectedSeat
            // 
            this.txtSelectedSeat.Location = new System.Drawing.Point(232, 605);
            this.txtSelectedSeat.Margin = new System.Windows.Forms.Padding(4);
            this.txtSelectedSeat.Name = "txtSelectedSeat";
            this.txtSelectedSeat.ReadOnly = true;
            this.txtSelectedSeat.Size = new System.Drawing.Size(132, 22);
            this.txtSelectedSeat.TabIndex = 40;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(114, 559);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(41, 16);
            this.lblPrice.TabIndex = 41;
            this.lblPrice.Text = "Price:";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(231, 556);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(132, 22);
            this.txtPrice.TabIndex = 42;
            // 
            // btnBook
            // 
            this.btnBook.Location = new System.Drawing.Point(231, 635);
            this.btnBook.Margin = new System.Windows.Forms.Padding(4);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(133, 37);
            this.btnBook.TabIndex = 43;
            this.btnBook.Text = "Book Ticket";
            this.btnBook.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(441, 635);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 37);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // NewBookingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 691);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblRoute);
            this.Controls.Add(this.cmbRoute);
            this.Controls.Add(this.lblBus);
            this.Controls.Add(this.cmbBus);
            this.Controls.Add(this.lblTravelDate);
            this.Controls.Add(this.dtpTravelDate);
            this.Controls.Add(this.lblSeatSelection);
            this.Controls.Add(this.pnlSeats);
            this.Controls.Add(this.lblSelectedSeat);
            this.Controls.Add(this.txtSelectedSeat);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.btnBook);
            this.Controls.Add(this.btnCancel);
            this.Name = "NewBookingForm";
            this.Text = "NewBookingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRoute;
        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.Label lblBus;
        private System.Windows.Forms.ComboBox cmbBus;
        private System.Windows.Forms.Label lblTravelDate;
        private System.Windows.Forms.DateTimePicker dtpTravelDate;
        private System.Windows.Forms.Label lblSeatSelection;
        private System.Windows.Forms.Panel pnlSeats;
        private System.Windows.Forms.Label lblSelectedSeat;
        private System.Windows.Forms.TextBox txtSelectedSeat;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Button btnBook;
        private System.Windows.Forms.Button btnCancel;
    }
}