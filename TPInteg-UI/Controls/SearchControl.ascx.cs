using System;
using System.Web.UI;

namespace TPInteg_UI.Controls
{
    public partial class SearchControl : UserControl
    {
        public event EventHandler<string> SearchClicked;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = searchInput.Text;
            SearchClicked?.Invoke(this, searchTerm);
        }
    }
}
