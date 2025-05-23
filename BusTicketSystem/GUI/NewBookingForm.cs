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
    public partial class NewBookingForm : Form
    {
        private readonly RouteBLL _routeBLL;
        private readonly BusBLL _busBLL;
        private readonly BookingBLL _bookingBLL;
        private List<string> _bookedSeats;
        private string _selectedSeat;
        private decimal _price;

        public NewBookingForm()
        {
            InitializeComponent();
            _routeBLL = new RouteBLL();
            _busBLL = new BusBLL();
            _bookingBLL = new BookingBLL();
            _bookedSeats = new List<string>();
            NewBookingForm_Load(null, null);
        }

        private void NewBookingForm_Load(object sender, EventArgs e)
        {
            // Set minimum date to today
            dtpTravelDate.MinDate = DateTime.Now.Date;
            dtpTravelDate.Value = DateTime.Now.Date.AddDays(1);

            // Load routes
            LoadRoutes();

            // Disable book button initially
            btnBook.Enabled = false;
        }

        private void LoadRoutes()
        {
            try
            {
                var routes = _routeBLL.GetActiveRoutes();
                if (routes.Count > 0)
                {
                    cmbRoute.DataSource = routes;
                    cmbRoute.DisplayMember = "DisplayText";
                    cmbRoute.ValueMember = "Id";
                }
                else
                {
                    cmbRoute.DataSource = null;
                    MessageBox.Show("No active routes available.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading routes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cmbRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRoute.SelectedItem != null)
            {
                var selectedRoute = (Route)cmbRoute.SelectedItem;
                // Update price
                _price = selectedRoute.Price;
                txtPrice.Text = _price.ToString("C");

                // Load buses for this route
                LoadBusesForRoute(selectedRoute.Id);

                // Clear selected seat
                _selectedSeat = null;
                txtSelectedSeat.Text = string.Empty;
                btnBook.Enabled = false;
            }
        }

        private void LoadBusesForRoute(int routeId)
        {
            try
            {
                var buses = _busBLL.GetBusesByRouteId(routeId);

                if (buses.Count > 0)
                {
                    cmbBus.DataSource = buses;
                    cmbBus.DisplayMember = "DisplayText";
                    cmbBus.ValueMember = "Id";
                }
                else
                {
                    cmbBus.DataSource = null;
                    MessageBox.Show("No buses available for this route.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading buses: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cmbBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSeatLayout();
        }

        private void dtpTravelDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateSeatLayout();
        }

        private void UpdateSeatLayout()
        {
            if (cmbRoute.SelectedItem == null || cmbBus.SelectedItem == null)
            {
                return;
            }

            try
            {
                var selectedRoute = (Route)cmbRoute.SelectedItem;
                var selectedBus = (Bus)cmbBus.SelectedItem;
                var travelDate = dtpTravelDate.Value.Date;

                // Get booked seats
                _bookedSeats = _bookingBLL.GetBookedSeats(selectedBus.Id, selectedRoute.Id, travelDate);

                // Clear seat panel
                pnlSeats.Controls.Clear();

                // Create seat layout
                int seatSize = 40;
                int margin = 5;
                int rows = 5;
                int cols = selectedBus.Capacity / rows;
                if (selectedBus.Capacity % rows > 0) cols++;

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        int seatNumber = row * cols + col + 1;
                        if (seatNumber > selectedBus.Capacity)
                            continue;

                        string seatId = $"{(char)('A' + row)}{col + 1}";

                        Button seatButton = new Button();
                        seatButton.Name = $"seat_{seatId}";
                        seatButton.Text = seatId;
                        seatButton.Size = new Size(seatSize, seatSize);
                        seatButton.Location = new Point(col * (seatSize + margin) + margin, row * (seatSize + margin) + margin);
                        seatButton.Tag = seatId;

                        // Check if seat is booked
                        if (_bookedSeats.Contains(seatId))
                        {
                            seatButton.BackColor = Color.Red;
                            seatButton.Enabled = false;
                            seatButton.Text = $"{seatId}\nBooked";
                        }
                        else
                        {
                            seatButton.BackColor = Color.LightGreen;
                            seatButton.Click += SeatButton_Click;
                        }

                        pnlSeats.Controls.Add(seatButton);
                    }
                }

                // Clear selected seat
                _selectedSeat = null;
                txtSelectedSeat.Text = string.Empty;
                btnBook.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating seat layout: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SeatButton_Click(object sender, EventArgs e)
        {
            // Reset all unbooked seats to default color
            foreach (Control control in pnlSeats.Controls)
            {
                if (control is Button button && button.Enabled && button.BackColor != Color.Red)
                {
                    button.BackColor = Color.LightGreen;
                }
            }

            // Highlight selected seat
            Button selectedSeatButton = (Button)sender;
            selectedSeatButton.BackColor = Color.Yellow;

            // Update selected seat
            _selectedSeat = selectedSeatButton.Tag.ToString();
            txtSelectedSeat.Text = _selectedSeat;

            // Enable book button
            btnBook.Enabled = true;
        }

        private void pnlSeats_Paint(object sender, PaintEventArgs e)
        {
            // This event is not needed for functionality
        }

        private void txtSelectedSeat_TextChanged(object sender, EventArgs e)
        {
            // This event is not needed for functionality
            // The text is updated programmatically
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            // This event is not needed for functionality
            // The text is updated programmatically
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedSeat))
            {
                MessageBox.Show("Please select a seat.", "Booking Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var selectedRoute = (Route)cmbRoute.SelectedItem;
                var selectedBus = (Bus)cmbBus.SelectedItem;
                var travelDate = dtpTravelDate.Value.Date;

                // Confirm booking
                DialogResult result = MessageBox.Show(
                    "Confirm booking details:\n\n" +
                    $"Route: {selectedRoute.DepartureLocation} to {selectedRoute.ArrivalLocation}\n" +
                    $"Bus: {selectedBus.BusNumber} - {selectedBus.BusType}\n" +
                    $"Travel Date: {travelDate.ToString("dd/MM/yyyy")}\n" +
                    $"Seat: {_selectedSeat}\n" +
                    $"Price: {_price.ToString("C")}\n\n" +
                    "Do you want to proceed with this booking?",
                    "Confirm Booking",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Create booking
                    var booking = new Booking
                    {
                        AccountId = Session.CurrentUser.Id,
                        BusId = selectedBus.Id,
                        RouteId = selectedRoute.Id,
                        BookingDate = DateTime.Now,
                        TravelDate = travelDate,
                        SeatNumber = _selectedSeat,
                        Status = "Confirmed",
                        TotalPrice = _price
                    };

                    // Save booking
                    bool success = _bookingBLL.CreateBooking(booking);
                    if (success)
                    {
                        MessageBox.Show("Booking created successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Ask if user wants to make another booking
                        DialogResult continueResult = MessageBox.Show(
                            "Do you want to make another booking?",
                            "Continue",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (continueResult == DialogResult.Yes)
                        {
                            // Reset form for new booking
                            ResetForm();
                        }
                        else
                        {
                            // Close form
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to create booking. Please try again.",
                            "Booking Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating booking: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            // Reset date
            dtpTravelDate.Value = DateTime.Now.Date.AddDays(1);

            // Reset selected seat
            _selectedSeat = null;
            txtSelectedSeat.Text = string.Empty;

            // Disable book button
            btnBook.Enabled = false;

            // Reload routes (which will cascade to reload buses and seats)
            LoadRoutes();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Ask if user wants to cancel the booking process
            DialogResult result = MessageBox.Show(
                "Are you sure you want to cancel this booking process?",
                "Cancel Booking",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}