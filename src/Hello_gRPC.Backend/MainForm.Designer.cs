namespace HelloGrpc.Backend;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private System.Windows.Forms.DataGridView grpcServicesGrid;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        grpcServicesGrid = new System.Windows.Forms.DataGridView();
        ((System.ComponentModel.ISupportInitialize)(grpcServicesGrid)).BeginInit();

        grpcServicesGrid.AllowUserToAddRows = false;
        grpcServicesGrid.AllowUserToDeleteRows = false;
        grpcServicesGrid.ReadOnly = true;
        grpcServicesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        grpcServicesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        grpcServicesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        grpcServicesGrid.Columns.Clear();
        grpcServicesGrid.Columns.Add("ServiceName", "Service gRPC");
        grpcServicesGrid.Columns.Add("Implementation", "Classe d'implémentation");
        grpcServicesGrid.Columns.Add("Assembly", "Assembly");
        grpcServicesGrid.Columns.Add("Port", "Port gRPC");

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(600, 300);
        Controls.Add(grpcServicesGrid);

        ((System.ComponentModel.ISupportInitialize)(grpcServicesGrid)).EndInit();
    }
}
