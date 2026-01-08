using University.Dao.Entity;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace University.Api.Controls
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ReportViewer1.Load += ReportViewer1_Load;

            if (!IsPostBack)
            {
                //txbPage.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;");

                string strEntity = Request["entity"] ?? string.Empty;

                if (!string.IsNullOrEmpty(strEntity))
                {
                    string strPathRdlc = string.Empty;

                    ReportDao daoReport = new ReportDao();
                    DataSet dsData = daoReport.GetDataSetEntity(strEntity, Request, out strPathRdlc);

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportDefinition/" + strPathRdlc + ".rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();

                    foreach (DataTable dtData in dsData.Tables)
                    {
                        ReportDataSource datasource = new ReportDataSource(dtData.TableName, dtData);
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                    }

                    //ReportViewer1.LocalReport.Refresh();
                }

            }


        }

        //protected void ReportViewer1_Load(object sender, EventArgs e)
        //{
        //    lblTotalPage.Text = ReportViewer1.LocalReport.GetTotalPages().ToString();
        //}
        //#region "Event"

        //protected void btnNavFirst_Click(object sender, EventArgs e)
        //{
        //    ReportViewer1.CurrentPage = 1;
        //    txbPage.Text = ReportViewer1.CurrentPage.ToString();
        //}

        //protected void btnNavPrevious_Click(object sender, EventArgs e)
        //{
        //    if (ReportViewer1.CurrentPage > 1)
        //    {
        //        ReportViewer1.CurrentPage -= 1;
        //        txbPage.Text = ReportViewer1.CurrentPage.ToString();
        //    }
        //}

        //protected void btnNavNext_Click(object sender, EventArgs e)
        //{
        //    if (ReportViewer1.CurrentPage < ReportViewer1.LocalReport.GetTotalPages())
        //    {
        //        ReportViewer1.CurrentPage += 1;
        //        txbPage.Text = ReportViewer1.CurrentPage.ToString();
        //    }
        //}

        //protected void btnNavLast_Click(object sender, EventArgs e)
        //{
        //    ReportViewer1.CurrentPage = ReportViewer1.LocalReport.GetTotalPages();
        //    txbPage.Text = ReportViewer1.CurrentPage.ToString();
        //}

        //protected void txbPage_TextChanged(object sender, EventArgs e)
        //{
        //    int intCurrentPage = Convert.ToInt32(txbPage.Text);
        //    if (intCurrentPage >= 1 && intCurrentPage <= ReportViewer1.LocalReport.GetTotalPages())
        //    {
        //        ReportViewer1.CurrentPage = intCurrentPage;
        //    }
        //}

        //protected void btnRefresh_Click(object sender, EventArgs e)
        //{
        //    ReportViewer1.LocalReport.Refresh();

        //    int intCurrentPage = Convert.ToInt32(txbPage.Text);
        //    if (intCurrentPage > 1)
        //    {
        //        txbPage.Text = "1";
        //    }
        //}

        //protected void btnPrint_Click(object sender, EventArgs e)
        //{
        //    var report = ReportViewer1.LocalReport;
        //    var pageSettings = new PageSettings();
        //    pageSettings.PaperSize = report.GetDefaultPageSettings().PaperSize;
        //    pageSettings.Landscape = report.GetDefaultPageSettings().IsLandscape;
        //    pageSettings.Margins = report.GetDefaultPageSettings().Margins;

        //    ReportViewer1.LocalReport.Print(pageSettings);
        //}

        //protected void ddlZoom_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ReportViewer1.ZoomPercent = Convert.ToInt32(ddlZoom.SelectedValue);
        //}

        //#endregion

    }
}