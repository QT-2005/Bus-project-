using BusTicketSystem.BLL;
using BusTicketSystem.Config;
using BusTicketSystem.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusTicketSystem.GUI
{
    public partial class BookingManagementForm : Form
    {
        private readonly BookingBLL _bookingBLL;
        private List<Booking> _bookings;
        private DataTable bookingsTable; // Data source for bookings

        public BookingManagementForm()
        {
            InitializeComponent();
            _bookingBLL = new BookingBLL();
        }

        private void BookingManagementForm_Load(object sender, EventArgs e)
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

            DataGridViewTextBoxColumn customerColumn = new DataGridViewTextBoxColumn();
            customerColumn.DataPropertyName = "CustomerName";
            customerColumn.HeaderText = "Customer";
            customerColumn.Width = 120;
            dgvBookings.Columns.Add(customerColumn);

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

            DataGridViewTextBoxColumn bookingDateColumn = new DataGridViewTextBoxColumn();
            bookingDateColumn.DataPropertyName = "BookingDate";
            bookingDateColumn.HeaderText = "Booking Date";
            bookingDateColumn.Width = 120;
            bookingDateColumn.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvBookings.Columns.Add(bookingDateColumn);

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
                // Get all bookings (admin view, so no account ID filter)
                _bookings = _bookingBLL.GetAllBookings();

                // Initialize DataTable
                bookingsTable = new DataTable();
                bookingsTable.Columns.Add("Id", typeof(int));
                bookingsTable.Columns.Add("CustomerName", typeof(string));
                bookingsTable.Columns.Add("RouteName", typeof(string));
                bookingsTable.Columns.Add("BusInfo", typeof(string));
                bookingsTable.Columns.Add("TravelDate", typeof(DateTime));
                bookingsTable.Columns.Add("BookingDate", typeof(DateTime));
                bookingsTable.Columns.Add("SeatNumber", typeof(string));
                bookingsTable.Columns.Add("Status", typeof(string));
                bookingsTable.Columns.Add("TotalPrice", typeof(decimal));

                // Populate DataTable
                foreach (var b in _bookings)
                {
                    bookingsTable.Rows.Add(
                        b.Id,
                        b.Account.FullName,
                        $"{b.Route.DepartureLocation} to {b.Route.ArrivalLocation}",
                        $"{b.Bus.BusNumber} - {b.Bus.BusType}",
                        b.TravelDate,
                        b.BookingDate,
                        b.SeatNumber,
                        b.Status,
                        b.TotalPrice
                    );
                }

                // Bind to DataGridView
                dgvBookings.DataSource = bookingsTable;

                // Update label with count
                label2.Text = $"Total Bookings ({_bookings.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            try
            {
                string searchText = txtSearch.Text.ToLower();

                // If search text is empty, show all bookings
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    dgvBookings.DataSource = bookingsTable;
                    label2.Text = $"Total Bookings ({bookingsTable.Rows.Count})";
                    return;
                }

                // Search in multiple columns
                var filteredRows = bookingsTable.AsEnumerable()
                    .Where(row =>
                        row.Field<string>("CustomerName")?.ToLower().Contains(searchText) == true ||
                        row.Field<string>("RouteName")?.ToLower().Contains(searchText) == true ||
                        row.Field<string>("BusInfo")?.ToLower().Contains(searchText) == true ||
                        row.Field<string>("SeatNumber")?.ToLower().Contains(searchText) == true ||
                        row.Field<string>("Status")?.ToLower().Contains(searchText) == true);

                // Create a new DataTable with the filtered results
                if (filteredRows.Any())
                {
                    DataTable filteredTable = filteredRows.CopyToDataTable();
                    dgvBookings.DataSource = filteredTable;

                    // Update label to show filtered count
                    label3.Text = $"Search Results ({filteredTable.Rows.Count})";
                }
                else
                {
                    // No results found
                    dgvBookings.DataSource = bookingsTable.Clone(); // Empty table with same structure
                    label3.Text = "Search Results (0)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filters: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a button cell was clicked
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvBookings.Columns.Count - 1) // View Details button
            {
                // Get the booking ID from the selected row
                int bookingId = Convert.ToInt32(dgvBookings.Rows[e.RowIndex].Cells["Id"].Value);

                // Find the booking in our list
                var booking = _bookings.FirstOrDefault(b => b.Id == bookingId);
                if (booking == null) return;

                // Show booking details
                ShowBookingDetails(booking);
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadBookings();
            ApplyFilters();

        }




        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void BookingManagementForm_Load_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}