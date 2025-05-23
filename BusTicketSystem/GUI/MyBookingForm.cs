using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusTicketSystem.BLL;
using BusTicketSystem.Config;
using BusTicketSystem.DTO;

namespace BusTicketSystem.GUI
{
    public partial class MyBookingForm : Form
    {
        private readonly BookingBLL _bookingBLL;
        private List<Booking> _bookings;

        public MyBookingForm()
        {
            InitializeComponent();
            _bookingBLL = new BookingBLL();
        }

        private void MyBookingForm_Load(object sender, EventArgs e)
        {
            // Set up DataGridView
            ConfigureDataGridView();

            // Load bookings
            LoadBookings();
        }

        private void ConfigureDataGridView()
        {
            // Configure DataGridView columns
            dgvBookings.AutoGenerateColumns = false;

            // Clear existing columns
            dgvBookings.Columns.Clear();

            // Add columns
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "Id";
            idColumn.HeaderText = "ID";
            idColumn.Visible = false;
            dgvBookings.Columns.Add(idColumn);

            DataGridViewTextBoxColumn routeColumn = new DataGridViewTextBoxColumn();
            routeColumn.DataPropertyName = "RouteName";
            routeColumn.HeaderText = "Route";
            routeColumn.Width = 150;
            dgvBookings.Columns.Add(routeColumn);

            DataGridViewTextBoxColumn busColumn = new DataGridViewTextBoxColumn();
            busColumn.DataPropertyName = "BusInfo";
            busColumn.HeaderText = "Bus";
            busColumn.Width = 120;
            dgvBookings.Columns.Add(busColumn);

            DataGridViewTextBoxColumn travelDateColumn = new DataGridViewTextBoxColumn();
            travelDateColumn.DataPropertyName = "TravelDate";
            travelDateColumn.HeaderText = "Travel Date";
            travelDateColumn.Width = 100;
            travelDateColumn.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvBookings.Columns.Add(travelDateColumn);

            DataGridViewTextBoxColumn seatColumn = new DataGridViewTextBoxColumn();
            seatColumn.DataPropertyName = "SeatNumber";
            seatColumn.HeaderText = "Seat";
            seatColumn.Width = 60;
            dgvBookings.Columns.Add(seatColumn);

            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.DataPropertyName = "Status";
            statusColumn.HeaderText = "Status";
            statusColumn.Width = 80;
            dgvBookings.Columns.Add(statusColumn);

            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();
            priceColumn.DataPropertyName = "TotalPrice";
            priceColumn.HeaderText = "Price";
            priceColumn.Width = 80;
            priceColumn.DefaultCellStyle.Format = "C";
            dgvBookings.Columns.Add(priceColumn);

            DataGridViewButtonColumn viewColumn = new DataGridViewButtonColumn();
            viewColumn.HeaderText = "Details";
            viewColumn.Text = "View";
            viewColumn.UseColumnTextForButtonValue = true;
            viewColumn.Width = 70;
            dgvBookings.Columns.Add(viewColumn);

            DataGridViewButtonColumn cancelColumn = new DataGridViewButtonColumn();
            cancelColumn.HeaderText = "Action";
            cancelColumn.Text = "Cancel";
            cancelColumn.UseColumnTextForButtonValue = true;
            cancelColumn.Width = 70;
            dgvBookings.Columns.Add(cancelColumn);

            // Set other properties
            dgvBookings.AllowUserToAddRows = false;
            dgvBookings.AllowUserToDeleteRows = false;
            dgvBookings.ReadOnly = true;
            dgvBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBookings.RowHeadersVisible = false;
        }

