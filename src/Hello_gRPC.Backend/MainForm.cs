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
        // Fenêtre redimensionnable
        FormBorderStyle = FormBorderStyle.Sizable;
        MaximizeBox = true;
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(600, 300);

        // Charge dynamiquement la liste des services gRPC exposés
        LoadGrpcServices();
    }

    private void LoadGrpcServices()
    {
        grpcServicesGrid.Rows.Clear();
        var services = Program.GrpcServiceTypes;
        int grpcPort = 5001; // Port gRPC défini dans Program.cs
        if (services.Length > 0)
        {
            foreach (var type in services)
            {
                grpcServicesGrid.Rows.Add(
                    type.Name,
                    type.FullName ?? string.Empty,
                    type.Assembly.GetName().Name ?? string.Empty,
                    grpcPort.ToString()
                );
            }
        }
        else
        {
            grpcServicesGrid.Rows.Add("Aucun service gRPC exposé", "", "", "");
        }
    }

    }
