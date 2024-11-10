using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPInteg_UI.Services;

namespace TPInteg_UI.Pages
{
    public partial class ListadoProveedores : Page
    {
        protected UpdateProgress UpdateProgress1;
        protected UpdatePanel UpdatePanel1;
        protected Panel PanelError;
        protected Panel NoDataPanel;
        protected GridView GridViewProveedores;
        protected Label LabelError;
        protected Button ButtonReintentar;

        private readonly ProveedorService _proveedorService;

        public ListadoProveedores()
        {
            _proveedorService = new ProveedorService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(LoadProveedoresAsync));
            }
        }

        private void ShowLoading(bool show)
        {
            if (UpdateProgress1 != null)
                UpdateProgress1.Visible = show;
        }

        private void ShowError(bool show, string message = "")
        {
            if (PanelError != null)
            {
                PanelError.Visible = show;
                if (LabelError != null)
                    LabelError.Text = message;
            }
            if (GridViewProveedores != null)
                GridViewProveedores.Visible = !show;
        }

        private async Task LoadProveedoresAsync()
        {
            try
            {
                ShowLoading(true);
                ShowError(false, string.Empty);

                var result = await _proveedorService.GetProveedoresAsync();

                if (result.Success)
                {
                    if (result.Data != null && result.Data.Count > 0)
                    {
                        if (GridViewProveedores != null)
                        {
                            GridViewProveedores.DataSource = result.Data;
                            GridViewProveedores.DataBind();
                            GridViewProveedores.Visible = true;
                        }
                        if (NoDataPanel != null)
                            NoDataPanel.Visible = false;
                    }
                    else
                    {
                        if (GridViewProveedores != null)
                            GridViewProveedores.Visible = false;
                        if (NoDataPanel != null)
                            NoDataPanel.Visible = true;
                    }
                }
                else
                {
                    ShowError(true, result.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error inesperado. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                ShowLoading(false);
            }
        }

        protected void ButtonReintentar_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(LoadProveedoresAsync));
        }
    }
}