        private void LoadBookings()
        {
            try
            {
                // Get bookings for current user
                _bookings = _bookingBLL.GetBookingsByAccountId(Session.CurrentUser.Id);

                // Create a custom view model for the grid
                var bookingViewModels = _bookings.Select(b => new
                {
                    b.Id,
                    RouteName = $"{b.Route.DepartureLocation} to {b.Route.ArrivalLocation}",
                    BusInfo = $"{b.Bus.BusNumber} - {b.Bus.BusType}",
                    b.TravelDate,
                    b.SeatNumber,
                    b.Status,
                    b.TotalPrice
                }).ToList();

                // Bind to DataGridView
                dgvBookings.DataSource = bookingViewModels;

                // Update label with count
                label1.Text = $"My Bookings ({_bookings.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // This event handler is not needed for functionality
            // but we'll keep it since it's in the form designer
        }

        private void dgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a button cell was clicked
            if (e.RowIndex >= 0)
            {
                // Get the booking ID from the selected row
                int bookingId = Convert.ToInt32(dgvBookings.Rows[e.RowIndex].Cells["Id"].Value);

                // Find the booking in our list
                var booking = _bookings.FirstOrDefault(b => b.Id == bookingId);
                if (booking == null) return;

                // View Details button (second to last column)
                if (e.ColumnIndex == dgvBookings.Columns.Count - 2)
                {
                    ShowBookingDetails(booking);
                }

                // Cancel button (last column)
                else if (e.ColumnIndex == dgvBookings.Columns.Count - 1)
                {
                    CancelBooking(booking);
                }
            }
        }

        private void ShowBookingDetails(Booking booking)
        {
            // Create a message with booking details
            string details = $"Booking Details:\n\n" +
                            $"Booking ID: {booking.Id}\n" +
                            $"Route: {booking.Route.DepartureLocation} to {booking.Route.ArrivalLocation}\n" +
                            $"Bus: {booking.Bus.BusNumber} - {booking.Bus.BusType}\n" +
                            $"Travel Date: {booking.TravelDate.ToString("dd/MM/yyyy")}\n" +
                            $"Booking Date: {booking.BookingDate.ToString("dd/MM/yyyy HH:mm")}\n" +
                            $"Seat Number: {booking.SeatNumber}\n" +
                            $"Status: {booking.Status}\n" +
                            $"Total Price: {booking.TotalPrice.ToString("C")}";

            MessageBox.Show(details, "Booking Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CancelBooking(Booking booking)
        {
            // Check if booking can be cancelled
            if (booking.Status == "Cancelled")
            {
                MessageBox.Show("This booking is already cancelled.", "Cancel Booking",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (booking.TravelDate < DateTime.Now.Date)
            {
                MessageBox.Show("Cannot cancel past bookings.", "Cancel Booking",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm cancellation
            DialogResult result = MessageBox.Show(
                "Are you sure you want to cancel this booking?\n\n" +
                $"Route: {booking.Route.DepartureLocation} to {booking.Route.ArrivalLocation}\n" +
                $"Travel Date: {booking.TravelDate.ToString("dd/MM/yyyy")}\n" +
                $"Seat: {booking.SeatNumber}",
                "Confirm Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = _bookingBLL.CancelBooking(booking.Id);
                    if (success)
                    {
                        MessageBox.Show("Booking cancelled successfully.", "Cancel Booking",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBookings(); // Refresh the list
                    }
                    else
                    {
                        MessageBox.Show("Failed to cancel booking. Please try again.", "Cancel Booking",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error cancelling booking: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Assuming button1 is the Refresh button
            LoadBookings();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dgvBookings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking to delete.", "Delete Booking",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the booking ID from the selected row
            int bookingId = Convert.ToInt32(dgvBookings.SelectedRows[0].Cells["Id"].Value);

            // Find the booking in our list
            var booking = _bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return;

            // Confirm deletion
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this booking?\n\n" +
                $"Route: {booking.Route.DepartureLocation} to {booking.Route.ArrivalLocation}\n" +
                $"Travel Date: {booking.TravelDate.ToString("dd/MM/yyyy")}\n" +
                $"Seat: {booking.SeatNumber}",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = _bookingBLL.DeleteBooking(booking.Id);
                    if (success)
                    {
                        MessageBox.Show("Booking deleted successfully.", "Delete Booking",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBookings(); // Refresh the list
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete booking. Please try again.", "Delete Booking",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting booking: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}