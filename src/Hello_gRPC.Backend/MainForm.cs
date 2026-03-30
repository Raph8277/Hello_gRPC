namespace HelloGrpc.Backend;

/// <summary>
/// Formulaire principal de l'application backend.
/// </summary>
public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        Text = "Hello gRPC - Backend Server";
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(400, 200);

        var label = new Label
        {
            Text = "Serveur gRPC en cours d'exécution sur le port 5001...",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 12)
        };
        Controls.Add(label);
    }
}
