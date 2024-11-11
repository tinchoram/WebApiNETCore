using System;

namespace TPInteg_UI.Controls
{
    public partial class CuitNumberFormatter : System.Web.UI.UserControl
    {
        public string CuitNumber 
        {
            get { return txtCuitNumber.Text; }
            set
            {
                txtCuitNumber.Text = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}